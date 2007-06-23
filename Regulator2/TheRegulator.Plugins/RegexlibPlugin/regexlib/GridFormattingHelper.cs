using System;
using C1.Win.C1FlexGrid;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Regulator.SDK.Plugins.RegexLib
{
	/// <summary>
	/// Summary description for GridFormattingHelper.
	/// </summary>
	public class GridFormattingHelper
	{
		public static void FormatGrid(DataGrid grid,string primaryTableMappingName)
		{
			try
			{
				DataGridTextBoxColumn colDescription = new DataGridTextBoxColumn();
				DataGridTextBoxColumn colRegex = new DataGridTextBoxColumn();
				DataGridTextBoxColumn colWillMatch= new DataGridTextBoxColumn();
				//DataGridTextBoxColumn colWontMatch= new DataGridTextBoxColumn();

				FormatColumn(colDescription,"description","Notes",Color.Black);
				FormatColumn(colRegex,"regular_expression","Regex",Color.Black);
				FormatColumn(colWillMatch,"matches","Example",Color.Green);
				//FormatColumn(colWontMatch,"not_matches","not ok",Color.Red);


				DataGridTableStyle newStyle = null;
			
				if (grid.TableStyles.Count==0)
				{
					newStyle = new  DataGridTableStyle();
				}
				else
				{
					newStyle = grid.TableStyles[0];
				}
			
				newStyle.PreferredRowHeight=40;
				newStyle.PreferredColumnWidth=60;
				newStyle.MappingName=primaryTableMappingName;

				newStyle.GridColumnStyles.Add(colDescription);
				newStyle.GridColumnStyles.Add(colRegex);
				newStyle.GridColumnStyles.Add(colWillMatch);
				//newStyle.GridColumnStyles.Add(colWontMatch);

				if (grid.TableStyles.Count==0)
				{
					grid.TableStyles.Add(newStyle);
				}

			}
			catch(Exception )
			{
			    
			}

		}

		private static void FormatColumn(DataGridTextBoxColumn col,string mappingName,string caption,Color forecolor)
		{
			col.MappingName= mappingName;
			
			col.ReadOnly=true;
			col.HeaderText=caption;
			col.Width=80;
			col.TextBox.Multiline=true;
			col.TextBox.AutoSize=true;
			col.TextBox.BackColor=Color.Orange;
			col.TextBox.BorderStyle=BorderStyle.None;
			col.TextBox.ScrollBars=ScrollBars.Vertical;
			col.TextBox.ForeColor=forecolor;
			
		}

		public static void FormatGrid(C1.Win.C1FlexGrid.C1FlexGrid grd)
		{
			foreach (C1.Win.C1FlexGrid.Column col in grd.Cols)
			{
				col.Visible=false;
			}

			ShowColumnWithHeader(grd,"regular_expression","Expression");
			ShowColumnWithHeader(grd,"not_matches","Won't match");
			ShowColumnWithHeader(grd,"matches","Will match");
			ShowColumnWithHeader(grd,"description","Description");
		}

		private static void ShowColumnWithHeader(C1.Win.C1FlexGrid.C1FlexGrid grd,string originalColName,string newColName)
		{
			C1.Win.C1FlexGrid.Column col = grd.Cols[originalColName];
			col.Caption= newColName;
			col.Visible=true;
			col.AllowEditing=false;
			
			
		}

		public static  DataView CreateFormattedDataView(DataTable table)
		{
			DataView dv = new DataView(table);//regular_expression, matches,not_matches,description
			foreach (DataColumn col  in dv.Table.Columns)
			{
				col.ColumnMapping= MappingType.Hidden;
			}

//			ShowColumnWithHeader(dv,"regular_expression","Expression");
//			ShowColumnWithHeader(dv,"not_matches","Won't match");
//			ShowColumnWithHeader(dv,"matches","Will match");
//			ShowColumnWithHeader(dv,"description","Description");

			ShowColumnWithHeader(dv,"description","Description");
			ShowColumnWithHeader(dv,"matches","matches");
			ShowColumnWithHeader(dv,"regular_expression","regular_expression");
			//			ShowColumnWithHeader(dv,"not_matches","not_matches");

			return dv;
		}

		private static  void ShowColumnWithHeader(DataView dv,string colName,string header)
		{
			DataColumn col =dv.Table.Columns[colName];
			col.ColumnMapping=MappingType.Element;
			col.Caption= header;
			col.ColumnName=header;
			col.ReadOnly=true;
		}

	}
}
