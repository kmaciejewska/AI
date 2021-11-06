using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] puzzle =
            {
                0, 1, 2,
                3, 4, 5, 
                6, 7, 8
            };

            Node initPuzzle = new Node(puzzle);
            initPuzzle.SetGoal();
            UninformedSearch ui = new UninformedSearch();

            List<Node> solution = ui.BFS(initPuzzle);

            if (solution.Count > 0)
            {
                for (int i = 0; i < solution.Count; i++)
                {
                    solution[i].PrintPuzzle();
                }
            }
            else
            {
                Console.WriteLine("No solution found");
            }
            Console.Read();
        }
    }
}
