using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15Puzzle
{
    class UninformedSearch
    {
        public UninformedSearch()
        {

        }

        public List<Node> BFS(Node root)
        {
            List<Node> path = new List<Node>(); //all the nodes that led to the solution
            List<Node> queue = new List<Node>(); //all the nodes that can be expanded
            List<Node> searched = new List<Node>(); //nodes that are already expanded

            queue.Add(root);
            searched.Add(root);

            while (queue.Count > 0)
            {
                Node current = queue[0];
                queue.RemoveAt(0);
                searched.Add(current);

                if (current.GoalTest())
                {
                    Console.WriteLine("Solved!");
                    //trace path to root node 
                    TracePath(path, current);
                    break;
                }

                current.ExpandNode();   //perform all legal moves

                for (int i = 0; i < current.edges.Count; i++)
                {
                    Node currentEdge = current.edges[i];

                    /* queue contains current edge ? && current edge is not searched */
                    if (!ContainsNode(queue, currentEdge) && !ContainsNode(searched, currentEdge))
                        queue.Add(currentEdge);
                }
            }
            return path;
        }

        public void TracePath(List<Node> path, Node n)
        {
            Console.WriteLine("Tracing path...");
            Node current = n;
            path.Add(current);
            while (current.parent != null)   //root node doesnt have parents
            {
                current = current.parent;
                path.Add(current);
            }
        }

        public static bool ContainsNode(List<Node> list, Node n)
        {
            //check for every list element if it has the same puzzle as the given node
            bool contains = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IsSamePuzzle(n.puzzle))
                    contains = true;

            }
            return contains;
        }
    }
}
