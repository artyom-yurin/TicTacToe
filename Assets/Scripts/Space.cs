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
        textButton.text = gameController.GetPlayerSide();
        button.interactable = false;
    }

    public void SetGameControllerReference(GameController gameController)
    {
        this.gameController = gameController;
    }
}
