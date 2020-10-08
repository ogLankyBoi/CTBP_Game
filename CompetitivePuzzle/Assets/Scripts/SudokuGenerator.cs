using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SudokuGenerator : MonoBehaviour
{
    public int[,] sudokuMatrix = new int[9,9];
    public int missingDigits;

    //public SavedData savedData;

    void Start()
    {
        //savedData = GameObject.Find("SavedData").GetComponent<SavedData>();
        missingDigits = 60;
        FillDiagonal();
        FillRemaining(0, 3);
        RemoveDigits();
        PrintSudoku();
    }

    void FillDiagonal()
    {
        for(int i = 0; i<9; i += 3)
        {
            FillBox(i, i);
        }
    }

    void FillBox(int row, int col)
    {
        int num;
        for (int i=0; i<3; i++)
        {
            for (int j=0; j<3; j++)
            {
                num = RandomGenerator(9);
                while (!unUsedInBox(row, col, num));
                sudokuMatrix[row + i,col + j] = num;
            }
        }
    }

    int RandomGenerator(int num)
    {
        System.Random random = new System.Random();
        int k = random.Next(num);
        return k;
    }

    bool unUsedInBox(int rowStart, int colStart, int num)
    {
        for (int i = 0; i<3; i++)
        {
            for (int j = 0; j<3; j++)
            {
                if (sudokuMatrix[rowStart + i,colStart + j] == num)
                {
                    return false;
                }
            }
        }
        return true;
    }

    bool unUsedInRow(int i, int num)
    {
        for (int j = 0; j<9; j++)
        {
            if (sudokuMatrix[i,j] == num)
            {
                return false;
            }
        }
        return true;
    }

    bool unUsedInCol(int j, int num)
    {
        for (int i = 0; i < 9; i++)
        {
            if (sudokuMatrix[i,j] == num)
            {
                return false;
            }
        }
        return true;
    }

    bool FillRemaining(int i, int j)
    {
        if (j>=9 && i < 8)
        {
            i += 1;
            j = 0;
        }
        if (i>=9 && j >= 9)
        {
            return true;
        }
        if (i < 3)
        {
            if (j < 3)
            {
                j = 3;
            }
        }
        else if(i < 6)
        {
            if (j == (int)(i / 3) * 3)
            {
                j += 3;
            }
        }
        else
        {
            if (j == 6)
            {
                i += 1;
                j = 0;
                if (i >= 9)
                {
                    return true;
                }
            }
        }
        for (int num = 1; num<=9; num++)
        {
            if (CheckIfSafe(i, j, num))
            {
                sudokuMatrix[i,j] = num;
                if (FillRemaining(i, j + 1))
                {
                    return true;
                }
                sudokuMatrix[i,j] = 0;
            }
        }
        return false;
    }

    bool CheckIfSafe(int i, int j, int num)
    {
        return (unUsedInRow(i, num) && unUsedInCol(j, num) && unUsedInBox(i - i % 3, j - j % 3, num));
    }

    public void RemoveDigits()
    {
        int count = missingDigits;
        while (count != 0)
        {
            int cellId = RandomGenerator(81);
            int i = (cellId / 9);
            int j = cellId % 9;
            if (j != 0)
            {
                j -= 1;
            }
            if (sudokuMatrix[i,j] != 0)
            {
                count--;
                sudokuMatrix[i,j] = 0;
            }
        }
    }

    public void PrintSudoku()
    {
        for (int i = 0; i<9; i++)
        {
            for (int j = 0; i<9; j++)
            {
                Debug.Log(sudokuMatrix[i,j] + " ");

            }
            Debug.Log("\n");
        }
        Debug.Log("\n");
    }
}
