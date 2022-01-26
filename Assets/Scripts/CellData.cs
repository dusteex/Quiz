using UnityEngine;

[CreateAssetMenu(fileName = "CellData" , order = 51)]
public class CellData : ScriptableObject
{
    [SerializeField] private string _identifier;
    public string Identifier => _identifier;

    [SerializeField] private string _taskText;
    public string TaskText => _taskText;


    [SerializeField] private Sprite _contentSprite;
    public Sprite ContentSprite => _contentSprite;
}
