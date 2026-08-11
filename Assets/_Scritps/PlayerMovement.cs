using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;

    private void Start()
    {
        // Obtém a referência do CharacterController anexado ao jogador
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Movimento no eixo X e Z (WASD ou Setas)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calcula a direção baseada na rotação atual do jogador
        Vector3 move = transform.right * x + transform.forward * z;

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