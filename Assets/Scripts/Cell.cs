using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _contentSR;
    private CellData _cellData;
    public CellData CellData => _cellData;

    private CellAnimationsController _cellAC;
    public CellAnimationsController CellAC => _cellAC;

    private void Awake()
    {
        _cellAC = GetComponent<CellAnimationsController>();
    }

    public void Initialization(CellData cellData)
    {
        this._cellData = cellData;
        _contentSR.sprite = _cellData.ContentSprite;
    }

    public void ClampSprite(Vector2 borderSize)
    {
        Sprite resultSprite = _contentSR.sprite;
        Vector2 spriteSize = new Vector2(resultSprite.rect.width/resultSprite.pixelsPerUnit , resultSprite.rect.height/resultSprite.pixelsPerUnit);
        Vector2 deviation =  new Vector2 (spriteSize.x - borderSize.x , spriteSize.y - borderSize.y );
        float scaleCF;
        if(deviation.x > 0 || deviation.y > 0)
        {
            if(deviation.x > deviation.y) 
            {
                scaleCF = 1 - ( deviation.x / spriteSize.x);
            }
            else scaleCF = 1 - ( deviation.y / spriteSize.y);
            scaleCF-=0.2f;
            _contentSR.transform.localScale = new Vector2(scaleCF,scaleCF);
        }
    }

    private void OnMouseDown()
    {
        if(_cellAC.OnAnimation == false)
            ChoiceChecker.ChoiceEvent.Invoke(this);
    }
}
