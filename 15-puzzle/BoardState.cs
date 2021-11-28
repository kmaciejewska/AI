using System;

namespace _15_Puzzle
{
    public class BoardState : IComparable<BoardState>
    {
        public BoardState parent;
        public string lastMove;
        public Board currentBoard;
        public int[,] GoalState { get; set; }
        public int Distance { get; set; }
        public int Cost { get; set; }
        public int CostDistance { get; set; }
        public string HeuristicID { get; set; }

        public BoardState(Board currentBoard, BoardState parent, string lastMove, int cost, string heuristicID = "")
        {
            this.currentBoard = currentBoard;
            this.parent = parent;
            this.lastMove = lastMove;
            this.Cost = cost;
            this.HeuristicID = heuristicID;

            SetGoalState(currentBoard.puzzle.GetLength(0), currentBoard.puzzle.GetLength(1));
            SetDistance();
            
        }

        public void SetGoalState(int row, int col)
        {
            this.GoalState = new int[row, col];
            int num = 1;
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if ((i == row - 1) && (j == col - 1))
                    {
                        this.GoalState[i, j] = 0;
                    }
                    else
                    {
                        this.GoalState[i, j] = num;
                        num++;
                    }
                }
            }
        }

        public void SetDistance()
        {
            if (this.HeuristicID == "")
                this.Distance = 0;
            else if (HeuristicID == "m")
                this.Distance = this.ManhatanDistance();
            else if (HeuristicID == "e")
                this.Distance = this.EuclideanDistance();
        }

        //heuristics
        public int ManhatanDistance()
        {
            var matrix = this.currentBoard.puzzle;
            int manhatanDistance = 0;

            for (int i = 1; i < matrix.Length; i++)
            {
                var indexGoal = IndexOf(i, GoalState);
                var index = IndexOf(i, matrix);
                manhatanDistance += Math.Abs(indexGoal.Item1 - index.Item1) + Math.Abs(indexGoal.Item2 - index.Item2);
            }

            return manhatanDistance;
        }

        public int EuclideanDistance()
        {
            var matrix = this.currentBoard.puzzle;
            int euclideanDistance = 0;

            for (int i = 1; i < matrix.Length; i++)
            {
                var indexGoal = IndexOf(i, GoalState);
                var index = IndexOf(i, matrix);
                euclideanDistance += (int)Math.Sqrt(Math.Pow((double)(indexGoal.Item1 - index.Item1), 2) + Math.Pow((double)(indexGoal.Item2 - index.Item2), 2));
            }

            return euclideanDistance;
        }

        private Tuple<int, int> IndexOf(int number, int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j].Equals(number))
                        return Tuple.Create(i, j);
                }
            }
            return Tuple.Create(-1, -1);
        }

        //moves
        public BoardState MoveToRight(int x, int y)
        {
            if (y == (this.currentBoard.puzzle.Length / this.currentBoard.puzzle.GetLength(0)) - 1)
                return null;


            var clonedPuzzle = Clone();

            //swap places
            var temp = clonedPuzzle.currentBoard.puzzle[x, y + 1];
            clonedPuzzle.currentBoard.puzzle[x, y + 1] = clonedPuzzle.currentBoard.puzzle[x, y];
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
            clonedPuzzle.currentBoard.puzzle[x, y - 1] = clonedPuzzle.currentBoard.puzzle[x, y];
            clonedPuzzle.currentBoard.puzzle[x, y] = temp;

            return clonedPuzzle;
        }

        public BoardState MoveDown(int x, int y)
        {
            if (x == (this.currentBoard.puzzle.Length / this.currentBoard.puzzle.GetLength(0)) - 1)
                return null;

            var clonedPuzzle = Clone();

            //swap places
            var temp = clonedPuzzle.currentBoard.puzzle[x + 1, y];
            clonedPuzzle.currentBoard.puzzle[x + 1, y] = clonedPuzzle.currentBoard.puzzle[x, y];
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
            clonedPuzzle.currentBoard.puzzle[x - 1, y] = clonedPuzzle.currentBoard.puzzle[x, y];
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

            return new BoardState(clonedBoard, this.parent, this.lastMove, this.Cost, this.HeuristicID);
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

        //used for informed algorithms to compare heuristics of successor boards
        //in interval heaps
        public int CompareTo(BoardState other)
        {
            // Heuristic value
            var thisValue = this.Distance;
            var otherValue = other.Distance;

            if (thisValue < otherValue)
            {
                return 1;
            }
            else if (thisValue > otherValue)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
