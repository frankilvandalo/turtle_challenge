using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using TurtleChallenge.Payload;

namespace TurtleChallenge
{
	class Program
	{
		private const int MaxNumberOfColumns = 100;
		private const int MaxNumberOfRows = 100;
		private const int MaxNumberOfMines = 10;

		static void Main(string[] args)
		{
			GameObject gameObject = null;

			Console.WriteLine("*****************************************************************************");
			Console.WriteLine("****** Turtle Challenge by Matteo Vaghetti 05 May 2020 **********************");
			Console.WriteLine("*****************************************************************************");
			Console.WriteLine("*****************************************************************************");
			Console.WriteLine("*****************************************************************************");

			if (args != null && args.Length > 0)
			{
				var gameGrid = JsonConvert.DeserializeObject<GameGrid>(File.ReadAllText(args[0]));
				
				gameObject = new GameObject(gameGrid.Rows, 
					gameGrid.Columns, 
					gameGrid.TurtlePosition.Position,
					gameGrid.Mines.Select(m => m.Position),
					gameGrid.Exit.Position);

				Console.WriteLine($"Game Grid initialized from path: {args[0]}");
				
				var turtleSequence = JsonConvert.DeserializeObject<TurtleSequence>(File.ReadAllText(args[1]));

				Console.WriteLine($"Turtle moves sequence initialized from path: {args[1]}");

				Console.WriteLine($"Turtle Position is x: {gameObject.Turtle.CurrentPosition.X}, y: {gameObject.Turtle.CurrentPosition.Y}, orientation is: {gameObject.Turtle.CurrentOrientation}");

				for (int i = 0; i <= turtleSequence.Sequence.Length-1; i++)
				{
					Console.WriteLine("Turtle is moving ...");

					if (turtleSequence.Sequence[i].Rotate == true)
					{
						gameObject.RotateTurtle90DegreesRight();
					}

					if (turtleSequence.Sequence[i].MoveForward == true)
					{
						gameObject.MoveTurtleUp();
					}

					var currentTurtlePosition = gameObject.Turtle.CurrentPosition;
					var currentTurtleOrientation = gameObject.Turtle.CurrentOrientation;
					
					Console.WriteLine($"Turtle new Position is x: {currentTurtlePosition.X}, y: {currentTurtlePosition.Y}, orientation is: {currentTurtleOrientation}");
					
					if (gameObject.CheckForMines(currentTurtlePosition) == false)
					{
						if (gameObject.CheckForExit(currentTurtlePosition))
						{
							Console.WriteLine("Congratulation you found the EXIT !!!.");
							gameObject.GameEnd = true;
							Console.WriteLine("Press ESC to exit the program");
							if(Console.ReadKey(true).Key == ConsoleKey.Escape)
							{
								Environment.Exit(0);
							}
						}
						else
						{
							if (i == turtleSequence.Sequence.Length - 1)
							{
								Console.WriteLine("This was the last element of the sequence, EXIT not found!.");
								Console.WriteLine("Press ESC to exit the program");
								if(Console.ReadKey(true).Key == ConsoleKey.Escape)
								{
									Environment.Exit(0);
								}
							}
						}
					}
					else
					{
						Console.WriteLine("You hit a Mine!! Try with a different moves sequence.");
						Console.WriteLine("Press ESC to exit the program");
						if(Console.ReadKey(true).Key == ConsoleKey.Escape)
						{
							Environment.Exit(0);
						}
					}
				}
			}
			else
			{
				
				Console.WriteLine("Enter number of columns: ");
				var numberOfColumns = Convert.ToInt32(Console.ReadLine());
				if (numberOfColumns > MaxNumberOfColumns) numberOfColumns = MaxNumberOfColumns;
				Console.WriteLine("Enter number of rows: ");
				var numberOfRows = Convert.ToInt32(Console.ReadLine());
				if (numberOfRows > MaxNumberOfRows) numberOfColumns = MaxNumberOfRows;
				Console.WriteLine("Enter number of Mines: ");
				var numberOfMines = Convert.ToInt32(Console.ReadLine());
				if (numberOfMines > MaxNumberOfMines) numberOfColumns = MaxNumberOfMines;
			
				gameObject = new GameObject(numberOfRows, numberOfColumns, numberOfMines);
				Console.WriteLine($"Turtle Position is x: {gameObject.Turtle.CurrentPosition.X}, y: {gameObject.Turtle.CurrentPosition.Y}, orientation is: {gameObject.Turtle.CurrentOrientation}");

				while (!gameObject.GameEnd)
				{
					OrientTheTurtle(gameObject);
				}

				if (gameObject.GameEnd == true)
				{
					Console.WriteLine($"Congratulations! You won the game in {gameObject.Turtle.Moves.Count} moves. Press any  key to close the game.");
					Console.WriteLine("Press ESC to exit the program");
					if(Console.ReadKey(true).Key == ConsoleKey.Escape)
					{
						Environment.Exit(0);
					}
				}
			}
		}


