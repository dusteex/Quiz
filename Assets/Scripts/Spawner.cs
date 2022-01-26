using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class Spawner : MonoBehaviour
{
    static public UnityEvent NextLevelEvent = new UnityEvent(); 

    [SerializeField] private CellsPack[] _cellsPacks;
    [Header("X - rows count , Y - columns count")]
    [SerializeField] private Vector2Int[] _sizesByLevel;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private TextMeshProUGUI _taskTextField;
    [SerializeField] private ChoiceChecker _choiceChecker;

    private Vector3 _startPosition;
    private int _currentLevel;
    private Vector2 _cellSize;
    private List<CellData> _availableCells;
    private CellData[] _settedCells;
    private int n , m;

    void Start()
    {
        _startPosition = transform.position;
        NextLevelEvent.AddListener(NextLevel);
        SpriteRenderer cellSR = _cellPrefab.GetComponent<SpriteRenderer>();
        _cellSize = new Vector2(cellSR.sprite.rect.width/cellSR.sprite.pixelsPerUnit , cellSR.sprite.rect.height/cellSR.sprite.pixelsPerUnit);
        SetCells(true);
    }

    private void NextLevel()
    {
        _currentLevel++;
        for(int i = 0 ; i < transform.childCount ; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        if(_currentLevel >= _sizesByLevel.Length) Restart();
        else SetCells();

    }

    private void Restart()
    {
        _currentLevel = 0;
        SetCells(true);
    }

    private void SetCells(bool isFirstSpawn = false)
    {
        CellsPack randomPack = _cellsPacks[UnityEngine.Random.Range(0,_cellsPacks.Length)];
        _availableCells = new List<CellData>(randomPack.Cells);

        n = _sizesByLevel[_currentLevel].x;
        m = _sizesByLevel[_currentLevel].y;        
        _settedCells = new CellData[n*m];

        for(int i = 0 ; i < n ; i++)
        {
            for(int j = 0 ; j < m ; j++)
            {
                Cell newCell = Instantiate(_cellPrefab,transform.position + GetOffset(new Vector2(j,i)),Quaternion.identity).GetComponent<Cell>();
                newCell.transform.parent = transform;
                int randomCellIndex = UnityEngine.Random.Range(0,_availableCells.Count);
                newCell.Initialization(_availableCells[randomCellIndex]);
                newCell.ClampSprite(_cellSize);
                _settedCells[m*i + j] = _availableCells[randomCellIndex];
                _availableCells.RemoveAt(randomCellIndex);
                if(isFirstSpawn) newCell.CellAC.BounceSpawn();
            }
        }
        transform.position  = _startPosition + new Vector3(Convert.ToByte((m%2)==0)*_cellSize.x/4,Convert.ToByte((n%2)==0)*_cellSize.x/4,transform.position.z);
        SetTask();
    }

    private void SetTask()
    {
        CellData randomCell = _settedCells[UnityEngine.Random.Range(0,n*m)];
        _taskTextField.text = randomCell.TaskText;
        _choiceChecker.SetAnswer(randomCell.Identifier);
    }

    private Vector3 GetOffset(Vector2 Indexes)
    {
        SpriteRenderer cellSR = _cellPrefab.GetComponent<SpriteRenderer>();
        return new Vector3(_cellPrefab.transform.localScale.x * cellSR.sprite.rect.width*(Indexes.x-(int)(m/2))/cellSR.sprite.pixelsPerUnit ,
                           _cellPrefab.transform.localScale.y *cellSR.sprite.rect.height*(Indexes.y-(int)(n/2))/cellSR.sprite.pixelsPerUnit,0); 
    }

}
