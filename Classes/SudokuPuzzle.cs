using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sudoku_Solver.Classes
{
	public class SudokuPuzzle
	{
		public SudokuPuzzle()
		{

		}

		public int[,] Grid { get; set; }

		public int[,] BlankPuzzle()
		{
			Grid = new int[9, 9]
			{
				{ 0,0,0,0,0,0,0,0,0 },
				{ 0,0,0,0,0,0,0,0,0 },
				{ 0,0,0,0,0,0,0,0,0 },
				{ 0,0,0,0,0,0,0,0,0 },
				{ 0,0,0,0,0,0,0,0,0 },
				{ 0,0,0,0,0,0,0,0,0 },
				{ 0,0,0,0,0,0,0,0,0 },
				{ 0,0,0,0,0,0,0,0,0 },
				{ 0,0,0,0,0,0,0,0,0 }
			};
			return Grid;
		}

		public int[,] TestPuzzle()
		{
			Grid = new int[9, 9]
			{
				{ 7,8,0,4,0,0,1,2,0 },
				{ 6,0,0,0,7,5,0,0,9 },
				{ 0,0,0,6,0,1,0,7,8 },
				{ 0,0,7,0,4,0,2,6,0 },
				{ 0,0,1,0,5,0,9,3,0 },
				{ 9,0,4,0,6,0,0,0,5 },
				{ 0,7,0,3,0,0,0,1,2 },
				{ 1,2,0,0,0,7,4,0,0 },
				{ 0,4,9,2,0,6,0,0,7 }
			};
			return Grid;
		}

		public int RandomSudokuNumbers()
		{
			Random r = new Random();
			return r.Next(1, 10);
			//List<int> random = new List<int>();
			//while (random.Count < 9)
			//{
			//	int num = r.Next(1, 10);
			//	if (!random.Contains(num))
			//	{
			//		random.Add(num);
			//	}
			//}
			//return random;

		}

		public int[,] GenerateRandomPuzzle()
		{
			Grid = BlankPuzzle();
			FillDiagonal();
			Solve();

			return Grid;
		}

		public bool NumberUnusedInBox(int rowStart, int colStart, int num)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if(Grid[rowStart+i,colStart+j] == num)
					{
						return false;
					}
				}
			}
			return true;
		}

		public void FillBox(int row, int col)
		{
			int num;
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					do
					{
						num = RandomSudokuNumbers();
					} while (!NumberUnusedInBox(row, col, num));

					Grid[row + i, col + j] = num;
				}

			}
		}

		public void FillDiagonal()
		{
			for (int i = 0; i < 9; i+=3)
			{
				FillBox(i, i);
			}
		}

		public bool Solve()
		{
			int[] find = FindEmpty(Grid);
			var xPos = find[0];
			var yPos = find[1];
			var outOfBounds = 10;

			if(xPos == outOfBounds && yPos == outOfBounds)
			{
				return true;
			}
			for (int i = 1; i < 10; i++) // 1-9 are the possible numbers to check
			{
				if (IsValid(Grid, i, find))
				{
					Grid[xPos, yPos] = i;
					if (Solve())  // use recursion to find the next empty and repeat process
					{
						return true;
					}
					else
					{
						Grid[xPos, yPos] = 0;  // BACKTRACKING ALGORYTHM: if no solution for position is found, a 0 is placed and for loop continues
					}
				}
			}
			return false;  // no solution possible;
			
		}

		public int[] FindEmpty(int[,] sudokuPuzzle)
		{
			var puzzleWidth = sudokuPuzzle.GetLength(1);
			var puzzleHeight = sudokuPuzzle.GetLength(0);

			for (int i = 0; i < puzzleHeight; i++)
			{
				for (int j = 0; j < puzzleWidth; j++)
				{
					if (sudokuPuzzle[i, j] == 0)
					{
						return new int[2] { i, j };  //location of empty
					}
				}
			}
			return new int[2] { 10, 10 };  //impossible value
		}

		public bool IsValid(int[,] sudokuPuzzle, int number, int[] position)
		{
			//Check the row
			var xPos = position[1];
			var yPos = position[0];
			var puzzleHeight = sudokuPuzzle.GetLength(0);
			var puzzleWidth = sudokuPuzzle.GetLength(1);

			for (int i = 0; i < puzzleHeight; i++)
			{
				if ((int)sudokuPuzzle.GetValue(yPos, i) == number && xPos != i) //check each row position for number except for loction of number
				{
					return false;
				}
			}

			//Check the column
			for (int i = 0; i < puzzleWidth; i++)
			{
				if ((int)sudokuPuzzle.GetValue(i, xPos) == number && yPos != i)
				{
					return false;
				}
			}

			//Check the box

			//make 9x9 into 3x3 with integer division
			int box_X = xPos / 3;
			int box_Y = yPos / 3;

			for (int i = box_Y * 3; i < (box_Y*3)+3; i++)
			{
				for (int j = box_X * 3; j < (box_X * 3)+3; j++)
				{
					if(sudokuPuzzle[i,j] == number && (yPos != j || xPos != i))
					{
						return false;
					}
				}
			}
			return true;
		}

		public void DisplayPuzzle()
		{
			var puzzleHeight = Grid.GetLength(0);
			var puzzleWidth = Grid.GetLength(1);

			for (int i = 0; i < puzzleHeight; i++)
			{
				if(i%3 == 0 && i != 0)
				{
					Console.WriteLine("- - - - - - - - - - - -");
				}
				for (int j = 0; j < puzzleWidth; j++)
				{
					if(j%3 == 0 && j != 0)
					{
						Console.Write(" | ");
					}
					if(j == 8)
					{
						Console.Write($"{Grid[i, j]}\n");
					}
					else
					{
						Console.Write($"{Grid[i, j]} ");
					}
				}
			}
		}
	}
}
