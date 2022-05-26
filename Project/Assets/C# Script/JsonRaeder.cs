using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class JsonRaeder : MonoBehaviour
{
    LetterList letterData;
    HintList hintData;
    GameData gameData;
    Player playerData;
    GameObject button;

    string jsonPath = "Assets/Data/letters.json";
    string jsonPath2 = "Assets/Data/data.json";
    string jsonPath3 = "Assets/Data/player.json";
    string jsonPath4 = "Assets/Data/hints.json";

    int index;
    
    [SerializeField]
    Text sender;

    [SerializeField]
    Text subject;

    [SerializeField]
    Text letterText;

    [SerializeField]
    Text hint;

    [SerializeField]
    Text buttonText;

    void Start()
    {
        button = GameObject.Find("Letter Button");

        string jsonLetterData = File.ReadAllText(jsonPath);
        letterData = JsonUtility.FromJson<LetterList>(jsonLetterData);

        string jsonGameData = File.ReadAllText(jsonPath2);
        gameData = JsonUtility.FromJson<GameData>(jsonGameData);

        string jsonPlayerData = File.ReadAllText(jsonPath3);
        playerData = JsonUtility.FromJson<Player>(jsonPlayerData);

        string jsonHintData = File.ReadAllText(jsonPath4);
        hintData = JsonUtility.FromJson<HintList>(jsonHintData);

        index = gameData.currentLetter;
        sender.text = letterData.letters[index].sender;
        subject.text = letterData.letters[index].subject;
        letterText.text = letterData.letters[index].text;

        if (letterData.letters[index].buttonText == "") button.SetActive(false);
        else
        {
            button.SetActive(true);
            buttonText.text = letterData.letters[index].buttonText;
        }
    }

    public void ClickOnAnswer()
    {
        if(letterData.letters[index].liable)
        {
            sender.text = letterData.letters[index].sender;
            letterText.text = letterData.letters[index].text;
            playerData.currency += 250;
            gameData.currentAnswer = 0;
            gameData.earnedMoney += 250;
            gameData.correct += 1;
        }
        else
        {
            playerData.currency -= 250;
            gameData.currentAnswer = 1;
            gameData.lostMoney += 250;
            gameData.incorrect += 1;
        }
        string data = JsonUtility.ToJson(gameData);
        File.WriteAllText(jsonPath2, data);

        string data2 = JsonUtility.ToJson(playerData);
        File.WriteAllText(jsonPath3, data2);

        SceneManager.LoadScene("LetterResult");
    }

    public void ClickOnDelete()
    {
        if (!letterData.letters[index].liable)
        {
            sender.text = letterData.letters[index].sender;
            letterText.text = letterData.letters[index].text;
            playerData.currency += 250;
            gameData.currentAnswer = 0;
            gameData.earnedMoney += 250;
            gameData.correct += 1;
        }
        else
        {
            gameData.currentAnswer = 2;
            gameData.avoidedLetters += 1;

        }
        string data = JsonUtility.ToJson(gameData);
        File.WriteAllText(jsonPath2, data);

        string data2 = JsonUtility.ToJson(playerData);
        File.WriteAllText(jsonPath3, data2);

        SceneManager.LoadScene("LetterResult");
    }

    public void ClickOnHint()
    {
        hint.text = hintData.hints[index].hint;
    }

    [System.Serializable]
    public class Letter
    {
        public string sender;
        public string subject;
        public string text;
        public bool liable;
        public string buttonText;
        public string explanation;
    }

    [System.Serializable]
    public class LetterList
    {
        public List<Letter> letters;
    }

    [System.Serializable]
    public class Hint
    {
        public string hint;
    }

    [System.Serializable]
    public class HintList
    {
        public List<Hint> hints;
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
