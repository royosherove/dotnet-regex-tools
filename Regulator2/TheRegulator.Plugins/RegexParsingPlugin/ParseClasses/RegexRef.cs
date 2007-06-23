/*
 * Original code written by Eric Gunnerson
 * for his Regex Workbench tool:
 * http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=43D952B8-AFC6-491B-8A5F-01EBD32F2A6C
 * */
using System;

namespace RegexTest
{
	/// <summary>
	/// Summary description for RegexRef.
	/// </summary>
	public class RegexRef: IComparable
	{
		int start;
		int end;
		string stringValue;

		public RegexRef(RegexItem regexItem, int start, int end)
		{
			stringValue = regexItem.ToString(0);
			this.start = start;
			this.end = end;
		}

		public int CompareTo(object o2)
		{
			RegexRef ref2 = (RegexRef) o2;
			if (this.Length < ref2.Length)
				return -1;
			else if (this.Length > ref2.Length)
				return 1;
			else
				return 0;
		}

		public bool InRange(int location)
		{
			if ((location >= start) && (location <= end))
				return true;
			else
				return false;

		}

		public string StringValue
		{
			get
			{
				return stringValue;
			}
			set
			{
				stringValue = value;
			}
		}

		public int Start
		{
			get
			{
				return start;
			}
		}

		public int End
		{
			get
			{
				return end;
			}
		}

		public int Length
		{
			get
			{
				return end - start + 1;
			}
			set
			{
				end = start + value - 1;
			}
		}
	}
}
