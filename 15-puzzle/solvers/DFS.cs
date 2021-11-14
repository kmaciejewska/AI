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
        HashSet<Board> visited = new HashSet<Board>();

        public override void Solve(BoardState root)
        {
            visited.Add(root.currentBoard);
            if (root.currentBoard.IsEqual(this.GoalState))
            {
                //trace path to root node 
                this.PrintSolution(root);
                return;
            }

            var zero = root.currentBoard.IndexOfZero();
            var zeroX = zero.Item1;
            var zeroY = zero.Item2;

            var children = this.ExpandBoard(root, zeroX, zeroY);
            for (int i = 0; i < children.Count; i++)
            {
                BoardState currentChild = children[i];

                if(!visited.Contains(currentChild.currentBoard))
                {
                    this.Solve(currentChild);
                }
            }

            /*Stack<BoardState> stack = new Stack<BoardState>(); //last in first out
            HashSet<Board> visited = new HashSet<Board>();

            stack.Push(root);
            visited.Add(root.currentBoard);

            while (stack.Count > 0)
            {
                root = stack.Pop();

                if (root.currentBoard.IsEqual(this.GoalState))
                {
                    //trace path to root node 
                    this.PrintSolution(root);
                    break;
                }

                var zero = root.currentBoard.IndexOfZero();
                var zeroX = zero.Item1;
                var zeroY = zero.Item2;

                var children = this.ExpandBoard(root, zeroX, zeroY);

                for (int i = 0; i < children.Count; i++)
                {
                    var currentChild = children[i];

                    if(!visited.Contains(currentChild.currentBoard))
                    {
                        stack.Push(currentChild);
                        visited.Add(currentChild.currentBoard);
                    }
                }
            }*/
        }
        
    }
}
