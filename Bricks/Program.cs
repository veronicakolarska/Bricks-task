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

        public static void Solve(int rows, int columns, string[,] firstBrickLayer)
        {

        }

        static void Main(string[] args)
        {
            var firstLayerBricks = ReadInput();
            PrintBrickLayer(firstLayerBricks);
            Console.WriteLine(ValidateBrickSpanning(firstLayerBricks));
        }
    }
}
