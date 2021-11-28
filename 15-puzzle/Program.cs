using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace _15_Puzzle
{
    class Program
    {
        static readonly Regex solverValidator = new Regex(@"^[bdhiasBDHIAS]+$");
        static readonly Regex orderValidator = new Regex(@"^[DULRdulr]+$");
        static readonly Regex heuristicValidator = new Regex(@"^[me]+$");

        static void Main(string[] args)
        {
            string solver, order, heuristic;
            //args = b/d/h/i/a/s order m/e
            do
            {
                Console.WriteLine("Input solver id (b, d, h, i, a or s): ");
                solver = Console.ReadLine();
                Console.WriteLine("Input solving order: ");
                order = Console.ReadLine();
                Console.WriteLine("Input heuristic id (if necessary) or hit enter: ");
                heuristic = Console.ReadLine();
            } while (!IsValid(solver, solverValidator) || !IsValid(order, orderValidator) || (!IsValid(heuristic, heuristicValidator) && heuristic != ""));
            
            Console.WriteLine("Input number of rows: ");
            int rows = Convert.ToInt32(Console.In.ReadLine());
            int cols;
            do
            {
                Console.WriteLine("Input the same number of columns: ");
                cols = Convert.ToInt32(Console.In.ReadLine());
            } while (cols != rows);

            int[,] array = new int[rows, cols];
            string puzzle;
            string[,] tokens;
            do
            {
                Console.WriteLine("Enter " + rows * cols + " characters from 0 to " + (rows * cols - 1) + " in arbitrary order, divided by spaces: ");
                puzzle = Console.ReadLine();
                tokens = GetTokens(array, puzzle);
            } while (tokens.Length != rows * cols || !puzzle.Contains('0'));
            ReadInPuzzle(array, tokens);

            Board initPuzzle = new Board(array);
            var startingState = new BoardState(initPuzzle, null, null, 0, heuristic);
            Solver s;
            
            switch (Convert.ToChar(solver))
            {
                case 'b':
                    Console.WriteLine("BFS chosen");
                    s = new BFS(order);
                    break;
                case 'd':
                    Console.WriteLine("DFS chosen");
                    s = new DFS(order);
                    break;
                case 'h':
                    Console.WriteLine("BF chosen");
                    s = new BF(order);
                    break;
                case 'i':
                    Console.WriteLine("IDDFS chosen");
                    s = new IDFS(order);
                    break;
                case 'a':
                    Console.WriteLine("A* chosen");
                    s = new AStar(order);
                    break;
                case 's':
                    Console.WriteLine("SMA* chosen");
                    s = new SMA(order);
                    break;
                default:
                    s = new BFS(order);
                    break;
            }
            BoardState solution = s.Solve(startingState);
            s.PrintSolution(solution);

            Console.Read();
        }

        public static string[,] GetTokens(int[,] array, string puzzle)
        {
            int row = array.GetLength(0);
            int col = array.GetLength(1);
            string[,] tokens = new string[row, col];
            int index = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    tokens[i, j] = puzzle.Split((char[])null)[index];
                    index++;
                }
            }
            return tokens;
        }

        public static void ReadInPuzzle(int[,] array, string[,] tokens)
        {
            for(int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = Convert.ToInt32(tokens[i, j]);
                }
            }
        }

        public static bool IsValid(string str, Regex validator)
        {
            return validator.IsMatch(str);
        }
    }
}
