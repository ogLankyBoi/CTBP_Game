using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Random = System.Random;

public class SinglePlayerSudokuHandler : MonoBehaviour
{
    public SudokuGenerator sudokuGen;
    public SceneChanger sceneChanger;
    public SavedData savedData;

    public Slider colorSlider;
    public Slider musicSlider;

    public int[] finishedSudoku = new int[81];
    public int[] missingSudoku = new int[81];
    public string selectedGrid = null;

    public List<string> placedSquares = new List<string>();

    public GameObject sudokuBoard;
    public GameObject wonPopUp;
    public GameObject settingsPopUp;

    public Text checkButtonText;

    public Color lightBlue;
    public Color lightPink;
    public Color lightGreen;

    public Camera mainCamera;
    public GameObject settingsBackground;
    public GameObject wonBackground;

    System.Random random = new System.Random();

    void Update()
    {
        if (placedSquares.Count != 0)
        {
            checkButtonText.text = "Check";
        }
        else
        {
            checkButtonText.text = "Hint";
        }
    }

    public void StartHandler()
    {
        savedData = GameObject.Find("SavedData").GetComponent<SavedData>();
        colorSlider.value = savedData.appColor;
        musicSlider.value = savedData.appMusic;
        settingsPopUp = GameObject.Find("SettingsScreen");
        settingsPopUp.SetActive(false);
        wonPopUp = GameObject.Find("WonGameScreen");
        wonPopUp.SetActive(false);
        sceneChanger = GetComponent<SceneChanger>();
        checkButtonText = GameObject.Find("CheckButton").GetComponentInChildren<Text>();
        sudokuBoard = GameObject.Find("SudokuBoard");
        sudokuGen = GetComponent<SudokuGenerator>();
        finishedSudoku = sudokuGen.finishedGrid;
        missingSudoku = sudokuGen.missingGrid;
        PrintSudokuInGrid();
    }

    public void PrintSudokuInGrid()
    {

        for (int i = 0; i<81; i++)
        {
            if (missingSudoku[i] != 0)
            {
                GameObject square = GameObject.Find(sudokuBoard.transform.GetChild(i + 1).name);
                Text q = square.GetComponentInChildren<Text>();
                q.text = finishedSudoku[i].ToString();
                square.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void OnGridButton()
    {
        GameObject selectedSquare = EventSystem.current.currentSelectedGameObject;
        selectedGrid = selectedSquare.name;
    }

    public void OnNumberButton()
    {
        if (selectedGrid != null)
        {
            string selectedAnswer = EventSystem.current.currentSelectedGameObject.name;
            string selectedNum = selectedAnswer[6].ToString();
            GameObject square = GameObject.Find(selectedGrid);
            if (!placedSquares.Contains(selectedGrid))
            {
                placedSquares.Add(selectedGrid);
            }
            Text q = square.GetComponentInChildren<Text>();
            q.text = selectedNum;
            q.color = Color.black;
            selectedGrid = null;
            int row = int.Parse(square.name[1].ToString()) - 1;
            int col = int.Parse(square.name[2].ToString()) - 1;
            missingSudoku[(row * 9) + col] = int.Parse(selectedAnswer[6].ToString());
            bool check = CheckPuzzleDone();
            if (check == true)
            {
                WonSudoku();
            }
        }
    }

    public void OnClearButton()
    {
        if (selectedGrid != null)
        {
            GameObject square = GameObject.Find(selectedGrid);
            Text q = square.GetComponentInChildren<Text>();
            q.text = " ";
            selectedGrid = null;
            placedSquares.Remove(square.name);
            int row = int.Parse(square.name[1].ToString()) - 1;
            int col = int.Parse(square.name[2].ToString()) - 1;
            missingSudoku[(row * 9) + col] = 0;
        }
    }

    public void OnSettingsButton()
    {
        settingsPopUp.SetActive(true);
    }

    public void OnColorSlider()
    {
        if (colorSlider.value == 0)
        {
            mainCamera.backgroundColor = lightBlue;
            settingsBackground.GetComponent<Image>().color = lightBlue;
            wonBackground.GetComponent<Image>().color = lightBlue;
        }
        else if (colorSlider.value == 1)
        {
            mainCamera.backgroundColor = lightPink;
            settingsBackground.GetComponent<Image>().color = lightPink;
            wonBackground.GetComponent<Image>().color = lightPink;
        }
        else
        {
            mainCamera.backgroundColor = lightGreen;
            settingsBackground.GetComponent<Image>().color = lightGreen;
            wonBackground.GetComponent<Image>().color = lightGreen;
        }
    }

    public void OnExitSettingsButton()
    {
        settingsPopUp.SetActive(false);
    }

    public void OnCheckButton()
    {
        if (placedSquares.Count != 0)
        {
            for (int i = 0; i < placedSquares.Count; i++)
            {
                int row = int.Parse(placedSquares[i][1].ToString()) - 1;
                int col = int.Parse(placedSquares[i][2].ToString()) - 1;
                if (finishedSudoku[(row*9)+col] == missingSudoku[(row * 9) + col])
                {
                    GameObject square = GameObject.Find(placedSquares[i]);
                    Text q = square.GetComponentInChildren<Text>();
                    q.color = Color.white;
                    square.GetComponent<Button>().interactable = false;
                }
                else
                {
                    GameObject square = GameObject.Find(placedSquares[i]);
                    Text q = square.GetComponentInChildren<Text>();
                    q.text = " ";
                    q.color = Color.white;
                    int row1 = int.Parse(square.name[1].ToString()) - 1;
                    int col1 = int.Parse(square.name[2].ToString()) - 1;
                    missingSudoku[(row1 * 9) + col1] = 0;
                }
            }
        }
        else
        {
            List<int> zeros = new List<int>();
            for (int i = 0; i<81; i++)
            {
                if (missingSudoku[i] == 0)
                {
                    zeros.Add(i);
                }
            }
            if (zeros.Count != 0)
            {
                int randomNum = random.Next(zeros.Count);
                GameObject square = GameObject.Find("S" + (zeros[randomNum] / 9 + 1).ToString() + (zeros[randomNum] % 9 + 1).ToString());
                Text q = square.GetComponentInChildren<Text>();
                q.text = finishedSudoku[zeros[randomNum]].ToString();
                missingSudoku[zeros[randomNum]] = finishedSudoku[zeros[randomNum]];
                square.GetComponent<Button>().interactable = false;
            }
            bool check = CheckPuzzleDone();
            if (check == true)
            {
                WonSudoku();
            }
        }
    }

    bool CheckPuzzleDone()
    {
        bool check = true;
        for (int i = 0; i<81; i++)
        {
            if (missingSudoku[i] != finishedSudoku[i])
            {
                check = false;
                break;
            }
        }
        return check;
    }

    void WonSudoku()
    {
        wonPopUp.SetActive(true);
    }

    public void OnYesButton()
    {
        savedData.appColor = (int)colorSlider.value;
        sceneChanger.SceneLoad("SinglePlayerSudoku");
    }

    public void OnNoButton()
    {
        savedData.appColor = (int)colorSlider.value;
        sceneChanger.SceneLoad("MainMenu");
    }
}