using UnityEngine;
using UnityEngine.InputSystem; // Obrigatório para o Novo Input System

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Input Actions")]
    [Tooltip("Configura as teclas de movimento (ex: WASD) no Inspector.")]
    public InputAction moveAction;

    private CharacterController controller;
    private Vector3 velocity;

    private void Start()
    {
        // Obtém a referência do CharacterController anexado ao jogador
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        // O novo sistema exige que as ações sejam ativadas
        moveAction.Enable();
    }

    private void OnDisable()
    {
        // E desativadas quando o objeto não está ativo
        moveAction.Disable();
    }

    private void Update()
    {
        // Lê os valores do Novo Input System como um Vector2 (X = Esquerda/Direita, Y = Frente/Trás)
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        // DEBUG: Se o input for diferente de zero, imprime na consola. 
        // Isto ajuda-nos a saber se as teclas estão bem configuradas!
        if (moveInput != Vector2.zero)
        {
            Debug.Log("Movimento Detetado! Valores X: " + moveInput.x + " | Y: " + moveInput.y);
        }

        // Calcula a direção baseada na rotação atual do jogador
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        // Aplica o movimento
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Aplica a gravidade para garantir que o Jorge fica no chão
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Reseta a velocidade vertical se estiver no chão
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
}