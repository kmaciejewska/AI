using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15Puzzle
{
    class Node
    {
        public List<Node> edges = new List<Node>();
        public Node parent;
        public static int col = 4;
        public static int row = 4;
        public static int puzzleSize = col * row;
        //to move left we decrease index by 1
        //to move right we increase index by 1
        //to move up we decrease by column size 
        //to move down we increase by column size
        public int[] puzzle = new int[puzzleSize];
        public int emptyIndex = 0;

        public Node(int[] puzzle)
        {
            SetPuzzle(puzzle);
        }

        public void SetPuzzle(int[] puzzle)
        {
            for (int i = 0; i < puzzle.Length; i++)
            {
                this.puzzle[i] = puzzle[i];
            }
        }

        public void ExpandNode()
        {
            for (int i = 0; i < puzzleSize; i++)
            {
                if (puzzle[i] == 0)
                    emptyIndex = i;
            }
            MoveToRight(puzzle, emptyIndex);
            MoveToLeft(puzzle, emptyIndex);
            MoveUp(puzzle, emptyIndex);
            MoveDovn(puzzle, emptyIndex);
        }

        public void CopyPuzzle(int[] copy, int[] org)
        {
            for (int i = 0; i < org.Length; i++)
            {
                copy[i] = org[i];
            }
        }

        public void MoveToRight(int[] p, int i)
        {
            if (i % col < col - 1)
            {
                int[] newPuzzle = new int[puzzleSize];
                CopyPuzzle(newPuzzle, p);

                //swap places
                int temp = newPuzzle[i + 1];
                newPuzzle[i + 1] = newPuzzle[i];
                newPuzzle[i] = temp;

                Node edge = new Node(newPuzzle);
                edges.Add(edge);
                edge.parent = this;
            }
        }

        public void MoveToLeft(int[] p, int i)
        {
            if (i % col > 0)
            {
                int[] newPuzzle = new int[puzzleSize];
                CopyPuzzle(newPuzzle, p);

                //swap places
                int temp = newPuzzle[i - 1];
                newPuzzle[i - 1] = newPuzzle[i];
                newPuzzle[i] = temp;

                Node edge = new Node(newPuzzle);
                edges.Add(edge);
                edge.parent = this;
            }
        }

        public void MoveDovn(int[] p, int i)
        {
            if (i + col < puzzleSize)
            {
                int[] newPuzzle = new int[puzzleSize];
                CopyPuzzle(newPuzzle, p);

                //swap places
                int temp = newPuzzle[i + col];
                newPuzzle[i + col] = newPuzzle[i];
                newPuzzle[i] = temp;

                Node edge = new Node(newPuzzle);
                edges.Add(edge);
                edge.parent = this;
            }
        }

        public void MoveUp(int[] p, int i)
        {
            if (i - col >= 0)
            {
                int[] newPuzzle = new int[puzzleSize];
                CopyPuzzle(newPuzzle, p);

                //swap places
                int temp = newPuzzle[i - col];
                newPuzzle[i - col] = newPuzzle[i];
                newPuzzle[i] = temp;

                Node edge = new Node(newPuzzle);
                edges.Add(edge);
                edge.parent = this;
            }
        }

        public bool GoalTest()
        {
            bool isGoal = true;
            int current = puzzle[0];

            //if any of the numbers is greater than the next one
            //then the puzzle is not solved
            for (int i = 0; i < puzzle.Length; i++)
            {
                if (current > puzzle[i])
                    isGoal = false;

                current = puzzle[i];
            }
            return isGoal;
        }

        public bool IsSamePuzzle(int[] p)
        {
            bool samePuzzle = true;
            for (int i = 0; i < p.Length; i++)
            {
                if (puzzle[i] != p[i])
                    samePuzzle = false;
            }
            return samePuzzle;
        }

        public void PrintPuzzle()
        {
            Console.WriteLine();
            int m = 0;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    Console.Write(puzzle[m] + " ");
                    m++;
                }
                Console.WriteLine();
            }
        }
    }
}
