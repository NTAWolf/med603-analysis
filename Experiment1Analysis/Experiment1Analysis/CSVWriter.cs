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
using System.Text;


namespace Experiment1Analysis
{
	public static class CSVWriter
	{

		public static void Write(string path, string[] header, string[][] elements, char separator = ',')
		{
			string newHeader = InsertSeparator(header, separator);
			string block = ElementsToBlock(elements, separator);

			Write(path, newHeader, block);
		}

		public static string ElementsToBlock(string[][] elements, char separator = ',')
		{
			return LinesToBlock(ElementsToLines(elements, separator));
		}

		public static string LinesToBlock(string[] lines)
		{
			StringBuilder output = new StringBuilder();

			for(int i = 0; i < lines.Length - 1; i++)
			{
				output.Append(lines[i] + "\r\n");
			}
			output.Append(lines[lines.Length - 1]);

			return output.ToString();
		}

		public static string[] ElementsToLines(string[][] elements, char separator = ',')
		{
			string[] lines = new string[elements.Length];

			for(int i = 0; i < elements.Length; i++)
			{
				lines[i] = InsertSeparator(elements[i], separator);
			}

			return lines;
		}

		public static string InsertSeparator(string[] elements, char separator=',')
		{
			StringBuilder output = new StringBuilder();
			for(int i = 0; i < elements.Length - 1; i++)
			{
				output.Append(elements[i] + separator);
			}
			output.Append(elements[elements.Length - 1]);

			return output.ToString();
		}

		public static void Write(string path, string header, string contents)
		{
			Write(path, header + "\r\n" + contents);
		}

		public static void Write(string path, string header, float[] contents)
		{
			StringBuilder scontents = new StringBuilder(contents.Length * 3);

			foreach(float f in contents)
			{
				scontents.AppendLine(f.ToString());
			}

			Write(path, header, scontents.ToString());
		}

		public static void Write(string path, string contents)
		{
			System.IO.File.WriteAllText(path, contents);
		}
	}
}

