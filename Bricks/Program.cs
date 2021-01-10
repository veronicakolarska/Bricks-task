using System;
using System.Collections.Generic;
using System.Linq;
using Bricks.ConsoleIO;

namespace Bricks
{
    public class Program
    {
        /// <summary>
        /// validate no brick spans 3 rows / columns
        /// </summary>
        /// <param name="firstLayer"> the brick layer to be validated</param>
        /// <returns>an array of errors if there are any</returns>
        public static IEnumerable<BrickSpanValidationError> ValidateBrickSpanning(int[,] firstLayer)
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
            return brickFrequency
                    .Where(x => x.Value > 2)
                    .Select(x => new BrickSpanValidationError(x.Key, x.Value));
        }


        /// <summary>
        /// reads input - two dimensions and first layer of bricks and validates them
        /// </summary>
        /// <param name="consoleIO">console input</param>
        /// <returns>first layer array or throws exception</returns>
        public static int[,] ReadInput(IConsoleIO consoleIO)
        {
            // get the two dimensions from the console
            var dimensionInputs = consoleIO.ReadLine()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(dimension => int.Parse(dimension))
                        .ToArray();

            var rows = dimensionInputs[0];
            var columns = dimensionInputs[1];

            if (rows > 1 && rows <= 100 && rows % 2 == 0)
            {
                throw new ArgumentOutOfRangeException("Rows are not in range!");
            }
            if (columns > 1 && columns <= 100 && columns % 2 == 0)
            {
                throw new ArgumentOutOfRangeException("Columns are not in range!");
            }

            var firstBrickLayer = new int[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                var brickRow = consoleIO.ReadLine()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(dimension => int.Parse(dimension))
                        .ToArray();

                // check if columns are the same as area
                if (brickRow.Length != columns)
                {
                    throw new ArgumentOutOfRangeException("Column length is not the same!");
                }

                for (int col = 0; col < columns; col++)
                {
                    firstBrickLayer[row, col] = brickRow[col];
                }
            }
            return firstBrickLayer;
        }

        /// <summary>
        /// basic print method for every layer
        /// </summary>
        /// <param name="brickLayer">(first) brick layer</param>
        /// <param name="consoleIO">console input</param>
        /// <typeparam name="T">every type</typeparam>
        public static void PrintBrickLayer<T>(T[,] brickLayer, IConsoleIO consoleIO)
        {
            var rows = brickLayer.GetLength(0);
            var columns = brickLayer.GetLength(1);
            // for every row that doesn't exceed max rows
            for (var row = 0; row < rows; row++)
            {
                // for every column that doesn't exceed max columns
                for (var col = 0; col < columns; col++)
                {
                    consoleIO.Write(brickLayer[row, col].ToString().PadLeft(3));
                }
                consoleIO.WriteLine();
            }
        }


        /// <summary>
        /// print bricks with asterisk from top and left of every position
        /// </summary>
        /// <param name="brickLayer">(first) brick layer</param>
        /// <param name="consoleIO">console input</param>
        public static void PrintBrickLayerFormatted(int[,] brickLayer, IConsoleIO consoleIO)
        {
            // print character
            var wallSymbol = '*';
            // get number of rows and columns
            var rows = brickLayer.GetLength(0);
            var columns = brickLayer.GetLength(1);
            for (var row = 0; row < rows; row++)
            {
                // last row will print a 'line' of characters after itself
                var isLastRow = row == rows - 1;
                //
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
                                consoleIO.Write($"{wallSymbol}   ");
                            }
                            else
                            {
                                consoleIO.Write(new string(wallSymbol, 4));
                            }
                        }

                        // processing part 2
                        if (subPrintIndex == 1)
                        {
                            var valueToPrint = brickLayer[row, col].ToString().PadLeft(3);
                            if (hasLeftBrick)
                            {
                                consoleIO.Write($" {valueToPrint}");
                            }
                            else if (hasRightBrick)
                            {
                                consoleIO.Write($"{wallSymbol}{valueToPrint}");
                            }
                            else
                            {
                                consoleIO.Write($"{wallSymbol}{valueToPrint}");
                            }
                        }

