using System;
using System.Collections.Generic;
using System.Linq;

namespace TurtleChallenge
{
	public class GameObject
	{
		private const int DefaultNumberOfRows = 4;
		private const int DefaultNumberOfColumn = 6;

		public GameObject(int? rows, int? columns, Position turtleCoordinates, IEnumerable<Position> minesCoordinates, Position exitCoordinates)
		{
			GameEnd = false;
			Rows = rows ?? DefaultNumberOfRows;
			Columns = columns ?? DefaultNumberOfColumn;
			BuildGameGrid(rows, columns);
			CreateAndPlaceTurtleInTheGrid(turtleCoordinates);
			SetMines(minesCoordinates);
			SetExit(exitCoordinates);
		}

		public GameObject(int? rows, int? columns, int numberOfMines)
		{
			GameEnd = false;
			Rows = rows ?? DefaultNumberOfRows;
			Columns = columns ?? DefaultNumberOfColumn;
			BuildGameGrid(rows, columns);
			SetMines(numberOfMines);
			SetExit();
			CreateAndPlaceTurtleInTheGrid();
		}

		public void TurnTurtle(Orientation orientation)
		{
			Turtle.CurrentOrientation = orientation;
		}

		public void RotateTurtle90DegreesRight()
		{
			var currentOrientation = Turtle.CurrentOrientation;

			switch (currentOrientation)
			{
				case Orientation.North:
					Turtle.CurrentOrientation = Orientation.East;
					break;
				case Orientation.East:
					Turtle.CurrentOrientation = Orientation.South;
					break;
				case Orientation.South:
					Turtle.CurrentOrientation = Orientation.West;
					break;
				case Orientation.West:
					Turtle.CurrentOrientation = Orientation.North;
					break;
			}
		}

		public void MoveTurtleUp()
		{
			MoveTurtle(Move.Up);
		}

