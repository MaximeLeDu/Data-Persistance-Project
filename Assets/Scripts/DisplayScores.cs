using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScores : MonoBehaviour
{

    [SerializeField] TMP_Text players;
    [SerializeField] TMP_Text scores;

    // Start is called before the first frame update
    void Start()
    {
        DisplayHighScores();
    }
    
    private void DisplayHighScores()
    {
        PlayerScore[] playerScore = SaveManager.Instance.LoadHighScores();

        players.text = "";
        scores.text = "";
        for(int i= playerScore.Length - 1; i >= 0; i--)
        {
            if (!string.IsNullOrEmpty(playerScore[i].player))
            {
                players.text += playerScore[i].player;
                players.text += "\n";
                scores.text += playerScore[i].score;
                scores.text += "\n";
            }
            else
            {
                players.text += "___";
                players.text += "\n";
                scores.text += 0;
                scores.text += "\n";
            }
        }
    }
}
