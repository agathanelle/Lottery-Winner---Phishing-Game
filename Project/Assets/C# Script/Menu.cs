using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    GameObject button;
    string jsonPath3 = "Assets/Data/player.json";
    Player playerData;

    void Start()
    {
        button = GameObject.Find("Button - Continue");

        string jsonPlayerData = File.ReadAllText(jsonPath3);
        playerData = JsonUtility.FromJson<Player>(jsonPlayerData);

        if (playerData.firstTime) button.SetActive(false);
        else button.SetActive(true);
    }
    [System.Serializable]
    public class Player
    {
        public string name;
        public int currency;
        public bool firstTime;
    }
}
