using System.Collections.Generic;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    public static NotebookManager Instance;

    [Header("Dados do Detetive")]
    public List<Clue> collectedSecrets = new List<Clue>();
    public List<Clue> collectedItems = new List<Clue>();

    [Header("Notas do Detetive")]
    [TextArea(5, 10)]
    public string playerNotes = ""; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddClue(Clue newClue)
    {
        // Separa as pistas para a lista correta dependendo do tipo
        if (newClue.type == ClueType.Segredo)
        {
            if (!collectedSecrets.Contains(newClue))
            {
                collectedSecrets.Add(newClue);
                Debug.Log("Segredo adicionado: " + newClue.clueName);
            }
        }
        else if (newClue.type == ClueType.ItemFisico)
        {
            if (!collectedItems.Contains(newClue))
            {
                collectedItems.Add(newClue);
                Debug.Log("Item adicionado ao inventário: " + newClue.clueName);
            }
        }
    }
}