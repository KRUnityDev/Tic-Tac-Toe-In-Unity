using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public GameBoard gameBoard;
    public enum turn
    {
        firstPlayerTurn,
        secondPlayerTurn
    }
    private enum gameResoult
    {
        nobodyWon,
        somebodyWon,
        draw
    }
    public turn currentTurn;

    //Players marks
    private Field.StateOfField firstPlayerMarker;
    private Field.StateOfField secondPlayerMarker;

    //currentTurnUIRepresentation
    public UnityEngine.UI.Image crossCurrentTurnRepresentation;
    public UnityEngine.UI.Image circleCurrentTurnRepresentation;

    public void Awake()
    {

        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            firstPlayerMarker = Field.StateOfField.CROSS;
            secondPlayerMarker = Field.StateOfField.CIRCLE;
            currentTurn = turn.firstPlayerTurn;
            crossCurrentTurnRepresentation.enabled = true;
            circleCurrentTurnRepresentation.enabled = false;
        }   
        else
        {
            Destroy(this);
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restart Game");
        gameBoard.LeftTop.State = Field.StateOfField.EMPTY;
        gameBoard.MiddleTop.State = Field.StateOfField.EMPTY;
        gameBoard.RightTop.State = Field.StateOfField.EMPTY;
        gameBoard.LeftMiddle.State = Field.StateOfField.EMPTY;
        gameBoard.Center.State = Field.StateOfField.EMPTY;
        gameBoard.RightMiddle.State = Field.StateOfField.EMPTY;
        gameBoard.LeftDown.State = Field.StateOfField.EMPTY;
        gameBoard.MiddleDown.State = Field.StateOfField.EMPTY;
        gameBoard.RightDown.State = Field.StateOfField.EMPTY;
    }

    //This method checking if someone won game
    private gameResoult someoneWon()
    {
        //Horizontal
        if (gameBoard.LeftTop.State == gameBoard.MiddleTop.State &&
           gameBoard.LeftTop.State == gameBoard.RightTop.State &&
           gameBoard.MiddleTop.State == gameBoard.RightTop.State &&
           gameBoard.LeftTop.State != Field.StateOfField.EMPTY)
              return gameResoult.somebodyWon;

        if (gameBoard.LeftMiddle.State == gameBoard.Center.State &&
            gameBoard.LeftMiddle.State == gameBoard.RightMiddle.State &&
            gameBoard.Center.State == gameBoard.RightMiddle.State &&
           gameBoard.LeftMiddle.State != Field.StateOfField.EMPTY)
              return gameResoult.somebodyWon;

        if (gameBoard.LeftDown.State == gameBoard.MiddleDown.State &&
           gameBoard.LeftDown.State == gameBoard.RightDown.State &&
           gameBoard.MiddleDown.State == gameBoard.RightDown.State &&
           gameBoard.LeftDown.State != Field.StateOfField.EMPTY)
              return gameResoult.somebodyWon;
        //Vertical
        if (gameBoard.LeftTop.State == gameBoard.LeftMiddle.State &&
           gameBoard.LeftTop.State == gameBoard.LeftDown.State &&
           gameBoard.LeftMiddle.State == gameBoard.LeftDown.State &&
           gameBoard.LeftTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.MiddleTop.State == gameBoard.Center.State &&
           gameBoard.MiddleTop.State == gameBoard.MiddleDown.State &&
           gameBoard.Center.State == gameBoard.MiddleDown.State &&
           gameBoard.MiddleTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.RightTop.State == gameBoard.RightMiddle.State &&
           gameBoard.RightTop.State == gameBoard.RightDown.State &&
           gameBoard.RightMiddle.State == gameBoard.RightDown.State &&
           gameBoard.RightTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;
        //Cross
        if (gameBoard.LeftTop.State == gameBoard.Center.State &&
           gameBoard.LeftTop.State == gameBoard.RightDown.State &&
           gameBoard.Center.State == gameBoard.RightDown.State &&
           gameBoard.LeftTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.RightTop.State == gameBoard.Center.State &&
           gameBoard.RightTop.State == gameBoard.LeftDown.State &&
           gameBoard.Center.State == gameBoard.LeftDown.State &&
           gameBoard.RightTop.State != Field.StateOfField.EMPTY)
            return gameResoult.somebodyWon;

        if (gameBoard.LeftTop.State != Field.StateOfField.EMPTY &&
            gameBoard.MiddleTop.State != Field.StateOfField.EMPTY &&
            gameBoard.RightTop.State != Field.StateOfField.EMPTY &&
            gameBoard.LeftMiddle.State != Field.StateOfField.EMPTY &&
            gameBoard.Center.State != Field.StateOfField.EMPTY &&
            gameBoard.RightMiddle.State != Field.StateOfField.EMPTY &&
            gameBoard.LeftDown.State != Field.StateOfField.EMPTY &&
            gameBoard.MiddleDown.State != Field.StateOfField.EMPTY &&
            gameBoard.RightDown.State != Field.StateOfField.EMPTY)
            return gameResoult.draw;

        return gameResoult.nobodyWon;
    }

    public void FieldWasClicked(Field field)
    {
        if (field.State != Field.StateOfField.EMPTY) return;
        field.State = currentTurn == turn.firstPlayerTurn ? firstPlayerMarker : secondPlayerMarker;
        //Checking if someone won
        gameResoult currentGameReoult = someoneWon();
        if (currentGameReoult != gameResoult.nobodyWon) endGame(currentGameReoult);

        if (currentTurn == turn.firstPlayerTurn)
        {
            currentTurn = turn.secondPlayerTurn;
            crossCurrentTurnRepresentation.enabled = false;
            circleCurrentTurnRepresentation.enabled = true;
        }
        else
        {
            currentTurn = turn.firstPlayerTurn;
            crossCurrentTurnRepresentation.enabled = true;
            circleCurrentTurnRepresentation.enabled = false;
        }
    }

    private void endGame(gameResoult currentGameReoult)
    {
        Debug.Log("endGame");
        if(currentGameReoult == gameResoult.somebodyWon)
        {
            Debug.Log("somebodyWon");
            string winnerName = currentTurn == turn.firstPlayerTurn ? "First Player" : "Second Player";
            StatementsManager.Instance.ShowStatement(winnerName + " won", "Restart Game", this.RestartGame);
        }
        else if(currentGameReoult == gameResoult.draw)
        {
            Debug.Log("draw");
            StatementsManager.Instance.ShowStatement("Draw! Nobody won!", "Restart Game", this.RestartGame);
        }
        
    }
}
