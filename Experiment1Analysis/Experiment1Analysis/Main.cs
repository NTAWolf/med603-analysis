using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

namespace Experiment1Analysis
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string basePath;
			if(args.Length > 0)
			{
				basePath = args[0];
				Console.WriteLine("Using provided base path " + basePath);
			}
			else
			{
				basePath = "/Users/NTAWolf/Dropbox/MED6/Results/Experiment1Participants";
				Console.WriteLine("Using default base path " + basePath);
			}

			ExperimentData experiment = new ExperimentData(basePath);
			//experiment.DiscardTrialsWithTooFewReverses(3);

			foreach(Participant p in experiment.participants)
			{
				foreach(Trial t in p.trials)
				{
					Console.WriteLine(p + " " + t);
				}
			}

/*			string[] gles = {
				"09-38-12-8530135,1204.288,388.4016,1203.995,437.6996",
				"09-39-28-0043119,1253.259,505.9728,1208.927,518.5724",
				"09-39-28-5213415,1252.661,499.824,1208.927,518.5724"
			};
*/

			/*Trial tt;
			string obs = "Stimulus, Value\n70,1\n60,-1\n50,1\n40,-1\n30,1\n20,-1\n10,1\n6,-1\n0,1\n2000,-1";
			string gz = "09-30-12-8530135,1204.288,388.4016,1203.995,437.6996\n09-31-13-6510591,1238.682,392.4838,1203.995,437.6996\n09-32-13-6690602,1238.46,393.2184,1203.995,437.6996\n09-33-18-4303325,1047.287,624.6796,1203.995,437.6996\n09-34-18-4463334,1048.415,632.8615,1203.995,437.6996\n09-35-18-4623343,1055.353,638.443,1203.995,437.6996\n09-36-20-9104743,928.2126,666.749,1203.995,437.6996\n09-37-20-9274753,924.9333,672.6703,1203.995,437.6996\n09-38-22-4939967,912.7782,352.4776,729.4939,193.1669\n09-39-26-4932255,1239.695,489.0759,1208.927,518.5724";

			using(StreamReader observation = new StreamReader(GenerateStreamFromString(obs)))
			{
				using(StreamReader gazelog = new StreamReader(GenerateStreamFromString(gz)))
				{
					tt = new Trial(observation, gazelog, 1);
				}
			}


			Console.WriteLine(tt.ID);
			Console.WriteLine(tt.NumberOfReverses);
			Console.WriteLine(tt.Threshold);
*/
			/*
			string[] files = Directory.GetFiles("/Users/Thorbjorn/Dropbox/MED6/Results/Pixelation test - TwentyParticipants/Experiments/E1 Best PEST pixelation/Participants/0001");
			
			List<string> observationFiles = new List<string>(4);
			List<string> gazeLogFiles = new List<string>(4);
			
			foreach(string s in files)
			{
				if(s.Contains("gazelog"))
				{
					gazeLogFiles.Add(s);
				}
				else
				{
					observationFiles.Add(s);
				}
			}
			
			observationFiles.Sort();
			gazeLogFiles.Sort();

			foreach(string s in observationFiles)
			{
				Console.WriteLine("Obs: " + s);
			}
			foreach(string s in gazeLogFiles)
			{
				Console.WriteLine("Gaz: " + s);
			}
*/
			//Demographics demoTest = new Demographics(new StreamReader("/Users/Thorbjorn/Dropbox/MED6/Results/Pixelation test - TwentyParticipants/Experiments/E1 Best PEST pixelation/Participants/demographic.csv"));
			//Console.Write(demoTest.GetDemographicFromID(25).GameUse);

			/*using(StreamReader observation = new StreamReader("/Users/Thorbjorn/Dropbox/MED6/Results/E1 Best PEST pixelation/Participants/0002/BestPestTrial at 2014-05-08_14-38-24-7246053.csv"))
			{
				using(StreamReader gazelog = new StreamReader("/Users/Thorbjorn/Dropbox/MED6/Results/E1 Best PEST pixelation/Participants/0002/BestPestTrial at 2014-05-08_14-33-40-4443454 gazelog.csv"))
				{
					tt = new Trial(observation, gazelog);
				}
			}*/


			/*float[] valuesToBeSmoothed = {0, 0, 0, 0, 0, 1, 1, 1000, 1, 1, 0, 0, 0, 0, 0};
			float[] smoothed = Statistics.SmoothClip(valuesToBeSmoothed, 2);

			foreach(float v in smoothed)
			{
				Console.WriteLine(v);
			}*/


		}

		public static Stream GenerateStreamFromString(string s)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
	}
}
