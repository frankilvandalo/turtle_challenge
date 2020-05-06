using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TurtleChallenge.Payload
{
	public class GameGrid
	{
		[JsonProperty("rows")]
		public int Rows { get; set; }

		[JsonProperty("columns")]
		public int Columns { get; set; }

		[JsonProperty("turtlePosition")]
		public TurtlePosition TurtlePosition { get; set; }

		[JsonProperty("exit")]
		public Exit Exit { get; set; }

		[JsonProperty("mines")]
		public Mine[] Mines { get; set; }
	}

	public class Exit
	{
		[JsonProperty("position")]
		public Position Position { get; set; }
	}

	public class TurtlePosition
	{
		[JsonProperty("position")]
		public Position Position { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public Orientation Orientation { get; set; }
	}

	public class Mine
	{
		[JsonProperty("position")] 
		public Position Position { get; set; }
	}


}
