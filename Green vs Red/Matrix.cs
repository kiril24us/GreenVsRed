using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Green_vs_Red
{
    /// <summary>
    /// The class Matrix contains all functionality code
    /// </summary>
    public class Matrix
    {
        private char[,] firstMatrix;
        private char[,] secondMatrix;
        private int counter;
        private int calculatingGreenWantedCell;
        private int rowCoordinate;
        private int colCoordinate;
        private int turns;
        private int rows;
        private int cols;

        public Matrix(int[] sizeOfTheMatrix)
        {
            rows = sizeOfTheMatrix[1];
            cols = sizeOfTheMatrix[0];
            firstMatrix = new char[rows, cols];
            secondMatrix = new char[rows, cols];
            counter = 1;
            calculatingGreenWantedCell = 0;
        }

        public int Run(int[] coordinatesAndTurns)
        {
            turns = coordinatesAndTurns[2];
            rowCoordinate = coordinatesAndTurns[1];
            colCoordinate = coordinatesAndTurns[0];

            // Checking the matrix for a green cell for the first time
            if (firstMatrix[rowCoordinate, colCoordinate] == 'G')
            {
                // Parameter for counting the wanted green cell
                calculatingGreenWantedCell++;
            }

            // Making second matrix with the same values
            FillingTheSecondMatrix();

            while (counter != turns)
            {
                for (int row = 0; row < firstMatrix.GetLength(0); row++)
                {
                    for (int col = 0; col < firstMatrix.GetLength(1); col++)
                    {
                        int counterGreenNeighbour = 0;
                        /* Checking all neigbours
                           for green cells*/
                        bool IsThereDown = DownNeighbour(row, col, ref counterGreenNeighbour);
                        bool IsThereRight = RightNeighbour(row, col, ref counterGreenNeighbour);
                        if (IsThereDown && IsThereRight)
                        {
                            RightDownDiagonalNeighbour(row, col, ref counterGreenNeighbour);
                        }
                        bool IsThereLeft = LeftNeighbour(row, col, ref counterGreenNeighbour);
                        if (IsThereDown && IsThereLeft)
                        {
                            LeftDownDiagonalNeighbour(row, col, ref counterGreenNeighbour);
                        }
                        bool IsThereUp = UpNeighbour(row, col, ref counterGreenNeighbour);
                        if (IsThereLeft && IsThereUp)
                        {
                            LeftUpDiagonalNeighbour(row, col, ref counterGreenNeighbour);
                        }
                        if (IsThereRight && IsThereUp)
                        {
                            RightUpDiagonalNeighbour(row, col, ref counterGreenNeighbour);
                        }
                        // Checking if the cell changes the colour
                        ValidateCurrentCell(row, col, ref counterGreenNeighbour);
                    }
                }
                counter++;
                if (secondMatrix[rowCoordinate, colCoordinate] == 'G')
                {
                    // If the cell is green, increasing the counter
                    calculatingGreenWantedCell++;
                }
                // coping values from second matrix to first matrix
                CopyingTwoMatrixes();
            }
            return calculatingGreenWantedCell;
        }

        public void FillingTheFirstMatrix(Func<string> inputRowsMatrix)
        {
            for (int row = 0; row < firstMatrix.GetLength(0); row++)
            {
                string inputRowMatrix = inputRowsMatrix();
                for (int col = 0; col < firstMatrix.GetLength(1); col++)
                {
                    if (inputRowMatrix.Length == firstMatrix.GetLength(1))
                    {
                        if (inputRowMatrix[col] == '1')
                        {
                            firstMatrix[row, col] = 'G';
                        }
                        if (inputRowMatrix[col] == '0')
                        {
                            firstMatrix[row, col] = 'R';
                        }
                    }
                }
            }
        }

        public void FillingTheSecondMatrix()
        {
            for (int row = 0; row < firstMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < firstMatrix.GetLength(1); col++)
                {
                    secondMatrix[row, col] = firstMatrix[row, col];
                }
            }
        }

        public bool DownNeighbour(int row, int col, ref int counterGreenNeighbour)
        {
            if (row < firstMatrix.GetLength(0) - 1)
            {
                if (firstMatrix[row + 1, col] == 'G')
                {
                    counterGreenNeighbour++;
                }
                return true;
            }
            return false;
        }

        public bool RightNeighbour(int row, int col, ref int counterGreenNeighbour)
        {
            if (col < firstMatrix.GetLength(1) - 1)
            {
                if (firstMatrix[row, col + 1] == 'G')
                {
                    counterGreenNeighbour++;
                }
                return true;
            }
            return false;
        }

        public void RightDownDiagonalNeighbour(int row, int col, ref int counterGreenNeighbour)
        {
            if (firstMatrix[row + 1, col + 1] == 'G')
            {
                counterGreenNeighbour++;
            }
        }

        public bool LeftNeighbour(int row, int col, ref int counterGreenNeighbour)
        {
            if (col > 0)
            {
                if (firstMatrix[row, col - 1] == 'G')
                {
                    counterGreenNeighbour++;
                }
                return true;
            }
            return false;
        }

        public void LeftDownDiagonalNeighbour(int row, int col, ref int counterGreenNeighbour)
        {
            if (firstMatrix[row + 1, col - 1] == 'G')
            {
                counterGreenNeighbour++;
            }
        }

        public bool UpNeighbour(int row, int col, ref int counterGreenNeighbour)
        {
            if (row > 0)
            {
                if (firstMatrix[row - 1, col] == 'G')
                {
                    counterGreenNeighbour++;
                }
                return true;
            }
            return false;
        }

        public void LeftUpDiagonalNeighbour(int row, int col, ref int counterGreenNeighbour)
        {
            if (firstMatrix[row - 1, col - 1] == 'G')
            {
                counterGreenNeighbour++;
            }
        }

        public void RightUpDiagonalNeighbour(int row, int col, ref int counterGreenNeighbour)
        {
            if (firstMatrix[row - 1, col + 1] == 'G')
            {
                counterGreenNeighbour++;
            }
        }

        public void CopyingTwoMatrixes()
        {
            for (int row = 0; row < firstMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < firstMatrix.GetLength(1); col++)
                {
                    firstMatrix[row, col] = secondMatrix[row, col];
                }
            }
        }

        public void ValidateCurrentCell(int row, int col, ref int counterGreenNeighbour)
        {
            if (firstMatrix[row, col] == 'R')
            {
                if (counterGreenNeighbour == 3 || counterGreenNeighbour == 6)
                {
                    secondMatrix[row, col] = 'G';
                }
            }
            if (firstMatrix[row, col] == 'G')
            {
                if (counterGreenNeighbour == 0 || counterGreenNeighbour == 1 ||
                    counterGreenNeighbour == 4 || counterGreenNeighbour == 5 ||
                    counterGreenNeighbour == 7 || counterGreenNeighbour == 8)
                {
                    secondMatrix[row, col] = 'R';
                }
            }
            counterGreenNeighbour = 0;
        }
    }
}
