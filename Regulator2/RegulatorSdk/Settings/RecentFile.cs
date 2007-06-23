using System;

namespace Regulator.SDK.ApplicationSettings
{

	

	/// <summary>
	/// Summary description for RecentFile.
	/// </summary>
	public class RecentFile
	{
		private string m_strName;

		public RecentFile()
		{
			
		}
		public RecentFile(string filename)
		{
			Name = filename;
		}

		public string Name
		{
			get
			{
				return m_strName;
			}
			set
			{
				m_strName = value;
			}
		}

	}

	/// <summary>
	///     A collection that stores 'RecentFile' objects.
	/// </summary>
	[Serializable()]
	public class RecentFileCollection : System.Collections.CollectionBase 
	{
    
		/// <summary>
		///     Initializes a new instance of 'RecentFileCollection'.
		/// </summary>
		public RecentFileCollection() 
		{
		}
    
		/// <summary>
		///     Initializes a new instance of 'RecentFileCollection' based on an already existing instance.
		/// </summary>
		/// <param name='recValue'>
		///     A 'RecentFileCollection' from which the contents is copied
		/// </param>
		public RecentFileCollection(RecentFileCollection recValue) 
		{
			this.AddRange(recValue);
		}
    
		/// <summary>
		///     Initializes a new instance of 'RecentFileCollection' with an array of 'RecentFile' objects.
		/// </summary>
		/// <param name='recValue'>
		///     An array of 'RecentFile' objects with which to initialize the collection
		/// </param>
		public RecentFileCollection(RecentFile[] recValue) 
		{
			this.AddRange(recValue);
		}
    
		/// <summary>
		///     Represents the 'RecentFile' item at the specified index position.
		/// </summary>
		/// <param name='intIndex'>
		///     The zero-based index of the entry to locate in the collection.
		/// </param>
		/// <value>
		///     The entry at the specified index of the collection.
		/// </value>
		public RecentFile this[int intIndex] 
		{
			get 
			{
				return ((RecentFile)(List[intIndex]));
			}
			set 
			{
				List[intIndex] = value;
			}
		}
    
		/// <summary>
		///     Adds a 'RecentFile' item with the specified value to the 'RecentFileCollection'
		/// </summary>
		/// <param name='recValue'>
		///     The 'RecentFile' to add.
		/// </param>
		/// <returns>
		///     The index at which the new element was inserted.
		/// </returns>
		public int Add(RecentFile recValue) 
		{
			return List.Add(recValue);
		}
    
		/// <summary>
		///     Copies the elements of an array at the end of this instance of 'RecentFileCollection'.
		/// </summary>
		/// <param name='recValue'>
		///     An array of 'RecentFile' objects to add to the collection.
		/// </param>
		public void AddRange(RecentFile[] recValue) 
		{
			for (int intCounter = 0; (intCounter < recValue.Length); intCounter = (intCounter + 1)) 
			{
				this.Add(recValue[intCounter]);
			}
		}
    
		/// <summary>
		///     Adds the contents of another 'RecentFileCollection' at the end of this instance.
		/// </summary>
		/// <param name='recValue'>
		///     A 'RecentFileCollection' containing the objects to add to the collection.
		/// </param>
		public void AddRange(RecentFileCollection recValue) 
		{
			for (int intCounter = 0; (intCounter < recValue.Count); intCounter = (intCounter + 1)) 
			{
				this.Add(recValue[intCounter]);
			}
		}
    
		/// <summary>
		///     Gets a value indicating whether the 'RecentFileCollection' contains the specified value.
		/// </summary>
		/// <param name='recValue'>
		///     The item to locate.
		/// </param>
		/// <returns>
		///     True if the item exists in the collection; false otherwise.
		/// </returns>
		public bool Contains(RecentFile recValue) 
		{
			return List.Contains(recValue);
		}
    
		/// <summary>
		///     Copies the 'RecentFileCollection' values to a one-dimensional System.Array
		///     instance starting at the specified array index.
		/// </summary>
		/// <param name='recArray'>
		///     The one-dimensional System.Array that represents the copy destination.
		/// </param>
		/// <param name='intIndex'>
		///     The index in the array where copying begins.
		/// </param>
		public void CopyTo(RecentFile[] recArray, int intIndex) 
		{
			List.CopyTo(recArray, intIndex);
		}
    
		/// <summary>
		///     Returns the index of a 'RecentFile' object in the collection.
		/// </summary>
		/// <param name='recValue'>
		///     The 'RecentFile' object whose index will be retrieved.
		/// </param>
		/// <returns>
		///     If found, the index of the value; otherwise, -1.
		/// </returns>
		public int IndexOf(RecentFile recValue) 
		{
			return List.IndexOf(recValue);
		}
    
		/// <summary>
		///     Inserts an existing 'RecentFile' into the collection at the specified index.
		/// </summary>
		/// <param name='intIndex'>
		///     The zero-based index where the new item should be inserted.
		/// </param>
		/// <param name='recValue'>
		///     The item to insert.
		/// </param>
		public void Insert(int intIndex, RecentFile recValue) 
		{
			List.Insert(intIndex, recValue);
		}
    
		/// <summary>
		///     Returns an enumerator that can be used to iterate through
		///     the 'RecentFileCollection'.
		/// </summary>
		public new RecentFileEnumerator GetEnumerator() 
		{
			return new RecentFileEnumerator(this);
		}
    
		/// <summary>
		///     Removes a specific item from the 'RecentFileCollection'.
		/// </summary>
		/// <param name='recValue'>
		///     The item to remove from the 'RecentFileCollection'.
		/// </param>
		public void Remove(RecentFile recValue) 
		{
			List.Remove(recValue);
		}
    
		/// <summary>
		///     A strongly typed enumerator for 'RecentFileCollection'
		/// </summary>
		public class RecentFileEnumerator : object, System.Collections.IEnumerator 
		{
        
			private System.Collections.IEnumerator iEnBase;
        
			private System.Collections.IEnumerable iEnLocal;
        
			/// <summary>
			///     Enumerator constructor
			/// </summary>
			public RecentFileEnumerator(RecentFileCollection recMappings) 
			{
				this.iEnLocal = ((System.Collections.IEnumerable)(recMappings));
				this.iEnBase = iEnLocal.GetEnumerator();
			}
        
			/// <summary>
			///     Gets the current element from the collection (strongly typed)
			/// </summary>
			public RecentFile Current 
			{
				get 
				{
					return ((RecentFile)(iEnBase.Current));
				}
			}
        
			/// <summary>
			///     Gets the current element from the collection
			/// </summary>
			object System.Collections.IEnumerator.Current 
			{
				get 
				{
					return iEnBase.Current;
				}
			}
        
			/// <summary>
			///     Advances the enumerator to the next element of the collection
			/// </summary>
			public bool MoveNext() 
			{
				return iEnBase.MoveNext();
			}
        
			/// <summary>
			///     Advances the enumerator to the next element of the collection
			/// </summary>
			bool System.Collections.IEnumerator.MoveNext() 
			{
				return iEnBase.MoveNext();
			}
        
			/// <summary>
			///     Sets the enumerator to the first element in the collection
			/// </summary>
			public void Reset() 
			{
				iEnBase.Reset();
			}
        
			/// <summary>
			///     Sets the enumerator to the first element in the collection
			/// </summary>
			void System.Collections.IEnumerator.Reset() 
			{
				iEnBase.Reset();
			}
		}
	}

}
