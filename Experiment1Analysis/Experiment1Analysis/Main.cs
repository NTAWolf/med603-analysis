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
				basePath = "/Users/WorkMachine/Dropbox/MED6 - Dropbox/MED6/Results/Experiment1Participants";
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

			Console.WriteLine(Statistics.GetThresholdValueFromSigmoid(0.5f));
			//WriteLogisticRegressionDataToCSV(basePath,experiment);
		}

		static void WriteLogisticRegressionDataToCSV(string path, ExperimentData experiment)
		{
			List<float> respons = new List<float>();
			List<float> stimuli = new List<float>();
			//Get data for logistic regression
			foreach(Participant p in experiment.participants)
			{
				foreach(Trial t in p.trials)
				{
					foreach(Observation o in t.observations)
					{
						if(o.response == -1)
							respons.Add(0);
						else
							respons.Add(o.response);
						
						stimuli.Add(o.stimulus);
					}
				}
			}
			
			CSVWriter.Write(Path.Combine(path,"output/logistic_regression_data"),"respons",respons.ToArray(),"stimuli",stimuli.ToArray());
		}
	}
}