		private static void OrientTheTurtle(GameObject gameObject)
		{
			Console.WriteLine("Select Turtle Orientation:");
			Console.WriteLine("\tn -north");
			Console.WriteLine("\ts -south");
			Console.WriteLine("\tw -west");
			Console.WriteLine("\te -east");
			Console.WriteLine($"\tx -current Orientation ({gameObject.Turtle.CurrentOrientation})");
			switch (Console.ReadLine())
			{
				case "n":
					gameObject.TurnTurtle(Orientation.North);
					SetMoveAndCheckPosition(gameObject);
					break;
				case "s":
					gameObject.TurnTurtle(Orientation.South);
					SetMoveAndCheckPosition(gameObject);
					break;
				case "w":
					gameObject.TurnTurtle(Orientation.West);
					SetMoveAndCheckPosition(gameObject);
					break;
				case "e":
					gameObject.TurnTurtle(Orientation.East);
					SetMoveAndCheckPosition(gameObject);
					break;
				case "x":
					SetMoveAndCheckPosition(gameObject);
					break;
			}
		}

		private static void SetMoveAndCheckPosition(GameObject gameObject)
		{
			Console.WriteLine("Move the Turtle:");
			Console.WriteLine("\tu -up");
			Console.WriteLine("\td -down");
			Console.WriteLine("\tl -left");
			Console.WriteLine("\tr -right");
			try
			{
				switch (Console.ReadLine())
				{
					case "u":
						gameObject.MoveTurtle(Move.Up);
						break;
					case "d":
						gameObject.MoveTurtle(Move.Down);
						break;
					case "l":
						gameObject.MoveTurtle(Move.Left);
						break;
					case "r":
						gameObject.MoveTurtle(Move.Right);
						break;

				}

				var currentTurtlePosition = gameObject.Turtle.CurrentPosition;
				if (gameObject.CheckForMines(currentTurtlePosition) == false)
				{
					if (gameObject.CheckForExit(currentTurtlePosition))
					{
						Console.WriteLine("Congratulation you found the EXIT !!!.");
						gameObject.GameEnd = true;
						Console.WriteLine("Press ESC to exit the program");
						if(Console.ReadKey(true).Key == ConsoleKey.Escape)
						{
							Environment.Exit(0);
						}
					}
					else
					{
						Console.WriteLine($"Turtle Position is x: {gameObject.Turtle.CurrentPosition.X}, y: {gameObject.Turtle.CurrentPosition.Y}, orientation is: {gameObject.Turtle.CurrentOrientation}");
						OrientTheTurtle(gameObject);
					}
				}
				else
				{
					gameObject.Turtle.Moves.TryPop(out _);
					Console.WriteLine("You hit a Mine!! Try again.");
					gameObject.Turtle.Moves.TryPeek(out var lastPosition);
					if (lastPosition != null)
					{
						gameObject.Turtle.CurrentPosition = lastPosition.Item3;
					}
					Console.WriteLine($"Turtle Position is x: {gameObject.Turtle.CurrentPosition.X}, y: {gameObject.Turtle.CurrentPosition.Y}, orientation is: {gameObject.Turtle.CurrentOrientation}");
					SetMoveAndCheckPosition(gameObject);
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
				gameObject.Turtle.Moves.TryPeek(out var lastPosition);
				if (lastPosition != null)
				{
					gameObject.Turtle.CurrentPosition = lastPosition.Item3;
				}
				Console.WriteLine($"Turtle Position is x: {gameObject.Turtle.CurrentPosition.X}, y: {gameObject.Turtle.CurrentPosition.Y}, orientation is: {gameObject.Turtle.CurrentOrientation}");
				SetMoveAndCheckPosition(gameObject);
			}
		}
	}
}
