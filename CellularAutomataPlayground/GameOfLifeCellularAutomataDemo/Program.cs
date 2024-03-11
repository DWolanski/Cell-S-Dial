using ElementaryCellularAutomataDemo;

namespace GameOfLifeCellularAutomataDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var automaton = new GameOfLifeCellularAutomaton(
                columnsCount: 120,
                rowsCount: 40,
                generationsCount: 500);

            Console.ReadKey();

            for (int numberOfGeneration = 0; numberOfGeneration < automaton.Generations.GetLength(0); numberOfGeneration++)
            {
                var rows = new string[automaton.Generations.GetLength(1)];
                for (var numberOfRow = 0; numberOfRow < automaton.Generations.GetLength(1); numberOfRow++)
                {
                    var row = string.Empty;
                    for (int numberOfColumn = 0; numberOfColumn < automaton.Generations.GetLength(2); numberOfColumn++)
                    {
                        row += automaton.Generations[numberOfGeneration, numberOfRow, numberOfColumn] == 1 ? "*" : " ";
                    }
                    rows[numberOfRow] = row;
                }
                Console.WriteLine(string.Join(Environment.NewLine, rows));
                Thread.Sleep(80);
                Console.Clear();
            }
        }
    }
}
