using UnityEngine;

public class DeductionManager : MonoBehaviour
{
    // Instância global para podermos chamar este gestor a partir da Interface do Caderno
    public static DeductionManager Instance;

    [Header("A Verdade do Crime (O Gabarito)")]
    [Tooltip("O nome exato do culpado.")]
    public string correctSuspect = "Paulo";
    
    [Tooltip("A arma do crime.")]
    public string correctWeapon = "Faca";
    
    [Tooltip("A vítima do crime.")]
    public string correctVictim = "Ana";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Função que será chamada pelo botão final do Caderno de Acusação
    public void SubmitAccusation(string playerSuspect, string playerWeapon, string playerVictim)
    {
        Debug.Log("--- NOVA ACUSAÇÃO SUBMETIDA ---");
        Debug.Log($"O Jorge acusa: {playerSuspect} de matar {playerVictim} com {playerWeapon}.");

        // Compara as escolhas do jogador com as respostas certas
        bool isSuspectCorrect = (playerSuspect == correctSuspect);
        bool isWeaponCorrect = (playerWeapon == correctWeapon);
        bool isVictimCorrect = (playerVictim == correctVictim);

        // O jogador só ganha se acertar nos 3 elementos
        if (isSuspectCorrect && isWeaponCorrect && isVictimCorrect)
        {
            TriggerVictory();
        }
        else
        {
            TriggerDefeat(isSuspectCorrect, isWeaponCorrect, isVictimCorrect);
        }
    }

    private void TriggerVictory()
    {
        // Aqui no futuro vamos chamar a Scene de Final Feliz ou UI de Vitória
        Debug.Log("VITÓRIA! O mistério de Almaceda foi resolvido com sucesso!");
    }

    private void TriggerDefeat(bool suspectCorrect, bool weaponCorrect, bool victimCorrect)
    {
        // Aqui no futuro vamos chamar o Game Over ou dar feedback na UI
        Debug.Log("DERROTA! A teoria tem falhas. O Jorge concluiu mal a investigação.");
        
        // Dicas opcionais na consola para te ajudar a testar:
        if (!suspectCorrect) Debug.Log("-> O suspeito escolhido está errado.");
        if (!weaponCorrect) Debug.Log("-> A arma escolhida não faz sentido.");
        if (!victimCorrect) Debug.Log("-> A vítima não é essa.");
    }
}