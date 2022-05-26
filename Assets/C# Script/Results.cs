using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    GameData gameData;
    Player player;

    [SerializeField]
    Text correctAmount;

    [SerializeField]
    Text wrongAmount;

    [SerializeField]
    Text avoidedLetters;

    [SerializeField]
    Text earnedAmount;

    [SerializeField]
    Text lostAmount;

    string jsonPath2 = "Assets/Data/data.json";
    string jsonPath3 = "Assets/Data/player.json";

    // Start is called before the first frame update
    void Start()
    {
        string jsonGameData = File.ReadAllText(jsonPath2);
        gameData = JsonUtility.FromJson<GameData>(jsonGameData);

        string jsonPlayerData = File.ReadAllText(jsonPath3);
        player = JsonUtility.FromJson<Player>(jsonPlayerData);

        correctAmount.text = gameData.correct.ToString();
        wrongAmount.text = gameData.incorrect.ToString();
        avoidedLetters.text = gameData.avoidedLetters.ToString();
        earnedAmount.text = gameData.earnedMoney.ToString();
        lostAmount.text = gameData.lostMoney.ToString();
    }

    public void ContinueClicked()
    {
        if(player.currency > 0)
        {
            gameData.topic += 1;
            gameData.currentLetter += 1;
            gameData.correct = 0;
            gameData.incorrect = 0;
            gameData.avoidedLetters = 0;
            gameData.currentAnswer = 0;
            gameData.earnedMoney = 0;
            gameData.lostMoney = 0;
            string data = JsonUtility.ToJson(gameData);
            File.WriteAllText(jsonPath2, data);
            SceneManager.LoadScene("Main");
        }
        else
        {
            int oldCurrency = player.currency;
            int newCurrency = oldCurrency - gameData.earnedMoney + gameData.lostMoney;
            player.currency = newCurrency;
            gameData.currentLetter -= 2;
            gameData.correct = 0;
            gameData.incorrect = 0;
            gameData.avoidedLetters = 0;
            gameData.currentAnswer = 0;
            gameData.earnedMoney = 0;
            gameData.lostMoney = 0;
            string data = JsonUtility.ToJson(gameData);
            File.WriteAllText(jsonPath2, data);
            string data2 = JsonUtility.ToJson(player);
            File.WriteAllText(jsonPath3, data2);
            SceneManager.LoadScene("GameOver");
        }
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
