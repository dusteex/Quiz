using UnityEngine;
using UnityEngine.Events;

public class ChoiceChecker : MonoBehaviour
{
    static public UnityEvent<Cell> ChoiceEvent = new UnityEvent<Cell>(); 

    private string _correctAnswer;

    private void Start()
    {
        ChoiceEvent.AddListener(CheckAnswer);
    }

    public void SetAnswer(string answer)
    {
        _correctAnswer = answer;
    }

    public void CheckAnswer(Cell cell)
    {
        if(cell.CellData.Identifier == _correctAnswer)
        {
            Spawner.NextLevelEvent.Invoke();
        }
        else 
        {
            cell.CellAC.WrongChoiceState();
        }
    }

}
