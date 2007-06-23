using System;
using System.Drawing;
using System.Windows.Forms;

namespace Regulator.SDK.ApplicationSettings
{
	/// <summary>
	/// Summary description for FormSettings.
	/// </summary>
	/// 
	[Serializable]
	public class FormSettings
	{
		private FormWindowState m_forWindowState;

		private string m_strFormName;

		private Size m_sizFormSize;

		private Point m_poiLocation;

		public FormSettings(System.Windows.Forms.Form form)
		{
			
			SetValues(form.Name,form.Location,form.Size,form.WindowState);
		}
		

		public void SetValues(string name,Point location,Size size,FormWindowState winstate)
		{
			FormName=name;
			
			if (winstate!=FormWindowState.Minimized)
			{
				WindowState=winstate;
			}
			if (winstate!=FormWindowState.Maximized &&
				winstate!=FormWindowState.Minimized)
			{
				this.FormSize= size;
				Location=location;
			}

		}

		public FormSettings()
		{
			
		}

		public Point Location
		{
			get
			{
				return m_poiLocation;
			}
			set
			{
				m_poiLocation = value;
			}
		}


		public Size FormSize
		{
			get
			{
				return m_sizFormSize;
			}
			set
			{
				m_sizFormSize = value;
			}
		}


		public string FormName
		{
			get
			{
				return m_strFormName;
			}
			set
			{
				m_strFormName = value;
			}
		}



		public void SetValues(System.Windows.Forms.Form form)
		{
			SetValues(form.Name,form.Location,form.Size,form.WindowState);
		}

		public void ApplyFormSettings(System.Windows.Forms.Form winform)
		{
			

			if(!Location.Equals(new Point()))
			{
				winform.Location= Location;
				
			}			
			if(!FormSize.Equals(new Size()))
			{
				winform.Size= FormSize;
			
			}

			winform.WindowState=WindowState;
		}

		public FormWindowState WindowState
		{
			get
			{
				return m_forWindowState;
			}
			set
			{
				m_forWindowState = value;
			}
		}

	}



	#region "'FormSettingsCollection' strongly typed collection class"


	/// <summary>
	///     A collection that stores 'FormSettings' objects.
	/// </summary>
	[Serializable()]
	public class FormSettingsCollection : System.Collections.CollectionBase 
	{
    
		/// <summary>
		///     Initializes a new instance of 'FormSettingsCollection'.
		/// </summary>
		public FormSettingsCollection() 
		{
		}
    
		/// <summary>
		///     Initializes a new instance of 'FormSettingsCollection' based on an already existing instance.
		/// </summary>
		/// <param name='forValue'>
		///     A 'FormSettingsCollection' from which the contents is copied
		/// </param>
		public FormSettingsCollection(FormSettingsCollection forValue) 
		{
			this.AddRange(forValue);
		}
    
		/// <summary>
		///     Initializes a new instance of 'FormSettingsCollection' with an array of 'FormSettings' objects.
		/// </summary>
		/// <param name='forValue'>
		///     An array of 'FormSettings' objects with which to initialize the collection
		/// </param>
		public FormSettingsCollection(FormSettings[] forValue) 
		{
			this.AddRange(forValue);
		}
    
		/// <summary>
		///     Represents the 'FormSettings' item at the specified index position.
		/// </summary>
		/// <param name='intIndex'>
		///     The zero-based index of the entry to locate in the collection.
		/// </param>
		/// <value>
		///     The entry at the specified index of the collection.
		/// </value>
		public FormSettings this[int intIndex] 
		{
			get 
			{
				return ((FormSettings)(List[intIndex]));
			}
			set 
			{
				List[intIndex] = value;
			}
		}
    
		/// <summary>
		///     Adds a 'FormSettings' item with the specified value to the 'FormSettingsCollection'
		/// </summary>
		/// <param name='forValue'>
		///     The 'FormSettings' to add.
		/// </param>
		/// <returns>
		///     The index at which the new element was inserted.
		/// </returns>
		public int Add(FormSettings form) 
		{
			if(Contains(form.FormName))
			{
				Remove(Get(form.FormName));
			}
			return List.Add(form);
		}

		public void RegisterForAutomaticSettingsBackup(System.Windows.Forms.Form winform)
		{
			winform.Closing+=new System.ComponentModel.CancelEventHandler(winform_Closing);
			ApplyFormSettings(winform);
		}

		public void SetFormSettings(System.Windows.Forms.Form winform) 
		{
			FormSettings form = new FormSettings(winform);
			if(Contains(form.FormName))
			{
				Remove(Get(form.FormName));
			}
			List.Add(form);
		}

		public void ApplyFormSettings(System.Windows.Forms.Form winform)
		{
			if(Contains(winform.Name))
			{
				Get(winform.Name).ApplyFormSettings(winform);
			}
		}
    
