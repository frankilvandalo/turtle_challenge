using System;
using System.IO;
using System.Linq;
using Moq;
using Newtonsoft.Json;
using TurtleChallenge;
using TurtleChallenge.Payload;
using Xunit;

namespace TurtleChallenge.UnitTests{
public class TurtleChallengeUnitTests
	{
		private readonly GameObject _gameObject;
		private readonly GameGrid _gameGrid;
		public TurtleChallengeUnitTests()
		{
			_gameGrid = JsonConvert.DeserializeObject<GameGrid>(File.ReadAllText("TestGameGrid.json"));
				
			_gameObject = new GameObject(_gameGrid.Rows, 
				_gameGrid.Columns, 
				_gameGrid.TurtlePosition.Position,
				_gameGrid.Mines.Select(m => m.Position),
				_gameGrid.Exit.Position);
		}

		[Fact]
		public void Given_the_current_game_object_when_turtle_hits_a_mine_return_true()
		{
			var minePositions = _gameGrid.Mines.Select(m => m.Position);
			foreach (var position in minePositions)
			{
				var turtle = new Turtle(position.X, position.Y, It.IsAny<Orientation>());
				Assert.True(_gameObject.CheckForMines(turtle.CurrentPosition));
			}
		}

		[Fact]
		public void Given_the_current_game_object_when_turtle_hits_exit_return_true()
		{
			var exitPosition = _gameGrid.Exit.Position;
			
			var turtle = new Turtle(exitPosition.X, exitPosition.Y, It.IsAny<Orientation>());
			
			Assert.True(_gameObject.CheckForExit(turtle.CurrentPosition));
		}

		[Fact]
		public void Given_the_Turtle_North_Orientation_When_moving_up_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.North);
			_gameObject.MoveTurtle(Move.Up);
			
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(0,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_North_Orientation_When_moving_down_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.North);
			_gameObject.MoveTurtle(Move.Down);
			
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(2,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_North_Orientation_When_moving_left_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.North);
			_gameObject.MoveTurtle(Move.Left);
			
			Assert.Equal(0,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_North_Orientation_When_moving_right_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.North);
			_gameObject.MoveTurtle(Move.Right);
			
			Assert.Equal(2,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_East_Orientation_When_moving_up_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.East);
			_gameObject.MoveTurtle(Move.Up);
			
			Assert.Equal(2,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_East_Orientation_When_moving_down_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.East);
			_gameObject.MoveTurtle(Move.Down);
			
			Assert.Equal(0,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_East_Orientation_When_moving_left_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.East);
			_gameObject.MoveTurtle(Move.Left);
			
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(0,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_East_Orientation_When_moving_right_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.East);
			_gameObject.MoveTurtle(Move.Right);
			
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(2,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_South_Orientation_When_moving_up_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.South);
			_gameObject.MoveTurtle(Move.Up);
			
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(2,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_South_Orientation_When_moving_down_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.South);
			_gameObject.MoveTurtle(Move.Down);
			
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(0,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_South_Orientation_When_moving_left_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.South);
			_gameObject.MoveTurtle(Move.Left);
			
			Assert.Equal(2,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_South_Orientation_When_moving_right_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.South);
			_gameObject.MoveTurtle(Move.Right);
			
			Assert.Equal(0,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.Y);
		}

		
		[Fact]
		public void Given_the_Turtle_West_Orientation_When_moving_up_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.West);
			_gameObject.MoveTurtle(Move.Up);
			
			Assert.Equal(0,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_West_Orientation_When_moving_down_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.West);
			_gameObject.MoveTurtle(Move.Down);
			
			Assert.Equal(2,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_West_Orientation_When_moving_left_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.West);
			_gameObject.MoveTurtle(Move.Left);
			
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(2,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_West_Orientation_When_moving_right_the_expected_cell_is_hit()
		{
			_gameObject.TurnTurtle(Orientation.West);
			_gameObject.MoveTurtle(Move.Right);
			
			Assert.Equal(1,_gameObject.Turtle.CurrentPosition.X);
			Assert.Equal(0,_gameObject.Turtle.CurrentPosition.Y);
		}

		[Fact]
		public void Given_the_Turtle_When_Rotating90DegreesRight_the_expected_orientation_is_met()
		{
			var initialOrientation = _gameObject.Turtle.CurrentOrientation;

			_gameObject.RotateTurtle90DegreesRight();
			
			Assert.Equal(Orientation.East,_gameObject.Turtle.CurrentOrientation);

			_gameObject.RotateTurtle90DegreesRight();

			Assert.Equal(Orientation.South,_gameObject.Turtle.CurrentOrientation);

			_gameObject.RotateTurtle90DegreesRight();

			Assert.Equal(Orientation.West,_gameObject.Turtle.CurrentOrientation);

			_gameObject.RotateTurtle90DegreesRight();

			Assert.Equal(Orientation.North,_gameObject.Turtle.CurrentOrientation);

			Assert.Equal(initialOrientation,_gameObject.Turtle.CurrentOrientation);

			
		}
	}
}
