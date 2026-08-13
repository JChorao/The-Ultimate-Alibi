using UnityEngine;

// Define os tipos de pistas que existem no jogo
public enum ClueType 
{ 
    Segredo, 
    ItemFisico 
}

[CreateAssetMenu(fileName = "Nova Pista", menuName = "Mistério/Pista")]
public class Clue : ScriptableObject
{
    [Tooltip("Escolhe se é um segredo ouvido ou um objeto físico apanhado.")]
    public ClueType type; 

    [Tooltip("O nome da pista, ex: Faca Ensanguentada")]
    public string clueName;
    
    [Tooltip("A descrição que o Jorge regista no caderno")]
    [TextArea(3, 5)]
    public string description;
}