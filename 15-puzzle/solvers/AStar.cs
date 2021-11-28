using System;
using System.Collections.Generic;
using System.Linq;

namespace _15_Puzzle.solvers
{
    public class AStar : Solver
    {
        public AStar(string order) : base(order)
        {

        }
        public override BoardState Solve(BoardState root)
        {
            var queue = new List<BoardState>(); 
            HashSet<Board> visited = new HashSet<Board>();

            queue.Add(root);

            while (queue.Count > 0)
            {
                root = queue.OrderBy(x => x.CostDistance).First();

                if (root.currentBoard.IsEqual(root.GoalState))
                {
                    Console.WriteLine("Solved!");
                    return root;
                }

                visited.Add(root.currentBoard);
                queue.Remove(root);

                var zero = root.currentBoard.IndexOfZero();
                var zeroX = zero.Item1;
                var zeroY = zero.Item2;

                var children = this.ExpandBoard(root, zeroX, zeroY);

                for (int i = 0; i < children.Count; i++)
                {
                    var currentChild = children[i];
                    if (visited.Contains(currentChild.currentBoard))
                        continue;

                    //might already be in the list 
                    if (queue.Any(x=> x == currentChild))
                    {
                        var existingChild = queue.First(x => x == currentChild);
                        if (existingChild.CostDistance > root.CostDistance)
                        {
                            queue.Remove(existingChild);
                            queue.Add(currentChild);
                        }
                    }
                    else
                    {
                        //new tile before so add it to the list. 
                        queue.Add(currentChild);
                    }
                }
            }
            return null;
        }
    }
}