                        // processing part 3
                        if (subPrintIndex == 2)
                        {
                            if (hasDownBrick)
                            {
                                consoleIO.Write($"{wallSymbol}   ");
                            }
                            else
                            {
                                consoleIO.Write(new string(wallSymbol, 4));
                            }
                        }

                        // prints the last column of asterisk for every row
                        if (isLastCol)
                        {
                            consoleIO.Write(wallSymbol.ToString());
                        }
                    }
                    // end of row - new line
                    consoleIO.WriteLine();
                }
            }
        }

        /// <summary>
        /// Decides if there is a solution to arrange the second layer
        /// </summary>
        /// <param name="firstBrickLayer">first layer of bricks</param>
        /// <returns>second layer if finds a solution, or null if not</returns>
        public static int[,] Solve(int[,] firstBrickLayer)
        {
            var rows = firstBrickLayer.GetLength(0);
            var cols = firstBrickLayer.GetLength(1);
            var secondBrickLayer = new int[rows, cols];
            var usedPosition = new bool[rows * cols];
            // saves the position that the brick part can go (max 4 directions)
            // from a flatten array
            var positionsToNavigate = new Dictionary<int, List<int>>();
            var targetsAndOrigins = new int[rows * cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // skips every second element so there are origins and destinations
                    if ((row + col) % 2 == 0)
                    {
                        continue;
                    }
                    // check for every odd position all the cells around it (without diagonal) - up, down, left, right

                    var brickPositionNumber = (row * cols) + col;
                    var currentBrickCoordinate = firstBrickLayer[row, col];
                    // value of the neighbour cell (if exists) that is new 
                    int newBrickValue = 0;

                    // if there is a cell on the right and is not the same as the current brick and the column is not the last one
                    if ((col < cols - 1) && currentBrickCoordinate != firstBrickLayer[row, col + 1])
                    {
                        newBrickValue = (row * cols) + col + 1;
                        // add its value to the Dictionary
                        AddToListInPosition(brickPositionNumber, newBrickValue, positionsToNavigate);
                    }

                    // if there is a cell on the bottom and is not the same as the current brick and the row is not the last one
                    if ((row < rows - 1) && currentBrickCoordinate != firstBrickLayer[row + 1, col])
                    {
                        newBrickValue = ((row + 1) * cols) + col;
                        AddToListInPosition(brickPositionNumber, newBrickValue, positionsToNavigate);
                    }

                    // if there is a cell on the left and is not the same as the current brick and the column is not the first (zero index) one
                    if ((col > 0) && currentBrickCoordinate != firstBrickLayer[row, col - 1])
                    {
                        newBrickValue = (row * cols) + col - 1;
                        AddToListInPosition(brickPositionNumber, newBrickValue, positionsToNavigate);
                    }

                    // if there is a cell on the top and is not the same as the current brick and the row is not the first (zero index) one
                    if ((row > 0) && currentBrickCoordinate != firstBrickLayer[row - 1, col])
                    {
                        newBrickValue = ((row - 1) * cols) + col;
                        AddToListInPosition(brickPositionNumber, newBrickValue, positionsToNavigate);
                    }
                }
            }

            var connectedOriginsTargetsCount = 0;
            // reset it for every element 
            Array.Fill(targetsAndOrigins, -1);

            for (int firstLayerRowColPosition = 0; firstLayerRowColPosition < targetsAndOrigins.Length; firstLayerRowColPosition++)
            {
                // sets the whole array to false
                Array.Clear(usedPosition, 0, usedPosition.Length);
                var isMatch = BuildTargetsAndOrigins(firstLayerRowColPosition, positionsToNavigate, usedPosition, targetsAndOrigins);
                if (isMatch)
                {
                    connectedOriginsTargetsCount++;
                }
            }

            // if all bricks can be arranged - so there is a solution
            if (connectedOriginsTargetsCount == (rows * cols / 2))
            {
                int brickNumber = 0;

                for (int index = 0; index < rows * cols; index++)
                {
                    // if it is an origin part of the brick
                    if (targetsAndOrigins[index] != -1)
                    {
                        // count the brick - brick number (ready to put a new brick on the second layer)
                        brickNumber++;
                        // first part of the brick - converts it back to two-dimensional array
                        secondBrickLayer[index / cols, index % cols] = brickNumber;
                        // second part of the brick - searches from the targets
                        // that gives the coordinate where the second part should be put
                        secondBrickLayer[targetsAndOrigins[index] / cols, targetsAndOrigins[index] % cols] = brickNumber;
                    }
                }
                return secondBrickLayer;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Builds possible ways to move the bricks
        /// </summary>
        /// <param name="firstLayerRowColPosition">flat representation of the area coordinates</param>
        /// <param name="positionsToNavigate">all possible ways a brick part can rotate - max 4 ways</param>
        /// <param name="usedPosition"></param>
        /// <param name="targetsAndOrigins">array of origins and destinations of the brick number used from the construction of the second layer</param>
        /// <returns>returns true if building was successful; false otherwise</returns>
        public static bool BuildTargetsAndOrigins(
            int firstLayerRowColPosition,
            Dictionary<int, List<int>> positionsToNavigate,
            bool[] usedPosition,
            // -1 marks the target of a brick and the destination of the brick is found by taking the index of the origin and searching for a value in the array with that index
            // the index of that value is the destination of the brick  
            int[] targetsAndOrigins)
        {
            // we are on position that we don't have in the dictionary and can't go
            if (!positionsToNavigate.ContainsKey(firstLayerRowColPosition))
            {
                return false;
            }

            // max count is the four directions that the brick part can go
            // from the list of the dictionary
            for (int index = 0; index < positionsToNavigate[firstLayerRowColPosition].Count; index++)
            {
                var possiblePosition = positionsToNavigate[firstLayerRowColPosition][index];

                // if position is not already taken
                if (!usedPosition[possiblePosition])
                {
                    // add the position to the used values
                    usedPosition[possiblePosition] = true;
                    // moves a brick and runs again for the case it overrides
                    if (targetsAndOrigins[possiblePosition] == -1 || BuildTargetsAndOrigins(targetsAndOrigins[possiblePosition], positionsToNavigate, usedPosition, targetsAndOrigins))
                    {
                        targetsAndOrigins[possiblePosition] = firstLayerRowColPosition;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Add possible positions of the second part of the brick in a List in Dictionary
        /// </summary>
        /// <param name="brickPositionNumber">flat representation of the brick position</param>
        /// <param name="newBrickValue">the value to add to positionsToNavigate</param>
        /// <param name="positionsToNavigate">all possible ways a brick part can rotate - max 4 ways</param>
        public static void AddToListInPosition(int brickPositionNumber, int newBrickValue, Dictionary<int, List<int>> positionsToNavigate)
        {
            if (!positionsToNavigate.ContainsKey(brickPositionNumber))
            {
                positionsToNavigate.Add(brickPositionNumber, new List<int>() { newBrickValue });
            }
            else
            {
                positionsToNavigate[brickPositionNumber].Add(newBrickValue);
            }
        }

        public static void Main(string[] args)
        {
            var systemConsoleIO = new SystemConsoleIO();
            var firstLayerBricks = ReadInput(systemConsoleIO);
            var errors = ValidateBrickSpanning(firstLayerBricks);
            foreach (var error in errors)
            {
                systemConsoleIO.WriteLine(error.ToString());
            }

            var secondBrickLayer = Solve(firstLayerBricks);

            if (secondBrickLayer != null)
            {
                PrintBrickLayerFormatted(secondBrickLayer, systemConsoleIO);
            }
            else
            {
                systemConsoleIO.WriteLine("-1. No solution.");
            }
        }
    }
}