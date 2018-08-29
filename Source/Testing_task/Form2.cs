/*
 * Created by SharpDevelop.
 * User: Taha
 * Date: 6/3/2018
 * Time: 10:08 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Testing_task
{
	/// <summary>
	/// Description of Form2.
	/// </summary>
	public partial class Form2 : Form
	{
		public int cc = 0;
		private void update(int cnt)
		{
			cc = cnt;
			label2.Text = cnt + " file/s";
			
		}
		public delegate void UpdateDelegate(object sender, UpdateEventArgs args);
		public event UpdateDelegate UpdateEventHandler;
		Form1 frm1;
		public class UpdateEventArgs : EventArgs
		{
			public string Data { get; set; }
		}
		public Form2(Form1 f1)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			
			InitializeComponent();
			frm1 = f1;
			listBox1.HorizontalScrollbar = true;
			listBox1.DataSource = Globals.full_files_name_variables;
			update(Globals.full_files_name_variables.Count);
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		UpdateEventArgs args = new UpdateEventArgs();
		void Button2Click(object sender, EventArgs e)
		{
			//listBox1.BeginUpdate();
			foreach(string s in listBox1.SelectedItems)
				Globals.full_files_name_variables.Remove(s);
			listBox1.DataSource = null;
			listBox1.DataSource = Globals.full_files_name_variables;
			update(Globals.full_files_name_variables.Count);
			textBox1.Text = "";
			
			UpdateEventHandler.Invoke(this, args);
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			SortedSet<string> st = new SortedSet<string>();
			foreach(string s in Globals.full_files_name_variables)
			{
				if(s.Contains(textBox1.Text))
				{
					st.Add(s);
				}
			}
			listBox1.DataSource = null;
			listBox1.DataSource = st.ToList();
			update(st.Count);
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			OpenFileDialog FD = new System.Windows.Forms.OpenFileDialog();
			FD.Multiselect = true;
			//  textBox1.Text = "";
			if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				foreach(string s in FD.FileNames)
			{
				//  TaskDialog.Show("Revit",s);
				if(!Globals.full_files_name_variables.Contains(s))
					Globals.full_files_name_variables.Add(s);
			}
			listBox1.DataSource = null;
			listBox1.DataSource = Globals.full_files_name_variables;
			update(Globals.full_files_name_variables.Count);
			//UpdateEventArgs args = new UpdateEventArgs();
			UpdateEventHandler.Invoke(this, args);
		}
	}
}
