namespace D360
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.label1 = new System.Windows.Forms.Label();
            this.LeftTriggerComboBox = new System.Windows.Forms.ComboBox();
            this.RightTriggerLabel = new System.Windows.Forms.Label();
            this.RightTriggerComboBox = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveAndCloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Left Trigger";
            // 
            // LeftTriggerComboBox
            // 
            this.LeftTriggerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LeftTriggerComboBox.FormattingEnabled = true;
            this.LeftTriggerComboBox.Items.AddRange(new object[] {
            "Action Bar Skill 1",
            "Action Bar Skill 2",
            "Action Bar Skill 3",
            "Action Bar Skill 4",
            "Inventory",
            "Map",
            "Potion",
            "Town Portal"});
            this.LeftTriggerComboBox.Location = new System.Drawing.Point(86, 6);
            this.LeftTriggerComboBox.Name = "LeftTriggerComboBox";
            this.LeftTriggerComboBox.Size = new System.Drawing.Size(186, 21);
            this.LeftTriggerComboBox.TabIndex = 1;
            this.LeftTriggerComboBox.SelectedIndexChanged += new System.EventHandler(this.LeftTriggerComboBox_SelectedIndexChanged);
            // 
            // RightTriggerLabel
            // 
            this.RightTriggerLabel.AutoSize = true;
            this.RightTriggerLabel.Location = new System.Drawing.Point(12, 36);
            this.RightTriggerLabel.Name = "RightTriggerLabel";
            this.RightTriggerLabel.Size = new System.Drawing.Size(68, 13);
            this.RightTriggerLabel.TabIndex = 2;
            this.RightTriggerLabel.Text = "Right Trigger";
            // 
            // RightTriggerComboBox
            // 
            this.RightTriggerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RightTriggerComboBox.FormattingEnabled = true;
            this.RightTriggerComboBox.Items.AddRange(new object[] {
            "Action Bar Skill 1",
            "Action Bar Skill 2",
            "Action Bar Skill 3",
            "Action Bar Skill 4",
            "Inventory",
            "Map",
            "Potion",
            "Town Portal"});
            this.RightTriggerComboBox.Location = new System.Drawing.Point(86, 33);
            this.RightTriggerComboBox.Name = "RightTriggerComboBox";
            this.RightTriggerComboBox.Size = new System.Drawing.Size(186, 21);
            this.RightTriggerComboBox.TabIndex = 3;
            this.RightTriggerComboBox.SelectedIndexChanged += new System.EventHandler(this.RightTriggerComboBox_SelectedIndexChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(42, 73);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(93, 23);
            this.cancelButton.TabIndex = 25;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveAndCloseButton
            // 
            this.saveAndCloseButton.Location = new System.Drawing.Point(153, 73);
            this.saveAndCloseButton.Name = "saveAndCloseButton";
            this.saveAndCloseButton.Size = new System.Drawing.Size(93, 23);
            this.saveAndCloseButton.TabIndex = 24;
            this.saveAndCloseButton.Text = "Save and Close";
            this.saveAndCloseButton.UseVisualStyleBackColor = true;
            this.saveAndCloseButton.Click += new System.EventHandler(this.saveAndCloseButton_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 108);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveAndCloseButton);
            this.Controls.Add(this.RightTriggerComboBox);
            this.Controls.Add(this.RightTriggerLabel);
            this.Controls.Add(this.LeftTriggerComboBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "D360 Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.ConfigForm_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox LeftTriggerComboBox;
        private System.Windows.Forms.Label RightTriggerLabel;
        private System.Windows.Forms.ComboBox RightTriggerComboBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveAndCloseButton;
    }
}