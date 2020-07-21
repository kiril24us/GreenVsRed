using System;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Dynamic;
using System.Linq;

namespace Green_vs_Red
{
    class Program
    {
        static void Main()
        {
            try
            {
                // Reading the size of the matrix
                int[] sizeOfTheMatrix = Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries)
                                                          .Select(int.Parse)
                                                          .ToArray();
                Matrix matrix;
                if (sizeOfTheMatrix.Length == 2 && sizeOfTheMatrix[1] > 0 &&
                    sizeOfTheMatrix[1] < 1000 && sizeOfTheMatrix[0] > 0
                    && sizeOfTheMatrix[0] <= sizeOfTheMatrix[1])
                {
                    matrix = new Matrix(sizeOfTheMatrix);
                }
                else
                {
                    throw new Exception("Invalid input!");
                }

                // Reading the input and filling the cells in the matrix with colours
                matrix.FillingTheFirstMatrix(Console.ReadLine);

                // Reading the coordinates with the wanted cell and turns
                int[] coordinatesAndTurns = Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries)
                                                                   .Select(int.Parse)
                                                                   .ToArray();
                int calculatingGreenWantedCell = 0;
                if (coordinatesAndTurns.Length == 3 &&
                    coordinatesAndTurns[0] < sizeOfTheMatrix[0] &&
                    coordinatesAndTurns[1] < sizeOfTheMatrix[1])
                {
                    calculatingGreenWantedCell = matrix.Run(coordinatesAndTurns);
                }
                else
                {
                    throw new Exception("Invalid input!");
                }

                /* The second example with 4x4 grid
                   the expected result is 14...
                   I think that it must be some kind of misunderstanding
                   because the first two turns, the cell with coordinations 2-2
                   remains red, after that becomes green and for the rest of the turns remains
                   green, so 15 turns - 2 turns(staying red) = 13 turns for green.
                 */
                Console.WriteLine(calculatingGreenWantedCell);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
