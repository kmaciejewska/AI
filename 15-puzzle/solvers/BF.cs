using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _15Puzzle;
using C5;

namespace _15_puzzle.solvers
{
    public class BF : Solver
    {
        public override BoardState Solve(BoardState root)
        {
            var queue = new IntervalHeap<BoardState>(); //odrered by distance
            System.Collections.Generic.HashSet<Board> visited = new System.Collections.Generic.HashSet<Board>();

            queue.Add(root);
            visited.Add(root.currentBoard);

            while (queue.Count > 0)
            {
                //we are using DeleteMax because CompareTo 
                //returns 1 if the distance is smaller
                root = queue.DeleteMax();   
                
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
                        queue.Add(currentChild);
                        visited.Add(currentChild.currentBoard);
                    }
                }
            }
            return null;
        }
    }
}
