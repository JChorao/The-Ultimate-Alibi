using UnityEngine;

public interface IInteractable
{
    // Qualquer script que use esta interface terá de ter a sua própria versão do método Interact
    void Interact();
    
    // Opcional: Para mostrar o nome da interação na UI (ex: "Falar com Padre")
    string GetInteractText();
}