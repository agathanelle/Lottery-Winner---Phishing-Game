using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    Text sender;

    [SerializeField]
    Text subject;

    [SerializeField]
    Text letterText;

    [SerializeField]
    Text explanation;

    [SerializeField]
    Text topicTutorial;

    [SerializeField]
    Text buttonText;

    GameObject button;
    LetterList letterData;
    GameData gameData;

    string jsonPath = "Assets/Data/tutorial.json";
    string jsonPath2 = "Assets/Data/data.json";

    int index;
    
    void Start()
    {
        button = GameObject.Find("Letter Button");
        string jsonLetterData = File.ReadAllText(jsonPath);
        letterData = JsonUtility.FromJson<LetterList>(jsonLetterData);
        
        string jsonGameData = File.ReadAllText(jsonPath2);
        gameData = JsonUtility.FromJson<GameData>(jsonGameData);
        
        index = gameData.topic - 1;
        sender.text = letterData.letters[index].sender;
        subject.text = letterData.letters[index].subject;
        letterText.text = letterData.letters[index].text;
        explanation.text = letterData.letters[index].tutorial;
        topicTutorial.text = "Topic #" + gameData.topic + " tutorial";

        if(letterData.letters[index].buttonText == "") button.SetActive(false);
        else
        {
            button.SetActive(true);
            buttonText.text = letterData.letters[index].buttonText;
        }
    }

    public void NextTutorialStep()
    {
        SceneManager.LoadScene("LetterTutorial2");
    }

    public void AnswerClicked()
    {
        if(letterData.letters[index].liable) gameData.currentAnswer = 0;
        else gameData.currentAnswer = 1;
        
        string data = JsonUtility.ToJson(gameData);
        File.WriteAllText(jsonPath2, data);
        SceneManager.LoadScene("TutorialResults");
    }

    public void DeleteClicked()
    {
        if (!letterData.letters[index].liable) gameData.currentAnswer = 0;
        else gameData.currentAnswer = 1;
        
        string data = JsonUtility.ToJson(gameData);
        File.WriteAllText(jsonPath2, data);
        SceneManager.LoadScene("TutorialResults");
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
        public string tutorial;
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
}
