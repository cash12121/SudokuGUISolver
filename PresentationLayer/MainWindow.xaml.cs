using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private SudokuManager sudokuManager;
		private TextBox[,] txtGrid = new TextBox[9, 9];
		private int[,] intBoard = new int[9, 9];

		public MainWindow()
		{
			sudokuManager = new SudokuManager();
			InitializeComponent();
		}

		/// <summary>
		/// Method for executing on window load.
		/// Fills in the uniform grid with TextBoxes to use for Sudoku.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			for (int i = 0; i < txtGrid.GetLength(0); i++)
			{
				for (int j = 0; j < txtGrid.GetLength(1); j++)
				{
					// Add new TextBoxes
					txtGrid[i, j] = new TextBox();
					ugaBoard.Children.Add(txtGrid[i, j]);
					ChangeText(i, j);

					// Add Attriubutes
					txtGrid[i, j].TextChanged += new TextChangedEventHandler(TextBox_TextChanged);
					txtGrid[i, j].MaxLength = 1;
					txtGrid[i, j].MaxLines = 1;
					txtGrid[i, j].FontSize = 16;
					txtGrid[i, j].TextAlignment = TextAlignment.Center;

					// Add Borders
					txtGrid[i, j].BorderBrush = Brushes.Black;
					Thickness setThickness = new Thickness(.5, .5, .5, .5);

					//Check Boundaries
					//Check Top
					if (i == 0)
					{
						setThickness.Top = 2;
					}

					// Check Bottom
					if (i == 8)
					{
						setThickness.Bottom = 2;
					}

					// Check Left
					if (j == 0)
					{
						setThickness.Left = 2;
					}

					// Check Right
					if (j == 8)
					{
						setThickness.Right = 2;
					}

					//Check and add Dividers

					if (j == 2 || j == 5)
					{
						setThickness.Right = 2;
					}

					if (i == 2 || i == 5)
					{
						setThickness.Bottom = 2;
					}

					//Apply Border Thickness
					txtGrid[i, j].BorderThickness = setThickness;
				}
			}

			//Print2DArray<int>(sudokuManager.SudokuBoard);
			//sudokuManager.SolveBoard();
			//Console.WriteLine("\nSolved Board\n");
			//Print2DArray<int>(sudokuManager.SudokuBoard);
		}

		private void ChangeText(int i, int j)
		{
			if(sudokuManager.SudokuBoard[i,j].ToString().Equals("0"))
			{
				txtGrid[i, j].Text = "";
			} else
			{
				txtGrid[i, j].Text = sudokuManager.SudokuBoard[i, j].ToString();
			}
		}

		//Debug Tool
		public static void Print2DArray<T>(T[,] matrix)
		{
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					Console.Write(matrix[i, j] + "\t");
				}
				Console.WriteLine();
			}
		}

		private void btnReset_Click(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					txtGrid[i, j].Text = "";
				}
			}
			UpdateBoard();
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			UpdateBoard();
		}

		private void UpdateBoard()
		{
			try
			{
				for (int i = 0; i < 9; i++)
				{
					for (int j = 0; j < 9; j++)
					{
						if(txtGrid[i, j].Text == null || txtGrid[i, j].Text.Equals(""))
						{
							intBoard[i, j] = 0;
						} else
						{
							intBoard[i, j] = Int32.Parse(txtGrid[i, j].Text);
						}
					}
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		private void btnSubmit_Click(object sender, RoutedEventArgs e)
		{
			sudokuManager.UpdatedBoard(intBoard);
			sudokuManager.SolveBoard();
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					ChangeText(i, j);
				}
			}
		}
	}
}
