namespace ElementaryCellularAutomataDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class GameOfLifeCellularAutomaton
    {
        internal int[,,] Generations { get; }

        public GameOfLifeCellularAutomaton(
            string importedInitialGeneration,
            char aliveSign,
            int generationsCount)
        {
            var importedInitialGenerationRows = importedInitialGeneration.Split(Environment.NewLine);
            var rowsCount = importedInitialGenerationRows.Length;
            var columnsCount = importedInitialGenerationRows[0].Length;
             
            Generations = new int[generationsCount, rowsCount, columnsCount];

            for (int numberOfRow = 0; numberOfRow < rowsCount; numberOfRow++)
            {
                for (int numberOfColumn = 0; numberOfColumn < columnsCount; numberOfColumn++)
                {
                    Generations[0, numberOfRow, numberOfColumn] = importedInitialGenerationRows[numberOfRow][numberOfColumn] == aliveSign
                        ? 1
                        : 0;
                }
            }

            Generate(
                columnsCount: columnsCount,
                rowsCount: rowsCount,
                generationsCount: generationsCount);
        }

        public GameOfLifeCellularAutomaton(
            int columnsCount,
            int rowsCount,
            int generationsCount)
        {
            var random = new Random();

            Generations = new int[generationsCount, rowsCount, columnsCount];

            for (int numberOfRow = 0; numberOfRow < rowsCount; numberOfRow++)
            {
                for (int numberOfColumn = 0; numberOfColumn < columnsCount; numberOfColumn++)
                {
                    Generations[0, numberOfRow, numberOfColumn] = random.Next(2);
                }
            }

            Generate(
                columnsCount: columnsCount,
                rowsCount: rowsCount,
                generationsCount: generationsCount);
        }

        private void Generate(
            int columnsCount,
            int rowsCount,
            int generationsCount)
        {
            for (int numberOfGeneration = 0; numberOfGeneration < generationsCount - 1; numberOfGeneration++)
            {
                for (var numberOfRow = 1; numberOfRow < rowsCount - 1; numberOfRow++)
                {
                    for (int numberOfColumn = 1; numberOfColumn < columnsCount - 1; numberOfColumn++)
                    {
                        var currentCell = Generations[numberOfGeneration, numberOfRow, numberOfColumn];

                        var upCell = Generations[numberOfGeneration, numberOfRow - 1, numberOfColumn];

                        var upRightCell = Generations[numberOfGeneration, numberOfRow - 1, numberOfColumn + 1];

                        var rightCell = Generations[numberOfGeneration, numberOfRow, numberOfColumn + 1];

                        var downRightCell = Generations[numberOfGeneration, numberOfRow + 1, numberOfColumn + 1];

                        var downCell = Generations[numberOfGeneration, numberOfRow + 1, numberOfColumn];

                        var downLeftCell = Generations[numberOfGeneration, numberOfRow + 1, numberOfColumn - 1];

                        var leftCell = Generations[numberOfGeneration, numberOfRow, numberOfColumn - 1];

                        var upLeftCell = Generations[numberOfGeneration, numberOfRow + 1, numberOfColumn - 1];

                        var neighborsCount = upCell
                            + upRightCell
                            + rightCell
                            + downRightCell
                            + downCell
                            + downLeftCell
                            + leftCell
                            + upLeftCell;

                        var nextCell = ApplyRuleset(currentCell, neighborsCount);

                        Generations[numberOfGeneration + 1, numberOfRow, numberOfColumn] = nextCell;
                    }
                }
            }
        }
        private int ApplyRuleset(int current, int neighborsCount)
        {
            //Death
            if (current == 1 && (neighborsCount >= 4 || neighborsCount <= 1))
            {
                return 0;
            }

            //Life
            if (current == 0 && neighborsCount == 3)
            {
                return 1;
            }

            //Statis
            return current;
        }
    }
}
