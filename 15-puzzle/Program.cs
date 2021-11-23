using _15_puzzle;
using _15_puzzle.solvers;
using System;

namespace _15Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[3, 3] { { 1, 2, 5 }, { 3, 0, 4 }, { 6, 7, 8 } };

            Board initPuzzle = new Board(arr);
            var startingState = new BoardState(initPuzzle, null, null);
            Solver bfs = new DFS();

            bfs.Solve(startingState);

            Console.Read();
        }
    }
}
