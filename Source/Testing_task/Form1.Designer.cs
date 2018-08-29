/*
 * Created by SharpDevelop.
 * User: Taha
 * Date: 4/24/2018
 * Time: 5:40 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Testing_task
{
	partial class Form1
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button10 = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.button9 = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.triStateTreeView1 = new TriStateTreeView();
			this.to_remove = new System.Windows.Forms.Button();
			this.to_add = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.lblCount = new System.Windows.Forms.Label();
			this.cbSPtype = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.cbDesc = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtGUID = new System.Windows.Forms.TextBox();
			this.cbGroup = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.ckbInstance = new System.Windows.Forms.CheckBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.triStateTreeView3 = new TriStateTreeView();
			this.button4 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.button5 = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(608, 50);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(107, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Browser by file/s";
			this.button1.UseCompatibleTextRendering = true;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(16, 52);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(402, 20);
			this.textBox1.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.ItemSize = new System.Drawing.Size(10, 18);
			this.tabControl1.Location = new System.Drawing.Point(12, 90);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(706, 320);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button10);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.textBox3);
			this.tabPage1.Controls.Add(this.button9);
			this.tabPage1.Controls.Add(this.listBox1);
			this.tabPage1.Controls.Add(this.triStateTreeView1);
			this.tabPage1.Controls.Add(this.to_remove);
			this.tabPage1.Controls.Add(this.to_add);
			this.tabPage1.Controls.Add(this.checkBox1);
			this.tabPage1.Controls.Add(this.btnAdd);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(698, 294);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Add";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// button10
			// 
			this.button10.Location = new System.Drawing.Point(254, 251);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(32, 23);
			this.button10.TabIndex = 12;
			this.button10.Text = "Ch.";
			this.button10.UseCompatibleTextRendering = true;
			this.button10.UseVisualStyleBackColor = true;
			this.button10.Click += new System.EventHandler(this.Button10Click);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(540, 70);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(152, 23);
			this.label6.TabIndex = 11;
			this.label6.Text = "0 SP/s";
			this.label6.UseCompatibleTextRendering = true;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(540, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 23);
			this.label5.TabIndex = 10;
			this.label5.Text = "Search:";
			this.label5.UseCompatibleTextRendering = true;
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(540, 32);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(152, 20);
			this.textBox3.TabIndex = 9;
			this.textBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox3KeyDown);
			// 
			// button9
			// 
			this.button9.Location = new System.Drawing.Point(254, 177);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(32, 23);
			this.button9.TabIndex = 8;
			this.button9.Text = "Ex.";
			this.button9.UseCompatibleTextRendering = true;
			this.button9.UseVisualStyleBackColor = true;
			this.button9.Click += new System.EventHandler(this.Button9Click);
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(309, 8);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(225, 277);
			this.listBox1.Sorted = true;
			this.listBox1.TabIndex = 7;
			// 
			// triStateTreeView1
			// 
			this.triStateTreeView1.Location = new System.Drawing.Point(11, 8);
			this.triStateTreeView1.Name = "triStateTreeView1";
			this.triStateTreeView1.Size = new System.Drawing.Size(225, 277);
			this.triStateTreeView1.TabIndex = 6;
			this.triStateTreeView1.TriStateStyleProperty = TriStateTreeView.TriStateStyles.Standard;
			this.triStateTreeView1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TriStateTreeView1AfterCollapse);
			this.triStateTreeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TriStateTreeView1AfterExpand);
			// 
			// to_remove
			// 
			this.to_remove.Location = new System.Drawing.Point(254, 104);
			this.to_remove.Name = "to_remove";
			this.to_remove.Size = new System.Drawing.Size(32, 23);
			this.to_remove.TabIndex = 5;
			this.to_remove.Text = "<=";
			this.to_remove.UseCompatibleTextRendering = true;
			this.to_remove.UseVisualStyleBackColor = true;
			this.to_remove.Click += new System.EventHandler(this.To_removeClick);
			// 
			// to_add
			// 
			this.to_add.Location = new System.Drawing.Point(254, 25);
			this.to_add.Name = "to_add";
			this.to_add.Size = new System.Drawing.Size(32, 23);
			this.to_add.TabIndex = 4;
			this.to_add.Text = "=>";
			this.to_add.UseCompatibleTextRendering = true;
			this.to_add.UseVisualStyleBackColor = true;
			this.to_add.Click += new System.EventHandler(this.To_addClick);
			// 
			// checkBox1
			// 
			this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkBox1.Location = new System.Drawing.Point(575, 211);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(94, 24);
			this.checkBox1.TabIndex = 3;
			this.checkBox1.Text = "is instance";
			this.checkBox1.UseCompatibleTextRendering = true;
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(544, 243);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(148, 37);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseCompatibleTextRendering = true;
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.Button2Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.lblCount);
			this.tabPage2.Controls.Add(this.cbSPtype);
			this.tabPage2.Controls.Add(this.label8);
			this.tabPage2.Controls.Add(this.treeView1);
			this.tabPage2.Controls.Add(this.cbDesc);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.txtGUID);
			this.tabPage2.Controls.Add(this.cbGroup);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Controls.Add(this.btnUpdate);
			this.tabPage2.Controls.Add(this.ckbInstance);
			this.tabPage2.Controls.Add(this.txtName);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(698, 294);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Update";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// lblCount
			// 
			this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCount.Location = new System.Drawing.Point(433, 111);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(259, 21);
			this.lblCount.TabIndex = 27;
			this.lblCount.UseCompatibleTextRendering = true;
			// 
			// cbSPtype
			// 
			this.cbSPtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSPtype.FormattingEnabled = true;
			this.cbSPtype.Location = new System.Drawing.Point(433, 180);
			this.cbSPtype.Name = "cbSPtype";
			this.cbSPtype.Size = new System.Drawing.Size(141, 21);
			this.cbSPtype.Sorted = true;
			this.cbSPtype.TabIndex = 26;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(430, 149);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 16);
			this.label8.TabIndex = 25;
			this.label8.Text = "SP type:";
			this.label8.UseCompatibleTextRendering = true;
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(11, 8);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(225, 277);
			this.treeView1.TabIndex = 24;
			this.treeView1.DoubleClick += new System.EventHandler(this.TreeView1DoubleClick);
			// 
			// cbDesc
			// 
			this.cbDesc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDesc.FormattingEnabled = true;
			this.cbDesc.Location = new System.Drawing.Point(276, 180);
			this.cbDesc.Name = "cbDesc";
			this.cbDesc.Size = new System.Drawing.Size(141, 21);
			this.cbDesc.Sorted = true;
			this.cbDesc.TabIndex = 21;
			this.cbDesc.SelectedIndexChanged += new System.EventHandler(this.CbDescSelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(273, 149);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 16);
			this.label7.TabIndex = 20;
			this.label7.Text = "Discipline:";
			this.label7.UseCompatibleTextRendering = true;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(430, 19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(218, 16);
			this.label4.TabIndex = 19;
			this.label4.Text = "GUID";
			this.label4.UseCompatibleTextRendering = true;
			// 
			// txtGUID
			// 
			this.txtGUID.Enabled = false;
			this.txtGUID.Location = new System.Drawing.Point(433, 43);
			this.txtGUID.Name = "txtGUID";
			this.txtGUID.Size = new System.Drawing.Size(259, 20);
			this.txtGUID.TabIndex = 18;
			// 
			// cbGroup
			// 
			this.cbGroup.FormattingEnabled = true;
			this.cbGroup.Location = new System.Drawing.Point(276, 111);
			this.cbGroup.Name = "cbGroup";
			this.cbGroup.Size = new System.Drawing.Size(141, 21);
			this.cbGroup.Sorted = true;
			this.cbGroup.TabIndex = 17;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(273, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 16;
			this.label2.Text = "Group:";
			this.label2.UseCompatibleTextRendering = true;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(273, 19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 12;
			this.label3.Text = "Name:";
			this.label3.UseCompatibleTextRendering = true;
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(544, 243);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(148, 37);
			this.btnUpdate.TabIndex = 10;
			this.btnUpdate.Text = "Update";
			this.btnUpdate.UseCompatibleTextRendering = true;
			this.btnUpdate.UseVisualStyleBackColor = true;
			this.btnUpdate.Click += new System.EventHandler(this.Button3Click);
			// 
			// ckbInstance
			// 
			this.ckbInstance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ckbInstance.Location = new System.Drawing.Point(276, 249);
			this.ckbInstance.Name = "ckbInstance";
			this.ckbInstance.Size = new System.Drawing.Size(104, 24);
			this.ckbInstance.TabIndex = 9;
			this.ckbInstance.Text = "is instance";
			this.ckbInstance.UseCompatibleTextRendering = true;
			this.ckbInstance.UseVisualStyleBackColor = true;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(276, 43);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(141, 20);
			this.txtName.TabIndex = 8;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.triStateTreeView3);
			this.tabPage3.Controls.Add(this.button4);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(698, 294);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Remove";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// triStateTreeView3
			// 
			this.triStateTreeView3.Location = new System.Drawing.Point(11, 8);
			this.triStateTreeView3.Name = "triStateTreeView3";
			this.triStateTreeView3.Size = new System.Drawing.Size(225, 277);
			this.triStateTreeView3.TabIndex = 15;
			this.triStateTreeView3.TriStateStyleProperty = TriStateTreeView.TriStateStyles.Standard;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(544, 243);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(148, 37);
			this.button4.TabIndex = 11;
			this.button4.Text = "Remove";
			this.button4.UseCompatibleTextRendering = true;
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(16, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(145, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "You Selected 0 File/s";
			this.label1.UseCompatibleTextRendering = true;
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(12, 445);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(111, 20);
			this.button5.TabIndex = 3;
			this.button5.Text = "delete backups";
			this.button5.UseCompatibleTextRendering = true;
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.Button5Click);
			// 
			// btnRefresh
			// 
			this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnRefresh.Location = new System.Drawing.Point(607, 445);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(111, 20);
			this.btnRefresh.TabIndex = 4;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseCompatibleTextRendering = true;
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.Button6Click);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(483, 50);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(107, 23);
			this.button7.TabIndex = 5;
			this.button7.Text = "Browser by dir./s";
			this.button7.UseCompatibleTextRendering = true;
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.Button7Click);
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(311, 12);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(107, 23);
			this.button8.TabIndex = 6;
			this.button8.Text = "Show/Edit files";
			this.button8.UseCompatibleTextRendering = true;
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new System.EventHandler(this.Button8Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(14, 417);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(703, 20);
			this.progressBar1.TabIndex = 7;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(732, 473);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "SP Management";
			this.TopMost = true;
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox cbSPtype;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cbDesc;
		private TriStateTreeView triStateTreeView3;
		private System.Windows.Forms.ComboBox cbGroup;
		private System.Windows.Forms.TextBox txtGUID;
		private System.Windows.Forms.ProgressBar progressBar1;
		//private System.Windows.Forms.ProgressBar progressBar1;
		
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.ListBox listBox1;
		private TriStateTreeView triStateTreeView1;
		private System.Windows.Forms.Button to_add;
		private System.Windows.Forms.Button to_remove;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.CheckBox ckbInstance;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		
		
	}
}
