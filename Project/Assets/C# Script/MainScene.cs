using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    GameObject panel;
    bool activePanel;
    GameData gameData;
    Player playerData;

    string jsonPath2 = "Assets/Data/data.json";
    string jsonPath3 = "Assets/Data/player.json";

    [SerializeField]
    Text label;

    [SerializeField]
    Text coins;

    // Start is called before the first frame update
    void Start()
    {
        label.enabled = false;
        string jsonGameData = File.ReadAllText(jsonPath2);
        gameData = JsonUtility.FromJson<GameData>(jsonGameData);

        string jsonPlayerData = File.ReadAllText(jsonPath3);
        playerData = JsonUtility.FromJson<Player>(jsonPlayerData);
        coins.text = playerData.currency.ToString();

        panel = GameObject.Find("Panel");
        activePanel = false;
        panel.SetActive(activePanel);
    }

    public void GoToLetters()
    {
        if (gameData.topic > 4 || gameData.topic < 0)
        {
            label.enabled = true;
        }
        else
        {
            label.enabled = false;
            SceneManager.LoadScene("LetterTutorial");
        }
    }

    public void HideShowLabel()
    {
        activePanel = !activePanel;
        panel.SetActive(activePanel);
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
