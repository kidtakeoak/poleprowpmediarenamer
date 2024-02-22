namespace PoleproWpMediaRenamer
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            pbDropArea = new PictureBox();
            dgvMediaFile = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            groupBox1 = new GroupBox();
            label1 = new Label();
            tbDefineString = new TextBox();
            rbPatternD = new RadioButton();
            rbPatternC = new RadioButton();
            rbPatternB = new RadioButton();
            rbPatternA = new RadioButton();
            groupBox2 = new GroupBox();
            btnResetLog = new Button();
            chkbDeleteFile = new CheckBox();
            btnDialog = new Button();
            chkbOpenDir = new CheckBox();
            label2 = new Label();
            tbExportDir = new TextBox();
            groupBox3 = new GroupBox();
            label3 = new Label();
            btnRename = new Button();
            btnRemove = new Button();
            btnReference = new Button();
            ((System.ComponentModel.ISupportInitialize)pbDropArea).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMediaFile).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // pbDropArea
            // 
            pbDropArea.BackgroundImage = Properties.Resources.droparea;
            pbDropArea.Location = new Point(12, 20);
            pbDropArea.Name = "pbDropArea";
            pbDropArea.Size = new Size(320, 80);
            pbDropArea.TabIndex = 0;
            pbDropArea.TabStop = false;
            pbDropArea.DragDrop += pbDropArea_DragDrop;
            pbDropArea.DragEnter += pbDropArea_DragEnter;
            // 
            // dgvMediaFile
            // 
            dgvMediaFile.AllowUserToAddRows = false;
            dgvMediaFile.AllowUserToDeleteRows = false;
            dgvMediaFile.AllowUserToResizeColumns = false;
            dgvMediaFile.AllowUserToResizeRows = false;
            dgvMediaFile.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMediaFile.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            dgvMediaFile.Location = new Point(12, 106);
            dgvMediaFile.MultiSelect = false;
            dgvMediaFile.Name = "dgvMediaFile";
            dgvMediaFile.ReadOnly = true;
            dgvMediaFile.RowHeadersVisible = false;
            dgvMediaFile.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMediaFile.Size = new Size(320, 288);
            dgvMediaFile.TabIndex = 1;
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column1.HeaderText = "オリジナルファイル名";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column2.HeaderText = "リネームファイル名";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(tbDefineString);
            groupBox1.Controls.Add(rbPatternD);
            groupBox1.Controls.Add(rbPatternC);
            groupBox1.Controls.Add(rbPatternB);
            groupBox1.Controls.Add(rbPatternA);
            groupBox1.Location = new Point(356, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(416, 148);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "ファイル名パターン";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(234, 26);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 5;
            label1.Text = "） + 連番";
            // 
            // tbDefineString
            // 
            tbDefineString.Location = new Point(112, 22);
            tbDefineString.Name = "tbDefineString";
            tbDefineString.Size = new Size(120, 23);
            tbDefineString.TabIndex = 4;
            tbDefineString.TextChanged += tbDefineString_TextChanged;
            // 
            // rbPatternD
            // 
            rbPatternD.AutoSize = true;
            rbPatternD.Location = new Point(16, 114);
            rbPatternD.Name = "rbPatternD";
            rbPatternD.Size = new Size(58, 19);
            rbPatternD.TabIndex = 3;
            rbPatternD.TabStop = true;
            rbPatternD.Text = "ランダム";
            rbPatternD.UseVisualStyleBackColor = true;
            rbPatternD.CheckedChanged += rbPatternD_CheckedChanged;
            // 
            // rbPatternC
            // 
            rbPatternC.AutoSize = true;
            rbPatternC.Location = new Point(16, 84);
            rbPatternC.Name = "rbPatternC";
            rbPatternC.Size = new Size(107, 19);
            rbPatternC.TabIndex = 2;
            rbPatternC.TabStop = true;
            rbPatternC.Text = "ファイル更新時刻";
            rbPatternC.UseVisualStyleBackColor = true;
            rbPatternC.CheckedChanged += rbPatternC_CheckedChanged;
            // 
            // rbPatternB
            // 
            rbPatternB.AutoSize = true;
            rbPatternB.Location = new Point(16, 54);
            rbPatternB.Name = "rbPatternB";
            rbPatternB.Size = new Size(121, 19);
            rbPatternB.TabIndex = 1;
            rbPatternB.TabStop = true;
            rbPatternB.Text = "今日の日付 + 連番";
            rbPatternB.UseVisualStyleBackColor = true;
            rbPatternB.CheckedChanged += rbPatternB_CheckedChanged;
            // 
            // rbPatternA
            // 
            rbPatternA.AutoSize = true;
            rbPatternA.Location = new Point(16, 24);
            rbPatternA.Name = "rbPatternA";
            rbPatternA.Size = new Size(97, 19);
            rbPatternA.TabIndex = 0;
            rbPatternA.TabStop = true;
            rbPatternA.Text = "任意文字列（";
            rbPatternA.UseVisualStyleBackColor = true;
            rbPatternA.CheckedChanged += rbPatternA_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnResetLog);
            groupBox2.Controls.Add(chkbDeleteFile);
            groupBox2.Controls.Add(btnDialog);
            groupBox2.Controls.Add(chkbOpenDir);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(tbExportDir);
            groupBox2.Location = new Point(356, 252);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(416, 142);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "オプション";
            // 
            // btnResetLog
            // 
            btnResetLog.Location = new Point(314, 106);
            btnResetLog.Name = "btnResetLog";
            btnResetLog.Size = new Size(96, 23);
            btnResetLog.TabIndex = 5;
            btnResetLog.Text = "履歴リセット";
            btnResetLog.UseVisualStyleBackColor = true;
            btnResetLog.Click += btnResetLog_Click;
            // 
            // chkbDeleteFile
            // 
            chkbDeleteFile.AutoSize = true;
            chkbDeleteFile.Location = new Point(16, 108);
            chkbDeleteFile.Name = "chkbDeleteFile";
            chkbDeleteFile.Size = new Size(158, 19);
            chkbDeleteFile.TabIndex = 4;
            chkbDeleteFile.Text = "オリジナルファイルを削除する";
            chkbDeleteFile.UseVisualStyleBackColor = true;
            chkbDeleteFile.CheckedChanged += chkbDeleteFile_CheckedChanged;
            // 
            // btnDialog
            // 
            btnDialog.Location = new Point(346, 44);
            btnDialog.Name = "btnDialog";
            btnDialog.Size = new Size(64, 23);
            btnDialog.TabIndex = 3;
            btnDialog.Text = "変更";
            btnDialog.UseVisualStyleBackColor = true;
            btnDialog.Click += btnDialog_Click;
            // 
            // chkbOpenDir
            // 
            chkbOpenDir.AutoSize = true;
            chkbOpenDir.Location = new Point(16, 78);
            chkbOpenDir.Name = "chkbOpenDir";
            chkbOpenDir.Size = new Size(157, 19);
            chkbOpenDir.TabIndex = 2;
            chkbOpenDir.Text = "完了後、出力フォルダを開く";
            chkbOpenDir.UseVisualStyleBackColor = true;
            chkbOpenDir.CheckedChanged += chkbOpenDir_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 26);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 1;
            label2.Text = "出力フォルダ";
            // 
            // tbExportDir
            // 
            tbExportDir.Enabled = false;
            tbExportDir.Location = new Point(16, 44);
            tbExportDir.Name = "tbExportDir";
            tbExportDir.Size = new Size(324, 23);
            tbExportDir.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label3);
            groupBox3.Location = new Point(356, 176);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(416, 60);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "ファイル名サンプル";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 26);
            label3.Name = "label3";
            label3.Size = new Size(66, 15);
            label3.TabIndex = 2;
            label3.Text = "出力フォルダ";
            // 
            // btnRename
            // 
            btnRename.Location = new Point(644, 406);
            btnRename.Name = "btnRename";
            btnRename.Size = new Size(128, 23);
            btnRename.TabIndex = 5;
            btnRename.Text = "リネーム実行";
            btnRename.UseVisualStyleBackColor = true;
            btnRename.Click += btnRename_Click;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(204, 406);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(128, 23);
            btnRemove.TabIndex = 6;
            btnRemove.Text = "選択ファイルを除く";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnReference
            // 
            btnReference.Location = new Point(12, 406);
            btnReference.Name = "btnReference";
            btnReference.Size = new Size(128, 23);
            btnReference.TabIndex = 7;
            btnReference.Text = "公式リファレンス";
            btnReference.UseVisualStyleBackColor = true;
            btnReference.Click += btnReference_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 441);
            Controls.Add(btnReference);
            Controls.Add(btnRemove);
            Controls.Add(btnRename);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(dgvMediaFile);
            Controls.Add(pbDropArea);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WPメディア命名くん Ver.1.0";
            FormClosing += FormMain_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pbDropArea).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMediaFile).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbDropArea;
        private DataGridView dgvMediaFile;
        private GroupBox groupBox1;
        private RadioButton rbPatternD;
        private RadioButton rbPatternC;
        private RadioButton rbPatternB;
        private RadioButton rbPatternA;
        private Label label1;
        private TextBox tbDefineString;
        private GroupBox groupBox2;
        private Button btnDialog;
        private CheckBox chkbOpenDir;
        private Label label2;
        private TextBox tbExportDir;
        private CheckBox chkbDeleteFile;
        private GroupBox groupBox3;
        private Label label3;
        private Button btnRename;
        private Button btnRemove;
        private Button btnResetLog;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private Button btnReference;
    }
}
