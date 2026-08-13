using UnityEngine;
using TMPro; 
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class NotebookUI : MonoBehaviour
{
    [Header("Estrutura do Caderno")]
    public GameObject notebookPanel;
    public GameObject secretsPage;
    public GameObject inventoryPage;
    public GameObject notesPage;

    [Header("Elementos - Detalhes (Segredos)")]
    public TextMeshProUGUI secretDetailNameText;
    public TextMeshProUGUI secretDetailDescText;

    [Header("Elementos - Detalhes (Inventário)")]
    public TextMeshProUGUI inventoryDetailNameText;
    public TextMeshProUGUI inventoryDetailDescText;

    [Header("Elementos - Grelha (Esquerda)")]
    public Transform secretsGridParent;
    public Transform inventoryGridParent;
    public GameObject clueSlotPrefab;

    [Header("Elementos - Texto Livre")]
    public TMP_InputField notesInputField;

    [Header("Input Actions")]
    public InputAction toggleNotebookAction;

    private bool isOpen = false;
    private ClueType currentActiveTab;

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
        currentActiveTab = ClueType.Segredo;
        PopulateGrid(NotebookManager.Instance.collectedSecrets, secretsGridParent);
    }

    public void OpenInventoryPage()
    {
        secretsPage.SetActive(false);
        inventoryPage.SetActive(true);
        notesPage.SetActive(false);
        currentActiveTab = ClueType.ItemFisico;
        PopulateGrid(NotebookManager.Instance.collectedItems, inventoryGridParent);
    }

    public void OpenNotesPage()
    {
        secretsPage.SetActive(false);
        inventoryPage.SetActive(false);
        notesPage.SetActive(true);
    }

    // --- SISTEMA DE GRELHA AUTOMÁTICA ---

    private void PopulateGrid(List<Clue> clues, Transform gridParent)
    {
        // 1. Destrói os botões antigos para não duplicar
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        // 2. Limpa o ecrã da direita consoante a página aberta
        if (currentActiveTab == ClueType.Segredo)
        {
            if (secretDetailNameText) secretDetailNameText.text = "Selecione um segredo";
            if (secretDetailDescText) secretDetailDescText.text = "";
        }
        else
        {
            if (inventoryDetailNameText) inventoryDetailNameText.text = "Selecione um item";
            if (inventoryDetailDescText) inventoryDetailDescText.text = "";
        }

        // 3. Cria os novos botões
        if (clues.Count > 0)
        {
            foreach (Clue clue in clues)
            {
                GameObject newSlot = Instantiate(clueSlotPrefab, gridParent);
                ClueSlotUI slotUI = newSlot.GetComponent<ClueSlotUI>();
                if (slotUI != null)
                {
                    slotUI.Setup(clue, this);
                }
            }
            // Seleciona automaticamente o primeiro
            ShowClueDetails(clues[0]);
        }
    }

    // --- SISTEMA DE DETALHES (LADO DIREITO) ---

    public void ShowClueDetails(Clue clueToShow)
    {
        // Atualiza os textos dos Segredos
        if (secretDetailNameText != null) secretDetailNameText.text = clueToShow.clueName;
        if (secretDetailDescText != null) secretDetailDescText.text = clueToShow.description;
        
        // Atualiza os textos do Inventário
        if (inventoryDetailNameText != null) inventoryDetailNameText.text = clueToShow.clueName;
        if (inventoryDetailDescText != null) inventoryDetailDescText.text = clueToShow.description;
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