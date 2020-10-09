using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class SudokuGenerator : MonoBehaviour
{
    public int[] finishedGrid = new int[81];
    public int[] missingGrid = new int[81];

    public int[,] sudokuMatrix = new int[9,9];
    public int missingDigits;
    List<int> sudokuNums = new List<int>();
    public Text sudokuGrid;
    System.Random random = new System.Random();
    public SinglePlayerSudokuHandler spsHandler;

    void Start()
    {
        spsHandler = GetComponent<SinglePlayerSudokuHandler>();
        missingDigits = 60;
        for (int i = 1; i < 10; i++)
        {
            sudokuNums.Add(i);
        }
        FillBoxes();
        SortNumbers();
        EraseNumbers(missingDigits);
        spsHandler.StartHandler();
    }

    void FillBoxes()
    {
        List<int> diagonalNums = new List<int>();
        int num = 9;
        for(int i = 0; i<9; i++)
        {
            if (i == 0)
            {
                for (int p = 1; p < 10; p++)
                {
                    diagonalNums.Add(p);
                }
                num = 9;
            }
            if (i == 3)
            {
                for (int p = 1; p < 10; p++)
                {
                    diagonalNums.Add(p);
                }
                num = 9;
            }
            if (i == 6)
            {
                for (int p = 1; p < 10; p++)
                {
                    diagonalNums.Add(p);
                }
                num = 9;
            }
            for (int j = 0; j < 3; j++)
            {
                int randomNum = RandomGenerator(num);
                sudokuMatrix[i, j] = diagonalNums[randomNum];
                diagonalNums.RemoveAt(randomNum);
                num--;
            }
        }
        for (int i = 0; i < 9; i++)
        {
            if (i == 0)
            {
                for (int p = 1; p < 10; p++)
                {
                    diagonalNums.Add(p);
                }
                num = 9;
            }
            if (i == 3)
            {
                for (int p = 1; p < 10; p++)
                {
                    diagonalNums.Add(p);
                }
                num = 9;
            }
            if (i == 6)
            {
                for (int p = 1; p < 10; p++)
                {
                    diagonalNums.Add(p);
                }
                num = 9;
            }
            for (int j = 3; j < 6; j++)
            {
                int randomNum = RandomGenerator(num);
                sudokuMatrix[i, j] = diagonalNums[randomNum];
                diagonalNums.RemoveAt(randomNum);
                num--;
            }
        }
        for (int i = 0; i < 9; i++)
        {
            if (i == 0)
            {
                for (int p = 1; p < 10; p++)
                {
                    diagonalNums.Add(p);
                }
                num = 9;
            }
            if (i == 3)
            {
                for (int p = 1; p < 10; p++)
                {
                    diagonalNums.Add(p);
                }
                num = 9;
            }
            if (i == 6)
            {
                for (int p = 1; p < 10; p++)
                {
                    diagonalNums.Add(p);
                }
                num = 9;
            }
            for (int j = 6; j < 9; j++)
            {
                int randomNum = RandomGenerator(num);
                sudokuMatrix[i, j] = diagonalNums[randomNum];
                diagonalNums.RemoveAt(randomNum);
                num--;
            }
        }
    }

    void SortNumbers()
    {
        int[] grid = new int[81];
        int gridNum = 0;
        for (int i = 0; i<9; i++)
        {
            for (int j = 0; j<9; j++)
            {
                grid[gridNum] = sudokuMatrix[i, j];
                gridNum++;
            }
        }
        bool[] sorted = new bool[81];
        for (int i = 0; i < 9; i++)
        {
            bool backtrack = false;
            for (int a = 0; a<2; a++)
            {
                bool[] registered = new bool[10];
                int rowOrigin = i * 9;
                int colOrigin = i;

                for (int j = 0; j<9; j++)
                {
                    int step;
                    if (a%2 == 0)
                    {
                        step = rowOrigin + j;
                    }
                    else
                    {
                        step = colOrigin + j * 9;
                    }
                    int num = grid[step];
                    if (!registered[num])
                    {
                        registered[num] = true;
                    }
                    else
                    {
                        for (int y = j; y >= 0; y--)
                        {
                            int scan;
                            if (a % 2 == 0)
                            {
                                scan = i*9+y;
                            }
                            else
                            {
                                scan = i+9*y;
                            }
                            if (grid[scan] == num)
                            {
                                for(int z = (a % 2 == 0 ? (i % 3 + 1) * 3 : 0); z < 9; z++)
                                {
                                    if (a%2==1 && z%3 <= i % 3)
                                    {
                                        continue;
                                    }
                                    int boxOrigin = ((scan % 9) / 3) * 3 + (scan / 27) * 27;
                                    int boxStep = boxOrigin + (z / 3) * 9 + (z % 3);
                                    int boxNum = grid[boxStep];
                                    if ((!sorted[scan] && !sorted[boxStep] && !registered[boxNum]) || (sorted[scan] && !registered[boxNum] && (a % 2 == 0 ? boxStep % 9 == scan % 9 : boxStep / 9 == scan / 9)))
                                    {
                                        grid[scan] = boxNum;
                                        grid[boxStep] = num;
                                        registered[boxNum] = true;
                                        goto ROW_COL;
                                    }
                                    else if (z == 8)
                                    {
                                        int searchingNo = num;
                                        bool[] blindSwapIndex = new bool[81];
                                        for (int q = 0; q < 18; q++)
                                        {
                                            for (int b = 0; b <= j; b++)
                                            {
                                                int pacing = (a % 2 == 0 ? rowOrigin + b : colOrigin + b * 9);
                                                if (grid[pacing] == searchingNo)
                                                {
                                                    int adjacentCell = -1;
                                                    int adjacentNo = -1;
                                                    int decrement = (a % 2 == 0 ? 9 : 1);

                                                    for (int c = 1; c < 3 - (i % 3); c++)
                                                    {
                                                        adjacentCell = pacing + (a % 2 == 0 ? (c + 1) * 9 : c + 1);

                                                        //this creates the preference for swapping with unregistered numbers
                                                        if ((a % 2 == 0 && adjacentCell >= 81)
                                                              || (a % 2 == 1 && adjacentCell % 9 == 0)) adjacentCell -= decrement;
                                                        else
                                                        {
                                                            adjacentNo = grid[adjacentCell];
                                                            if (i % 3 != 0
                                                                           || c != 1
                                                                           || blindSwapIndex[adjacentCell]
                                                                           || registered[adjacentNo])
                                                                adjacentCell -= decrement;
                                                        }
                                                        adjacentNo = grid[adjacentCell];

                                                        //as long as it hasn't been swapped before, swap it
                                                        if (!blindSwapIndex[adjacentCell])
                                                        {
                                                            blindSwapIndex[adjacentCell] = true;
                                                            grid[pacing] = adjacentNo;
                                                            grid[adjacentCell] = searchingNo;
                                                            searchingNo = adjacentNo;

                                                            if (!registered[adjacentNo])
                                                            {
                                                                registered[adjacentNo] = true;
                                                                goto ROW_COL;
                                                            }
                                                            goto ENDSWAP;
                                                        }
                                                    }
                                                }
                                                
                                            }
                                            ENDSWAP:
                                            continue;
                                        }
                                        backtrack = true;
                                        goto END_ROW_COL;
                                    }
                                }
                            }
                        }
                    }
                    ROW_COL:
                    continue;
                }
                END_ROW_COL:
                if (a % 2 == 0)
                    for (int j = 0; j < 9; j++) sorted[i * 9 + j] = true; //setting row as sorted
                else if (!backtrack)
                    for (int j = 0; j < 9; j++) sorted[i + j * 9] = true; //setting column as sorted
                else //reseting sorted cells through to the last iteration
                {
                    backtrack = false;
                    for (int j = 0; j < 9; j++) sorted[i * 9 + j] = false;
                    for (int j = 0; j < 9; j++) sorted[(i - 1) * 9 + j] = false;
                    for (int j = 0; j < 9; j++) sorted[i - 1 + j * 9] = false;
                    i -= 2;
                }
            }
        }
        gridNum = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                sudokuMatrix[i, j] = grid[gridNum];
                gridNum++;
            }
        }
        finishedGrid = grid;
    }

    void EraseNumbers(int wantedOut)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i<81; i++)
        {
            indexes.Add(i);
        }
        int[] grid = new int[81];
        int gridNum = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                grid[gridNum] = sudokuMatrix[i, j];
                gridNum++;
            }
        }

        for (int p = 0; p < wantedOut; p++)
        {
            int num = RandomGenerator(indexes.Count);
            grid[indexes[num]] = 0;

            indexes.RemoveAt(num);
        }
        missingGrid = grid;

        gridNum = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                sudokuMatrix[i, j] = grid[gridNum];
                gridNum++;
            }
        }
    }

    int RandomGenerator(int num)
    {
        int k = random.Next(num);
        return k;
    }
}
