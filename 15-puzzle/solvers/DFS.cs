using System;
using System.Collections.Generic;

namespace _15_Puzzle
{
    public class DFS : Solver
    {
        public DFS(string order) : base(order)
        {

        }

        public override BoardState Solve(BoardState root)
        {
            Stack<BoardState> stack = new Stack<BoardState>(); //last in first out
            HashSet<Board> visited = new HashSet<Board>();

            stack.Push(root);
            visited.Add(root.currentBoard);

            while (stack.Count > 0)
            {
                root = stack.Pop();

                if (root.currentBoard.IsEqual(root.GoalState))
                {
                    Console.WriteLine("Solved!");
                    return root;
                }

                var zero = root.currentBoard.IndexOfZero();
                var zeroX = zero.Item1;
                var zeroY = zero.Item2;

                var children = this.ExpandBoard(root, zeroX, zeroY);

                for (int i = 0; i < children.Count; i++)
                {
                    var currentChild = children[i];

                    if (!visited.Contains(currentChild.currentBoard))
                    {
                        stack.Push(currentChild);
                        visited.Add(currentChild.currentBoard);
                    }
                }
            }
            return null;
        }

    }
}
