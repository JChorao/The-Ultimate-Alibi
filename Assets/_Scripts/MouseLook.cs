using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("Configurações da Câmara")]
    [Tooltip("Controla a velocidade da câmara. Ajusta se estiver muito rápido ou lento.")]
    public float mouseSensitivity = 50f;

    [Tooltip("Arrasta o objeto do Jorge (o jogador) para aqui.")]
    public Transform playerBody;

    [Header("Input Actions")]
    [Tooltip("Configura o movimento do rato no Inspector.")]
    public InputAction lookAction;

    private float xRotation = 0f;

    private void OnEnable()
    {
        lookAction.Enable();
    }

    private void OnDisable()
    {
        lookAction.Disable();
    }

    private void Start()
    {
        // Tranca o cursor do rato no centro do ecrã e esconde-o para não te atrapalhar a jogar
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Lê o movimento do rato (Delta)
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        // Multiplicamos pela sensibilidade e pelo tempo para o movimento ser suave
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Calcula a rotação para cima e para baixo (eixo X)
        xRotation -= mouseY;
        
        // Limita a rotação para o Jorge não partir o pescoço ao olhar demasiado para trás (-90 a 90 graus)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Aplica a rotação de cima/baixo na câmara
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Roda o corpo do jogador para a esquerda e direita (eixo Y)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}