		public void MoveTurtle(Move move)
		{
			var currentPosition = Turtle.CurrentPosition;
			var currentOrientation = Turtle.CurrentOrientation;
			Position nextCellPosition = null;
			
				switch (currentOrientation)
				{
					case Orientation.North:
						if (move == Move.Up)
							nextCellPosition = new Position(currentPosition.X, currentPosition.Y - 1);
						else if (move == Move.Down) nextCellPosition = new Position(currentPosition.X, currentPosition.Y + 1);
						else if (move == Move.Left) nextCellPosition = new Position(currentPosition.X -1, currentPosition.Y);
						else if (move == Move.Right) nextCellPosition = new Position(currentPosition.X +1, currentPosition.Y);

						VerifyAndSetTurtleCurrentPosition(move, nextCellPosition);
						break;
					case Orientation.West:
						if (move == Move.Up)
							nextCellPosition = new Position(currentPosition.X - 1, currentPosition.Y);
						else if (move == Move.Down) nextCellPosition = new Position(currentPosition.X + 1, currentPosition.Y);
						else if (move == Move.Left) nextCellPosition = new Position(currentPosition.X, currentPosition.Y +1);
						else if (move == Move.Right) nextCellPosition = new Position(currentPosition.X, currentPosition.Y -1);
						
						VerifyAndSetTurtleCurrentPosition(move, nextCellPosition);
						break;
					case Orientation.East:
						if (move == Move.Up)
							nextCellPosition = new Position(currentPosition.X + 1, currentPosition.Y);
						else if (move == Move.Down) nextCellPosition = new Position(currentPosition.X - 1, currentPosition.Y);
						else if (move == Move.Left) nextCellPosition = new Position(currentPosition.X, currentPosition.Y -1);
						else if (move == Move.Right) nextCellPosition = new Position(currentPosition.X, currentPosition.Y +1);

						VerifyAndSetTurtleCurrentPosition(move, nextCellPosition);
						break;
					case Orientation.South:
						if (move == Move.Up)
							nextCellPosition = new Position(currentPosition.X, currentPosition.Y + 1);
						else if (move == Move.Down) nextCellPosition = new Position(currentPosition.X, currentPosition.Y - 1);
						else if (move == Move.Left) nextCellPosition = new Position(currentPosition.X +1, currentPosition.Y);
						else if (move == Move.Right) nextCellPosition = new Position(currentPosition.X -1, currentPosition.Y);

						VerifyAndSetTurtleCurrentPosition(move, nextCellPosition);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
		}

		private void VerifyAndSetTurtleCurrentPosition(Move move, Position nextCellPosition)
		{

			if (nextCellPosition.X < 0 || nextCellPosition.Y < 0)
			{
				throw new ArgumentOutOfRangeException($"These coordinates contains negative values, x: {nextCellPosition.X} y: {nextCellPosition.Y} try again.");
			}
			else
			{
				if (Cells.SingleOrDefault(c =>
					c.Position.X == nextCellPosition.X && c.Position.Y == nextCellPosition.Y) != null)
				{
					Turtle.CurrentPosition = nextCellPosition;
					Turtle.Moves.Push(
						new Tuple<Orientation, Move, Position>(Turtle.CurrentOrientation, move,
							Turtle.CurrentPosition));
				}
				else
				{
					throw new ArgumentOutOfRangeException(
						$"This Cell does not exists, x: {nextCellPosition.X} y: {nextCellPosition.Y} try again.");
				}
			}

		}

		public const int MaxNumberOfMines = 6;
		public int Columns { get; }
		public int Rows { get; }
		public Turtle Turtle { get; private set; }
		private List<Cell> Cells { get; set; }

		private void SetMines(int numberOfMines)
		{
			if(numberOfMines > MaxNumberOfMines) throw new ArgumentOutOfRangeException($"Max number of mines is {MaxNumberOfMines}");

			var cellsCount = Cells.Count-1;
			var randomize = new Random();
			for (var i = 0; i < numberOfMines; i++)
			{
				var minePosition = randomize.Next(1, cellsCount);
				Cells[minePosition].IsMined = true;
			}
		}

		private void SetMines(IEnumerable<Position> minePositions)
		{
			foreach (var minesPosition in minePositions)
			{
				var idx = GetCellIdx(minesPosition.X, minesPosition.Y);
				Cells[idx].IsMined = true;
			}
		}

		private void SetExit()
		{
			var sideCells = Cells.Where(x => x.IsMined == false && IsASideCell(x.Position)).ToList();

			var rndSideCellIdx = new Random().Next(sideCells.Count-1);

			var exitCell = sideCells[rndSideCellIdx];

			var exitCellIdx = GetCellIdx(exitCell.Position.X, exitCell.Position.Y);

			Cells[exitCellIdx].IsExit = true;
		}

		private void SetExit(Position exitCoordinates)
		{
			var sideCells = Cells.Where(x => x.IsMined == false && IsASideCell(x.Position)).ToList();

			if (sideCells.SingleOrDefault(c =>
				c.Position.X == exitCoordinates.X && c.Position.Y == exitCoordinates.Y) != null)
			{
				var idx = GetCellIdx(exitCoordinates.X, exitCoordinates.Y);
				Cells[idx].IsExit = true;
			}
		}

		public void CreateAndPlaceTurtleInTheGrid()
		{
			var freeCells = Cells.Where(c => c.IsMined == false && c.IsExit == false).ToList();
			var freeCellIdx = new Random().Next(freeCells.Count -1);
			var freeCellPosition = freeCells[freeCellIdx].Position;

			var orientations = Enum.GetValues(typeof(Orientation));
			var randomOrientation = (Orientation) orientations.GetValue(new Random().Next(orientations.Length));

			if (Turtle == null)
			{
				Turtle = new Turtle(freeCellPosition.X, freeCellPosition.Y, randomOrientation);
			}
			else
			{
				Turtle.CurrentPosition = new Position(freeCellPosition.X,freeCellPosition.Y);
				Turtle.CurrentOrientation = randomOrientation;
			}
		}

		public void CreateAndPlaceTurtleInTheGrid(Position position)
		{
			if (Cells.SingleOrDefault(c =>
				c.Position.X == position.X && c.Position.Y == position.Y) != null)
			{
				Turtle = new Turtle(position.X, position.Y, Orientation.North);
			}

		}

		public bool CheckForMines(Position position)
		{
			var idx = GetCellIdx(position.X, position.Y);

			var isMined = Cells[idx].IsMined;

			if (isMined)
			{
				return true;
			}

			return false;
		}

		public bool CheckForExit(Position position)
		{
			var idx = GetCellIdx(position.X, position.Y);

			var isExit = Cells[idx].IsExit;

			if (isExit)
			{
				return true;
			}

			return false;
		}


		private int GetCellIdx(int x, int y)
		{
			for (var i = 0; i <= Cells.Count -1; i++)
			{
				if (Cells[i].Position.X == x && Cells[i].Position.Y == y)
				{
					return i;
				}
			}

			return 0;
		}

		private bool IsASideCell(Position position)
		{
			if (position.X >= 0 && position.X <= Columns - 1 && position.Y == Rows - 1 ||
			    position.X == 0 && position.Y >= 0 && position.Y <= Rows - 1 ||
			    position.X >= 0 && position.X <= Columns - 1 && position.Y == 0 ||
			    position.X == Columns - 1 && position.Y >= 0 && position.Y <= Rows - 1)
			{
				return true;
			}

			return false;
		}


		private void BuildGameGrid(int? rows, int? columns)
		{
			if (Cells == null)
			{
				Cells = new List<Cell>();
			}
			else
			{
				Cells.Clear();
			}

			for (var i = 0; i <= Columns-1; i++)
			{
				for (var j = 0; j <= Rows-1; j++)
				{
					Cells.Add(new Cell(i, j));
				}
			}
			
		}

		public bool GameEnd { get; set; }
	}
}
