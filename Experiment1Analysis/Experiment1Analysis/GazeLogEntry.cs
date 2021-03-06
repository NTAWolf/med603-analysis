// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
namespace Experiment1Analysis
{
	public struct GazeLogEntry
	{
		public DateTime timestamp;
		public float x;
		public float y;
		public float ref_x;
		public float ref_y;
		public float distance;

		public GazeLogEntry(string entry)
		: this(entry.Split(new char[] { ',' }))
		{
			// Deliberately empty
		}

		private GazeLogEntry(string[] contents)
		{
			timestamp = DateTime.ParseExact(contents[0], "HH-mm-ss-fffffff", System.Globalization.CultureInfo.InvariantCulture);
			x = Single.Parse(contents[1]);
			y = Single.Parse(contents[2]);
			ref_x = Single.Parse(contents[3]);
			ref_y = Single.Parse(contents[4]);
			distance = Statistics.Distance(x, y, ref_x, ref_y);
		}

		public override string ToString ()
		{
			return string.Format ("GazeLogEntry at {0},{1} should be {2},{3}. Distance {4}", x, y, ref_x, ref_y, distance);
		}

		public bool IsOutsideBounds(int maxX, int maxY)
		{
			if(x < 0 || y < 0)
			{
				return true;
			}

			if(x > maxX || y > maxY)
			{
				return true;
			}

			return false;
		}

		public bool IsWithinDistance(GazeLogEntry other, float distance)
		{
			if( Math.Abs(this.distance - other.distance) < distance)
			{
				return true;
			}

			return false;
		}
	}
}

