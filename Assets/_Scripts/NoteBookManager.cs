using System.Collections.Generic;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    // A instância global do caderno acessível de qualquer lado
    public static NotebookManager Instance;

    [Header("Pistas Recolhidas")]
    public List<Clue> collectedClues = new List<Clue>();

    private void Awake()
    {
        // Garante que só existe um Gestor de Caderno na cena inteira
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Função que será chamada quando o Jorge apanhar uma pista
    public void AddClue(Clue newClue)
    {
        // Verifica se ainda não temos esta pista
        if (!collectedClues.Contains(newClue))
        {
            collectedClues.Add(newClue);
            Debug.Log("Pista adicionada ao caderno: " + newClue.clueName);
        }
        else
        {
            Debug.Log("O Jorge já registou esta pista!");
        }
    }
}