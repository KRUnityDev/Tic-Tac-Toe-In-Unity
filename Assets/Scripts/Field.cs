using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Field : MonoBehaviour, 
        IPointerClickHandler
{

    public enum StateOfField
    {
        EMPTY = 0,
        CIRCLE = 1,
        CROSS = 2
    }

    public UnityEngine.UI.Image FieldRepresentation;

    private Sprite crossSprite, circleSprite, emptySprite;
    private StateOfField state;
    public StateOfField State
    {
        get { return state; }
        set { changeFieldState(value); } 
    }

    public void Awake()
    {
        crossSprite = Resources.Load<Sprite>("crossSprite");
        circleSprite = Resources.Load<Sprite>("circleSprite");
        emptySprite = Resources.Load<Sprite>("emptySprite");
    }

    private void changeFieldState(StateOfField newState)
    {
        state = newState;
        switch(newState)
        {
            case Field.StateOfField.EMPTY:
                FieldRepresentation.sprite = emptySprite;
                break;
            case Field.StateOfField.CIRCLE:
                FieldRepresentation.sprite = circleSprite;
                break;
            case Field.StateOfField.CROSS:
                FieldRepresentation.sprite = crossSprite;
                break;
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("I Was clicked!");
        GameManager.Instance.FieldWasClicked(this);
    }
}
