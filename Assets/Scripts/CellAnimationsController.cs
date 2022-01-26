using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CellAnimationsController : MonoBehaviour
{
    private Cell _cell;
    private Transform _cellContentT;
    private bool _onAnimation;
    public bool OnAnimation => _onAnimation;

 
    private void Awake()
    {
        _cell = GetComponent<Cell>();
        _cellContentT = _cell.transform.GetChild(0).transform;
    }

    public void WrongChoiceState()
    {
        ToggleChoicePossibility();
        StartCoroutine(AllowChoice());
       _cellContentT.DOPunchPosition(Vector3.right*0.5f,1).SetEase(Ease.InBounce); 
    }

    public void BounceSpawn()
    {
        _cell.transform.DOPunchScale(new Vector3(0.2f,0.2f,0),1f,2);
    }

    private IEnumerator AllowChoice()
    {
        yield return new WaitForSeconds(2f);
        ToggleChoicePossibility();
    }

    public void ToggleChoicePossibility()
    {
        _onAnimation = !_onAnimation;
    }
}
