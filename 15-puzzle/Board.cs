using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15Puzzle
{
    public class Board
    {
        private static readonly int COL = 3;
        private static readonly int ROW = 3;
        private static readonly int SIZE = COL * ROW;

        public Board parent;
        public List<Board> children;
        public int[] puzzle;
        public string lastMove;
        public bool visited;

        /**
        * Constructor of the class Board.
        *
        * @param solver determines type of solver
        */
        public Board(int[] p)
        {
            this.puzzle = new int[SIZE];
            this.children = new List<Board>();
            this.parent = null;
            this.lastMove = "";
            this.visited = false;

            this.puzzle = p;
        }

        /**
        * Check if board is correct.
        *
        * @return boolean - if the board is correct, then true
        */
        public bool CheckPuzzle()
        {
            bool isGoal = true;
            int current = puzzle[0];

            for (int i = 0; i < SIZE; i++)
            {
                if (current > puzzle[i])
                    isGoal = false;

                current = puzzle[i];
            }
            return isGoal;
        }

        /**
        * Find index of 0 element
        */
        public int IndexOfZero()
        {
            for (int i = 0; i < SIZE; i++)
            {
                if (puzzle[i].Equals(0))
                    return i;
            }
            return -1;
        }

        /*
        *  to move left we decrease index by 1
        *  to move right we increase index by 1
        *  to move up we decrease by column size 
        *  to move down we increase by column size
        * */
        public void ExpandBoard()
        {
            int i = IndexOfZero();

            MoveToRight(puzzle, i);
            MoveToLeft(puzzle, i);
            MoveUp(puzzle, i);
            MoveDown(puzzle, i);
        }

        public void MoveToRight(int[] p, int i)
        {
            if (i % COL < COL - 1)
            {
                var clonedPuzzle = CopyPuzzle(p);

                //swap places
                var temp = clonedPuzzle[i + 1];
                clonedPuzzle[i + 1] = clonedPuzzle[i];
                clonedPuzzle[i] = temp;

                Board child = new Board(clonedPuzzle);
                children.Add(child);
                child.lastMove = "R";
                child.parent = this;
            }
        }

        public void MoveToLeft(int[] p, int i)
        {
            if (i % COL > 0)
            {
                var clonedPuzzle = CopyPuzzle(p);

                //swap places
                var temp = clonedPuzzle[i - 1];
                clonedPuzzle[i - 1] = clonedPuzzle[i];
                clonedPuzzle[i] = temp;

                Board child = new Board(clonedPuzzle);
                children.Add(child);
                child.lastMove = "L";
                child.parent = this;
            }
        }

        public void MoveDown(int[] p, int i)
        {
            if (i + COL < SIZE)
            {
                var clonedPuzzle = CopyPuzzle(p);

                //swap places
                var temp = clonedPuzzle[i + COL];
                clonedPuzzle[i + COL] = clonedPuzzle[i];
                clonedPuzzle[i] = temp;

                Board child = new Board(clonedPuzzle);
                children.Add(child);
                child.lastMove = "D";
                child.parent = this;
            }
        }

        public void MoveUp(int[] p, int i)
        {
            if (i - COL >= 0)
            {
                var clonedPuzzle = CopyPuzzle(p);

                //swap places
                var temp = clonedPuzzle[i - COL];
                clonedPuzzle[i - COL] = clonedPuzzle[i];
                clonedPuzzle[i] = temp;

                Board child = new Board(clonedPuzzle);
                children.Add(child);
                child.lastMove = "U";
                child.parent = this;
            }
        }

        public int[] CopyPuzzle(int[] p)
        {
            int[] newPuzzle = new int[SIZE];

            for (int i = 0; i < SIZE; i++)
            {
                newPuzzle[i] = p[i];
            }

            return newPuzzle;
        }

        public bool GoalTest()
        {
            int current = puzzle[0];

            //if any of the numbers is greater than the next one
            //then the puzzle is not solved
            for (int i = 0; i < puzzle.Length; i++)
            {
                if (current > puzzle[i])
                    return false;

                current = puzzle[i];
            }
            return true;
        }

        public void PrintPuzzle()
        {
            Console.WriteLine();
            int m = 0;
            for (int i = 0; i < COL; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    Console.Write(puzzle[m] + " ");
                    m++;
                }
                Console.WriteLine();
            }
        }
    }
}
