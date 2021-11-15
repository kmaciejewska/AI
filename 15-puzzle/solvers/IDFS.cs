﻿using System;
using System.Linq;

namespace _15_puzzle.solvers
{
    public class IDFS : Solver
    {
        public override void Solve(BoardState root)
        {
            int depth = 1; 
            for(int i = 0; i <= depth; i++)
            {
                var foundRemaining = DLS(root, i);
                if (foundRemaining.Item1 != null)
                {
                    Console.WriteLine("Solved!");
                    //trace path to root node 
                    this.PrintSolution(foundRemaining.Item1);
                    break;
                }
                else if (!foundRemaining.Item2)
                {
                    return;
                }
                depth++;
            }

        }

        public Tuple<BoardState, bool> DLS(BoardState root, int limit)
        {
            // If reached the maximum depth, stop recursing.
            if (limit <= 0)
            {
                if (root.currentBoard.IsEqual(this.GoalState))
                    return new Tuple<BoardState, bool>(root, true);
                else
                    return new Tuple<BoardState, bool>(null, true);
            }

            bool remaining = false;

            //recur for all the vertices adjacent to source vertex
            var zero = root.currentBoard.IndexOfZero();
            var zeroX = zero.Item1;
            var zeroY = zero.Item2;

            var children = this.ExpandBoard(root, zeroX, zeroY);

            for (int i = 0; i < children.Count(); i++)
            {
                Tuple<BoardState, bool> foundRemaining = DLS(children[i], limit - 1);
                if (foundRemaining.Item1 != null)
                    return new Tuple<BoardState, bool>(foundRemaining.Item1, true);
                if (foundRemaining.Item2)
                    remaining = true;
            }
            return new Tuple<BoardState, bool>(null, remaining);
        }
    }
}
