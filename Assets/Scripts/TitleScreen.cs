using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    GameObject LoadScreen;
    [SerializeField]
    GameObject InstructionsScreen;

    private void Awake()
    {
        HidePopups();
    }

    public void HidePopups()
    {
        LoadScreen.SetActive(false);
        InstructionsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowLoadScreen()
    {
        LoadScreen.SetActive(true);
    }
    public void ShowInstructions()
    {
        InstructionsScreen.SetActive(true);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }
}
