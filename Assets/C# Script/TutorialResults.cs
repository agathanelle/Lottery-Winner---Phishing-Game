using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class TutorialResults : MonoBehaviour
{
    [SerializeField]
    Text learnedTopic;

    [SerializeField]
    Text comment;

    [SerializeField]
    Text explanation;

    LetterList letterData;
    GameData gameData;
    string jsonPath = "Assets/Data/tutorial.json";
    string jsonPath2 = "Assets/Data/data.json";

    int index;
    // Start is called before the first frame update
    void Start()
    {
        string jsonLetterData = File.ReadAllText(jsonPath);
        letterData = JsonUtility.FromJson<LetterList>(jsonLetterData);
        
        string jsonGameData = File.ReadAllText(jsonPath2);
        gameData = JsonUtility.FromJson<GameData>(jsonGameData);

        index = gameData.topic - 1;

        if(gameData.currentAnswer == 0)
        {
            learnedTopic.text = "YOU LEARNED TOPIC #"+gameData.topic.ToString();
            comment.text = "You were right!";
        }
        else if (gameData.currentAnswer == 1)
        {
            learnedTopic.text = "YOU STILL HAVE SOMETHING TO LEARN IN TOPIC #" + gameData.topic.ToString();
            comment.text = "Some things to point out:";   
        }
        explanation.text = letterData.letters[index].explanation;
    }

    public void StartLearning()
    {
        SceneManager.LoadScene("Letter");
    }


    [System.Serializable]
    public class Letter
    {
        public string sender;
        public string text;
        public bool liable;
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
