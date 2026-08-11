using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public string objectName = "Cubo Misterioso";

    public void Interact()
    {
        // Isto vai imprimir uma mensagem na consola do Unity quando carregares no E
        Debug.Log("O Jorge interagiu com: " + objectName);
    }

    public string GetInteractText()
    {
        return "Examinar " + objectName;
    }
}