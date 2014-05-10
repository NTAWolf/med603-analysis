using System;
using System.IO;
using System.Collections.Generic;

namespace Experiment1Analysis
{
	public enum GameUseType {Never, Some, Lots}

	/// <summary>
	/// This class handles grabbing demographics data for Experiment 1,
	/// either from interwebs or from csv, and making them available by
	/// request to other parts of the program.
	/// </summary>
	public class Demographics
	{
		public List<DemographicData> demoData = new List<DemographicData>();

		public Demographics(StreamReader demographicsStream)
		{
			List<string> lines = new List<string>();

			while(demographicsStream.EndOfStream != true)
			{
				lines.Add(demographicsStream.ReadLine());
			}

			foreach(string s in lines)
			{
				string[] data = s.Split(new char[]{','});

				int tmpID = Convert.ToInt16(data[1]);
				string tmpGender = data[2];
				int tmpAge = Convert.ToInt16(data[3]);
				string gameUseString = data[4];
				GameUseType tmpGameUse;

				switch(gameUseString)
				{
					case "I have never played video games.":
						tmpGameUse = GameUseType.Never;
						break;
					case "I have spent some time playing video games.":
						tmpGameUse = GameUseType.Some;
						break;
					case "I have spent a lot of time playing video games.":
						tmpGameUse = GameUseType.Lots;
						break;
					default:
						tmpGameUse = GameUseType.Never;
						break;
				}
	
				demoData.Add(new DemographicData(tmpID,tmpGender,tmpAge,tmpGameUse));
			}
		}

		public DemographicData GetDemographicFromID(int IdToFind)
		{
			foreach(DemographicData d in demoData)
			{
				if(d.ID == IdToFind)
				{
					return d;
				}
			}
			throw new InvalidOperationException("ID number: " + IdToFind + " does not exist");
		}
	}

	/// <summary>
	/// This struct contains demographic data for a single participant
	/// </summary>
	public struct DemographicData
	{
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

		public override string ToString ()
		{
			return ("DemographicData " + ID + " " + Gender + " " + Age + " " + GameUse);
		}
	}


}

