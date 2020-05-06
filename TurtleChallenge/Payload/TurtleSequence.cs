using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TurtleChallenge.Payload
{
	public class TurtleSequence
	{
		[JsonProperty("sequence")]
		public Sequence[] Sequence { get; set; }
	}

	public class Sequence
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("rotate")]
		public bool Rotate { get; set; }

		[JsonProperty("moveForward")]
		public bool MoveForward { get; set; }
	}
}
