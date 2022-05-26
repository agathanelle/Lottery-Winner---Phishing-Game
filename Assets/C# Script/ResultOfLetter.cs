using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class ResultOfLetter : MonoBehaviour
{
    LetterList data;
    GameData gameData;
    Player player;

    [SerializeField]
    Text totalCoins;

    [SerializeField]
    Text comment;

    [SerializeField]
    Text learnedLost;

    [SerializeField]
    Text coins;

    [SerializeField]
    Text comment2;

    [SerializeField]
    Text explanation;

    [SerializeField]
    Text buttonText;

    string jsonPath = "Assets/Data/letters.json";
    string jsonPath2 = "Assets/Data/data.json";
    string jsonPath3 = "Assets/Data/player.json";

    int index;
    int nextLetter;

    void Start()
    {
        string jsonLetterData = File.ReadAllText(jsonPath);
        data = JsonUtility.FromJson<LetterList>(jsonLetterData);

        string jsonGameData = File.ReadAllText(jsonPath2);
        gameData = JsonUtility.FromJson<GameData>(jsonGameData);

        string jsonPlayerData = File.ReadAllText(jsonPath3);
        player = JsonUtility.FromJson<Player>(jsonPlayerData);

        totalCoins.text = player.currency.ToString();
        index = gameData.currentLetter;
        int answer = gameData.currentAnswer;

        if(answer == 0)
        {
            comment.text = "GOOD JOB!";
            learnedLost.text = "YOU HAVE EARNED";
            coins.text = "250";
            comment2.text = "You were right!";
            explanation.text = data.letters[index].explanation;
        }
        else if (answer == 1)
        {
            comment.text = "COULD HAVE BEEN BETTER";
            learnedLost.text = "YOU HAVE LOST";
            coins.text = "250";
            comment2.text = "Some things to point out";
            explanation.text = data.letters[index].explanation;
        }
        else if (answer == 2)
        {
            comment.text = "YOU HAVE MISSED OPPORTUNITY";
            learnedLost.text = "YOU HAVE EARNED";
            coins.text = "0";
            comment2.text = "Some things to point out";
            explanation.text = data.letters[index].explanation;
        }
        nextLetter = index + 1;
        if(nextLetter%3==0)
        {
            buttonText.text = "See results";
        }
        else buttonText.text = "Continue";
    }

    public void NextScene()
    {
        if (nextLetter % 3 == 0)
        {
            SceneManager.LoadScene("Results");
        }
        else
        {
            gameData.currentLetter += 1;
            string data = JsonUtility.ToJson(gameData);
            File.WriteAllText(jsonPath2, data);
            SceneManager.LoadScene("Letter");
        }
    }

    [System.Serializable]
    public class Letter
    {
        public string sender;
        public string text;
        public bool liable;
        public string explanation;
    }

    [System.Serializable]
    public class LetterList
    {
        public List<Letter> letters;
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
