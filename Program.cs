using System;
using Sudoku_Solver.Classes;

namespace Sudoku_Solver
{
	class Program
	{
		static void Main(string[] args)
		{

			SudokuPuzzle Puzzle = new SudokuPuzzle();
			Puzzle.BlankPuzzle();
			Puzzle.DisplayPuzzle();
			Puzzle.GenerateRandomPuzzle();
			Console.WriteLine();
			Puzzle.DisplayPuzzle();
			//Puzzle.Solve(Puzzle.Grid);
		}
	}
}
