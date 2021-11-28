using System;
using System.Collections.Generic;

namespace _15_Puzzle.solvers
{
    public class BFS : Solver
    {
        public BFS(string order) : base(order)
        {

        }

        public override BoardState Solve(BoardState root)
        {
            Queue<BoardState> queue = new Queue<BoardState>(); //all the nodes that can be expanded
            var visited = new HashSet<Board>();

            queue.Enqueue(root);
            visited.Add(root.currentBoard);

            while (queue.Count > 0)
            {
                root = queue.Dequeue();

                if (root.currentBoard.IsEqual(root.GoalState))
                {
                    Console.WriteLine("Solved!");
                    return root;
                }

                var zero = root.currentBoard.IndexOfZero();
                var zeroX = zero.Item1;
                var zeroY = zero.Item2;
                var children = this.ExpandBoard(root, zeroX, zeroY); //perform all legal moves

                for (int i = 0; i < children.Count; i++)
                {
                    BoardState currentChild = children[i];

                    if (!visited.Contains(currentChild.currentBoard))
                    {
                        queue.Enqueue(currentChild);
                        visited.Add(currentChild.currentBoard);
                    }

                }
            }
            return null;
        }
    }
}