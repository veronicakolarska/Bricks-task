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

            // TODO: validate dimensions are whole even numbers 1-100
            // TODO: validate input has the input dimensions

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
            // for every row that doesn't exceed max rows
            for (var row = 0; row < rows; row++)
            {
                // for every column that doesn't exceed max columns
                for (var col = 0; col < columns; col++)
                {
                    Console.Write(brickLayer[row, col] + " ");
                }
                Console.WriteLine();
            }
        }

        // print bricks with asterisk  
        // up and left of every position
        public static void PrintBrickLayerFormatted(int[,] brickLayer)
        {
            // print character
            var wallSymbol = '*';
            // get number of rows and columns
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
                        // checks if the upper, left, right or botton brick exists
                        // and we are inside the area
                        var hasUpBrick = row > 0 && brickLayer[row, col] == brickLayer[row - 1, col];
                        var hasDownBrick = row < rows - 1 && brickLayer[row, col] == brickLayer[row + 1, col];
                        var hasLeftBrick = col > 0 && brickLayer[row, col] == brickLayer[row, col - 1];
                        var hasRightBrick = col < columns - 1 && brickLayer[row, col] == brickLayer[row, col + 1];
                        var isLastCol = col == columns - 1;

                        // process every row by few criteria
                        // row 0 is first line of processing
                        if (subPrintIndex == 0)
                        {
                            if (hasUpBrick)
                            {
                                Console.Write($"{wallSymbol}   ");
                            }
                            else
                            {
                                Console.Write(new string(wallSymbol, 4));
                            }
                        }

                        // processing part 2
                        if (subPrintIndex == 1)
                        {
                            var valueToPrint = brickLayer[row, col].ToString().PadLeft(3);
                            if (hasLeftBrick)
                            {
                                Console.Write($" {valueToPrint}");
                            }
                            else if (hasRightBrick)
                            {
                                Console.Write($"{wallSymbol}{valueToPrint}");
                            }
                            else
                            {
                                Console.Write($"{wallSymbol}{valueToPrint}");
                            }
                        }

                        // processing part 3
                        if (subPrintIndex == 2)
                        {
                            if (hasDownBrick)
                            {
                                Console.Write($"{wallSymbol}   ");
                            }
                            else
                            {
                                Console.Write(new string(wallSymbol, 4));
                            }
                        }

                        // prints the last column of asterisk for every row
                        if (isLastCol)
                        {
                            Console.Write(wallSymbol);
                        }
                    }
                    // end of row
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
            //  Console.WriteLine(ValidateBrickSpanning(firstLayerBricks));
            PrintBrickLayerFormatted(firstLayerBricks);
        }
    }
}
