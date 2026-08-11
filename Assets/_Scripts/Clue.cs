using UnityEngine;

[CreateAssetMenu(fileName = "Nova Pista", menuName = "Mistério/Pista")]
public class Clue : ScriptableObject
{
    [Tooltip("O nome da pista, ex: Faca Ensanguentada")]
    public string clueName;
    
    [Tooltip("A descrição que o Jorge regista no caderno")]
    [TextArea(3, 5)]
    public string description;
}