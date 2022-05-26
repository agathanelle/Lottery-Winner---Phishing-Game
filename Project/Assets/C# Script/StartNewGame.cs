using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class StartNewGame : MonoBehaviour
{
    GameData gameData;
    Player playerData;

    string jsonPath2 = "Assets/Data/data.json";
    string jsonPath3 = "Assets/Data/player.json";
    // Start is called before the first frame update
    void Start()
    {
        string jsonGameData = File.ReadAllText(jsonPath2);
        gameData = JsonUtility.FromJson<GameData>(jsonGameData);

        string jsonPlayerData = File.ReadAllText(jsonPath3);
        playerData = JsonUtility.FromJson<Player>(jsonPlayerData);

        gameData.currentLetter = 0;
        gameData.currentAnswer = 0;
        gameData.correct = 0;
        gameData.incorrect = 0;
        gameData.topic = 1;
        gameData.earnedMoney = 0;
        gameData.lostMoney = 0;

        playerData.currency = 500;
        playerData.firstTime = false;

        string data = JsonUtility.ToJson(gameData);
        File.WriteAllText(jsonPath2, data);

        string data2 = JsonUtility.ToJson(playerData);
        File.WriteAllText(jsonPath3, data2);
    }

    public void GoToMainPage()
    {

        SceneManager.LoadScene("Main");
    }

    [System.Serializable]
    public class GameData
    {
        public int currentLetter;
        public int currentAnswer; // 0 - correct, 1 - incorrect, 2 - delete correct
        public int correct;
        public int incorrect;
        public int topic;
        public int earnedMoney;
        public int lostMoney;
        public int avoidedLetters;
    }
    [System.Serializable]
    public class Player
    {
        public string name;
        public int currency;
        public bool firstTime;
    }
}
