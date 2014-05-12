using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

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
				basePath = "/Users/Thorbjorn/Dropbox/MED6/Results/Experiment1Participants";
				Console.WriteLine("Using default base path " + basePath);
			}

			double clipObservationDurationMillis = 2000.0;
			ExperimentData experiment = new ExperimentData(basePath, clipObservationDurationMillis);
			//experiment.DiscardTrialsWithTooFewReverses(3);

			/*
			List<float> observationDuration = new List<float>(10);
			List<float> observationDurationGrandTotal = new List<float>(25*10);
			
			List<float> gazeEntryDuration = new List<float>(4000);
			List<float> gazeEntryDurationGrandTotal = new List<float>(4000);

			

			foreach(Participant p in experiment.participants)
			{
				foreach(Trial t in p.trials)
				{
					int count = 1;
					for(int k = 0; k < t.observations.Length; k++)
					{
						Observation o = t.observations[k];
						float diffms = 0;

						if(k > 0)
						{
							TimeSpan diff = o.gazeEntries[0].timestamp 
								- t.observations[k - 1].gazeEntries[ t.observations[k - 1].gazeEntries.Length - 1 ].timestamp;
							 diffms = (float)diff.TotalMilliseconds;

						}

						for(int i = 1; i < o.gazeEntries.Length; i++)
						{
							GazeLogEntry old = o.gazeEntries[i - 1];
							GazeLogEntry present = o.gazeEntries[i];

							TimeSpan diff = present.timestamp - old.timestamp;
							
							gazeEntryDuration.Add((float)diff.TotalMilliseconds);
						}
						float obsDur = (float)(o.gazeEntries[o.gazeEntries.Length - 1].timestamp - o.gazeEntries[0].timestamp).TotalMilliseconds;
						Console.WriteLine(p.ID + "." + t.ID + "." + count++ + " obs time: " + obsDur + "\t\tObs gap to last " + diffms);
						observationDuration.Add (obsDur);
						//Console.WriteLine(p.ID + "." + t.ID + "." + count++ + " gaze entry count: " + o.gazeEntries.Length);
					}
				}
				Console.WriteLine(p.ID + ". Avg gaze entry duration: " + Statistics.Mean (gazeEntryDuration.ToArray())
				                  + "\tAvg observation duration: " + Statistics.Mean (observationDuration.ToArray()));		

				observationDurationGrandTotal.AddRange(observationDuration);
				gazeEntryDurationGrandTotal.AddRange(gazeEntryDuration);

				observationDuration.Clear();
				gazeEntryDuration.Clear();
			}

			Console.WriteLine();
			
			Console.WriteLine("Total avg gaze entry duration: " + Statistics.Mean (gazeEntryDurationGrandTotal.ToArray()));		
			Console.WriteLine("Total avg observation duration: " + Statistics.Mean (gazeEntryDurationGrandTotal.ToArray()));		
			
			*/

			//WriteTrialsByFirstResponse (basePath, experiment);
			Console.WriteLine();
			Console.WriteLine(experiment.QuickStats());
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine(experiment.DiscardBadTrials());
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			
			Console.WriteLine(experiment.QuickStats());
		}

		static void WriteTrialsByFirstResponse (string basePath, ExperimentData experiment)
		{
			string header;
			StringBuilder content = new StringBuilder (14000);
			string fileName;

			fileName = "Trials divided by first response - and their means and SDs";
			header = "";

			int counter = 0;
			int counter2 = 0;
			List<Trial> startedPositive = new List<Trial> ();
			List<int> startedPositiveID = new List<int> ();
			List<Trial> startedNegative = new List<Trial> ();
			List<int> startedNegativeID = new List<int> ();
			foreach (Participant p in experiment.participants) {
				bool printedP = false;
				int noOfFuckedTrials = 0;
				foreach (Trial t in p.trials) {
					bool printedT = false;
					Observation o1 = t.observations [0];
					if (o1.response > 0) 
					{
						startedPositive.Add (t);
						startedPositiveID.Add (p.demographics.ID);
					}
					else 
					{
						startedNegative.Add (t);
						startedNegativeID.Add (p.demographics.ID);
					}
				}
			}
			List<float> snThresholds = new List<float> (startedNegative.Count);
			List<float> snGazeMeans = new List<float> (startedNegative.Count);
			foreach (Trial t in startedNegative) {
				snThresholds.Add (t.Threshold);
				snGazeMeans.Add (t.observations [0].GazeDistanceMean);
			}
			List<float> spThresholds = new List<float> (startedPositive.Count);
			List<float> spGazeMeans = new List<float> (startedNegative.Count);
			foreach (Trial t in startedPositive) {
				spThresholds.Add (t.Threshold);
				spGazeMeans.Add (t.observations [0].GazeDistanceMean);
			}
			float snMean = Statistics.Mean (snThresholds.ToArray ());
			float spMean = Statistics.Mean (spThresholds.ToArray ());
			float snSD = Statistics.StandardDeviation (snThresholds.ToArray ());
			float spSD = Statistics.StandardDeviation (spThresholds.ToArray ());
			float sngMean = Statistics.Mean (snGazeMeans.ToArray ());
			float spgMean = Statistics.Mean (spGazeMeans.ToArray ());
			float sngSD = Statistics.StandardDeviation (spGazeMeans.ToArray ());
			float spgSD = Statistics.StandardDeviation (snGazeMeans.ToArray ());

			content.AppendLine ("Started negative:");
			content.AppendLine ("Mean resulting threshold: " + snMean);
			content.AppendLine ("SD resulting threshold: " + snSD);
			content.AppendLine ("Mean gaze deviation: " + sngMean);
			content.AppendLine ("SD gaze deviation: " + sngSD);

			for (int i = 0; i < startedNegative.Count; i++) {
				content.AppendLine ("\tParticipant " + startedNegativeID [i].ToString ("00") + " " + startedNegative [i] + "\tavg gaze dist: " + startedNegative [i].observations [0].GazeDistanceMean);
			}

			content.AppendLine ();
			content.AppendLine ();
			content.AppendLine ("Started positive:");
			content.AppendLine ("Mean resulting threshold: " + spMean);
			content.AppendLine ("SD resulting threshold: " + spSD);
			content.AppendLine ("Mean gaze deviation: " + spgMean);
			content.AppendLine ("SD gaze deviation: " + spgSD);

			for (int i = 0; i < startedPositive.Count; i++) {
				content.AppendLine ("\tParticipant " + startedPositiveID [i].ToString ("00") + " " + startedPositive [i] + "\tavg gaze dist: " + startedPositive [i].observations [0].GazeDistanceMean);
			}
			CSVWriter.Write (Path.Combine (basePath, Path.Combine ("OUTPUT", fileName + ".txt")), header, content.ToString ());
		}

		private static bool EqualPosition(GazeLogEntry g1, GazeLogEntry g2, float maxDiff)
		{
			if( Math.Abs(g1.x - g2.x) < maxDiff && Math.Abs(g1.y - g2.y) < maxDiff)
			{
				return true;
			}

			return false;
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
