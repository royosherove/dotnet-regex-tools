using System;
using Regulator.SDK.Plugins;
using System.Collections;

namespace Regulator.SDK.Plugins
{


	/// <summary>
	///     A collection that stores 'IPlugin' objects.
	/// </summary>
	[Serializable()]
	public class PluginCollection : System.Collections.CollectionBase 
	{
    
		/// <summary>
		///     Initializes a new instance of 'PluginCollection'.
		/// </summary>
		public PluginCollection() 
		{
		}
    
		/// <summary>
		///     Initializes a new instance of 'PluginCollection' based on an already existing instance.
		/// </summary>
		/// <param name='pluValue'>
		///     A 'PluginCollection' from which the contents is copied
		/// </param>
		public PluginCollection(PluginCollection pluValue) 
		{
			this.AddRange(pluValue);
		}
    
		/// <summary>
		///     Initializes a new instance of 'PluginCollection' with an array of 'IPlugin' objects.
		/// </summary>
		/// <param name='iPlValue'>
		///     An array of 'IPlugin' objects with which to initialize the collection
		/// </param>
		public PluginCollection(IPlugin[] iPlValue) 
		{
			this.AddRange(iPlValue);
		}
    
		/// <summary>
		///     Represents the 'IPlugin' item at the specified index position.
		/// </summary>
		/// <param name='intIndex'>
		///     The zero-based index of the entry to locate in the collection.
		/// </param>
		/// <value>
		///     The entry at the specified index of the collection.
		/// </value>
		public IPlugin this[int intIndex] 
		{
			get 
			{
				return ((IPlugin)(List[intIndex]));
			}
			set 
			{
				List[intIndex] = value;
			}
		}
    
		/// <summary>
		///     Adds a 'IPlugin' item with the specified value to the 'PluginCollection'
		/// </summary>
		/// <param name='iPlValue'>
		///     The 'IPlugin' to add.
		/// </param>
		/// <returns>
		///     The index at which the new element was inserted.
		/// </returns>
		public int Add(IPlugin iPlValue) 
		{
			return List.Add(iPlValue);
		}
    
		/// <summary>
		///     Copies the elements of an array at the end of this instance of 'PluginCollection'.
		/// </summary>
		/// <param name='iPlValue'>
		///     An array of 'IPlugin' objects to add to the collection.
		/// </param>
		public void AddRange(IPlugin[] iPlValue) 
		{
			for (int intCounter = 0; (intCounter < iPlValue.Length); intCounter = (intCounter + 1)) 
			{
				this.Add(iPlValue[intCounter]);
			}
		}
    
		/// <summary>
		///     Adds the contents of another 'PluginCollection' at the end of this instance.
		/// </summary>
		/// <param name='pluValue'>
		///     A 'PluginCollection' containing the objects to add to the collection.
		/// </param>
		public void AddRange(PluginCollection pluValue) 
		{
			for (int intCounter = 0; (intCounter < pluValue.Count); intCounter = (intCounter + 1)) 
			{
				this.Add(pluValue[intCounter]);
			}
		}
    
		/// <summary>
		///     Gets a value indicating whether the 'PluginCollection' contains the specified value.
		/// </summary>
		/// <param name='iPlValue'>
		///     The item to locate.
		/// </param>
		/// <returns>
		///     True if the item exists in the collection; false otherwise.
		/// </returns>
		public bool Contains(IPlugin iPlValue) 
		{
			return List.Contains(iPlValue);
		}
    
		/// <summary>
		///     Copies the 'PluginCollection' values to a one-dimensional System.Array
		///     instance starting at the specified array index.
		/// </summary>
		/// <param name='iPlArray'>
		///     The one-dimensional System.Array that represents the copy destination.
		/// </param>
		/// <param name='intIndex'>
		///     The index in the array where copying begins.
		/// </param>
		public void CopyTo(IPlugin[] iPlArray, int intIndex) 
		{
			List.CopyTo(iPlArray, intIndex);
		}
    
		/// <summary>
		///     Returns the index of a 'IPlugin' object in the collection.
		/// </summary>
		/// <param name='iPlValue'>
		///     The 'IPlugin' object whose index will be retrieved.
		/// </param>
		/// <returns>
		///     If found, the index of the value; otherwise, -1.
		/// </returns>
		public int IndexOf(IPlugin iPlValue) 
		{
			return List.IndexOf(iPlValue);
		}
    
		/// <summary>
		///     Inserts an existing 'IPlugin' into the collection at the specified index.
		/// </summary>
		/// <param name='intIndex'>
		///     The zero-based index where the new item should be inserted.
		/// </param>
		/// <param name='iPlValue'>
		///     The item to insert.
		/// </param>
		public void Insert(int intIndex, IPlugin iPlValue) 
		{
			List.Insert(intIndex, iPlValue);
		}
    
		/// <summary>
		///     Returns an enumerator that can be used to iterate through
		///     the 'PluginCollection'.
		/// </summary>
		public new IPluginEnumerator GetEnumerator() 
		{
			return new IPluginEnumerator(this);
		}
    
		/// <summary>
		///     Removes a specific item from the 'PluginCollection'.
		/// </summary>
		/// <param name='iPlValue'>
		///     The item to remove from the 'PluginCollection'.
		/// </param>
		public void Remove(IPlugin iPlValue) 
		{
			List.Remove(iPlValue);
		}
    
		/// <summary>
		///     TODO: Describe what custom processing this method does
		///     before insering a new item in the collection
		/// </summary>
		protected override void OnInsert(int intIndex, object objValue) 
		{
			//  TODO: Add code here to handle inserting a new item into the collection
		}
    
		/// <summary>
		///     TODO: Describe what custom processing this method does
		///     before clearing the collection's contents
		/// </summary>
		protected override void OnClear() 
		{
			//  TODO: Add code here to handle clearing the collection contents
		}
    
		/// <summary>
		///     TODO: Describe what custom processing this method does
		///     before removing an item from the collection
		/// </summary>
		protected override void OnRemove(int intIndex, object objValue) 
		{
			//  TODO: Add code here to handle removing an item from the collection
		}
    
		/// <summary>
		///     A strongly typed enumerator for 'PluginCollection'
		/// </summary>
		public class IPluginEnumerator : object, System.Collections.IEnumerator 
		{
        
			private System.Collections.IEnumerator iEnBase;
        
			private System.Collections.IEnumerable iEnLocal;
        
			/// <summary>
			///     Enumerator constructor
			/// </summary>
			public IPluginEnumerator(PluginCollection pluMappings) 
			{
				this.iEnLocal = ((System.Collections.IEnumerable)(pluMappings));
				this.iEnBase = iEnLocal.GetEnumerator();
			}
        
			/// <summary>
			///     Gets the current element from the collection (strongly typed)
			/// </summary>
			public IPlugin Current 
			{
				get 
				{
					return ((IPlugin)(iEnBase.Current));
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

