using _15Puzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15_puzzle
{
    public class BoardState
    {
        public BoardState parent;
        public string lastMove;
        public Board currentBoard;

        public BoardState(Board currentBoard, BoardState parent, string lastMove)
        {
            this.currentBoard = currentBoard;
            this.parent = parent;
            this.lastMove = lastMove;
        }

        public BoardState MoveToRight(int x, int y)
        {
            if (y == (this.currentBoard.puzzle.Length / 3) - 1)
                return null;


            var clonedPuzzle = Clone();

            //swap places
            var temp = clonedPuzzle.currentBoard.puzzle[x, y + 1];
            clonedPuzzle.currentBoard.puzzle[x, y + 1] = 0;
            clonedPuzzle.currentBoard.puzzle[x, y] = temp;

            return clonedPuzzle;
        }

        public BoardState MoveToLeft(int x, int y)
        {
            if (y == 0)
                return null;

            var clonedPuzzle = Clone();

            //swap places
            var temp = clonedPuzzle.currentBoard.puzzle[x, y - 1];
            clonedPuzzle.currentBoard.puzzle[x, y - 1] = 0;
            clonedPuzzle.currentBoard.puzzle[x, y] = temp;

            return clonedPuzzle;
        }

        public BoardState MoveDown(int x, int y)
        {
            if (x == (this.currentBoard.puzzle.Length / 3) - 1)
                return null;

            var clonedPuzzle = Clone();

            //swap places
            var temp = clonedPuzzle.currentBoard.puzzle[x + 1, y];
            clonedPuzzle.currentBoard.puzzle[x + 1, y] = 0;
            clonedPuzzle.currentBoard.puzzle[x, y] = temp;

            return clonedPuzzle;
        }

        public BoardState MoveUp(int x, int y)
        {
            if (x == 0)
                return null;

            var clonedPuzzle = Clone();

            //swap places
            var temp = clonedPuzzle.currentBoard.puzzle[x - 1, y];
            clonedPuzzle.currentBoard.puzzle[x - 1, y] = 0;
            clonedPuzzle.currentBoard.puzzle[x, y] = temp;

            return clonedPuzzle;
        }

        public BoardState Clone()
        {
            int row = this.currentBoard.puzzle.GetLength(0);
            int[,] newPuzzle = new int[row, row];

            for (int i = 0; i < this.currentBoard.puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < this.currentBoard.puzzle.GetLength(1); j++)
                    newPuzzle[i, j] = this.currentBoard.puzzle[i, j];
            }

            var clonedBoard = new Board(newPuzzle);

            return new BoardState(clonedBoard, this.parent, this.lastMove);
        }

        public override int GetHashCode()
        {
            return this.currentBoard.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var otherState = (BoardState)obj;

            return this.currentBoard.Equals(otherState.currentBoard);
        }
    }
}
