using _15_puzzle;
using _15_puzzle.solvers;
using System;

namespace _15Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[3, 3] { { 1, 0, 3 }, { 2, 4, 5 }, { 6, 7, 8 } };

            Board initPuzzle = new Board(arr);
            var startingState = new BoardState(initPuzzle, null, null, 0, "m");
            Solver solver = new BFS();

            BoardState solution = solver.Solve(startingState);
            solver.PrintSolution(solution);

            Console.Read();
        }
    }
}
