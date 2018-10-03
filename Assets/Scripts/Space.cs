using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Space : MonoBehaviour {
    public Button button;
    public Text textButton;

    private GameController gameController;

    public void SetSign()
    {
        textButton.text = gameController.GetCurrentSide();
        button.interactable = false;
    }

    public void onClick()
    {
        SetSign();
        gameController.EndTurn();
    }

    public void SetGameControllerReference(GameController gameController)
    {
        this.gameController = gameController;
    }
}
