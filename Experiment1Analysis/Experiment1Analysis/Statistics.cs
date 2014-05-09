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
	public static class Statistics
	{
		/// <summary>
		/// Smooth the specified values by taking the median value within the surrounding windowSize elements.
		/// Also clips off non-smoothed edges, i.e. the first and last windowSize elements.
		/// </summary>
		/// <param name="values">Values.</param>
		/// <param name="windowSize">Window size.</param>
		public static float[] SmoothClip(float[] values, int windowSize)
		{
			float[] smoothed = new float[values.Length - 2 * windowSize];
			float[] window = new float[2 * windowSize + 1];

			for(int i = 0; i < smoothed.Length; i++)
			{
				for(int o = 0; o < window.Length; o++)
				{
					window[o] = values[i + o];
				}

				/*double sum = 0;
				for(int o = -windowSize; o <= windowSize; o++)
				{
					sum += values[i + windowSize + o];
				}
				smoothed[i] = (float)(sum / window.Length);*/
				smoothed[i] = Median(window);
			}

			return smoothed;
		}

		public static float Median(float[] values)
		{
			Array.Sort(values);

			if(values.Length % 2 == 0)
			{
				return (float)(0.5 * (values[values.Length/2] + values[values.Length/2 - 1])); // Use mean of middle values
			}
			else
			{
				return values[values.Length/2]; // Use middle value
			}
		}

		public static float Mean(float[] values)
		{
			double sum = 0.0;
			foreach(float v in values)
			{
				sum += v;
			}
				
			sum /= values.Length;

			return (float)sum;
		}

		public static float StandardDeviation(float[] values)
		{
			return (float)Math.Sqrt(Variance(values));
		}

		public static float Variance(float[] values)
		{
			double mean = Mean (values);

			double sumOfSquaredDifferences = 0.0;

			foreach(float v in values)
			{
				double diff = v - mean;
				sumOfSquaredDifferences += diff * diff;
			}

			return (float)sumOfSquaredDifferences;
		}
	}
}

