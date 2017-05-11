using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatementsManager : MonoBehaviour {
    public static StatementsManager Instance;

    public GameObject statementGameObject;
    public UnityEngine.UI.Text statementMessage;
    public UnityEngine.UI.Text statementButtonText;
    public UnityEngine.UI.Button statementButton;

    public void Awake()
    {

        if (StatementsManager.Instance == null)
            StatementsManager.Instance = this;
        else
            Destroy(this);
    }

    //Showing default statement with close button
    public void ShowStatement(string message)
    {
        statementMessage.text = message;
        statementGameObject.SetActive(true);
    }

    public void ShowStatement(string message, string buttonText, UnityEngine.Events.UnityAction methodCalledWhenButtonBeenClicked)
    {
        statementMessage.text = message;
        statementButtonText.text = buttonText;
        statementButton.onClick.AddListener(methodCalledWhenButtonBeenClicked);
        statementGameObject.SetActive(true);
    }
}
