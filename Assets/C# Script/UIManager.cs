using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public void GoToMainPage()
    {

        SceneManager.LoadScene("Main");
    }

    public void GoToOpening()
    {

        SceneManager.LoadScene("Opening");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //go to other topic
    public void Topic1()
    {
        SceneManager.LoadScene("Notes1");
    }
    public void Topic2()
    {
        SceneManager.LoadScene("Notes2");
    }
    public void Topic3()
    {
        SceneManager.LoadScene("Notes3");
    }
    public void Topic4()
    {
        SceneManager.LoadScene("Notes4");
    }

    public void CheckInfo()
    {
        SceneManager.LoadScene("CheckInfo");
    }
    public void ReturnToLetter()
    {
        SceneManager.LoadScene("Letter");
    }

}
