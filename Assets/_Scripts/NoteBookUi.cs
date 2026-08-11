using UnityEngine;
using TMPro; // Obrigatório para usar TextMeshPro
using UnityEngine.InputSystem;

public class NoteBookUI : MonoBehaviour
{
    [Header("Elementos Visuais")]
    [Tooltip("Arrasta o Painel principal do caderno para aqui.")]
    public GameObject notebookPanel;
    
    [Tooltip("Arrasta o texto do Título para aqui.")]
    public TextMeshProUGUI clueNameText;
    
    [Tooltip("Arrasta o texto da Descrição para aqui.")]
    public TextMeshProUGUI clueDescriptionText;

    [Header("Input Actions")]
    [Tooltip("Configura a tecla para abrir/fechar o caderno (ex: TAB).")]
    public InputAction toggleNotebookAction;

    private int currentIndex = 0;
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
        // Garante que o caderno começa fechado quando o jogo arranca
        if (notebookPanel != null)
        {
            notebookPanel.SetActive(false);
        }
    }

    private void Update()
    {
        // Verifica se a tecla de abrir/fechar foi pressionada
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
            // Mostra o rato para poderes clicar nos botões de Próxima Pista
            Cursor.lockState = CursorLockMode.None;
            UpdateUI();
        }
        else
        {
            // Esconde o rato e tranca-o no centro novamente quando fechas o caderno
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Esta função será chamada pelo botão "Próximo" na UI
    public void NextClue()
    {
        if (NotebookManager.Instance.collectedClues.Count == 0) return;

        currentIndex++;
        if (currentIndex >= NotebookManager.Instance.collectedClues.Count)
        {
            currentIndex = 0; // Se chegar ao fim, volta à primeira pista
        }
        UpdateUI();
    }

    // Esta função será chamada pelo botão "Anterior" na UI
    public void PreviousClue()
    {
        if (NotebookManager.Instance.collectedClues.Count == 0) return;

        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = NotebookManager.Instance.collectedClues.Count - 1; // Se recuar na primeira, vai para a última
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Se o caderno estiver vazio
        if (NotebookManager.Instance.collectedClues.Count == 0)
        {
            clueNameText.text = "Caderno Vazio";
            clueDescriptionText.text = "Ainda não encontrei nenhuma pista em Almaceda...";
            return;
        }

        // Se houver pistas, mostra os dados da pista atual
        Clue currentClue = NotebookManager.Instance.collectedClues[currentIndex];
        clueNameText.text = currentClue.clueName;
        clueDescriptionText.text = currentClue.description;
    }
}