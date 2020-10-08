using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public Slider difficultySlider;
    public Slider timerSlider;

    public Text difficultyText;
    public string[] difficultyTextArray = { "Easy", "Normal", "Hard" };
    public Text timerText;
    public string[] timerTextArray = {"15s", "30s", "45s"};

    public Toggle sudokuToggle;
    public Toggle sudokuToggleToggle;
    public Toggle kakuroToggle;
    public Toggle kakuroToggleToggle;
    public Toggle nonogramToggle;
    public Toggle nonogramToggleToggle;

    public Toggle multiplayerToggle;
    public Toggle multiplayerToggleToggle;

    public InputField nameInput;
    public TouchScreenKeyboard keyboard;

    public Text playButtonText;
    public string[] playButtonTextArray = { "Play Game", "Find Game" };

    public SceneChanger sceneChanger;
    public SavedData savedData;

    void Start()
    {
        sudokuToggleToggle = sudokuToggle.GetComponent<Toggle>();
        kakuroToggleToggle = kakuroToggle.GetComponent<Toggle>();
        nonogramToggleToggle = nonogramToggle.GetComponent<Toggle>();
        multiplayerToggleToggle = multiplayerToggle.GetComponent<Toggle>();

        sceneChanger = GetComponent<SceneChanger>();
        savedData = GameObject.Find("SavedData").GetComponent<SavedData>();
    }

    void Update()
    {
        /*if(nameInput.isFocused == true && keyboard.active == false)
        {
            keyboard.active = true;
        }*/
    }

    public void DiffSliderChanged()
    {
        difficultyText.text = difficultyTextArray[(int)difficultySlider.value];
    }

    public void TimerSliderChanged()
    {
        timerText.text = timerTextArray[(int)timerSlider.value];
    }

    public void MultiplayerToggleChanged()
    {
        switch (multiplayerToggleToggle.isOn)
        {
            case true:
                playButtonText.text = playButtonTextArray[1];
                break;
            case false:
                playButtonText.text = playButtonTextArray[0];
                break;
        }
    }

    public void SudokuToggleChanged()
    {
        if (sudokuToggleToggle.isOn)
        {
            sudokuToggleToggle.interactable = false;
            kakuroToggleToggle.isOn = false;
            nonogramToggleToggle.isOn = false;
            kakuroToggleToggle.interactable = true;
            nonogramToggleToggle.interactable = true;
        }
    }

    public void KakuroToggleChanged()
    {
        if (kakuroToggleToggle.isOn)
        {
            kakuroToggleToggle.interactable = false;
            sudokuToggleToggle.isOn = false;
            nonogramToggleToggle.isOn = false;
            sudokuToggleToggle.interactable = true;
            nonogramToggleToggle.interactable = true;
        }
    }

    public void NonogramToggleChanged()
    {
        if (nonogramToggleToggle.isOn)
        {
            nonogramToggleToggle.interactable = false;
            kakuroToggleToggle.isOn = false;
            sudokuToggleToggle.isOn = false;
            kakuroToggleToggle.interactable = true;
            sudokuToggleToggle.interactable = true;
        }
    }
    
    public void OnPlayButton()
    {
        savedData.difficulty = (int)difficultySlider.value;
        savedData.timer = (int)timerSlider.value;

        switch (multiplayerToggleToggle.isOn)
        {
            case true:
                
                break;
            case false:
                //sceneChanger.SceneLoad("");
                break;
        }
    }
}
