using _15Puzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15_puzzle
{
    public class BFS : Solver
    {
        public override Board Solve(Board root)
        {
            Queue<Board> queue = new Queue<Board>(); //all the nodes that can be expanded

            queue.Enqueue(root);
            root.visited = true;

            while (queue.Count > 0)
            {
                Board current = queue.Dequeue();

                if (current.GoalTest())
                {
                    Console.WriteLine("Solved!");
                    //trace path to root node 
                    this.PrintSolution(current);
                    return current;
                }

                current.ExpandBoard(); //perform all legal moves

                for (int i = 0; i < current.children.Count; i++)
                {
                    Board currentChild = current.children[i];

                    /* queue contains current edge ? && current edge is not searched */
                    if (!currentChild.visited)
                    {
                        queue.Enqueue(currentChild);
                        currentChild.visited = true;
                    }
                        
                }
            }
            return null;
        }
    }
}