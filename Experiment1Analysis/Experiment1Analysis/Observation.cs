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
using System.Collections.Generic;
using System.IO;

namespace Experiment1Analysis
{
	public class Observation
	{
		public float stimulus;
		public short response;
		public GazeLogEntry[] gazeEntries;

		public Observation(string observationEntry, GazeLogEntry[] gazeLogEntries)
			: this(observationEntry.Split (new char[]{','}), gazeLogEntries)
		{

		}

		public Observation(string[] observationEntry, GazeLogEntry[] gazeLogEntries)
		{
			this.stimulus = Single.Parse(observationEntry[0]);
			this.response = Int16.Parse(observationEntry[1]);

			/*List<GazeLogEntry> gazeLogs = new List<GazeLogEntry>(2500);
			foreach(string s in gazeLogEntries)
			{
				gazeLogs.Add(new GazeLogEntry(s));
             }
             
             gazeEntries = gazeLogs.ToArray();*/
			this.gazeEntries = gazeLogEntries;
		}
	}
}
