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
			string outputDirectory;
			if(args.Length > 0)
			{
				basePath = args[0];
				Console.WriteLine("Using provided base path " + basePath);
			}
			else
			{
				basePath = "/Users/Thorbjorn/Dropbox/MED6/Results/Experiment1Participants";
				//basePath = "/Users/WorkMachine/Dropbox/MED6 - Dropbox/MED6/Results/Experiment1Participants";
				Console.WriteLine("Using default base path " + basePath);
			}

			outputDirectory = Path.Combine(basePath, "OUTPUT");

			double clipObservationDurationMillis = 2000.0;
			ExperimentData experiment = new ExperimentData(basePath, clipObservationDurationMillis);
			
			/*
			CSVWriter.Write(Path.Combine(outputDirectory, "26-1"), 
			                "GazeDistance", experiment.GetParticipant(26).GetTrial(1).ConcatenateGazeDistances().ToArray());
			CSVWriter.Write(Path.Combine(outputDirectory, "26-2"), 
			                "GazeDistance", experiment.GetParticipant(26).GetTrial(2).ConcatenateGazeDistances().ToArray());
			CSVWriter.Write(Path.Combine(outputDirectory, "26-3"), 
			                "GazeDistance", experiment.GetParticipant(26).GetTrial(3).ConcatenateGazeDistances().ToArray());
			CSVWriter.Write(Path.Combine(outputDirectory, "26-4"), 
			                "GazeDistance", experiment.GetParticipant(26).GetTrial(4).ConcatenateGazeDistances().ToArray());
*/
			//experiment.PrintParticipantStandardDeviations();
			Console.WriteLine();

			Console.WriteLine(experiment.QuickStats());
			
			float[] trialThresholds;
			int[] participantID, trialID;
			/*experiment.GetTrialThresholds(out trialThresholds, out participantID, out trialID);
			CSVWriter.Write(Path.Combine(outputDirectory, "TrialThresholdsNoisy"), 
			                "Thresholds", trialThresholds, 
			                "Participant", participantID, 
			                "Trial", trialID);
*/
			//CSVWriter.Write(Path.Combine(outputDirectory, "TrialThresholdsNoisy"), "Threshold", experiment.GetTrialThresholds()) ;
			Console.WriteLine(experiment.DiscardBadTrials());

/*			experiment.GetTrialThresholds(out trialThresholds, out participantID, out trialID);
			CSVWriter.Write(Path.Combine(outputDirectory, "TrialThresholdsDenoised"), 
			                "Thresholds", trialThresholds, 
			                "Participant", participantID, 
			                "Trial", trialID);
*/			
			Console.WriteLine(experiment.QuickStats());
		}
	}
}
