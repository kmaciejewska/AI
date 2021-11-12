using _15_puzzle;
using _15_puzzle.solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] puzzle =
            {
                1, 2, 5,
                3, 4, 0,
                6, 7, 8
            };

            Board initPuzzle = new Board(puzzle);
            Solver bfs = new DFS();

            Board solution = bfs.Solve(initPuzzle);

            if (solution != null && solution.puzzle.Length > 0)
            {
                solution.PrintPuzzle();
            }
            Console.Read();
        }
    }
}
