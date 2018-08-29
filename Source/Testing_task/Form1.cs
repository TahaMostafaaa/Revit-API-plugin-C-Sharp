/*
 * Created by SharpDevelop.
 * User: Taha
 * Date: 4/24/2018
 * Time: 5:40 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace Testing_task
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	/// 
	[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
	[Autodesk.Revit.DB.Macros.AddInId("6FF23B38-9337-4025-BD5C-D1CA5982FB4B")]
	
	public class cmp : IComparer<FamilyParameter>
	{
		public int Compare(FamilyParameter x, FamilyParameter y)
		{
			// TODO: Handle x or y being null, or them not having names
			return x.Definition.Name.CompareTo(y.Definition.Name);
		}
	}
	public class cmp_groups : IComparer<DefinitionGroup>
	{
		public int Compare(DefinitionGroup x, DefinitionGroup y)
		{
			// TODO: Handle x or y being null, or them not having names
			return x.Name.CompareTo(y.Name);
		}
	}
	public static class Globals
	{
		public static SortedSet<DefinitionGroup> all_groups = new SortedSet<DefinitionGroup>(new cmp_groups());
		
		public static SortedDictionary<string,SortedSet<string>> SP_with_groups
			=	new SortedDictionary<string,SortedSet<string>>();
		
		public static Dictionary<string,SortedSet<string>> SP_andAllItsFiles =
			new Dictionary<string,SortedSet<string>>();
		
		public static SortedDictionary<string,SortedSet<FamilyParameter>> files_with_SP
			=	new SortedDictionary<string,SortedSet<FamilyParameter>>();
		
		public static SortedSet<string> all_SP_variables = new SortedSet<string>();
		public static List<string> full_files_name_variables = new List<string>();
		public static List<string> full_pram_name_variables = new List<string>();
		public static List<string> common_pram_name_variables = new List<string>();
		public static bool instance_or_not = false;
		public static bool tab_mgr_flag = false;
		public static string new_name_for_rename;
		//	public static num_of_files = 0;
		public static ParameterType new_type;
		public static string new_group;
		public static string parm_to_Replace;
	}
	
	public class AddEvent : IExternalEventHandler
	{
		
		
		public void Execute(UIApplication app)
		{
			//          foreach(string s in Globals.full_files_name_variables)
			//              TaskDialog.Show("Revit",s);
			
			ThisApplication.Add_SP_To_Families(Globals.full_files_name_variables,
			                                   Globals.full_pram_name_variables,
			                                   Globals.instance_or_not);
			return;
		}
		
		public string GetName()
		{
			return "AddEvent : IExternalEventHandler";
		}
	}
	public class RenameEvent : IExternalEventHandler
	{
		
		
		public void Execute(UIApplication app)
		{
			ThisApplication.Rename_Family_Parameters(Globals.SP_andAllItsFiles[Globals.parm_to_Replace].ToList());
			
		}
		
		public string GetName()
		{
			return "RenameEvent : IExternalEventHandler";
		}
		
	}
	public class RemoveEvent : IExternalEventHandler
	{
		
		
		public void Execute(UIApplication app)
		{
			//          foreach(string s in Globals.full_files_name_variables)
			//              TaskDialog.Show("Revit",s);
			ThisApplication.Remove_Parameter(Globals.full_files_name_variables,
			                                 Globals.full_pram_name_variables);
			return;
		}
		
		public string GetName()
		{
			return "AddEvent : IExternalEventHandler";
		}
	}
	public partial class Form1 : System.Windows.Forms.Form
	{
		public static Dictionary<string, List<ParameterType>> GroupParameterTypesOnDiscipline()
		{
			Dictionary<string, List<ParameterType>> disciplineToParameterTypes = new Dictionary<string, List<ParameterType>>();

			string oriFile = ThisApplication.CachedApp.SharedParametersFilename;
			string tempFile = Path.GetTempFileName() + ".txt";
			try
			{
				using (File.Create(tempFile)) { }
				ThisApplication.CachedApp.SharedParametersFilename = tempFile;

				Definitions tempDefinitions = ThisApplication.CachedApp.OpenSharedParameterFile().Groups.Create("TemporaryDefintionGroup").Definitions;
				foreach (ParameterType pt in Enum.GetValues(typeof(ParameterType)))
				{
					if (pt != ParameterType.Invalid)
					{
						ExternalDefinitionCreationOptions op = new ExternalDefinitionCreationOptions(pt.ToString(), pt);
						Definition def = tempDefinitions.Create(op);
						UnitGroup ug = UnitUtils.GetUnitGroup(def.UnitType);
						if (disciplineToParameterTypes.ContainsKey(ug.ToString()))
						{
							disciplineToParameterTypes[ug.ToString()].Add(pt);
						}
						else
						{
							disciplineToParameterTypes.Add(ug.ToString(), new List<ParameterType> { pt });
						}
					}
				}

				File.Delete(tempFile);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString(), "GroupParameterTypesOnDiscipline");
			}
			finally
			{
				ThisApplication.CachedApp.SharedParametersFilename = oriFile;
			}

			return disciplineToParameterTypes;
		}
		Dictionary<string, List<ParameterType>> Group_ParameterTypesOn_Discipline
			= GroupParameterTypesOnDiscipline();
		public void DisciplineComboBoxFill(System.Windows.Forms.ComboBox cbD
		                                   ,System.Windows.Forms.ComboBox cbT)
		{
			foreach (KeyValuePair<string, List<ParameterType>> kvp in Group_ParameterTypesOn_Discipline)
			{
				cbD.Items.Add(kvp.Key);
			}
			//            string info = "";
			//            foreach (KeyValuePair<string, List<ParameterType>> kvp in targetObj)
			//            {
			//                info += string.Format("{0}" + Environment.NewLine,  kvp.Key);
			//                foreach (ParameterType pt in kvp.Value)
			//                {
			//                    string label = pt.ToString();
			//                    try
			//                    {
			//                        label = LabelUtils.GetLabelFor(pt);
			//                    }
			//                    catch
			//                    {
			//                        System.Diagnostics.Debug.WriteLine(string.Format("{0} doesn't have a label?!", pt));
			//                    }
//
			//                    info += string.Format("\t{0}  ||  {1}" + Environment.NewLine, label, pt);
			//                }
			//            }

			// System.Diagnostics.Debug.Write(info);
		}
		
		Dictionary<string,FamilyParameter> SP_andItsDetials =
			new Dictionary<string,FamilyParameter>();
		
		
		
		
		public Document doc{ get;set;}
		//  public Application app{get;set;}
		public Autodesk.Revit.ApplicationServices.Application app{ get;set;}
		public void Show(char type, string text)
		{
			if(type == 'n')
				MessageBox.Show(text,"Note");
			else if (type == 'w')
				MessageBox.Show(text,"Warrning");
			else
				MessageBox.Show(text,"Error");
			
		}
		//file_read();
		AddEvent ev_add_params;
		ExternalEvent ex_ev_add_params;
		RemoveEvent ev_remove_params;
		ExternalEvent ex_ev_remove_params;
		RenameEvent ev_rename_params;
		ExternalEvent ex_ev_rename_params;
		//      return_all_SP get_all_p;
		//      ExternalEvent my_sp_ex_ev;
		//      testing t;
		//      ExternalEvent ex;
		//public static System.Windows.Forms.ProgressBar progressBar1;
		public Form1()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//PrintOutToDebug(GroupParameterTypesOnDiscipline());
			treeView1.Nodes.Add("Common SP/s");
			treeView1.Nodes[0].ForeColor = System.Drawing.Color.Blue;
			//treeView1.Nodes[0].NodeFont = new Font(triStateTreeView1.Font, FontStyle.Bold);
//			Form1.progressBar1 = new System.Windows.Forms.ProgressBar();
//			this.Controls.Add(Form1.progressBar1);
//			Form1.progressBar1.Location = new System.Drawing.Point(12, 416);
//			Form1.progressBar1.Name = "progressBar1";
//			Form1.progressBar1.Size = new System.Drawing.Size(706, 19);
//			Form1.progressBar1.TabIndex = 7;
//			//TaskDialog.Show("R",Form1.progressBar1.Maximum.ToString());
			button8.Enabled =  false;
			//listBox1.Sorted = true;
			//      bool tab_mgr_flag = false;
			
			//  file_read("SP2","SP3");
			//          get_all_p = new return_all_SP();
			//          my_sp_ex_ev = ExternalEvent.Create(get_all_p);
			listBox1.SelectionMode = SelectionMode.MultiExtended;
			//listBox3.SelectionMode = SelectionMode.MultiExtended;
			//          t = new testing();
			//          ex = ExternalEvent.Create(t);
			//          my_sp_ex_ev.Raise();
			//  if(tmp.all_sp != null)
			//   checkBox2.Checked = true;
			add_SP_to_tri();
			ev_add_params = new AddEvent();
			ex_ev_add_params = ExternalEvent.Create(ev_add_params);
			ev_remove_params = new RemoveEvent();
			ex_ev_remove_params = ExternalEvent.Create(ev_remove_params);
			ev_rename_params = new RenameEvent();
			ex_ev_rename_params = ExternalEvent.Create(ev_rename_params);
			//Globals.all_SP_variables = ThisApplication.all_SP;
			
//			foreach (string sp in Globals.all_SP_variables)
//			{
//				listBox1.Items.Add(sp.ToString());
//				//  listBox2.Items.Add(sp.ToString());
//				//  listBox3.Items.Add(sp.ToString());
//				//  TaskDialog.Show("Revit","Test");
//			}
			//listBox2.SelectedValueChanged += new EventHandler(Listbox2_SelectedValueChanged);
			
			//     tabControl1.SelectedIndexChanged += new EventHandler(TabPage2Click);
			//  listBox3.SelectedValueChanged += new EventHandler(Listbox3_SelectedValueChanged);
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public void add_SP_to_tri()
		{
			triStateTreeView1.Nodes.Clear();
			int i = 0;
			foreach(var group_and_SPs in Globals.SP_with_groups)
			{
				triStateTreeView1.Nodes.Add(group_and_SPs.Key);
				//	string tmp = group_and_SPs.Key;
				//	TaskDialog.Show("Revit",group_and_SPs.Value.Count.ToString());
				triStateTreeView1.Nodes[i].ForeColor = System.Drawing.Color.Blue;
				//triStateTreeView1.Nodes[i].NodeFont.Bold = true;
				//	triStateTreeView1.Nodes[i].NodeFont = new Font(triStateTreeView1.Font, FontStyle.Bold);
				foreach(string SPs in group_and_SPs.Value)
				{
					
					triStateTreeView1.Nodes[i].Nodes.Add(SPs);
					Globals.all_SP_variables.Add(SPs);
				}
				// triStateTreeView1.Nodes[i]
				i++;
			}
		}
//		public void Listbox2_SelectedValueChanged(object sender, EventArgs e)
//		{
//			//  Listbox listbox = (Listbox)sender;
//			if(listBox2.SelectedItem == null)
//			{	label4.Text = ""; return; }
//			label4.Text =  listBox2.SelectedItem.ToString();
//		}
		
		void Button1Click(object sender, EventArgs e)
		{
			OpenFileDialog FD = new System.Windows.Forms.OpenFileDialog();
			FD.Multiselect = true;
			//  textBox1.Text = "";
			if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				
				label1.Text = ("You Selected "+FD.FileNames.Length+" File/s");
				Globals.full_files_name_variables.Clear();
				textBox1.Text = Path.GetDirectoryName(FD.FileName);
				//if(FD.FileNames.Length > 1)
				try{
					foreach(string s in FD.FileNames)
					{
						//  TaskDialog.Show("Revit",s);
						Globals.full_files_name_variables.Add(s);
					}
					//                    Special_case_TabPag2Click();
					//                    Special_case_Tabpage3Cick();
				}
				catch(SystemException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
				}
				
				//  else
				//                  Globals.full_files_name_variables.Add(FD.FileName);
				textBox1.ReadOnly =button8.Enabled= true;
				
				
			}
			
			
		}
		public List<String> DirSearch(string sDir)
		{
			List<String> files = new List<String>();
			try
			{
				foreach (string f in Directory.GetFiles(sDir))
				{
					if(f[f.Length-1] == 'a' && f[f.Length-2] == 'f' && f[f.Length-3] == 'r')
						if(f.Count(C => C == '.') == 1)
							files.Add(f);
				}
				foreach (string d in Directory.GetDirectories(sDir))
				{
					files.AddRange(DirSearch(d));
				}
			}
			catch (System.Exception excpt)
			{
				MessageBox.Show(excpt.Message);
			}
			
			return files;
		}
		void Button7Click(object sender, EventArgs e)
		{
			//Globals.full_files_name_variables.Clear();
			// var fbd = new FolderBrowserDialog();
			// DialogResult result = fbd.ShowDialog();
			using(var fbd = new FolderBrowserDialog())
			{
				DialogResult result = fbd.ShowDialog();
				
				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
				{
					//string[] files = Directory.GetFiles();
					Globals.full_files_name_variables.Clear();
					Globals.full_files_name_variables = DirSearch(fbd.SelectedPath);
					label1.Text = ("You Selected "+Globals.full_files_name_variables.Count+" File/s");
					textBox1.Text = fbd.SelectedPath;
					textBox1.ReadOnly =button8.Enabled= true;
				}
			}
			
			
			
		}
		//        void TabPage2Click(object sender, System.EventArgs e)
		//        {
		//            if(tabControl1.SelectedTab.Text != "Rename"
		//               || Globals.full_files_name_variables.Count == 0 || Globals.tab_mgr_flag)
		//                return;
		//            Globals.common_pram_name_variables.Clear();
		//            listBox2.Items.Clear();
		//            Globals.tab_mgr_flag = true;
		//            ThisApplication.get_all_paramters(Globals.full_files_name_variables);
		//            //Globals.tab_mgr_flag = true;
		//            if(Globals.full_files_name_variables.Count > 0)
		//            {
		//                foreach(string s in Globals.common_pram_name_variables)
		//                    listBox2.Items.Add(s);
		//            }
//
		//        }
		
		void Button2Click(object sender, EventArgs e)
		{
			Globals.full_pram_name_variables.Clear();
			//  List<string> parm_to_be_add = new List<string>();
			foreach (string s in listBox1.Items)
				Globals.full_pram_name_variables.Add(s);
			Globals.instance_or_not = checkBox1.Checked;
			//progressBar1.Increment(1);
			
			ex_ev_add_params.Raise();
			// delete_files();
			//Globals.full_pram_name_variables.Clear();
			
			//  ThisApplication.Add_SP_To_Families(Globals.full_files_name_variables,parm_to_be_add,checkBox1.Checked);
			//ThisApplication.Test();
			//          ex.Raise();
		}
		
		void Button4Click(object sender, EventArgs e)
		{
//			Globals.full_pram_name_variables.Clear();
//			foreach (string s in listBox3.SelectedItems)
//				Globals.full_pram_name_variables.Add(s);
			ex_ev_remove_params.Raise();
			//Globals.full_pram_name_variables.Clear();
		}
		bool backup_match(string orginal,string s1)
		{
			if(orginal.Length+4 >= s1.Length)
				return false;
			//  MessageBox.Show(orginal + " -- " + s1);
			for(int i =0; i < orginal.Length ;i++)
			{
				if(orginal[i]!=s1[i])
					return false;
			}
			
			return true;
		}
		public void delete_files()
		{
			if(textBox1.Text == "")
			{
				MessageBox.Show("No Seleceted file/s","Note");
				return;
			}
			DirectoryInfo di = new DirectoryInfo(textBox1.Text);
			//   var all_files - di.GetFiles(
			
			string name="";
			string set_display = "";
			List<string> hash_set = new List<string>();
			int count = 1;
			foreach(string s in Globals.full_files_name_variables)
			{
				string tmp = s.Remove(s.Length - 4);
				//tmp = tmp.Remove(textBox1.Text.Length);
				foreach(FileInfo file in di.GetFiles())
				{
					name = textBox1.Text + "\\" + file.Name;
					if(backup_match(tmp,name))
					{
						//  TaskDialog.Show("Revit",tmp);
						hash_set.Add(file.Name);
						set_display+=  count.ToString() +"- "+ file.Name + "\n";
						count++;
					}
				}
				
				hash_set.Sort();
				
				
				
				//              if(File.Exists(@file_name))
				//              {
				//                  TaskDialog.Show("Revit",hash_set.Count.ToString());
				//                  //File.Delete(@file_name);
				//              }
				//              else
				//              TaskDialog.Show("Error","File Doesn't exist");
			}
			set_display = "You deleted " + (count-1).ToString() + "file/s:\n" + set_display;
			if(count != 1)
			{
				for(int i =0; i < hash_set.Count; i++)
				{ File.Delete((textBox1.Text + "\\" + hash_set[i]));}
				Show('n',set_display);
			}
			else
				Show('n',"Their is no backups for the selected file/s");
			
		}

		
		
		
		void Button3Click(object sender, EventArgs e)
		{
			
			if(txtName.Text == "")
			{
				TaskDialog.Show("Warrning","The Name field is empty");
				return;
			}
			if(Globals.all_SP_variables.Contains(txtName.Text))
			{
				TaskDialog.Show("Warrning","There is a SP with the same name");
				return;
			}
//			ThisApplication.Create_SP(txtName.Text
//			                          ,SP_andItsDetials[treeView1.SelectedNode.Text].Definition.ParameterType
//			                          ,cbGroup.Text);
			
			Globals.new_group = cbGroup.Text;
			Globals.new_name_for_rename = txtName.Text;
			Globals.new_type = SP_andItsDetials[last_Selected_node].Definition.ParameterType;
			Globals.instance_or_not = ckbInstance.Checked;
			//Globals.parm_to_Replace = treeView1.SelectedNode.Text;
			Globals.parm_to_Replace = last_Selected_node;
//			Globals.new_name_for_rename = txtName.Text;
//
//			Globals.full_pram_name_variables.Clear();
//			foreach (string s in listBox2.SelectedItems)
//				Globals.full_pram_name_variables.Add(s);
			txtGUID.Text = txtName.Text = cbGroup.Text = cbSPtype.Text = cbDesc.Text = lblCount.Text = "";
			ex_ev_rename_params.Raise();
			txtGUID.Text = txtName.Text = cbGroup.Text = cbSPtype.Text = cbDesc.Text = lblCount.Text = "";
			
			//delete_files();
			//Globals.full_pram_name_variables.Clear();
			
		}
		string last_Selected_node = "";
		void Button5Click(object sender, EventArgs e)
		{
			delete_files();
		}
		public string get_file_name(string full_name)
		{
			string ot = "";
			bool start_add = false;
			int j =0;
			for(int i =full_name.Length-1; i>=0;i--)
			{
				//if(start_add)
				if(j == 4)
					start_add=true;
				if(start_add && full_name[i]=='\\')
					break;
				if(start_add)
					ot+=full_name[i];
				
				//TaskDialog.Show("Revit",ot);
				j++;
			}
			
			return new string(ot.Reverse().ToArray());
		}
		void Button6Click(object sender, EventArgs e)
		{
			
			treeView1.Nodes.Clear();
			treeView1.Nodes.Add("Common SP/s");
			treeView1.Nodes[0].ForeColor = System.Drawing.Color.Blue;
			Globals.SP_andAllItsFiles.Clear();
			SP_andItsDetials.Clear();
			cbSPtype.Items.Clear();
			cbGroup.Items.Clear();
			cbDesc.Items.Clear();
			//	treeView1.Nodes[0].NodeFont = new Font(triStateTreeView1.Font, FontStyle.Bold);
			//triStateTreeView1.Refresh();
			//listBox3.Items.Clear();
//			Globals.SP_with_groups.Clear();
//			ThisApplication.fill_all_SP(app.OpenSharedParameterFile());
			//add_SP_to_tri();
			int i = 1;
			//TaskDialog.Show("Revit",file_SPs.Value.Count.ToString());
			ThisApplication.get_all_paramters(Globals.full_files_name_variables);
			
			foreach(var file_SPs in Globals.files_with_SP)
			{
				treeView1.Nodes.Add(get_file_name(file_SPs.Key));
				
				//List<string> tmp = new List<string>();
				//	string tmp = group_and_SPs.Key;
				//		TaskDialog.Show("Revit",file_SPs.Value.Count.ToString());
				treeView1.Nodes[i].ForeColor = System.Drawing.Color.Blue;
				//triStateTreeView1.Nodes[i].NodeFont.Bold = true;
				//	treeView1.Nodes[i].NodeFont = new Font(triStateTreeView1.Font, FontStyle.Bold);
				
				foreach(FamilyParameter SP in file_SPs.Value)
				{
					
					treeView1.Nodes[i].Nodes.Add(SP.Definition.Name);
					if(!SP_andItsDetials.ContainsKey(SP.Definition.Name))
						SP_andItsDetials.Add(SP.Definition.Name,SP);
					else
						SP_andItsDetials[SP.Definition.Name]=SP;
					if(Globals.SP_andAllItsFiles.ContainsKey(SP.Definition.Name))
						Globals.SP_andAllItsFiles[SP.Definition.Name].Add(file_SPs.Key);
					else
					{
						Globals.SP_andAllItsFiles.Add(SP.Definition.Name,new SortedSet<string>());
						Globals.SP_andAllItsFiles[SP.Definition.Name].Add(file_SPs.Key);
					}
				}
				//tmp.Clear();
				// triStateTreeView1.Nodes[i]
				i++;
			}
			foreach(var ob in Globals.SP_andAllItsFiles)
			{
				//TaskDialog.Show("Revit",ob.Key+ " || "+Globals.full_files_name_variables.Count.ToString());
				if(ob.Value.Count == Globals.full_files_name_variables.Count)
					treeView1.Nodes[0].Nodes.Add(ob.Key);
			}
			foreach(DefinitionGroup s in Globals.all_groups)
				cbGroup.Items.Add(s.Name);
			DisciplineComboBoxFill(cbDesc,cbSPtype);
			txtName.Text = cbGroup.Text = cbSPtype.Text = cbDesc.Text = lblCount.Text= txtGUID.Text = "";
			// PrintOutToDebug(S
//			foreach(ParameterType p in ParameterType.)
//				cbSPType.Items.Add(p.ToString());
			// cbSPType.DataSource = ParameterType.
//			//Globals.all_SP_variables = ThisApplication.all_SP;
//			Globals.common_pram_name_variables.Clear();
//			if(Globals.full_files_name_variables.Count == 0)
//				return;
//			ThisApplication.get_all_paramters(Globals.full_files_name_variables);
//			foreach(string s in Globals.common_pram_name_variables)
//			{ listBox2.Items.Add(s); listBox3.Items.Add(s); }
		}
//		  public void Special_case_TabPag2Click()
		//        {
//
//
//
		//        //      TaskDialog.Show("Revit",Globals.full_files_name_variables.Count.ToString());
		//            listBox2.Items.Clear();
//
		//            Globals.tab_mgr_flag = true;
		//            if(Globals.full_files_name_variables.Count > 0)
		//            {
//
//
		//            }
		//        }
		//        public void Special_case_Tabpage3Cick()
		//        {
//
		//                if(Globals.full_files_name_variables.Count > 0)
		//            {
//
		//                foreach(string s in Globals.common_pram_name_variables)
		//                    listBox3.Items.Add(s);
		//            }
//
		//        }
		
		
		
		
		Form2 f2;
		void Button8Click(object sender, EventArgs e)
		{
			f2 = new Form2(this);
			f2.UpdateEventHandler += F2_UpdateEventHandler1;
			f2.Show();
		}
		private void F2_UpdateEventHandler1(object sender, Form2.UpdateEventArgs args)
		{
			label1.Text = "You Selected " + f2.cc + " file/s";
		}
		
		void To_addClick(object sender, EventArgs e)
		{
			//ArrayList tmp = new ArrayList();
			for(int i =0; i < triStateTreeView1.Nodes.Count;i++)
			{
				for(int j =0; j < triStateTreeView1.Nodes[i].Nodes.Count;j++)
				{
					if(triStateTreeView1.Nodes[i].Nodes[j].Checked)
						if(!listBox1.Items.Contains(triStateTreeView1.Nodes[i].Nodes[j].Text))
							listBox1.Items.Add(triStateTreeView1.Nodes[i].Nodes[j].Text);
				}
			}
			label6.Text = listBox1.Items.Count + " SP/s";
			
		}
		
		void To_removeClick(object sender, EventArgs e)
		{
			List<string> tmp = new List<string>();
			
			foreach(string s in listBox1.Items)
				tmp.Add(s);
			foreach(string s in listBox1.SelectedItems)
				tmp.Remove(s);
			listBox1.Items.Clear();
			foreach(string s in tmp)
				listBox1.Items.Add(s);
			//	listBox1.Items = tmp;
			label6.Text = listBox1.Items.Count + " SP/s";
		}
		bool expanded = false;
		void Button9Click(object sender, EventArgs e)
		{
			if(expanded)
				triStateTreeView1.CollapseAll();
			else
				triStateTreeView1.ExpandAll();
		}
		
		void TriStateTreeView1AfterExpand(object sender, TreeViewEventArgs e)
		{
			expanded = true;
		}
		
		void TriStateTreeView1AfterCollapse(object sender, TreeViewEventArgs e)
		{
			expanded = false;
		}
		
		void Button10Click(object sender, EventArgs e)
		{
			for(int i =0 ; i < triStateTreeView1.Nodes.Count;i++)
			{
				triStateTreeView1.Nodes[i].Checked = false;
			}
		}
		private void SearchNodes(string SearchText, TreeNode StartNode)
		{
			//TreeNode node = null;
			while (StartNode != null)
			{
				if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
				{
					CurrentNodeMatches.Add(StartNode);
				};
				if (StartNode.Nodes.Count != 0)
				{
					SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search
				};
				StartNode = StartNode.NextNode;
			};
			
		}
		private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

		private int LastNodeIndex = 0;
		
		private string LastSearchText;

//		void TextBox3TextChanged(object sender, EventArgs e)
//		{
//
//		}
		
		void TextBox3KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				string searchText = textBox3.Text;
				if (String.IsNullOrEmpty(searchText))
				{
					return;
				};


				if (LastSearchText != searchText)
				{
					//It's a new Search
					CurrentNodeMatches.Clear();
					LastSearchText = searchText;
					LastNodeIndex = 0;
					SearchNodes(searchText, triStateTreeView1.Nodes[0]);
				}

				if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
				{
					TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
					LastNodeIndex++;
					this.triStateTreeView1.SelectedNode = selectedNode;
					this.triStateTreeView1.SelectedNode.Expand();
					this.triStateTreeView1.Select();

				}
			}
		}
		string tmp_s ="";
		void TreeView1DoubleClick(object sender, EventArgs e)
		{
			TreeNode node = treeView1.SelectedNode;
			
			if(treeView1.SelectedNode.Equals(null))
				return;
			if(node.ForeColor == System.Drawing.Color.Blue)
				return;
			txtName.Text = last_Selected_node = node.Text;
			txtGUID.Text = SP_andItsDetials[node.Text].GUID.ToString();
			//cbGroup.Text = SP_andItsDetials[node.Text].Definition.
			
			ckbInstance.Checked = SP_andItsDetials[node.Text].IsInstance;
			UnitGroup ug = UnitUtils.GetUnitGroup(SP_andItsDetials[node.Text].Definition.UnitType);
			cbDesc.Text = tmp_s = ug.ToString();
			//txtDes.Text = SP_andItsDetials[node.Text].StorageType.ToString();
			DefinitionFile deFile = ThisApplication.CachedApp.OpenSharedParameterFile();
			ExternalDefinition ex = ThisApplication.RawFindExternalDefinition(
				deFile,treeView1.SelectedNode.Text);
			cbGroup.Text = ex.OwnerGroup.Name;
			lblCount.Text = "This SP founded in "+Globals.SP_andAllItsFiles[node.Text].Count+" file/s";
			cbSPtype.Items.Clear();
			List<ParameterType> tmp = Group_ParameterTypesOn_Discipline[cbDesc.Text];
			foreach(ParameterType p in tmp)
				cbSPtype.Items.Add(p.ToString());
			Globals.parm_to_Replace = txtName.Text;
			cbSPtype.Text = SP_andItsDetials[node.Text].Definition.ParameterType.ToString();
			
		}
		
		void CbDescSelectedIndexChanged(object sender, EventArgs e)
		{
			cbDesc.Text = tmp_s;
		}
	}
	
}
