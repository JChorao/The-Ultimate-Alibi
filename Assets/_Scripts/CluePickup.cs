using UnityEngine;

public class CluePickup : MonoBehaviour, IInteractable
{
    [Header("A Pista que este objeto representa")]
    [Tooltip("Arrasta o ficheiro da pista criado na janela Project para aqui.")]
    public Clue clueData;

    public void Interact()
    {
        if (clueData != null)
        {
            // Adiciona a pista ao caderno usando a instância global
            NotebookManager.Instance.AddClue(clueData);
            
            // Destrói o objeto 3D do mundo, pois o Jorge já o recolheu
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Este objeto não tem uma pista (Clue) associada no Inspector!");
        }
    }

    public string GetInteractText()
    {
        if (clueData != null)
        {
            return "Apanhar " + clueData.clueName;
        }
        return "Examinar objeto desconhecido";
    }
}