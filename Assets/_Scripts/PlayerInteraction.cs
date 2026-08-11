using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionDistance = 3f;
    public KeyCode interactKey = KeyCode.E;
    
    [Tooltip("Coloca aqui a câmara do jogador para o Raycast saber para onde olhar.")]
    public Camera playerCamera;

    private void Update()
    {
        // Cria um raio a partir do centro do ecrã (câmara)
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        // Verifica se o raio atinge algo dentro da distância de interação
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Tenta obter um componente que use a interface IInteractable
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                // Se o jogador pressionar a tecla de interação (E)
                if (Input.GetKeyDown(interactKey))
                {
                    interactable.Interact();
                }
            }
        }
    }
}