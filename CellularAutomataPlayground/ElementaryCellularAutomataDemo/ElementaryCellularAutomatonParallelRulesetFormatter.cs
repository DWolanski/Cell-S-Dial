namespace ElementaryCellularAutomataDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ElementaryCellularAutomatonParallelRulesetFormatter
    {
        private readonly IReadOnlyCollection<ElementaryCellularAutomaton> _automatons;
        private readonly int _parallelRulesetsCount;

        public ElementaryCellularAutomatonParallelRulesetFormatter(
            IReadOnlyCollection<ElementaryCellularAutomaton> automatons,
            int parallelRulesetCount)
        {
            _automatons = automatons;
            _parallelRulesetsCount = parallelRulesetCount;
        }

        public IReadOnlyCollection<string> GetStrings()
        {
            var generationWithTitlesPartitioned = _automatons
                .Select((automaton, index) => (automaton: AsStrings(automaton), index))
                .GroupBy(
                    group => group.index / _parallelRulesetsCount,
                    group => group.automaton)
                .ToArray();

            var generationRepresentative = generationWithTitlesPartitioned.First().First();
            var parallelGenerationWithTitles = new string[generationWithTitlesPartitioned.Length * generationRepresentative.Length];
            var rowOffset = 0;

            foreach (var generationWithTitlesGroup in generationWithTitlesPartitioned)
            {
                var generationWithTitles = generationWithTitlesGroup.ToArray();

                for (int numberOfRow = 0; numberOfRow < generationRepresentative.Length; numberOfRow++)
                {
                    foreach (var generationWithTitle in generationWithTitles)
                    {
                        parallelGenerationWithTitles[numberOfRow + rowOffset] = $"{parallelGenerationWithTitles[numberOfRow + rowOffset]} {generationWithTitle[numberOfRow]}";
                    }
                }

                rowOffset += generationRepresentative.Length;
            }

            return parallelGenerationWithTitles;
        }

        private string[] AsStrings(
            ElementaryCellularAutomaton automaton)
        {
            var titleText = $"Ruleset {automaton.NumberOfRuleset}";

            var rowLength = automaton.Generations.GetLength(1);
            var tittleHorizontalMarginLength = Math.Max(rowLength - titleText.Length, 0) / 2;
            var horizontalMargin = string.Join(string.Empty, Enumerable.Repeat(" ", tittleHorizontalMarginLength));

            var titleRows = new string[]
            {
                $"{horizontalMargin}{titleText}{horizontalMargin}",
            };
                
            var generationRows = new string[automaton.Generations.GetLength(0)];

            for (int numberOfGeneration = 0; numberOfGeneration < automaton.Generations.GetLength(0); numberOfGeneration++)
            {
                for(int numberOfCell = 0; numberOfCell <  automaton.Generations.GetLength(1); numberOfCell++)
                {
                    generationRows[numberOfGeneration] 
                        += automaton.Generations[numberOfGeneration, numberOfCell].Equals(0) 
                        ? " " 
                        : "*";
                }
            }

            var generationsWithTitle = titleRows
                .Concat(generationRows)
                .ToArray();

            return generationsWithTitle;
        }
    }
}
