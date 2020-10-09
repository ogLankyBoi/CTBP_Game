using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerSudokuHandler : MonoBehaviour
{
    public SudokuGenerator sudokuGen;
    public int[] finishedSudoku = new int[81];
    public int[] missingSudoku = new int[81];

    public GameObject sudokuBoard;

    public void StartHandler()
    {
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
}
