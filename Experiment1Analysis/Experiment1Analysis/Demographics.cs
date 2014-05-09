using System;

namespace Experiment1Analysis
{
	/// <summary>
	/// This class handles grabbing demographics data for Experiment 1,
	/// either from interwebs or from csv, and making them available by
	/// request to other parts of the program.
	/// </summary>
	public class Demographics
	{
		public Demographics ()
		{
		}

	}

	/// <summary>
	/// This struct contains demographic data for a single participant
	/// </summary>
	public struct DemographicData
	{
		public enum GameUseType {Never, Some, Lots}
		public int ID;
		public string Gender;
		public int Age;
		public GameUseType GameUse;

		public DemographicData(int ID, string Gender, int Age, GameUseType GameUse)
		{
			this.ID = ID;
			this.Gender = Gender;
			this.Age = Age;
			this.GameUse = GameUse;
		}
	}
}

