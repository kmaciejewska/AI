using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;

namespace _15_puzzle.solvers
{
    public class BF : Solver
    {
        public override void Solve(BoardState root)
        {
            var queue = new IntervalHeap<BoardState>(); //last in first out
            HashSet<Board> visited = new HashSet<Board>();

            stack.Push(root);
            visited.Add(root.currentBoard);

            while (stack.Count > 0)
        }
    }
}