		/// <summary>
		///     Copies the elements of an array at the end of this instance of 'FormSettingsCollection'.
		/// </summary>
		/// <param name='forValue'>
		///     An array of 'FormSettings' objects to add to the collection.
		/// </param>
		public void AddRange(FormSettings[] forValue) 
		{
			for (int intCounter = 0; (intCounter < forValue.Length); intCounter = (intCounter + 1)) 
			{
				this.Add(forValue[intCounter]);
			}
		}
    
		/// <summary>
		///     Adds the contents of another 'FormSettingsCollection' at the end of this instance.
		/// </summary>
		/// <param name='forValue'>
		///     A 'FormSettingsCollection' containing the objects to add to the collection.
		/// </param>
		public void AddRange(FormSettingsCollection forValue) 
		{
			for (int intCounter = 0; (intCounter < forValue.Count); intCounter = (intCounter + 1)) 
			{
				this.Add(forValue[intCounter]);
			}
		}
    
		/// <summary>
		///     Gets a value indicating whether the 'FormSettingsCollection' contains the specified value.
		/// </summary>
		/// <param name='forValue'>
		///     The item to locate.
		/// </param>
		/// <returns>
		///     True if the item exists in the collection; false otherwise.
		/// </returns>
		public bool Contains(FormSettings forValue) 
		{
			return List.Contains(forValue);
		}

		public FormSettings Get(string formName)
		{
			foreach (FormSettings f in this)
			{
				if(f.FormName== formName)
				{
					return f;
				}
			}
			return null;
		}
		public bool Contains(string formName) 
		{
			foreach (FormSettings f in this)
			{
				if(f.FormName== formName)
				{
					return true;
				}
			}
			return false;
		}
    
		/// <summary>
		///     Copies the 'FormSettingsCollection' values to a one-dimensional System.Array
		///     instance starting at the specified array index.
		/// </summary>
		/// <param name='forArray'>
		///     The one-dimensional System.Array that represents the copy destination.
		/// </param>
		/// <param name='intIndex'>
		///     The index in the array where copying begins.
		/// </param>
		public void CopyTo(FormSettings[] forArray, int intIndex) 
		{
			List.CopyTo(forArray, intIndex);
		}
    
		/// <summary>
		///     Returns the index of a 'FormSettings' object in the collection.
		/// </summary>
		/// <param name='forValue'>
		///     The 'FormSettings' object whose index will be retrieved.
		/// </param>
		/// <returns>
		///     If found, the index of the value; otherwise, -1.
		/// </returns>
		public int IndexOf(FormSettings forValue) 
		{
			return List.IndexOf(forValue);
		}
    
		/// <summary>
		///     Inserts an existing 'FormSettings' into the collection at the specified index.
		/// </summary>
		/// <param name='intIndex'>
		///     The zero-based index where the new item should be inserted.
		/// </param>
		/// <param name='forValue'>
		///     The item to insert.
		/// </param>
		public void Insert(int intIndex, FormSettings forValue) 
		{
			List.Insert(intIndex, forValue);
		}
    
		/// <summary>
		///     Returns an enumerator that can be used to iterate through
		///     the 'FormSettingsCollection'.
		/// </summary>
		public new FormSettingsEnumerator GetEnumerator() 
		{
			return new FormSettingsEnumerator(this);
		}
    
		/// <summary>
		///     Removes a specific item from the 'FormSettingsCollection'.
		/// </summary>
		/// <param name='forValue'>
		///     The item to remove from the 'FormSettingsCollection'.
		/// </param>
		public void Remove(FormSettings forValue) 
		{
			List.Remove(forValue);
		}
    
		/// <summary>
		///     A strongly typed enumerator for 'FormSettingsCollection'
		/// </summary>
		public class FormSettingsEnumerator : object, System.Collections.IEnumerator 
		{
        
			private System.Collections.IEnumerator iEnBase;
        
			private System.Collections.IEnumerable iEnLocal;
        
			/// <summary>
			///     Enumerator constructor
			/// </summary>
			public FormSettingsEnumerator(FormSettingsCollection forMappings) 
			{
				this.iEnLocal = ((System.Collections.IEnumerable)(forMappings));
				this.iEnBase = iEnLocal.GetEnumerator();
			}
        
			/// <summary>
			///     Gets the current element from the collection (strongly typed)
			/// </summary>
			public FormSettings Current 
			{
				get 
				{
					return ((FormSettings)(iEnBase.Current));
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

		private void winform_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SetFormSettings((Form)sender);
			
			AppContext.Instance.Settings.Save();
		}
	}

	#endregion //('FormSettingsCollection' strongly typed collection class)

}
