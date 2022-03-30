using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintBestScore : MonoBehaviour
{

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        PlayerScore playerScore = SaveManager.Instance.LoadHighestHighScore();
        if(playerScore.player != null)
        {
            text.text = "Best Score : " + playerScore.score + " By " + playerScore.player;
        }
        else
        {
            text.text = "Make a new best score !";

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
