using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSolver : MonoBehaviour
{
    [SerializeField]
    private GridSystem m_gridSystem;

    private const int c_startingX = 0;
    private const int c_startingY = 4;

    private const int c_finishX = 8;
    private const int c_finishY = 0;

    private Pipe m_currentPipe;
    private Pipe m_previousPipe;

    private PipeCell[,] m_cellGrid;

    public void SolveGrid()
    {
        bool win = false;

        m_cellGrid = m_gridSystem.CellGrid;

        Pipe startingPipe = m_cellGrid[c_startingX, c_startingY].Pipe;

        m_previousPipe = null;

        Debug.Log("First pipe color: "+ startingPipe.Color);
        Debug.Log("First pipe type: " + startingPipe.Type);

        int pipesCounter = 0;

        if (startingPipe.Color == PipeColors.Red && (startingPipe.Type == PipeTypes.Horizontal || startingPipe.Type == PipeTypes.LeftDown))
        {
            Debug.Log("First pipe pass!");

            int currentX = c_startingX;
            int currentY = c_startingY;

            m_currentPipe = startingPipe;

            bool finished = false;
            bool reachedEnd = false;

            do
            {
                int nextX1 = currentX;
                int nextY1 = currentY;

                int nextX2 = currentX;
                int nextY2 = currentY;

                switch (m_currentPipe.Type)
                {
                    case PipeTypes.Horizontal:
                        nextX1 = currentX + 1;
                        nextX2 = currentX - 1;
                        break;

                    case PipeTypes.Vertical:
                        nextY1 = currentY - 1;
                        nextY2 = currentY + 1;
                        break;

                    case PipeTypes.LeftDown:
                        nextY1 = currentY - 1;
                        nextX2 = currentX - 1;
                        break;

                    case PipeTypes.LeftUp:
                        nextY1 = currentY + 1;
                        nextX2 = currentX - 1;
                        break;

                    case PipeTypes.RightDown:
                        nextX1 = currentX + 1;
                        nextY2 = currentY - 1;
                        break;

                    case PipeTypes.RightUp:
                        nextX1 = currentX + 1;
                        nextY2 = currentY + 1;
                        break;
                }

                bool success = false;

                Debug.Log("Current X: "+currentX);
                Debug.Log("Current Y: " + currentY);
                Debug.Log("Pipe is last pipe!");

                // If is last pipe
                if (currentX == c_finishX && currentY == c_finishY)
                {
                    Debug.Log("Pipe is last pipe!");
                    Debug.Log("Last pipe type: "+ m_currentPipe.Type);
                    Debug.Log("Last pipe color: " + m_currentPipe.Color);

                    if ((m_currentPipe.Type == PipeTypes.Horizontal || m_currentPipe.Type == PipeTypes.RightUp) && m_currentPipe.Color == PipeColors.Green)
                    {
                        success = true;
                        finished = true;
                        reachedEnd = true;
                    }
                    else
                    {
                        success = true;
                        finished = true;
                    }
                }

                // Next pipe first attempt
                if (!success)
                {
                    if (PipeMatch(new Vector2(currentX, currentY), new Vector2(nextX1, nextY1)))
                    {
                        currentX = nextX1;
                        currentY = nextY1;

                        success = true;
                    }
                }

                // Next pipe second attempt
                if (!success)
                {
                    if (PipeMatch(new Vector2(currentX, currentY), new Vector2(nextX2, nextY2)))
                    {
                        currentX = nextX2;
                        currentY = nextY2;

                        success = true;
                    }
                }

                // Couldn't find next pipe
                if (!success)
                {
                    finished = true;
                }
                else
                {
                    pipesCounter++;
                    Debug.Log("Completed pipe "+pipesCounter);
                }
            } while (!finished);

            if (!reachedEnd)
            {
                Debug.Log("Couldn't reach end!");
            }
            else
            {
                win = true;
                Debug.Log("Sucess! You win!");
            }
        }
        else
        {
            Debug.Log("First pipe doesn't work!");
        }

        PipeGame.Instance.ShowEnd(win);
    }

    private bool PipeMatch(Vector2 pCurrentPipe, Vector2 pNextPipe)
    {

        Debug.Log("Current pipe color: " + m_currentPipe.Color);
        Debug.Log("Current pipe type: " + m_currentPipe.Type);

        bool match = false;
        if (pNextPipe.x < m_cellGrid.GetLength(0) && pNextPipe.x >= 0 && pNextPipe.y < m_cellGrid.GetLength(1) && pNextPipe.y >= 0)
        {
            Pipe nextPipe = m_cellGrid[Mathf.RoundToInt(pNextPipe.x), Mathf.RoundToInt(pNextPipe.y)].Pipe;

            if (m_previousPipe == null || nextPipe != m_previousPipe)
            {
                Debug.Log("Next pipe is acceptable");

                Debug.Log("Next pipe color: " + m_currentPipe.Color);
                Debug.Log("Next pipe type: " + m_currentPipe.Type);

                Pipe currentPipe = m_cellGrid[Mathf.RoundToInt(pCurrentPipe.x), Mathf.RoundToInt(pCurrentPipe.y)].Pipe;

                bool typeMatch = false;

                switch (currentPipe.Type)
                {
                    case PipeTypes.Horizontal:
                        switch (nextPipe.Type)
                        {
                            case PipeTypes.Horizontal:
                                typeMatch = true;
                                break;

                            case PipeTypes.LeftDown:
                                if(pCurrentPipe.x < pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.LeftUp:
                                if (pCurrentPipe.x < pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.RightDown:
                                if (pCurrentPipe.x > pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.RightUp:
                                if (pCurrentPipe.x > pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;
                        }
                        break;

                    case PipeTypes.Vertical:
                        switch (nextPipe.Type)
                        {
                            case PipeTypes.Vertical:
                                typeMatch = true;
                                break;

                            case PipeTypes.LeftDown:
                                if (pCurrentPipe.y < pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.RightDown:
                                if (pCurrentPipe.y < pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.LeftUp:
                                if (pCurrentPipe.y > pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.RightUp:
                                if (pCurrentPipe.y > pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;
                        }
                        break;

                    case PipeTypes.LeftDown:
                        switch (nextPipe.Type)
                        {
                            case PipeTypes.Horizontal:
                                if (pCurrentPipe.x < pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.Vertical:
                                if (pCurrentPipe.y < pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.LeftUp:
                                if (pCurrentPipe.y > pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.RightDown:
                                if (pCurrentPipe.x > pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.RightUp:
                                if (pCurrentPipe.y > pNextPipe.y || pCurrentPipe.x > pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;
                        }
                        break;

                    case PipeTypes.LeftUp:
                        switch (nextPipe.Type)
                        {
                            case PipeTypes.Horizontal:
                                if (pCurrentPipe.x > pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.Vertical:
                                if (pCurrentPipe.y > pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.LeftDown:
                                if (pCurrentPipe.y < pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.RightDown:
                                if (pCurrentPipe.x > pNextPipe.x || pCurrentPipe.y < pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.RightUp:
                                if (pCurrentPipe.x > pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;
                        }
                        break;

                    case PipeTypes.RightDown:
                        switch (nextPipe.Type)
                        {
                            case PipeTypes.Horizontal:
                                if (pCurrentPipe.x < pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.Vertical:
                                if (pCurrentPipe.y > pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.LeftDown:
                                if (pCurrentPipe.x < pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.LeftUp:
                                if (pCurrentPipe.y > pNextPipe.y || pCurrentPipe.x < pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.RightUp:
                                if (pCurrentPipe.y > pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;
                        }
                        break;

                    case PipeTypes.RightUp:
                        switch (nextPipe.Type)
                        {
                            case PipeTypes.Horizontal:
                                if (pCurrentPipe.x < pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.Vertical:
                                if (pCurrentPipe.y < pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.LeftDown:
                                if (pCurrentPipe.x < pNextPipe.x || pCurrentPipe.y < pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.LeftUp:
                                if (pCurrentPipe.x < pNextPipe.x)
                                {
                                    typeMatch = true;
                                }
                                break;

                            case PipeTypes.RightDown:
                                if (pCurrentPipe.y < pNextPipe.y)
                                {
                                    typeMatch = true;
                                }
                                break;
                        }
                        break;
                }

                bool colorMatch = false;

                switch (currentPipe.Color)
                {
                    case PipeColors.Blue:
                        if (nextPipe.Color == PipeColors.Green)
                        {
                            colorMatch = true;
                        }
                        break;

                    case PipeColors.Green:
                        if (nextPipe.Color == PipeColors.Red)
                        {
                            colorMatch = true;
                        }
                        break;

                    case PipeColors.Red:
                        if (nextPipe.Color == PipeColors.Blue)
                        {
                            colorMatch = true;
                        }
                        break;
                }

                match = typeMatch && colorMatch;

                // if match 
                if (match)
                {
                    m_previousPipe = m_currentPipe;
                    m_currentPipe = nextPipe;
                }

            }
        }
        else
        {
            Debug.Log("Couldn't get next pipe");
        }

        return match;
    }
}
