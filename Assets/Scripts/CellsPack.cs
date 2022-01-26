using UnityEngine;

[CreateAssetMenu(fileName = "CellsPack" , order = 51)]
public class CellsPack : ScriptableObject
{
    [SerializeField] private CellData[] _cells;
    public CellData[] Cells => _cells;

}
