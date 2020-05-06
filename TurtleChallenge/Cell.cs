using System;

namespace TurtleChallenge
{
	public class Cell
	{

		public Cell(int x, int y)
		{
			Position = new Position(x, y);
		}

		public Position Position { get; }
		public bool IsMined { get; set; }
		public bool IsExit { get; set; }
	}

}
