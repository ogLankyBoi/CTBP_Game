using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public Slider difficultySlider;
    public Slider timerSlider;
    public Slider colorSlider;
    public Slider musicSlider;

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

    public GameObject pickNameInput;
    public GameObject setNameInput;
    public string playerName;

    public Text playButtonText;
    public string[] playButtonTextArray = { "Play Game", "Find Game" };

    public SceneChanger sceneChanger;
    public SavedData savedData;
    public Camera mainCamera;

    public GameObject settingsScreen;
    public GameObject settingsBackground;
    public GameObject beginningScreen;
    public GameObject beginningBackground;

    public Color lightBlue;
    public Color lightPink;
    public Color lightGreen;

    void Start()
    {
        sudokuToggleToggle = sudokuToggle.GetComponent<Toggle>();
        kakuroToggleToggle = kakuroToggle.GetComponent<Toggle>();
        nonogramToggleToggle = nonogramToggle.GetComponent<Toggle>();
        multiplayerToggleToggle = multiplayerToggle.GetComponent<Toggle>();

        settingsScreen = GameObject.Find("SettingsScreen");
        settingsBackground = GameObject.Find("SettingsPopUp");
        beginningScreen = GameObject.Find("PopUpScreen");
        beginningBackground = GameObject.Find("BeginningPopUp");
        settingsScreen.SetActive(false);
        
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        sceneChanger = GetComponent<SceneChanger>();
        savedData = GameObject.Find("SavedData").GetComponent<SavedData>();

        playerName = savedData.playerName;
        if (playerName.Length == 0)
        {
            beginningScreen.SetActive(true);
        }
        else
        {
            beginningScreen.SetActive(false);
        }

        difficultySlider.value = savedData.difficulty;
        timerSlider.value = savedData.timer;
        colorSlider.value = savedData.appColor;
    }

    void Update()
    {
        
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
        savedData.appColor = (int)colorSlider.value;
        savedData.appMusic = (int)musicSlider.value;

        switch (multiplayerToggleToggle.isOn)
        {
            case true:
                FindGame();
                break;
            case false:
                sceneChanger.SceneLoad("SinglePlayerSudoku");
                break;
        }
    }

    public void FindGame()
    {

    }

    public void OnBeginningDoneButton()
    {
        playerName = pickNameInput.GetComponent<Text>().text;
        savedData.playerName = playerName;
        setNameInput.GetComponent<Text>().text = playerName;
        beginningScreen.SetActive(false);
    }

    public void OnSettingsButton()
    {
        settingsScreen.SetActive(true);
        setNameInput.GetComponent<Text>().text = playerName;
    }

    public void OnColorSlider()
    {
        if (colorSlider.value == 0)
        {
            mainCamera.backgroundColor = lightBlue;
            settingsBackground.GetComponent<Image>().color = lightBlue;
            beginningBackground.GetComponent<Image>().color = lightBlue;
        }
        else if (colorSlider.value == 1)
        {
            mainCamera.backgroundColor = lightPink;
            settingsBackground.GetComponent<Image>().color = lightPink;
            beginningBackground.GetComponent<Image>().color = lightPink;
        }
        else
        {
            mainCamera.backgroundColor = lightGreen;
            beginningBackground.GetComponent<Image>().color = lightGreen;
            settingsBackground.GetComponent<Image>().color = lightGreen;
        }
    }

    public void OnSetButton()
    {
        playerName = setNameInput.GetComponent<Text>().text;
        setNameInput.GetComponent<Text>().text = playerName;
        savedData.playerName = playerName;
    }

    public void OnExitSettingsButton()
    {
        settingsScreen.SetActive(false);
    }
}
