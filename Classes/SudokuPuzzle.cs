using System;
using System.Collections.Generic;
using System.Text;

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

		public bool Solve(int[,] sudokuPuzzle)
		{
			string f = FindEmpty(sudokuPuzzle);
			try
			{
				int[] find = new int[2] { int.Parse(f.Substring(0)), int.Parse(f.Substring(3)) };

			}
			catch (Exception)
			{
				return true;
			}
			


		}

		public string FindEmpty(int[,] sudokuPuzzle)
		{
			for (int i = 0; i < sudokuPuzzle.GetLength(1); i++) //Column
			{
				for (int j = 0; j < sudokuPuzzle.GetLength(0); j++) //Row
				{
					if (sudokuPuzzle[i, j] == 0)
					{
						return "i,j";
					}
				}
			}
			return "";
		}

		public bool IsValid(int[,] sudokuPuzzle, int number, int[] position)
		{
			//Check the row
			for (int i = 0; i < sudokuPuzzle.GetLength(0); i++)
			{
				if ((int)sudokuPuzzle.GetValue(position[1], position[0]) == number && position[1] != i)
				{
					return false;
				}
			}

			//Check the column
			for (int i = 0; i < sudokuPuzzle.GetLength(1); i++)
			{
				if ((int)sudokuPuzzle.GetValue(position[0], position[1]) == number && position[0] != i)
				{
					return false;
				}
			}

			//Check the box

			//make 9x9 into 3x3 with integer division
			int box_X = position[1] / 3;
			int box_Y = position[0] / 3;

			for (int i = 0; i < box_Y*3; i+=(box_Y*3)+3)
			{
				for (int j = 0; j < box_Y * 3; j+= (box_Y * 3) + 3)
				{
					if(sudokuPuzzle[i,j] == number && (position[0] != j || position[1] != i))
					{
						return false;
					}
				}
			}
			return true;
		}

		public int[,] DisplayPuzzle()
		{
			return Grid;
		}
	}
}
