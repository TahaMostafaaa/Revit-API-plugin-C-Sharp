/*
 * Created by SharpDevelop.
 * User: Taha
 * Date: 4/19/2018
 * Time: 4:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RvtApplication = Autodesk.Revit.ApplicationServices.Application;
using RvtDocument = Autodesk.Revit.DB.Document;

namespace Testing_task
{
	[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
	[Autodesk.Revit.DB.Macros.AddInId("6FF23B38-9337-4025-BD5C-D1CA5982FB4B")]
	public partial class ThisApplication : IExternalCommand
	{
		#region Cached Variables
		
		public static List<string> all_SP = new List<string>();
		private static ExternalCommandData _cachedCmdData;

		public static UIApplication CachedUiApp
		{
			get
			{
				return _cachedCmdData.Application;
			}
		}

		public static RvtApplication CachedApp
		{
			get
			{
				return CachedUiApp.Application;
			}
		}

		public static RvtDocument CachedDoc
		{
			get
			{
				return CachedUiApp.ActiveUIDocument.Document;
			}
		}

		#endregion
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			_cachedCmdData = commandData;
			//Create_SP("SP2", ParameterType.Area,"GROUP2");
			//TaskDialog.Show("Revit","Hello");
			//	TaskDialog.Show("Revit","Test");
			try
			{
				fill_all_SP(CachedApp.OpenSharedParameterFile());
				//TaskDialog.Show("Revit",all_SP.First());
				//    			foreach(string s in ThisApplication.all_SP)
				//    			{
				//    				TaskDialog.Show("Revit",s);
				//    			}
				Form1 myForm1 = new Form1();
				myForm1.doc = CachedDoc;
				myForm1.app = CachedApp;
				myForm1.Show();
				
			}
			catch (Autodesk.Revit.Exceptions.ArgumentNullException)
			{
				TaskDialog.Show("Error Warning","There is no Shared Parameters file Or There is no Shared Parameters");
				//trans.Commit();
				//	doc.Close(false);
				//	return;
				return Result.Failed;
			}
			catch(SystemException ae)
			{
				
				TaskDialog.Show("For Developer",ae.ToString() + "\n");
			}
			//TaskDialog.Show("Revit","Hello World From Excute!");
			return Result.Succeeded;
		}
		#region From FamilyParameter Creator
		public static List<T> RawConvertSetToList<T>(IEnumerable set)
		{
			List<T> list = (from T p in set select p).ToList<T>();
			return list;
		}
		public FamilyParameter RawFindFamilyParameter(FamilyManager fm, string parameterName)
		{
			FamilyParameter fp = RawConvertSetToList<FamilyParameter>(fm.Parameters).
				FirstOrDefault(e => e.Definition.Name.Equals(parameterName, StringComparison.CurrentCultureIgnoreCase));
			if (fp == null) throw new Exception("Invalid ParameterName Input!");
			
			return fp;
		}
		
		public FamilyType RawFindFamilyType(FamilyManager fm, string familyTypeName)
		{
			FamilyType famType = RawConvertSetToList<FamilyType>(fm.Types).
				FirstOrDefault(e => e.Name.Equals(familyTypeName, StringComparison.CurrentCultureIgnoreCase));
			if (famType == null) throw new Exception("Invalid FamilyTypeName Input!");
			
			return famType;
		}
		
		public void RawSetFamilyParameterValue(FamilyManager fm, string familyTypeName, string parameterName, object value)
		{
			RawSetFamilyParameterValue(fm, RawFindFamilyType(fm, familyTypeName), RawFindFamilyParameter(fm, parameterName), value);
		}
		
		public void RawSetFamilyParameterValue(FamilyManager fm, FamilyType ft, string parameterName, object value)
		{
			RawSetFamilyParameterValue(fm, ft, RawFindFamilyParameter(fm, parameterName), value);
		}
		
		public void RawSetFamilyParameterValue(FamilyManager fm, string familyTypeName, FamilyParameter fp, object value)
		{
			RawSetFamilyParameterValue(fm, RawFindFamilyType(fm, familyTypeName), fp, value);
		}
		
		public void RawSetFamilyParameterValue(FamilyManager fm, FamilyType ft, FamilyParameter fp, object value)
		{
			FamilyType curFamType = fm.CurrentType;
			fm.CurrentType = ft;
			
			try
			{
				switch (fp.StorageType)
				{
					case StorageType.None:
						break;
					case StorageType.Double:
						if (value.GetType().Equals(typeof(string)))
						{
							fm.Set(fp, double.Parse(value as string));
						}
						else
						{
							fm.Set(fp, Convert.ToDouble(value));
						}
						break;
					case StorageType.Integer:
						if (value.GetType().Equals(typeof(string)))
						{
							fm.Set(fp, int.Parse(value as string));
						}
						else
						{
							fm.Set(fp, Convert.ToInt32(value));
						}
						break;
					case StorageType.ElementId:
						if (value.GetType().Equals(typeof(ElementId)))
						{
							fm.Set(fp, value as ElementId);
						}
						else if (value.GetType().Equals(typeof(string)))
						{
							fm.Set(fp, new ElementId(int.Parse(value as string)));
						}
						else
						{
							fm.Set(fp, new ElementId(Convert.ToInt32(value)));
						}
						break;
					case StorageType.String:
						fm.Set(fp, value.ToString());
						break;
				}
			}
			catch
			{
				throw new Exception("Invalid Value Input!");
			}
			finally
			{
				fm.CurrentType = curFamType;
			}
		}
		
		#endregion
		
		
		public static ExternalDefinition RawFindExternalDefinition(DefinitionFile defFile, string name)
		{
			var v = (from DefinitionGroup dg in defFile.Groups
			         from ExternalDefinition d in dg.Definitions
			         where d.Name == name
			         select d);
			if (v == null || v.Count() < 1)
				return null;
			else
				return v.First();
		}
		private void Module_Startup(object sender, EventArgs e)
		{

		}

		private void Module_Shutdown(object sender, EventArgs e)
		{

		}
		public void ConvertSharedParameterToFamilyParameter(ExternalDefinition extDef, FamilyManager famMan, bool instance, string type, object value )
		{
			FamilyParameter fp = famMan.AddParameter(extDef, extDef.ParameterGroup, instance);
			if (value != null) RawSetFamilyParameterValue(famMan, type, fp, value);
		}
		public static void fill_all_SP(DefinitionFile defFile)
		{
			
			foreach(DefinitionGroup myGroup in defFile.Groups)
			{
				Globals.all_groups.Add(myGroup);
				SortedSet<string> tmp = new SortedSet<string>();
				Globals.SP_with_groups.Add(myGroup.Name,tmp);
				foreach(Definition df in myGroup.Definitions)
				{
					Globals.SP_with_groups[myGroup.Name].Add(df.Name);
//					TaskDialog.Show("Revit",df.Name);
//					return;
				}
				//	TaskDialog.Show("Revit",Globals.SP_with_groups[myGroup.Name].Count.ToString());
				
			}
			
			// TaskDialog.Show("Revit",df.Name);}
			
		}
		
//		public void Rename_Shared_Parameters(string p_name,string new_name)
//		{
		////			UIDocument uidoc = this.ActiveUIDocument;
		////			Document CachedDoc = uidoc.Document;
//			Document doc;
//			//Application app;
//			string folder = @"F:\ECG work\ECG_Shared_Parameters\Samples\sample 1\f1";
//
//			// loop through all files in the directory
//
//			foreach (string filename in System.IO.Directory.GetFiles(folder))
//			{
//				try
//				{
//					doc = Application.OpenDocumentFile(filename);
//					if (doc.IsFamilyDocument)
//					{
//
//						Transaction trans = new Transaction(doc, "Remove Param");
//						trans.Start();
//						using(trans)
//						{
//							//ExternalDefinition extdef = RawFindExternalDefinition(Application.OpenSharedParameterFile(), "CompanyName");
//							FamilyManager fm = doc.FamilyManager;
//							FamilyParameter fp = RawConvertSetToList<FamilyParameter>(fm.Parameters).
//								FirstOrDefault(e => e.Definition.Name.Equals(p_name, StringComparison.CurrentCultureIgnoreCase));
//							if (fp == null) throw new Exception("Invalid ParameterName Input!");
//							if(fp.IsShared)
//							{
//								ExternalDefinition extdef = RawFindExternalDefinition(Application.OpenSharedParameterFile(), p_name);
		////									    	Guid guid = extdef.GUID;
		////									    	ParameterType type = extdef.ParameterType;
		////									    	string group_name = extdef.ParameterGroup.ToString();
//
//								//Create_SP(new_name,type,group_name);
//
		////									    	fm.ReplaceParameter(fp,"temp_test",BuiltInParameterGroup.PG_TEXT,true);
		////									    	FamilyParameter new_fp = RawConvertSetToList<FamilyParameter>(fm.Parameters).
		////									        FirstOrDefault(e => e.Definition.Name.Equals("temp_test", StringComparison.CurrentCultureIgnoreCase));
		////									    	ExternalDefinition new_extdef = RawFindExternalDefinition(Application.OpenSharedParameterFile(), new_name);
		////									    	fm.ReplaceParameter(new_fp,new_extdef,new_extdef.ParameterGroup,true);
//							}
//							trans.Commit();
//							//		doc.SaveAs(filename);
//							doc.Close(true);
//						}
//					}
//
//				}
//				catch (Autodesk.Revit.Exceptions.ArgumentNullException ae)
//				{
//					TaskDialog.Show("Revit",ae.ToString());
//				//	return;
//				}
//				catch (Autodesk.Revit.Exceptions.FamilyContextException ae)
//				{
//					TaskDialog.Show("Revit",ae.ToString());
//				//	return;
//				}
//				catch (Autodesk.Revit.Exceptions.FileAccessException ae)
//				{
//					TaskDialog.Show("Revit",ae.ToString());
//				//	return;
//				}
//				catch (Autodesk.Revit.Exceptions.FileNotFoundException ae)
//				{
//					TaskDialog.Show("Revit",ae.ToString());
//				//	return;
//				}
//				catch (Autodesk.Revit.Exceptions.ApplicationException ae)
//				{
//					TaskDialog.Show("Revit",ae.ToString());
//					return;
//				}
//				catch (SystemException ae)
//				{
//					TaskDialog.Show("Revit",ae.ToString());
//					//return;
//				}
//			}
//
//			//TaskDialog.Show("Iam","Here");
//
//		}
	/*	public static string GetParameterValue(FamilyParameter p)
		{
			switch(p.StorageType)
			{
				case StorageType.Double:
					return p.AsValueString();
				case StorageType.ElementId:
					return p.AsElementId().IntegerValue.ToString();
				case StorageType.Integer:
					return p.AsValueString();
				case StorageType.None:
					return p.AsValueString();
				case StorageType.String:
					return p.AsString();
				default:
					return "";
					
			}
		}*/
	
		public static void Rename_Family_Parameters(List<string> full_files_name)
		{
//			UIDocument uidoc = this.ActiveUIDocument;
//			Document CachedDoc = uidoc.Document;
			Document doc;
			//Application app;
			//string folder = @"F:\ECG work\ECG_Shared_Parameters\Samples\sample 1\f1";
			
			// loop through all files in the directory
			
			foreach (string filename in full_files_name)
			{
				doc = CachedApp.OpenDocumentFile(filename);
				try
				{
					
					if (doc.IsFamilyDocument)
					{
						
						Transaction trans = new Transaction(doc, "Rename Param");
						
						using(trans)
						{
							//string s = Globals.new_name_for_rename;
							
								
								trans.Start();
								FamilyManager fm = doc.FamilyManager;
								FamilyParameter fp = RawConvertSetToList<FamilyParameter>(fm.Parameters).
									FirstOrDefault(e => e.Definition.Name.Equals(Globals.parm_to_Replace, StringComparison.CurrentCultureIgnoreCase));
								//		TaskDialog.Show("Revit","Shared Parameter !!");
								trans.Commit();
								if(fp.IsShared)
								{
									
							//		TaskDialog.Show("Revit",fm.Types.Size.ToString());
								//	Element e = FilteredElementCollector(doc).OfClass(fm.CurrentType);
									
								//	Parameter p = fm.Parameter(fp.Definition);
//									if (fp == null) throw new Exception("Invalid ParameterName Input!");
//									string tmp = "Parameter name: "+ fp.Definition.Name + "\n" +"Is Shared!";
									
									//TaskDialog.Show("Warrning",tmp);
									ExternalDefinition edf;
									//	if(!Globals.all_SP_variables.Contains(fp.Definition.Name))
									edf = Create_SP(Globals.new_name_for_rename,
									                Globals.new_type,
									                Globals.new_group);
									
									trans.Start();
									fm.AddParameter(edf,edf.ParameterGroup,Globals.instance_or_not);
									//	fm.Parameter(edf.Name).Set(fp.ToString());
									
									
									FamilyParameter fp_tmp = fm.get_Parameter(Globals.new_name_for_rename);
									foreach( FamilyType t in fm.Types )
									{
										if(t.HasValue(fp))
										{
											//TaskDialog.Show("R","Here");
											fm.CurrentType = t;
											if(fp_tmp.StorageType == StorageType.Double)
												fm.Set(fp_tmp,t.AsDouble(fp).Value);
											else if(fp_tmp.StorageType == StorageType.Integer)
												fm.Set(fp_tmp,t.AsInteger(fp).Value);
											else if(fp_tmp.StorageType == StorageType.ElementId)
												fm.Set(fp_tmp,t.AsElementId(fp).IntegerValue);
											else if(fp_tmp.StorageType == StorageType.String)
												fm.Set(fp_tmp,t.AsString(fp));
										}
										// TaskDialog.Show("R",); 
									}
									//fm.Types
									trans.Commit();
									
									trans.Start();
									fm.RemoveParameter(fp);
									trans.Commit();
								//	string k = "Parameter name: "+ edf.Name + "\n" +"Is Shared!";
									
									//	TaskDialog.Show("Warrning",k);
									
									//	fm.AddParameter();
									//		rename_in_shared_parm_file(fp.Definition.Name,Globals.new_name_for_rename);
									
									//doc.Close(false);
									continue;
								}
//								if (fp == null) throw new Exception("Invalid ParameterName Input!");
//								fm.RenameParameter(fp,new_name);
								// 		Test();
								trans.Commit();
							
							//ExternalDefinition extdef = RawFindExternalDefinition(Application.OpenSharedParameterFile(), "CompanyName");
//										FamilyManager fm = doc.FamilyManager;
//										FamilyParameter fp = RawConvertSetToList<FamilyParameter>(fm.Parameters).
//									        FirstOrDefault(e => e.Definition.Name.Equals(p_name, StringComparison.CurrentCultureIgnoreCase));
//									    if (fp == null) throw new Exception("Invalid ParameterName Input!");
//									    fm.RenameParameter(fp,new_name);
							// 										trans.Commit();
							//		doc.SaveAs(filename);
							doc.Close(true);
						}
					}
					
				}
				catch (Autodesk.Revit.Exceptions.ArgumentNullException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					//trans.Commit();
					doc.Close(false);
					//	return;
				}
				catch (Autodesk.Revit.Exceptions.FamilyContextException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//	return;
				}
				catch (Autodesk.Revit.Exceptions.FileAccessException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//	return;
				}
				catch (Autodesk.Revit.Exceptions.FileNotFoundException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//	return;
				}
				catch (Autodesk.Revit.Exceptions.ApplicationException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//	return;
				}
				catch (SystemException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//		return;
				}
			}
			
			//TaskDialog.Show("Iam","Here");
			
		}
		
		public static void get_all_paramters(List<string> full_files_name)
		{
//			foreach(DefinitionGroup myGroup in defFile.Groups)
//			{
//				SortedSet<string> tmp = new SortedSet<string>();
//				Globals.SP_with_groups.Add(myGroup.Name,tmp);
//				foreach(Definition df in myGroup.Definitions)
//				{
//					Globals.SP_with_groups[myGroup.Name].Add(df.Name);
			////					TaskDialog.Show("Revit",df.Name);
			////					return;
//				}
//			//	TaskDialog.Show("Revit",Globals.SP_with_groups[myGroup.Name].Count.ToString());
//
//			}
			Document doc;
			Globals.files_with_SP.Clear();
			
			foreach(string filename in full_files_name)
			{
				
				doc = CachedApp.OpenDocumentFile(filename);
				try{
					if (doc.IsFamilyDocument)
					{
						SortedSet<FamilyParameter> tmp = new SortedSet<FamilyParameter>(new cmp());
						FamilyManager fm = doc.FamilyManager;
						FamilyParameterSet fmSet = fm.Parameters;
						Globals.files_with_SP.Add(filename,tmp);
						foreach(FamilyParameter fp in fmSet)
						{
							if(fp.IsShared)
							{
								Globals.files_with_SP[filename].Add(fp);
								//tmp.Add(fp.Definition.Name);
							}
							
							//fp.Definition.
							//TaskDialog.Show("Revit",fp.Definition.Name);
						}
					}
				}
				catch(System.Exception ea)
				{
					TaskDialog.Show("Revit",ea.Message.ToString());
				}
			}
			
		}
		public static int count(List<string> lst, string f , int idx)
		{
			int cnt=0;
			foreach(string ss in lst)
			{
				if(lst[idx] == ss)
					cnt++;
			}
			return cnt;
		}
		public static void Remove_Parameter(List<string> full_files_name,List<string> parm_to_add)
		{
//			UIDocument uidoc = this.ActiveUIDocument;
//			Document CachedDoc = uidoc.Document;
			Document doc;
			//Application app;
			//string folder = @"F:\ECG work\ECG_Shared_Parameters\Samples\sample 1\f1";
			
			// loop through all files in the directory
			
			foreach (string filename in full_files_name)
			{
				doc = CachedApp.OpenDocumentFile(filename);
				try
				{
					
					
					if (doc.IsFamilyDocument)
					{
						FamilyManager fm = doc.FamilyManager;
						Transaction trans = new Transaction(doc, "Remove Param");
						
						using(trans)
						{
							//ExternalDefinition extdef = RawFindExternalDefinition(Application.OpenSharedParameterFile(), "CompanyName");
							
							foreach(string s in parm_to_add)
							{
								trans.Start();
								FamilyParameter fp = RawConvertSetToList<FamilyParameter>(fm.Parameters).
									FirstOrDefault(e => e.Definition.Name.Equals(s, StringComparison.CurrentCultureIgnoreCase));
								//		TaskDialog.Show("Revit","Shared Parameter !!");
								
								if (fp == null) throw new Exception("Invalid ParameterName Input!");
								fm.RemoveParameter(fp);
								trans.Commit();
							}
							//		doc.SaveAs(filename);
							doc.Close(true);
						}
					}
					
				}
				catch (Autodesk.Revit.Exceptions.ArgumentNullException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					//trans.Commit();
					doc.Close(false);
					//	return;
				}
				catch (Autodesk.Revit.Exceptions.FamilyContextException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//	return;
				}
				catch (Autodesk.Revit.Exceptions.FileAccessException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//	return;
				}
				catch (Autodesk.Revit.Exceptions.FileNotFoundException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//	return;
				}
				catch (Autodesk.Revit.Exceptions.ApplicationException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//	return;
				}
				catch (SystemException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//		return;
				}
			}
			
			//TaskDialog.Show("Iam","Here");
			
		}
		public static void Add_SP_To_Families(List<string> full_files_name,List<string> parm_to_add,bool instance)
		{
			Document doc;
			//Application app;
			//string folder = @"F:\ECG work\ECG_Shared_Parameters\Samples\sample 1\f1";
			
			// loop through all files in the directory
			//	TaskDialog.Show("Revit","Hey");
			foreach (string filename in full_files_name)
			{
				doc = CachedApp.OpenDocumentFile(filename);
				try
				{
					
					if (doc.IsFamilyDocument)
					{
						
						Transaction trans = new Transaction(doc, "Add Param");
						
						using(trans)
						{
							FamilyManager fm = doc.FamilyManager;
							DefinitionFile deFile = CachedApp.OpenSharedParameterFile();
							DefinitionGroups myGroups = deFile.Groups;
							
							foreach(string s in parm_to_add)
							{
								foreach(DefinitionGroup myGroup in myGroups)
								{  // DefinitionGroup myGroup = myGroups.get_Item("New len");
									trans.Start();
									Definitions myDefinitions = myGroup.Definitions;
									//TaskDialog.Show("Revit",s);
									ExternalDefinition eDef = myDefinitions.get_Item(s) as ExternalDefinition;
									if(eDef != null)
									{fm.AddParameter(eDef,eDef.ParameterGroup,instance);}
									//TaskDialog.Show("Revit","Hey");
									trans.Commit();
									//	if(eDef != null) break;
									
								}
							}
							
						}
						
						//		doc.SaveAs(filename);
						doc.Close(true);
						//int tmp = Form1.progressBar1.Maximum;
						//	int c = tmp/Globals.full_files_name_variables.Count;
						//Form1.progressBar1.Increment(c);
					}
					
				}
				catch (Autodesk.Revit.Exceptions.ArgumentNullException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					//trans.Commit();
					doc.Close(false);
					//	return;
				}
				catch (Autodesk.Revit.Exceptions.FamilyContextException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//	return;
				}
				catch (Autodesk.Revit.Exceptions.FileAccessException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//	return;
				}
				catch (Autodesk.Revit.Exceptions.FileNotFoundException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//	return;
				}
				catch (Autodesk.Revit.Exceptions.ApplicationException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//	return;
				}
				catch (SystemException ae)
				{
					TaskDialog.Show("Revit",ae.ToString());
					doc.Close(false);
					//		return;
				}
			}
			
		}
		public void Add_SP_to_Cat()
		{
			//UIApplication uiapp = this.Application;
			UIDocument uidoc = this.ActiveUIDocument;
			Document doc = uidoc.Document;
			//Application app = this.
			
			Category cat =  doc.Settings.Categories.get_Item(BuiltInCategory.OST_Walls);
			CategorySet cs = Application.Create.NewCategorySet();
			cs.Insert(cat);
			
			DefinitionFile deFile = Application.OpenSharedParameterFile();
			foreach (DefinitionGroup dg in deFile.Groups) {
				if(dg.Name == "New len")
				{
					ExternalDefinition exDef = dg.Definitions.get_Item("CompanyName") as ExternalDefinition;
					using(Transaction t = new Transaction(doc))
					{
						t.Start("Add Shared Parameters");
						InstanceBinding newIB = Application.Create.NewInstanceBinding(cs);
						doc.ParameterBindings.Insert(exDef,newIB,BuiltInParameterGroup.PG_TEXT);
						t.Commit();
					}
				}
			}
			
		}
		public static void Test()
		{
			TaskDialog.Show("Revit","Hello World!");
		}
		public static ExternalDefinition Create_SP(string sp_name
		                                           ,ParameterType type,string group_name)
		{
			DefinitionFile deFile = CachedApp.OpenSharedParameterFile();
			// create new group in the shared paramters files
			DefinitionGroups groups = deFile.Groups;
			bool founded = false;
			Definition myDefinition = groups.First().Definitions.First();
			foreach (DefinitionGroup dg in groups) {
				if(dg.Name == group_name)
				{
					ExternalDefinition exDef = dg.Definitions.get_Item(sp_name) as ExternalDefinition;
					if(exDef != null)
					{
						//exDef.Description
						return exDef;
					}
					//ExternalDefinition exDef = dg.Definitions.get_Item("CompanyName") as ExternalDefinition;
					ExternalDefinitionCreationOptions option = new ExternalDefinitionCreationOptions(sp_name, type);
					
					myDefinition = dg.Definitions.Create(option);
					
					founded = true;
					break;
				}
			}
			if(!founded)
			{
				DefinitionGroup myGroup = groups.Create(group_name);
				// Create a type definition
				ExternalDefinitionCreationOptions option = new ExternalDefinitionCreationOptions(sp_name, type);
				myDefinition = myGroup.Definitions.Create(option);
			}
			
			ExternalDefinition eDef = myDefinition as ExternalDefinition;
			
			return eDef;
			
			// 
			
		}
		public void View_SP()
		{
			DefinitionFile deFile = Application.OpenSharedParameterFile();
			StringBuilder fileInformation = new StringBuilder(500);
			// get the file name
			fileInformation.AppendLine("File Name: "+deFile.Filename);
			foreach(DefinitionGroup myGroup in deFile.Groups)
			{
				// get the group name
				fileInformation.AppendLine("Group Name: "+ myGroup.Name);
				// interator the definations
				foreach(Definition df in myGroup.Definitions)
				{
					fileInformation.AppendLine("Defination Name: "+ df.Name);
				}
			}
			TaskDialog.Show("Revit",fileInformation.ToString());
		}
		public static void rename_in_shared_parm_file(string s, string new_s)
		{
			DefinitionFile file = CachedApp.OpenSharedParameterFile();
			var lines = File.ReadLines(file.Filename);
			
			//StringComparison comp = StringComparison.Ordinal;
			new_s = "	" + new_s + "	";
			s = "	" + s + "	";
			string tmp = "";
			string orginal = "";
			foreach (string line in lines)
			{
				if (line.Contains(s))
				{
					tmp = orginal = line;
					tmp = line.Replace(s, new_s);
					
					
					//    MessageBox.Show(tmp);
					break;
				}
			}
			string readAll = File.ReadAllText(file.Filename);
			readAll = readAll.Replace(orginal, tmp);
			using(StreamWriter writer = new StreamWriter(file.Filename))
			{
				writer.Write(readAll);
				writer.Close();
			}
			
			//MessageBox.Show(readAll);
			//File.WriteAllText(file, readAll,Encoding.UTF8);
			//	return;
		}
		#region Revit Macros generated code
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(Module_Startup);
			this.Shutdown += new System.EventHandler(Module_Shutdown);
		}
		#endregion
	}
}