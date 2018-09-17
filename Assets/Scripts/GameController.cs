using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text[] buttonList;
    string playerSide;
    int turnCount;
    public GameObject winPanel;
    public Text winText;
    public Text Turn;
    public GameObject restartButton;
    bool endGame;

    void Awake()
    {
        endGame = false;
        turnCount = 0;
        winPanel.SetActive(false);
        restartButton.SetActive(false);
        playerSide = "X";
        Turn.text = playerSide;
        SetGameControllerReferenceOnButtons();
    }

    void SetGameControllerReferenceOnButtons()
    {
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Space>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        turnCount += 1;
        CheckWin();
        ChangePlayerSide();
    }

    public void CheckWin()
    {
        if(buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver();
        }

        if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver();
        }

        if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }

        if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }

        if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver();
        }

        if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }

        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }

        if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }

        if(!endGame && turnCount >= 9)
        {
            winPanel.SetActive(true);
            restartButton.SetActive(true);
            winText.text = "DRAW";
            SetBoardInteractable(false);
        }
    }

    public void SetBoardInteractable(bool state)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = state;
        }
    }

    void GameOver()
    {
        endGame = true;
        restartButton.SetActive(true);
        winPanel.SetActive(true);
        winText.text = playerSide + " WIN";
        SetBoardInteractable(false);
    }

    public void Restart()
    {
        turnCount = 0;
        endGame = false;
        winPanel.SetActive(false);
        restartButton.SetActive(false);
        playerSide = "X";
        Turn.text = playerSide;
        SetBoardInteractable(true);
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
    }

    public void ChangePlayerSide()
    {
        playerSide = (playerSide == "X") ? "O" :"X";
        Turn.text = playerSide;
    }
}
