using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

    public static GameBoard Instance;
    //Fields
    public Field LeftTop, MiddleTop, RightTop, LeftMiddle, Center, RightMiddle, LeftDown, MiddleDown, RightDown; 
    
    public void Awake()
    {
        if (GameBoard.Instance == null)
            GameBoard.Instance = this;
        else
            Destroy(this);
    }

}
