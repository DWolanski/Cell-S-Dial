namespace ElementaryCellularAutomataDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ElementaryCellularAutomaton
    {
        internal int[,] Generations { get; }
        internal byte NumberOfRuleset { get; }

        private readonly byte[] _ruleset;

        public ElementaryCellularAutomaton(
            byte numberOfRuleset,
            int cellsCount,
            int generationsCount,
            string strategy)
        {
            var random = new Random();

            Generations = new int[generationsCount, cellsCount];

            switch (strategy)
            {
                case "MiddlePoint":
                    Generations[0, cellsCount / 2] = 1;
                    break;

                case "Randomize":
                    for (int numberOfCell = 0; numberOfCell < cellsCount; numberOfCell++)
                    {
                        Generations[0, numberOfCell] = random.Next(2);
                    }
                    break;
            }


            NumberOfRuleset = numberOfRuleset;

            _ruleset = Convert
                .ToString(numberOfRuleset, 2)
                .PadLeft(8, '0')
                .Select(singleBit => (byte)char.GetNumericValue(singleBit))
                .ToArray();

            for (int numberOfGeneration = 0; numberOfGeneration < generationsCount - 1; numberOfGeneration++)
            {
                for (var cell = 1; cell < cellsCount - 1; cell++)
                {
                    var leftCell = Generations[numberOfGeneration, cell - 1];
                    var currentCell = Generations[numberOfGeneration, cell];
                    var rightCell = Generations[numberOfGeneration, cell + 1];
                    var nextCell = ApplyRuleset(leftCell, currentCell, rightCell);
                    Generations[numberOfGeneration + 1, cell] = nextCell;
                }
            }
        }
        private int ApplyRuleset(int left, int current, int right)
        {
            if (left == 1 && current == 1 && right == 1) return _ruleset[0];
            if (left == 1 && current == 1 && right == 0) return _ruleset[1];
            if (left == 1 && current == 0 && right == 1) return _ruleset[2];
            if (left == 1 && current == 0 && right == 0) return _ruleset[3];
            if (left == 0 && current == 1 && right == 1) return _ruleset[4];
            if (left == 0 && current == 1 && right == 0) return _ruleset[5];
            if (left == 0 && current == 0 && right == 1) return _ruleset[6];
            if (left == 0 && current == 0 && right == 0) return _ruleset[7];
            return 0;
        }
    }
}
