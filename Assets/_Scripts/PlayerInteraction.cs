using UnityEngine;
using UnityEngine.InputSystem; // Obrigatório para o Novo Input System

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionDistance = 3f;
    
    [Header("Input Actions")]
    [Tooltip("Configura a tecla de interação (ex: Tecla E) no Inspector.")]
    public InputAction interactAction;
    
    [Tooltip("Coloca aqui a câmara do jogador para o Raycast saber para onde olhar.")]
    public Camera playerCamera;

    private void OnEnable()
    {
        // Ativa a ação de input quando o objeto está ativo
        interactAction.Enable();
    }

    private void OnDisable()
    {
        // Desativa a ação para poupar memória quando não é necessária
        interactAction.Disable();
    }

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
                // Verifica se a tecla configurada foi pressionada EXATAMENTE nesta frame (equivalente ao GetKeyDown)
                if (interactAction.WasPressedThisFrame())
                {
                    interactable.Interact();
                }
            }
        }
    }
}