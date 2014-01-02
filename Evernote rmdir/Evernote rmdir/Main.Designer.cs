namespace Evernote_rmdir
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRun = new System.Windows.Forms.Button();
            this.chkbxDeleteAllReminders = new System.Windows.Forms.CheckBox();
            this.chkbxDaysOffset = new System.Windows.Forms.CheckBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.chkbxListNotebooks = new System.Windows.Forms.CheckedListBox();
            this.lblIncludedNotebooks = new System.Windows.Forms.Label();
            this.chkbxSelectAll = new System.Windows.Forms.CheckBox();
            this.txtbxNumDaysOffset = new System.Windows.Forms.TextBox();
            this.lblDaysAgo = new System.Windows.Forms.Label();
            this.btnCheckReminders = new System.Windows.Forms.Button();
            this.txtbxExcludedTag = new System.Windows.Forms.TextBox();
            this.chkbxExcludeTag = new System.Windows.Forms.CheckBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Enabled = false;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(164, 324);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(122, 40);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // chkbxDeleteAllReminders
            // 
            this.chkbxDeleteAllReminders.AutoSize = true;
            this.chkbxDeleteAllReminders.Checked = true;
            this.chkbxDeleteAllReminders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbxDeleteAllReminders.Location = new System.Drawing.Point(13, 13);
            this.chkbxDeleteAllReminders.Name = "chkbxDeleteAllReminders";
            this.chkbxDeleteAllReminders.Size = new System.Drawing.Size(170, 17);
            this.chkbxDeleteAllReminders.TabIndex = 1;
            this.chkbxDeleteAllReminders.Text = "Delete all completed reminders";
            this.chkbxDeleteAllReminders.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkbxDeleteAllReminders.UseVisualStyleBackColor = true;
            this.chkbxDeleteAllReminders.CheckedChanged += new System.EventHandler(this.chkbxDeleteAllReminders_CheckedChanged);
            // 
            // chkbxDaysOffset
            // 
            this.chkbxDaysOffset.AutoSize = true;
            this.chkbxDaysOffset.Location = new System.Drawing.Point(13, 36);
            this.chkbxDaysOffset.Name = "chkbxDaysOffset";
            this.chkbxDaysOffset.Size = new System.Drawing.Size(182, 17);
            this.chkbxDaysOffset.TabIndex = 2;
            this.chkbxDaysOffset.Text = "Only delete reminders completed ";
            this.chkbxDaysOffset.UseVisualStyleBackColor = true;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 371);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(298, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblStatusBar
            // 
            this.lblStatusBar.Name = "lblStatusBar";
            this.lblStatusBar.Size = new System.Drawing.Size(259, 17);
            this.lblStatusBar.Text = "Number of completed Reminders to be deleted:";
            // 
            // chkbxListNotebooks
            // 
            this.chkbxListNotebooks.CheckOnClick = true;
            this.chkbxListNotebooks.FormattingEnabled = true;
            this.chkbxListNotebooks.Location = new System.Drawing.Point(12, 134);
            this.chkbxListNotebooks.Name = "chkbxListNotebooks";
            this.chkbxListNotebooks.Size = new System.Drawing.Size(274, 184);
            this.chkbxListNotebooks.Sorted = true;
            this.chkbxListNotebooks.TabIndex = 7;
            // 
            // lblIncludedNotebooks
            // 
            this.lblIncludedNotebooks.AutoSize = true;
            this.lblIncludedNotebooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIncludedNotebooks.Location = new System.Drawing.Point(12, 84);
            this.lblIncludedNotebooks.Name = "lblIncludedNotebooks";
            this.lblIncludedNotebooks.Size = new System.Drawing.Size(132, 16);
            this.lblIncludedNotebooks.TabIndex = 8;
            this.lblIncludedNotebooks.Text = "Included Notebooks:";
            // 
            // chkbxSelectAll
            // 
            this.chkbxSelectAll.AutoSize = true;
            this.chkbxSelectAll.Checked = true;
            this.chkbxSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbxSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbxSelectAll.Location = new System.Drawing.Point(15, 109);
            this.chkbxSelectAll.Name = "chkbxSelectAll";
            this.chkbxSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkbxSelectAll.TabIndex = 9;
            this.chkbxSelectAll.Text = "Select All";
            this.chkbxSelectAll.UseVisualStyleBackColor = true;
            this.chkbxSelectAll.CheckedChanged += new System.EventHandler(this.chkbxSelectAll_CheckedChanged);
            // 
            // txtbxNumDaysOffset
            // 
            this.txtbxNumDaysOffset.Location = new System.Drawing.Point(190, 34);
            this.txtbxNumDaysOffset.MaxLength = 3;
            this.txtbxNumDaysOffset.Name = "txtbxNumDaysOffset";
            this.txtbxNumDaysOffset.Size = new System.Drawing.Size(25, 20);
            this.txtbxNumDaysOffset.TabIndex = 10;
            this.txtbxNumDaysOffset.Text = "7";
            this.txtbxNumDaysOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtbxNumDaysOffset.WordWrap = false;
            this.txtbxNumDaysOffset.TextChanged += new System.EventHandler(this.txtbxNumDaysOffset_TextChanged);
            // 
            // lblDaysAgo
            // 
            this.lblDaysAgo.AutoSize = true;
            this.lblDaysAgo.Location = new System.Drawing.Point(218, 37);
            this.lblDaysAgo.Name = "lblDaysAgo";
            this.lblDaysAgo.Size = new System.Drawing.Size(50, 13);
            this.lblDaysAgo.TabIndex = 11;
            this.lblDaysAgo.Text = "days ago";
            // 
            // btnCheckReminders
            // 
            this.btnCheckReminders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckReminders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckReminders.Location = new System.Drawing.Point(12, 324);
            this.btnCheckReminders.Name = "btnCheckReminders";
            this.btnCheckReminders.Size = new System.Drawing.Size(122, 40);
            this.btnCheckReminders.TabIndex = 12;
            this.btnCheckReminders.Text = "Check Reminders";
            this.btnCheckReminders.UseVisualStyleBackColor = true;
            this.btnCheckReminders.Click += new System.EventHandler(this.btnCheckReminders_Click);
            // 
            // txtbxExcludedTag
            // 
            this.txtbxExcludedTag.Location = new System.Drawing.Point(172, 57);
            this.txtbxExcludedTag.Name = "txtbxExcludedTag";
            this.txtbxExcludedTag.Size = new System.Drawing.Size(114, 20);
            this.txtbxExcludedTag.TabIndex = 14;
            // 
            // chkbxExcludeTag
            // 
            this.chkbxExcludeTag.AutoSize = true;
            this.chkbxExcludeTag.Location = new System.Drawing.Point(13, 59);
            this.chkbxExcludeTag.Name = "chkbxExcludeTag";
            this.chkbxExcludeTag.Size = new System.Drawing.Size(155, 17);
            this.chkbxExcludeTag.TabIndex = 15;
            this.chkbxExcludeTag.Text = "Exclude reminders with tag:";
            this.chkbxExcludeTag.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 393);
            this.Controls.Add(this.chkbxExcludeTag);
            this.Controls.Add(this.txtbxExcludedTag);
            this.Controls.Add(this.btnCheckReminders);
            this.Controls.Add(this.lblDaysAgo);
            this.Controls.Add(this.txtbxNumDaysOffset);
            this.Controls.Add(this.chkbxSelectAll);
            this.Controls.Add(this.lblIncludedNotebooks);
            this.Controls.Add(this.chkbxListNotebooks);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.chkbxDaysOffset);
            this.Controls.Add(this.chkbxDeleteAllReminders);
            this.Controls.Add(this.btnRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Evernote rmdir";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.CheckBox chkbxDeleteAllReminders;
        private System.Windows.Forms.CheckBox chkbxDaysOffset;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusBar;
        private System.Windows.Forms.CheckedListBox chkbxListNotebooks;
        private System.Windows.Forms.Label lblIncludedNotebooks;
        private System.Windows.Forms.CheckBox chkbxSelectAll;
        private System.Windows.Forms.TextBox txtbxNumDaysOffset;
        private System.Windows.Forms.Label lblDaysAgo;
        private System.Windows.Forms.Button btnCheckReminders;
        private System.Windows.Forms.TextBox txtbxExcludedTag;
        private System.Windows.Forms.CheckBox chkbxExcludeTag;
    }
}