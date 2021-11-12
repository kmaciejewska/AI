using _15Puzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15_puzzle
{
    public abstract class Solver
    {
        protected int MaxSearchDepth { get; set; }
        protected int NodesExpanded { get; set; }

        public int[] goal;

        public Solver()
        {
        }

        public abstract Board Solve(Board board);

        private List<string> TracePath(Board board)
        {
            var path = new List<string>();
            Console.WriteLine("Tracing path...");

            while (board.parent != null)
            {
                path.Add(board.lastMove);
                board = board.parent;
            }

            return path;
        }

        private string GetStringPath(List<string> path)
        {
            var s = new StringBuilder();

            foreach (var item in path)
            {
                s.Append("'");
                s.Append(item);
                s.Append("'");
                s.Append(", ");
            }

            return s.ToString();
        }

        public void PrintSolution(Board finalBoard)
        {
            var path = TracePath(finalBoard);
            string ps = GetStringPath(path);

            if (String.IsNullOrEmpty(ps))
                Console.WriteLine("No solution found");
            else
                Console.WriteLine($"Path to goal: {ps}");
        }
    }
}
