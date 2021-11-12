using _15Puzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15_puzzle.solvers
{
    public class DFS : Solver
    {
        public override Board Solve(Board root)
        {
            Stack<Board> stack = new Stack<Board>();    //first in last out
            HashSet<Board> visited = new HashSet<Board>();

            stack.Push(root);
            visited.Add(root);

            while (stack.Count > 0)
            {
                root = stack.Pop();

                if (root.GoalTest())
                {
                    Console.WriteLine("Solved!");
                    //trace path to root node 
                    this.PrintSolution(root);
                    return root;
                }

                root.ExpandBoard();
                
                for (int i = 0; i < root.children.Count; i++)
                {
                    Board currentChild = root.children[i];
                    if (!visited.Contains(currentChild))
                    {
                        stack.Push(currentChild);
                        visited.Add(currentChild);
                    }
                }
            }
            return null;
        }
    }
}
