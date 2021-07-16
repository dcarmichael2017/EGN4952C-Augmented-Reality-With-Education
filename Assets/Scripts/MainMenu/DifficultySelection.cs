using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using QuantumTek.SimpleMenu;
using System;

public class DifficultySelection : MonoBehaviour
{

    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        easyButton.onClick.AddListener(onEasyClicked);
        mediumButton.onClick.AddListener(onMediumClicked);
        hardButton.onClick.AddListener(onHardClicked);
    }

    private void onEasyClicked()
    {
        GameValues.currentDifficulty = GameValues.Difficulties.Easy;
        try
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log(sceneName + " Difficulty: " + GameValues.currentDifficulty);
        }catch (Exception e)
        {
            Debug.Log("sceneName is not initialized " + e);
        }
    }

    private void onMediumClicked()
    {
        GameValues.currentDifficulty = GameValues.Difficulties.Medium;
        try
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log(sceneName + " Difficulty: " + GameValues.currentDifficulty);
        }
        catch (Exception e)
        {
            Debug.Log("sceneName is not initialized " + e);
        }
    }

    private void onHardClicked()
    {
        GameValues.currentDifficulty = GameValues.Difficulties.Hard;
        try
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log(sceneName + " Difficulty: " + GameValues.currentDifficulty);
        } catch (Exception e)
        {
            Debug.Log("sceneName is not initialized " + e);
        }
    }
}