using UnityEngine;
using TMPro; 
using UnityEngine.InputSystem;

public class NotebookUI : MonoBehaviour
{
    [Header("Estrutura do Caderno")]
    public GameObject notebookPanel;
    public GameObject secretsPage;
    public GameObject inventoryPage; // NOVA PÁGINA
    public GameObject notesPage;

    [Header("Elementos - Segredos")]
    public TextMeshProUGUI secretNameText;
    public TextMeshProUGUI secretDescriptionText;

    [Header("Elementos - Inventário")]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

    [Header("Elementos - Texto Livre")]
    public TMP_InputField notesInputField;

    [Header("Input Actions")]
    public InputAction toggleNotebookAction;

    private int currentSecretIndex = 0;
    private int currentItemIndex = 0;
    private bool isOpen = false;

    private void OnEnable()
    {
        toggleNotebookAction.Enable();
    }

    private void OnDisable()
    {
        toggleNotebookAction.Disable();
    }

    private void Start()
    {
        if (notebookPanel != null) notebookPanel.SetActive(false);

        if (notesInputField != null)
        {
            notesInputField.text = NotebookManager.Instance.playerNotes;
            notesInputField.onValueChanged.AddListener(delegate { UpdatePlayerNotes(); });
        }
    }

    private void Update()
    {
        if (toggleNotebookAction.WasPressedThisFrame())
        {
            ToggleNotebook();
        }
    }

    private void ToggleNotebook()
    {
        isOpen = !isOpen;
        notebookPanel.SetActive(isOpen);

        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            OpenSecretsPage(); // Abre os segredos por defeito
            
            if (notesInputField != null)
            {
                notesInputField.text = NotebookManager.Instance.playerNotes;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // --- SISTEMA DE NAVEGAÇÃO DE PÁGINAS ---

    public void OpenSecretsPage()
    {
        secretsPage.SetActive(true);
        inventoryPage.SetActive(false);
        notesPage.SetActive(false);
        UpdateSecretsUI();
    }

    public void OpenInventoryPage()
    {
        secretsPage.SetActive(false);
        inventoryPage.SetActive(true);
        notesPage.SetActive(false);
        UpdateInventoryUI();
    }

    public void OpenNotesPage()
    {
        secretsPage.SetActive(false);
        inventoryPage.SetActive(false);
        notesPage.SetActive(true);
    }

    // --- SISTEMA DE DADOS (SEGREDOS) ---

    public void NextSecret()
    {
        if (NotebookManager.Instance.collectedSecrets.Count == 0) return;
        currentSecretIndex++;
        if (currentSecretIndex >= NotebookManager.Instance.collectedSecrets.Count) currentSecretIndex = 0; 
        UpdateSecretsUI();
    }

    public void PreviousSecret()
    {
        if (NotebookManager.Instance.collectedSecrets.Count == 0) return;
        currentSecretIndex--;
        if (currentSecretIndex < 0) currentSecretIndex = NotebookManager.Instance.collectedSecrets.Count - 1; 
        UpdateSecretsUI();
    }

    private void UpdateSecretsUI()
    {
        if (NotebookManager.Instance.collectedSecrets.Count == 0)
        {
            secretNameText.text = "Nenhum Segredo";
            secretDescriptionText.text = "Ainda não descobri os segredos de ninguém...";
            return;
        }

        Clue currentClue = NotebookManager.Instance.collectedSecrets[currentSecretIndex];
        secretNameText.text = currentClue.clueName;
        secretDescriptionText.text = currentClue.description;
    }

    // --- SISTEMA DE DADOS (INVENTÁRIO) ---

    public void NextItem()
    {
        if (NotebookManager.Instance.collectedItems.Count == 0) return;
        currentItemIndex++;
        if (currentItemIndex >= NotebookManager.Instance.collectedItems.Count) currentItemIndex = 0; 
        UpdateInventoryUI();
    }

    public void PreviousItem()
    {
        if (NotebookManager.Instance.collectedItems.Count == 0) return;
        currentItemIndex--;
        if (currentItemIndex < 0) currentItemIndex = NotebookManager.Instance.collectedItems.Count - 1; 
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        if (NotebookManager.Instance.collectedItems.Count == 0)
        {
            itemNameText.text = "Inventário Vazio";
            itemDescriptionText.text = "Ainda não recolhi nenhuma prova física.";
            return;
        }

        Clue currentClue = NotebookManager.Instance.collectedItems[currentItemIndex];
        itemNameText.text = currentClue.clueName;
        itemDescriptionText.text = currentClue.description;
    }

    // --- SISTEMA DE TEXTO LIVRE ---
    
    private void UpdatePlayerNotes()
    {
        if (notesInputField != null)
        {
            NotebookManager.Instance.playerNotes = notesInputField.text;
        }
    }
}