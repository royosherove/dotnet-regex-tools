using System;
using System.Data;


namespace Regulator.GUI.Util
{
	using System;
	using System.Runtime.InteropServices;
	using System.ComponentModel;
	using System.Threading;

	internal class TimeCounter
	{

		
//		public void GenerateDataSet()
//		{
//			DataSet ds = new DataSet("QuickMenu");
//			ds.ReadXml(@"..\..\QuickMenu.config");
//			ds.WriteXmlSchema(@"..\..\QuickMenu.xsd");
//		}
		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceCounter(
			out long lpPerformanceCount);

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(
			out long lpFrequency);

		private long startTime, stopTime;
		private long freq;


	  public TimeCounter()
		{
			startTime = 0;
			stopTime  = 0;

			if (QueryPerformanceFrequency(out freq) == false)
			{
				// high-performance counter not supported
				throw new Win32Exception();
			}
		}

		// Start the timer
		public void Start()
		{
			// lets do the waiting threads there work
			Thread.Sleep(0);

			QueryPerformanceCounter(out startTime);
			}

		// Stop the timer
		public double Stop()
		{
			QueryPerformanceCounter(out stopTime);
			return DurationSeconds;
		}

		// Returns the duration of the timer (in seconds)
		public double DurationSeconds
		{
			get
			{
				return (double)(stopTime - startTime) / (double) freq;
			}
		}
	}
}

