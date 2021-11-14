using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15Puzzle
{
    public class Board
    {
        public int[,] puzzle;

        /**
        * Constructor of the class Board.
        *
        * @param solver determines type of solver
        */
        public Board(int[,] p)
        {
            this.puzzle = p;
        }

        /**
        * Find index of 0 element
        */
        public Tuple<int, int> IndexOfZero()
        {
            for (int x = 0; x < puzzle.GetLength(0); ++x)
            {
                for (int y = 0; y < puzzle.GetLength(1); ++y)
                {
                    if (this.puzzle[x, y].Equals(0))
                    {
                        return Tuple.Create(x, y);
                    }
                }
            }

            return Tuple.Create(-1, -1);
        }

        /**
        * Check if board is correct.
        *
        * @return boolean - if the board is correct, then true
        */
        public bool IsEqual(int[,] matrixToTest)
        {
            for (int i = 0; i < this.puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < this.puzzle.GetLength(1); j++)
                {
                    if (this.puzzle[i, j] != matrixToTest[i, j])
                        return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            var otherBoard = (Board)obj;
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    if (this.puzzle[i, j] != otherBoard.puzzle[i, j])
                        return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int result = 0;
            int shift = 0;
            for (int i = 0; i < this.puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < this.puzzle.GetLength(1); j++)
                {
                    shift = (shift + 11) % 21;
                    result ^= (this.puzzle[i, j] + 1024) << shift;
                }
            }
            return result;
        }
    }
}
