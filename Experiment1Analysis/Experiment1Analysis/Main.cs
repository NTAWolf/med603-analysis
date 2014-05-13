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
				basePath = "/Users/Thorbjorn/Downloads/testdata";
				//basePath = "/Users/WorkMachine/Dropbox/MED6 - Dropbox/MED6/Results/Experiment1Participants";
				Console.WriteLine("Using default base path " + basePath);
			}

			outputDirectory = Path.Combine(basePath, "OUTPUT");

			double clipObservationDurationMillis = 2000.0;
			ExperimentData experiment = new ExperimentData(basePath, clipObservationDurationMillis);

			Console.WriteLine();
			Console.WriteLine(experiment.QuickStats());
			Console.WriteLine();
			Console.WriteLine(experiment.DiscardBadTrials());
			Console.WriteLine();
			Console.WriteLine(experiment.QuickStats());

			//GetLogisticRegressionCSV(basePath,experiment);
		}

		static void GetLogisticRegressionCSV(string path, ExperimentData experiment)
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
			
			CSVWriter.Write(Path.Combine(path,"output/logRegress.csv"),"respons",respons.ToArray(),"stimuli",stimuli.ToArray());
		}
	}
}
