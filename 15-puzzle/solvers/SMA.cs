using System;
using System.Collections.Generic;
using System.Linq;

namespace _15_Puzzle
{
    public class SMA : Solver
    {
        public SMA(string order) : base(order)
        {

        }
        public override BoardState Solve(BoardState root)
        {
            var queue = new List<BoardState>();
            queue.Add(root);
            int u = 1; //for recording nodes in memory

            while (queue.Count > 0)
            {
                root = queue.OrderBy(x => x.CostDistance).First();
                queue.Remove(root);

                if (root.currentBoard.IsEqual(root.GoalState))
                {
                    Console.WriteLine("Solved!");
                    return root;
                }
                else if (root.CostDistance == int.MaxValue)
                {
                    return null;
                }

                if (root.CostDistance > this.SearchDepth)
                {
                    this.SearchDepth = root.CostDistance;
                }

                var zero = root.currentBoard.IndexOfZero();
                var zeroX = zero.Item1;
                var zeroY = zero.Item2;

                var children = this.ExpandBoard(root, zeroX, zeroY);

                foreach (var child in children)
                {
                    if (!child.currentBoard.IsEqual(child.GoalState) && child.Cost == this.SearchDepth)
                    {
                        child.CostDistance = int.MaxValue;
                    }
                    else
                    {
                        child.CostDistance = Math.Max(root.CostDistance, child.Cost + child.Distance);
                    }

                    queue.Add(child);
                    u++;
                }
                //if memory full
                if (u > this.SearchDepth)
                {
                    var badNode = queue.OrderBy(x => x.CostDistance).LastOrDefault();
                    var parent = badNode.parent;
                    badNode.parent = null;
                    if (!queue.Contains(parent))
                        queue.Add(parent);
                }
            }
            return null;
        }
    }
}
