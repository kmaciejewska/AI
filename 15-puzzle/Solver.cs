using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _15_Puzzle
{
    public abstract class Solver
    {
        private List<char> order;

        public Solver(string order)
        {
            this.order = new List<char>();
            foreach (var letter in order)
                this.order.Add(letter);
        }

        protected int SearchDepth { get; set; }
        public abstract BoardState Solve(BoardState root);



        protected List<BoardState> ExpandBoard(BoardState currentState, int x, int y)
        {
            List<BoardState> children = new List<BoardState>();
            for (int i = 0; i < this.order.Count; i++)
            {
                if (order[0] == 'R')
                    order.Shuffle();

                foreach (var letter in this.order)
                {
                    switch (letter)
                    {
                        case 'D':
                            Down(currentState, children, x, y);
                            break;
                        case 'U':
                            Up(currentState, children, x, y);
                            break;
                        case 'L':
                            Left(currentState, children, x, y);
                            break;
                        case 'R':
                            Right(currentState, children, x, y);
                            break;

                    }
                }
            }          
            return children;
        }

        private void Right(BoardState currentState, List<BoardState> children, int x, int y)
        {
            var rightState = currentState.MoveToRight(x, y);
            if (rightState != null)
            {
                rightState.lastMove = "R";
                rightState.parent = currentState;
                rightState.Cost++;
                rightState.CostDistance = rightState.Cost + rightState.Distance;
                children.Add(rightState);
            }
        }
        
        private void Down(BoardState currentState, List<BoardState> children, int x, int y)
        {
            var downState = currentState.MoveDown(x, y);
            if (downState != null)
            {
                downState.lastMove = "D";
                downState.parent = currentState;
                downState.Cost++;
                downState.CostDistance = downState.Cost + downState.Distance;
                children.Add(downState);
            }
        }
        
        private void Up(BoardState currentState, List<BoardState> children, int x, int y)
        {
            var upState = currentState.MoveUp(x, y);
            if (upState != null)
            {
                upState.lastMove = "U";
                upState.parent = currentState;
                upState.Cost++;
                upState.CostDistance = upState.Cost + upState.Distance;
                children.Add(upState);
            }
        }
        
        private void Left(BoardState currentState, List<BoardState> children, int x, int y)
        {
            var leftState = currentState.MoveToLeft(x, y);
            if (leftState != null)
            {
                leftState.lastMove = "L";
                leftState.parent = currentState;
                leftState.Cost++;
                leftState.CostDistance = leftState.Cost + leftState.Distance;
                children.Add(leftState);
            }
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

            var pathToGoal = s.ToString().TrimEnd(new[] { ',', ' ' });

            return pathToGoal;
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
