using UnityEngine;
using TMPro; 
using UnityEngine.InputSystem;

public class NotebookUI : MonoBehaviour
{
    [Header("Estrutura do Caderno")]
    public GameObject notebookPanel;
    public GameObject secretsPage;
    public GameObject inventoryPage;
    public GameObject notesPage;

    [Header("Elementos - Detalhes da Pista (Direita)")]
    [Tooltip("O título do lado direito que muda quando clicas num botão.")]
    public TextMeshProUGUI detailNameText;
    [Tooltip("A descrição do lado direito que muda quando clicas num botão.")]
    public TextMeshProUGUI detailDescriptionText;

    [Header("Elementos - Grelha (Esquerda)")]
    [Tooltip("A 'caixa' vazia onde vão aparecer os botões dos Segredos.")]
    public Transform secretsGridParent;
    [Tooltip("A 'caixa' vazia onde vão aparecer os botões do Inventário.")]
    public Transform inventoryGridParent;
    [Tooltip("Arrasta o prefab do botão para aqui.")]
    public GameObject clueSlotPrefab;

    [Header("Elementos - Texto Livre")]
    public TMP_InputField notesInputField;

    [Header("Input Actions")]
    public InputAction toggleNotebookAction;

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
        PopulateGrid(NotebookManager.Instance.collectedSecrets, secretsGridParent);
    }

    public void OpenInventoryPage()
    {
        secretsPage.SetActive(false);
        inventoryPage.SetActive(true);
        notesPage.SetActive(false);
        PopulateGrid(NotebookManager.Instance.collectedItems, inventoryGridParent);
    }

    public void OpenNotesPage()
    {
        secretsPage.SetActive(false);
        inventoryPage.SetActive(false);
        notesPage.SetActive(true);
    }

    // --- SISTEMA DE GRELHA AUTOMÁTICA ---

    private void PopulateGrid(System.Collections.Generic.List<Clue> clues, Transform gridParent)
    {
        // 1. Destrói os botões antigos para não duplicar
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        // 2. Limpa o ecrã da direita
        detailNameText.text = "Selecione um item";
        detailDescriptionText.text = "";

        // 3. Cria os novos botões
        if (clues.Count > 0)
        {
            foreach (Clue clue in clues)
            {
                // Cria um botão invisível do prefab e põe-no dentro da grelha
                GameObject newSlot = Instantiate(clueSlotPrefab, gridParent);
                
                // Configura o texto e os dados desse botão
                ClueSlotUI slotUI = newSlot.GetComponent<ClueSlotUI>();
                if (slotUI != null)
                {
                    slotUI.Setup(clue, this);
                }
            }

            // Seleciona automaticamente o primeiro item da lista para não ficar vazio
            ShowClueDetails(clues[0]);
        }
    }

    // --- SISTEMA DE DETALHES (LADO DIREITO) ---

    // Esta função é chamada pelos botões da grelha quando clicas neles
    public void ShowClueDetails(Clue clueToShow)
    {
        detailNameText.text = clueToShow.clueName;
        detailDescriptionText.text = clueToShow.description;
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