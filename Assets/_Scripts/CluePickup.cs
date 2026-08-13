using UnityEngine;

public class CluePickup : MonoBehaviour, IInteractable
{
    [Header("Dados da Pista")]
    public Clue clueData;

    public void Interact()
    {
        // Tenta encontrar o NotebookManager na cena se a Instance estiver vazia
        if (NotebookManager.Instance == null)
        {
            // O comando atualizado que remove o aviso da consola
            NotebookManager.Instance = Object.FindFirstObjectByType<NotebookManager>();
        }

        // Agora verifica novamente
        if (clueData != null && NotebookManager.Instance != null)
        {
            NotebookManager.Instance.AddClue(clueData);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("ERRO: O NotebookManager não foi encontrado na cena ou o ClueData está vazio!");
        }
    }

    public string GetInteractText()
    {
        return clueData != null ? "Apanhar " + clueData.clueName : "Examinar";
    }
}