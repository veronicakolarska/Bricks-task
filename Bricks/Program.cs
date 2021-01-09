using System;
using System.Collections.Generic;
using System.Linq;

namespace Bricks
{
    class Program
    {
        // validate no brick spans 3 rows / columns
        // TODO: return validation errors
        public static bool ValidateBrickSpanning(int[,] firstLayer)
        {
            var brickFrequency = new Dictionary<int, int>();

            var rows = firstLayer.GetLength(0);
            var columns = firstLayer.GetLength(1);

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < columns; col++)
                {
                    var currentBrick = firstLayer[row, col];
                    if (brickFrequency.ContainsKey(currentBrick))
                    {
                        brickFrequency[currentBrick]++;
                    }
                    else
                    {
                        brickFrequency.Add(currentBrick, 1);
                    }
                }
            }
            return brickFrequency.All(x => x.Value == 2);
        }

        // read input - two dimensions and first layer of bricks
        public static int[,] ReadInput()
        {
            var dimensionInputs = Console.ReadLine()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(dimension => int.Parse(dimension))
                        .ToArray();

            // TODO: validate dimensions

            var rows = dimensionInputs[0];
            var columns = dimensionInputs[1];

            var firstBrickLayer = new int[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                var brickRow = Console.ReadLine()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(dimension => int.Parse(dimension))
                        .ToArray();

                for (int col = 0; col < columns; col++)
                {
                    firstBrickLayer[row, col] = brickRow[col];
                }
            }
            return firstBrickLayer;
        }

        public static void PrintBrickLayer<T>(T[,] brickLayer)
        {
            var rows = brickLayer.GetLength(0);
            var columns = brickLayer.GetLength(1);
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < columns; col++)
                {
                    Console.Write(brickLayer[row, col] + " ");

                }
                Console.WriteLine();
            }
        }

        // print bricks with asterisk 
        public static void PrintBrickLayerFormatted(int[,] brickLayer)
        {
            var rows = brickLayer.GetLength(0);
            var columns = brickLayer.GetLength(1);
            for (var row = 0; row < rows; row++)
            {
                var isLastRow = row == rows - 1;
                var maxSubIndex = isLastRow ? 3 : 2;

                for (var subPrintIndex = 0; subPrintIndex < maxSubIndex; subPrintIndex++)
                {
                    for (var col = 0; col < columns; col++)
                    {
                        var hasUpBrick = row > 0 && brickLayer[row, col] == brickLayer[row - 1, col];
                        var hasDownBrick = row < rows - 1 && brickLayer[row, col] == brickLayer[row + 1, col];
                        var hasLeftBrick = col > 0 && brickLayer[row, col] == brickLayer[row, col - 1];
                        var hasRightBrick = col < columns - 1 && brickLayer[row, col] == brickLayer[row, col + 1];
                        var isLastCol = col == columns - 1;

                        if (subPrintIndex == 0)
                        {
                            if (hasUpBrick)
                            {
                                Console.Write("*   ");
                            }
                            else
                            {
                                Console.Write("****");
                            }
                        }

                        if (subPrintIndex == 1)
                        {
                            var valueToPrint = brickLayer[row, col].ToString().PadLeft(3);
                            if (hasLeftBrick)
                            {
                                Console.Write($" {valueToPrint}");
                            }
                            else if (hasRightBrick)
                            {
                                Console.Write($"*{valueToPrint}");
                            }
                            else
                            {
                                Console.Write($"*{valueToPrint}");
                            }
                        }

                        if (subPrintIndex == 2)
                        {
                            if (hasDownBrick)
                            {
                                Console.Write("*   ");
                            }
                            else
                            {
                                Console.Write("****");
                            }
                        }

                        if (isLastCol)
                        {
                            Console.Write("*");
                        }

                    }

                    Console.WriteLine();
                }
            }
        }

        public static void Solve(int rows, int columns, string[,] firstBrickLayer)
        {

        }

        static void Main(string[] args)
        {
            var firstLayerBricks = ReadInput();
            PrintBrickLayerFormatted(firstLayerBricks);
            //  Console.WriteLine(ValidateBrickSpanning(firstLayerBricks));
        }
    }
}
