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
            var arr = new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };

            Board initPuzzle = new Board(arr);
            var startingState = new BoardState(initPuzzle, null, null);
            Solver bfs = new DFS();

            bfs.Solve(startingState);

            Console.Read();
        }
    }
}
