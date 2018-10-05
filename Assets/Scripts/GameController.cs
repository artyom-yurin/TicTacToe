using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;


public class GameController : MonoBehaviour {

    public int mode;
    public Text[] buttonList;
    string playerSide;
    string computerSide;
    string currentSide;
    public GameObject winPanel;
    public Text winText;
    public Text Turn;
    public GameObject restartButton;

    void Awake()
    {
        winPanel.SetActive(false);
        restartButton.SetActive(false);
        playerSide = "X";
        computerSide = "O";
        currentSide = "X";
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

    public string GetCurrentSide()
    {
        return currentSide;
    }

    public void EndTurn()
    {
        if(!CheckEndGame())
        {
            ChangePlayerSide();
        }
    }

    public bool Winning(Text[] buttonList, string playerSide)
    {
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            return true;
        }

        if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            return true;
        }

        if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            return true;
        }

        if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            return true;
        }

        if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            return true;
        }

        if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            return true;
        }

        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            return true;
        }

        if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            return true;
        }

        return false;
    }

    public bool CheckEndGame()
    {
        if(Winning(buttonList, currentSide))
        {
            GameOver();
            return true;
        }
        else if(GetAvailableCells(buttonList).Count == 0)
        {
            winPanel.SetActive(true);
            restartButton.SetActive(true);
            winText.text = "DRAW";
            SetBoardInteractable(false);
            return true;
        }
        return false;
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
        restartButton.SetActive(true);
        winPanel.SetActive(true);
        winText.text = currentSide + " WIN";
        SetBoardInteractable(false);
    }

    public void Restart()
    {
        winPanel.SetActive(false);
        restartButton.SetActive(false);
        playerSide = "X";
        computerSide = "O";
        currentSide = "X";
        Turn.text = playerSide;
        SetBoardInteractable(true);
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
    }

    public List<Text> GetAvailableCells(Text[] board)
    {
        List<Text> availableCell = new List<Text>();
        foreach (Text cell in board)
        {
            if(cell.text == "")
            {
                availableCell.Add(cell);
            }
        }
        return availableCell;
    }

    public int Minimax(Text[] board, int depth, string player)
    {
        List<Text> availableCells = GetAvailableCells(board);

        if(Winning(board, computerSide))
        {
            return (10 - depth);
        }
        else if (Winning(board, playerSide))
        {
            return (-10 + depth);
        }
        else if (availableCells.Count == 0)
        {
            return 0;
        }

        if(player == computerSide)
        {
            int best = -1000;
            for(int i = 0; i < availableCells.Count; i++)
            {
                availableCells[i].text = computerSide;
                best = Mathf.Max(best, Minimax(board, depth + 1, playerSide));
                availableCells[i].text = "";
            }
            return best;
        }
        else
        {
            int best = 1000;
            for (int i = 0; i < availableCells.Count; i++)
            {
                availableCells[i].text = playerSide;
                best = Mathf.Min(best, Minimax(board, depth + 1, computerSide));
                availableCells[i].text = "";
            }
            return best;
        }
    }

    public int bestTurn()
    {
        int bestVal = -1000;
        int index = 0;

        for (int i = 0; i < buttonList.Length; i++)
        {
            if (buttonList[i].text == "") {
                buttonList[i].text = computerSide;
                int currentVal = Minimax(buttonList, 0, playerSide);
                buttonList[i].text = "";
                if (currentVal > bestVal)
                {
                    bestVal = currentVal;
                    index = i;
                }
            }
        }

        return index;
    }

    public void ChangePlayerSide()
    {
        if(mode == 0)
        {
            if (currentSide == playerSide)
            {
                currentSide = computerSide;
                Turn.text = currentSide;
                buttonList[bestTurn()].GetComponentInParent<Space>().SetSign();

                EndTurn();
            }
            else
            {
                currentSide = playerSide;
                Turn.text = currentSide;
            }
        }
        else
        {
            currentSide = (currentSide == "X") ? "O" : "X";
            Turn.text = currentSide;
        }
    }
}
