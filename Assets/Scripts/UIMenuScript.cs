using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIMenuScript : MonoBehaviour
{

    [SerializeField] private TMP_InputField playerName;

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainGame()
    {
        if (playerName != null)
        {
            if (!string.IsNullOrWhiteSpace(playerName.text))
            {
                SaveManager.Instance.SavePlayer(playerName.text);
                SceneManager.LoadScene(1);
            }
        }
    }

    public void LoadHighScores()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        SaveManager.Instance.SaveToFile();
        Application.Quit();
    }
}
