using System;
using System.Collections.Generic;
using System.Text;

namespace _15_puzzle
{
    public abstract class Solver
    {
        public Solver()
        {
        }

        protected int SearchDepth { get; set; }
        public abstract BoardState Solve(BoardState root);

        protected List<BoardState> ExpandBoard(BoardState currentState, int x, int y)
        {
            List<BoardState> children = new List<BoardState>();

            var rightState = currentState.MoveToRight(x, y);
            if (rightState != null)
            {
                rightState.lastMove = "R";
                rightState.parent = currentState;
                rightState.Cost++;
                rightState.CostDistance = rightState.Cost + rightState.Distance;
                children.Add(rightState);
            }

            var leftState = currentState.MoveToLeft(x, y);
            if (leftState != null)
            {
                leftState.lastMove = "L";
                leftState.parent = currentState;
                leftState.Cost++;
                leftState.CostDistance = leftState.Cost + leftState.Distance;
                children.Add(leftState);
            }

            var downState = currentState.MoveDown(x, y);
            if (downState != null)
            {
                downState.lastMove = "D";
                downState.parent = currentState;
                downState.Cost++;
                downState.CostDistance = downState.Cost + downState.Distance;
                children.Add(downState);
            }

            var upState = currentState.MoveUp(x, y);
            if (upState != null)
            {
                upState.lastMove = "U";
                upState.parent = currentState;
                upState.Cost++;
                upState.CostDistance = upState.Cost + upState.Distance;
                children.Add(upState);
            }

            return children;
        }

        private List<string> TracePath(BoardState state)
        {
            if (state == null) 
                return null;

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
            if (path == null)
                return "";

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
            if (finalBoard == null)
            {
                Console.WriteLine("No solution found!");
            }
            var path = TracePath(finalBoard);
            string ps = GetStringPath(path);
            Console.WriteLine($"Path to goal: {ps}");
        }
    }
}
