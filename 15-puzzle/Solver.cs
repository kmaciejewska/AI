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
        protected int[,] GoalState { get; set; }

        public Solver()
        {
            this.GoalState = new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };
        }

        public abstract void Solve(BoardState root);

        protected List<BoardState> ExpandBoard(BoardState currentState, int x, int y)
        {
            List<BoardState> children = new List<BoardState>();

            var rightState = currentState.MoveToRight(x, y);
            if (rightState != null)
            {
                rightState.lastMove = "R";
                rightState.parent = currentState;
                children.Add(rightState);
            }

            var leftState = currentState.MoveToLeft(x, y);
            if (leftState != null)
            {
                leftState.lastMove = "L";
                leftState.parent = currentState;
                children.Add(leftState);
            }

            var downState = currentState.MoveDown(x, y);
            if (downState != null)
            {
                downState.lastMove = "D";
                downState.parent = currentState;
                children.Add(downState);
            }

            var upState = currentState.MoveUp(x, y);
            if (upState != null)
            {
                upState.lastMove = "U";
                upState.parent = currentState;
                children.Add(upState);
            }

            return children;
        }

        private List<string> TracePath(BoardState state)
        {
            var path = new List<string>();
            Console.WriteLine("Tracing path...");

            while (state.parent != null)
            {
                path.Add(state.lastMove);
                state = state.parent;
            }

            return path;
        }

        private string GetStringPath(List<string> path)
        {
            var s = new StringBuilder();

            for (int i = path.Count - 1; i >= 0; i--)
            {
                s.Append("'");
                s.Append(path[i]);
                s.Append("'");
                s.Append(", ");
            }

            return s.ToString();
        }

        public void PrintSolution(BoardState finalBoard)
        {
            var path = TracePath(finalBoard);
            string ps = GetStringPath(path);

            if (ps == null)
                Console.WriteLine("No solution found");
            else
                Console.WriteLine($"Path to goal: {ps}");
        }
    }
}
