using System;
using System.Diagnostics.Tracing;
using System.Dynamic;
using System.Linq;

namespace Green_vs_Red
{
    class Program
    {
        static void Main()
        {
            int rows = 0;
            int cols = 0;

            int[] sizeOfTheMatrix = Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries)
                                                      .Select(int.Parse)
                                                      .ToArray();

            if (sizeOfTheMatrix.Length == 2 && sizeOfTheMatrix[1] > 0 &&
                sizeOfTheMatrix[1] < 1000 && sizeOfTheMatrix[0] > 0
                && sizeOfTheMatrix[0] <= sizeOfTheMatrix[1])
            {
                rows = sizeOfTheMatrix[1];
                cols = sizeOfTheMatrix[0];
            }

            char[,] matrix = new char[rows, cols];
            char[,] secondMatrix = new char[rows, cols];
            FillingTheMatrix(matrix);
            FillingTheSecondMatrix(matrix, secondMatrix);
            int[] coordinatesAndTurns = Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries)
                                                               .Select(int.Parse)
                                                               .ToArray();
            int rowCoordinate = 0;
            int colCoordinate = 0;
            int turns = 0;
            if (coordinatesAndTurns[0] <= matrix.GetLength(1) - 1 &&
                coordinatesAndTurns[1] <= matrix.GetLength(0) - 1)
            {
                turns = coordinatesAndTurns[2];
                rowCoordinate = coordinatesAndTurns[1];
                colCoordinate = coordinatesAndTurns[0];
            }

            int counter = 1;
            int calculatingGreenWantedCell = 0;
            if (matrix[rowCoordinate, colCoordinate] == 'G')
            {
                calculatingGreenWantedCell++;
            }
            while (counter != turns)
            {                            
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        int counterGreenNeighbour = 0;
                        bool IsThereDown = DownNeighbour(matrix, row, col, ref counterGreenNeighbour);
                        bool IsThereRight = RightNeighbour(matrix, row, col, ref counterGreenNeighbour);
                        if (IsThereDown && IsThereRight)
                        {
                            RightDownDiagonalNeighbour(matrix, row, col, ref counterGreenNeighbour);
                        }
                        bool IsThereLeft = LeftNeighbour(matrix, row, col, ref counterGreenNeighbour);
                        if (IsThereDown && IsThereLeft)
                        {
                            LeftDownDiagonalNeighbour(matrix, row, col, ref counterGreenNeighbour);
                        }
                        bool IsThereUp = UpNeighbour(matrix, row, col, ref counterGreenNeighbour);
                        if (IsThereLeft && IsThereUp)
                        {
                            LeftUpDiagonalNeighbour(matrix, row, col, ref counterGreenNeighbour);
                        }
                        if (IsThereRight && IsThereUp)
                        {
                            RightUpDiagonalNeighbour(matrix, row, col, ref counterGreenNeighbour);
                        }
                        ValidateCurrentCell(matrix, secondMatrix, row, col, ref counterGreenNeighbour, ref counter);                        
                    }
                }
                counter++;
                if (secondMatrix[rowCoordinate, colCoordinate] == 'G')
                {
                    calculatingGreenWantedCell++;
                }
                CopyingTwoMatrixes(matrix, secondMatrix);
            }
            Console.WriteLine(calculatingGreenWantedCell);
        }

        private static void CopyingTwoMatrixes(char[,] matrix, char[,] secondMatrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = secondMatrix[row, col];
                }
            }
        }

        private static void FillingTheSecondMatrix(char[,] matrix, char[,] secondMatrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {                
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    secondMatrix[row, col] = matrix[row, col];
                }
            }
        }

        private static void ValidateCurrentCell(char[,] matrix, char[,] secondMatrix, int row, int col, ref int counterGreenNeighbour, ref int counter)
        {
            if (matrix[row, col] == 'R')
            {
                if (counterGreenNeighbour == 3 || counterGreenNeighbour == 6)
                {
                    secondMatrix[row, col] = 'G';
                }
            }
            if (matrix[row, col] == 'G')
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

        private static void RightUpDiagonalNeighbour(char[,] matrix, int row, int col, ref int counterGreenNeighbour)
        {
            if (matrix[row - 1, col + 1] == 'G')
            {
                counterGreenNeighbour++;
            }
        }

        private static void LeftUpDiagonalNeighbour(char[,] matrix, int row, int col, ref int counterGreenNeighbour)
        {
            if (matrix[row - 1, col - 1] == 'G')
            {
                counterGreenNeighbour++;
            }
        }

        private static bool UpNeighbour(char[,] matrix, int row, int col, ref int counterGreenNeighbour)
        {
            if (row > 0)
            {
                if (matrix[row - 1, col] == 'G')
                {
                    counterGreenNeighbour++;
                }
                return true;
            }
            return false;
        }

        private static void LeftDownDiagonalNeighbour(char[,] matrix, int row, int col, ref int counterGreenNeighbour)
        {
            if (matrix[row + 1, col - 1] == 'G')
            {
                counterGreenNeighbour++;
            }
        }

        private static bool LeftNeighbour(char[,] matrix, int row, int col, ref int counterGreenNeighbour)
        {
            if (col > 0)
            {
                if (matrix[row, col - 1] == 'G')
                {
                    counterGreenNeighbour++;
                }
                return true;
            }
            return false;
        }

        private static void RightDownDiagonalNeighbour(char[,] matrix, int row, int col, ref int counterGreenNeighbour)
        {
            if (matrix[row + 1, col + 1] == 'G')
            {
                counterGreenNeighbour++;
            }
        }

        private static bool RightNeighbour(char[,] matrix, int row, int col, ref int counterGreenNeighbour)
        {
            if (col < matrix.GetLength(1) - 1)
            {
                if (matrix[row, col + 1] == 'G')
                {
                    counterGreenNeighbour++;
                }
                return true;
            }
            return false;
        }

        private static bool DownNeighbour(char[,] matrix, int row, int col, ref int counterGreenNeighbour)
        {
            if (row < matrix.GetLength(0) - 1)
            {                
                if (matrix[row + 1, col] == 'G')
                {
                    counterGreenNeighbour++;
                }
                return true;
            }
            return false;
        }

        private static void FillingTheMatrix(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string inputRowMatrix = Console.ReadLine();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (inputRowMatrix.Length == matrix.GetLength(1))
                    {
                        if (inputRowMatrix[col] == '1')
                        {
                            matrix[row, col] = 'G';
                        }
                        if (inputRowMatrix[col] == '0')
                        {
                            matrix[row, col] = 'R';
                        }
                    }
                }
            }
        }
    }
}
