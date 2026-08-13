using UnityEngine;
using TMPro;
using UnityEngine.UI; // Necessário para os botões

public class ClueSlotUI : MonoBehaviour
{
    [Header("Elementos Visuais")]
    public TextMeshProUGUI slotNameText;

    private Clue myClue;
    private NotebookUI uiManager;

    // Função que o NotebookUI chama quando cria este botão
    public void Setup(Clue clue, NotebookUI manager)
    {
        myClue = clue;
        uiManager = manager;
        
        if (slotNameText != null)
        {
            slotNameText.text = clue.clueName;
        }
    }

    // Função que o botão vai chamar quando clicares nele
    public void OnSlotClicked()
    {
        if (uiManager != null && myClue != null)
        {
            uiManager.ShowClueDetails(myClue);
        }
    }
}