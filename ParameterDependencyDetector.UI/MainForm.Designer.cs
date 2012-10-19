namespace ParameterDependencyDetector.UI
{
    partial class MainForm
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
            this.uxStoredProcedures = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uxDetect = new System.Windows.Forms.Button();
            this.uxGrid = new System.Windows.Forms.DataGridView();
            this.uxProgress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.uxGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // uxStoredProcedures
            // 
            this.uxStoredProcedures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxStoredProcedures.FormattingEnabled = true;
            this.uxStoredProcedures.Location = new System.Drawing.Point(122, 12);
            this.uxStoredProcedures.Name = "uxStoredProcedures";
            this.uxStoredProcedures.Size = new System.Drawing.Size(517, 21);
            this.uxStoredProcedures.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Procedure to detect:";
            // 
            // uxDetect
            // 
            this.uxDetect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxDetect.Location = new System.Drawing.Point(645, 12);
            this.uxDetect.Name = "uxDetect";
            this.uxDetect.Size = new System.Drawing.Size(75, 23);
            this.uxDetect.TabIndex = 2;
            this.uxDetect.Text = "Detect";
            this.uxDetect.UseVisualStyleBackColor = true;
            this.uxDetect.Click += new System.EventHandler(this.DetectClick);
            // 
            // uxGrid
            // 
            this.uxGrid.AllowUserToAddRows = false;
            this.uxGrid.AllowUserToDeleteRows = false;
            this.uxGrid.AllowUserToOrderColumns = true;
            this.uxGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.uxGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uxGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.uxGrid.Location = new System.Drawing.Point(12, 49);
            this.uxGrid.Name = "uxGrid";
            this.uxGrid.ReadOnly = true;
            this.uxGrid.ShowCellErrors = false;
            this.uxGrid.ShowCellToolTips = false;
            this.uxGrid.ShowEditingIcon = false;
            this.uxGrid.ShowRowErrors = false;
            this.uxGrid.Size = new System.Drawing.Size(708, 357);
            this.uxGrid.TabIndex = 3;
            // 
            // uxProgress
            // 
            this.uxProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uxProgress.Location = new System.Drawing.Point(12, 207);
            this.uxProgress.Name = "uxProgress";
            this.uxProgress.Size = new System.Drawing.Size(708, 23);
            this.uxProgress.TabIndex = 4;
            this.uxProgress.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 418);
            this.Controls.Add(this.uxProgress);
            this.Controls.Add(this.uxGrid);
            this.Controls.Add(this.uxDetect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uxStoredProcedures);
            this.Name = "MainForm";
            this.Text = "Parameter Dependency Detector";
            ((System.ComponentModel.ISupportInitialize)(this.uxGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox uxStoredProcedures;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button uxDetect;
        private System.Windows.Forms.DataGridView uxGrid;
        private System.Windows.Forms.ProgressBar uxProgress;
    }
}

