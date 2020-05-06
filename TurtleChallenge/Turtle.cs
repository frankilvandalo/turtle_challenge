using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TurtleChallenge
{
	public class Turtle 
	{
		public Turtle(int x, int y, Orientation currentOrientation)
		{
			CurrentPosition = new Position(x,y);
			CurrentOrientation = currentOrientation;
			Moves = new Stack<Tuple<Orientation, Move, Position>>();
		}

		public Position CurrentPosition { get; set; }

		public Orientation CurrentOrientation { get; set; }

		public Stack<Tuple<Orientation, Move, Position>> Moves { get; set; }
	}

	public enum Orientation		
	{
		North = 1,
		West = 2,
		East = 3,
		South = 4
	}

	public enum Move
	{
		Up = 1,
		Down = 2,
		Left = 3,
		Right = 4

	}
}