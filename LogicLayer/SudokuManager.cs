using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	public class SudokuManager
	{
		public int[,] SudokuBoard { get; set; }

		public SudokuManager()
		{
			SetBoard();
			EmptyBoard();
		}

		public SudokuManager(int[,] intialBoard)
		{
			SetBoard();
			UpdatedBoard(intialBoard);
		}

		public void SetBoard()
		{
			SudokuBoard = new int[9, 9];
		}


		public void EmptyBoard()
		{
			for (int i = 0; i < SudokuBoard.GetLength(0); i++)
			{
				for (int j = 0; j < SudokuBoard.GetLength(1); j++)
				{
					SudokuBoard[i, j] = 0;
				}
			}
		}

		public void UpdatedBoard(int[,] updatedBoard)
		{
			for (int i = 0; i < SudokuBoard.GetLength(0); i++)
			{
				for (int j = 0; j < SudokuBoard.GetLength(1); j++)
				{
					SudokuBoard[i, j] = updatedBoard[i,j];
				}
			}
		}

		public bool CheckRow(int row)
		{
			bool isValid = true;
			HashSet<int> dupes = new HashSet<int>();

			for (int col = 0; col < SudokuBoard.GetLength(0); col++)
			{
				if (dupes.Add(SudokuBoard[row,col]) == false)
				{
					isValid = false;
				}
			}

			return isValid;
		}

		public bool CheckColumn(int col)
		{
			bool isValid = true;
			HashSet<int> dupes = new HashSet<int>();

			for (int row = 0; row < SudokuBoard.GetLength(0); row++)
			{
				if (dupes.Add(SudokuBoard[row, col]) == false)
				{
					isValid = false;
				}
			}

			return isValid;
		}

		public bool CheckBox(int row, int col)
		{
			bool isValid = true;
			HashSet<int> dupes = new HashSet<int>();

			row = (row / 3) * 3;
			col = (col / 3) * 3;

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (dupes.Add(SudokuBoard[row + i, col + j]) == false)
					{
						isValid = false;
					}
				}
			}

			return isValid;
		}

		public bool Possible(int row, int col, int num)
		{
			//bool isValid = true;

			// Check Row
			for (int i = 0; i < SudokuBoard.GetLength(0); i++)
			{
				if (SudokuBoard[row,i] == num)
				{
					return false;
				}
			}

			// Check Column
			for (int i = 0; i < SudokuBoard.GetLength(1); i++)
			{
				if (SudokuBoard[i, col] == num)
				{
					return false;
				}
			}

			// Floor Division

			row = (row / 3) * 3;
			col = (col / 3) * 3;

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (SudokuBoard[row + i, col + j] == num)
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Method for solving the board and returning that if the board is solved or not.
		/// </summary>
		/// <returns>bool Returns if the board was solved.</returns>
		public bool SolveBoard()
		{
			// Loop through row
			for (int x = 0; x < 9; x++)
			{
				// Loop through column
				for (int y = 0; y < 9; y++)
				{
					// Check if the selected spot is equal to 0
					if (SudokuBoard[x,y] == 0)
					{
						// Try looping through numbers 1 through 9
						for (int num = 1; num < 10; num++)
						{
							//Check if number is possible to add to spot
							if (Possible(x, y, num))
							{
								SudokuBoard[x, y] = num;

								// Check if it is a good number
								if (SolveBoard())
								{
									return true;
								}

								// If the number is not the apart of the solution reset number to 0
								SudokuBoard[x, y] = 0;
							}
						}

						// Return false if 1-9 cannot be the solution fo the spot
						// and return to previous spot via backtracking
						return false;
					}
					
				}
			}

			// Once this spot is reached it means have reached the end of the 2D
			// Array and the last number is correct. Therefore return true.
			return true;
		}
	}
}
