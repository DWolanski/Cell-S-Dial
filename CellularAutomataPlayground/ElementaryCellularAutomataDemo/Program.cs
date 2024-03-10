namespace ElementaryCellularAutomataDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;

    internal class Program
    {
        private static readonly IDictionary<string, string?> _parameters = new Dictionary<string, string?>
        {
            { "CellsCount", "30" },
            { "GenerationsCount", "10" },
            { "ParallelRulesetsCount", "3" },
            { "Rulesets", "50, 54, 102, 105, 110*10, 126, 150, 158, 188, 190, 220, 222, 254, 1..254" },
            { "GenerationsPerSecond", "1" },
        };

        static void Main(string[] args)
        {
            Console.ReadKey();

            foreach (var arg in args)
            {
                var keyValuePair = arg.Split('=');
                if (_parameters.ContainsKey(keyValuePair[0]))
                {
                    _parameters[keyValuePair[0]] = keyValuePair[1];
                }
            }

            var cellsCount = int.Parse(_parameters["CellsCount"]);
            var generationsCount = int.Parse(_parameters["GenerationsCount"]);
            var parallelRulesetCount = byte.Parse(_parameters["ParallelRulesetsCount"]);

            var rulesets = _parameters["Rulesets"]
                .Split(",")
                .SelectMany(ruleset => byte.TryParse(ruleset.Trim(), out var value)
                ? new[] { (value, strategy: "MiddlePoint") }
                : byte.TryParse(ruleset.Split("*")[0].Trim(), out var repeatedValue)
                    ? byte.TryParse(ruleset.Split("*")[1].Trim(), out var multiplier)
                        ? Enumerable.Repeat((value: repeatedValue, strategy: "Randomize"), multiplier).ToArray()
                        : new[] { (repeatedValue, strategy: "Randomize") }
                    : byte.TryParse(ruleset.Split("..")[0].Trim(), out var fromValue)
                        ? byte.TryParse(ruleset.Split("..")[1].Trim(), out var toValue)
                            ? Enumerable.Range(fromValue, toValue - fromValue).Select(value => (Convert.ToByte(value), strategy: "MiddlePoint")).ToArray()
                            : Enumerable.Range(fromValue, byte.MaxValue - fromValue - 1).Select(value => (Convert.ToByte(value), strategy: "MiddlePoint")).ToArray()
                        : Array.Empty<(byte value, string strategy)>())
                .ToArray();

            var generationOutputInterval =  1000 / int.Parse(_parameters["GenerationsPerSecond"]);

            var automatons = new List<ElementaryCellularAutomaton>();

            foreach (var ruleset in rulesets)
            {
                automatons.Add(new ElementaryCellularAutomaton(
                    numberOfRuleset: ruleset.Item1,
                    cellsCount: cellsCount,
                    generationsCount: generationsCount,
                    strategy: ruleset.Item2));
            }

            var automatonsStrings = new ElementaryCellularAutomatonParallelRulesetFormatter(
                automatons: automatons,
                parallelRulesetCount: parallelRulesetCount)
                .GetStrings();

            foreach (var automatonString in automatonsStrings)
            {
                Console.WriteLine(automatonString);
                Thread.Sleep(generationOutputInterval);
            }
        }
    }
}
