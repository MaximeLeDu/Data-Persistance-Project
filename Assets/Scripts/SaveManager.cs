using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


//Structures to store the name and score of each players in a way that is serializable in a JSON file
[Serializable]
public struct PlayerScore
{
    public string player;
    public int score;
}


//Comparer to classify an array of high scores between them
public class PlayerScoreComp : Comparer<PlayerScore>
{
    public override int Compare(PlayerScore player1, PlayerScore player2)
    {
        if (player1.score < player2.score) { return -1; }
        else if (player1.score > player2.score) { return 1; }
        else return 0;
    }
}

public class SaveManager : MonoBehaviour
{
    //Class that will actually save and load the data from file
    [Serializable]
    private class SaveData
    {
        public PlayerScore[] highScores = new PlayerScore[10];
        public PlayerScore lastPlayer;
    }
    private SaveData saveData = new SaveData();

    private PlayerScore currentPlayer;
    //On ne conserve que les 10 meilleurs joueurs
    private PlayerScore[] highScores = new PlayerScore[10];


    public static SaveManager Instance;
    //Indicates that a new high score has been reached
    public bool newHighScore = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        LoadFromFile();
    }

    public void SavePlayer(string name)
    {
        currentPlayer.player = name;
        currentPlayer.score = 0;
    }

    public void SavePlayerScore(int score)
    {
        currentPlayer.score = score;
        Debug.Log(highScores[0].score);
        if(currentPlayer.score > highScores[0].score)
        {
            highScores[0] = currentPlayer;
            Array.Sort(highScores, new PlayerScoreComp());
            Debug.Log("Whole list");
            for(int i = 0; i< highScores.Length; i++)
            {
                Debug.Log(highScores[i].score);
            }
            newHighScore = true;
            SaveToFile();
        }
    }

    public PlayerScore LoadPlayer()
    {
        return currentPlayer;
    }

    public PlayerScore[] LoadHighScores()
    {
        return highScores;
    }

    public PlayerScore LoadHighestHighScore()
    {
        return highScores[highScores.Length - 1];
    }

    public void SaveToFile()
    {
        saveData.highScores = highScores;
        saveData.lastPlayer = currentPlayer;
        string data = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "save_file.json", data);
    }

    public void LoadFromFile()
    {
        string filename = Application.persistentDataPath + "save_file.json";
        if (File.Exists(filename))
        {
            String data = File.ReadAllText(filename);
            saveData = JsonUtility.FromJson<SaveData>(data);

            highScores = saveData.highScores;
            currentPlayer = saveData.lastPlayer;
        }
    }

}
