namespace MqttTestCS
{
    partial class Form1
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
            this.publishButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.topicsCheckListBox = new System.Windows.Forms.CheckedListBox();
            this.firehoseButton = new System.Windows.Forms.Button();
            this.clientIdTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.subscribeCustomButton = new System.Windows.Forms.Button();
            this.customTopicTextBox = new System.Windows.Forms.TextBox();
            this.unsubscribeCustomButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // publishButton
            // 
            this.publishButton.Location = new System.Drawing.Point(12, 153);
            this.publishButton.Name = "publishButton";
            this.publishButton.Size = new System.Drawing.Size(75, 23);
            this.publishButton.TabIndex = 4;
            this.publishButton.Text = "&Publish";
            this.publishButton.UseVisualStyleBackColor = true;
            this.publishButton.Click += new System.EventHandler(this.publishButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(93, 155);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(406, 20);
            this.messageTextBox.TabIndex = 5;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(402, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(97, 23);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "&Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // logTextBox
            // 
            this.logTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logTextBox.Location = new System.Drawing.Point(12, 181);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(487, 280);
            this.logTextBox.TabIndex = 7;
            // 
            // topicsCheckListBox
            // 
            this.topicsCheckListBox.FormattingEnabled = true;
            this.topicsCheckListBox.Items.AddRange(new object[] {
            "topic1",
            "topic1/sub1",
            "topic1/sub2",
            "topic1/sub3",
            "topic2",
            "topic3",
            "topic3/sub1"});
            this.topicsCheckListBox.Location = new System.Drawing.Point(12, 38);
            this.topicsCheckListBox.Name = "topicsCheckListBox";
            this.topicsCheckListBox.Size = new System.Drawing.Size(123, 109);
            this.topicsCheckListBox.TabIndex = 3;
            this.topicsCheckListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.topicsCheckListBox_ItemCheck);
            // 
            // firehoseButton
            // 
            this.firehoseButton.Location = new System.Drawing.Point(402, 41);
            this.firehoseButton.Name = "firehoseButton";
            this.firehoseButton.Size = new System.Drawing.Size(97, 23);
            this.firehoseButton.TabIndex = 6;
            this.firehoseButton.Text = "Start &Firehose";
            this.firehoseButton.UseVisualStyleBackColor = true;
            this.firehoseButton.Click += new System.EventHandler(this.firehosePublishButton_Click);
            // 
            // clientIdTextBox
            // 
            this.clientIdTextBox.Location = new System.Drawing.Point(66, 12);
            this.clientIdTextBox.Name = "clientIdTextBox";
            this.clientIdTextBox.Size = new System.Drawing.Size(324, 20);
            this.clientIdTextBox.TabIndex = 1;
            this.clientIdTextBox.Text = "(auto)";
            this.clientIdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Client &Id:";
            // 
            // subscribeCustomButton
            // 
            this.subscribeCustomButton.Location = new System.Drawing.Point(13, 45);
            this.subscribeCustomButton.Name = "subscribeCustomButton";
            this.subscribeCustomButton.Size = new System.Drawing.Size(100, 23);
            this.subscribeCustomButton.TabIndex = 8;
            this.subscribeCustomButton.Text = "&Subscribe Custom";
            this.subscribeCustomButton.UseVisualStyleBackColor = true;
            this.subscribeCustomButton.Click += new System.EventHandler(this.subscribeCustomButton_Click);
            // 
            // customTopicTextBox
            // 
            this.customTopicTextBox.Location = new System.Drawing.Point(13, 19);
            this.customTopicTextBox.Name = "customTopicTextBox";
            this.customTopicTextBox.Size = new System.Drawing.Size(236, 20);
            this.customTopicTextBox.TabIndex = 9;
            this.customTopicTextBox.Text = "+/sub1";
            // 
            // unsubscribeCustomButton
            // 
            this.unsubscribeCustomButton.Location = new System.Drawing.Point(149, 45);
            this.unsubscribeCustomButton.Name = "unsubscribeCustomButton";
            this.unsubscribeCustomButton.Size = new System.Drawing.Size(100, 23);
            this.unsubscribeCustomButton.TabIndex = 10;
            this.unsubscribeCustomButton.Text = "&Unsubscribe";
            this.unsubscribeCustomButton.UseVisualStyleBackColor = true;
            this.unsubscribeCustomButton.Click += new System.EventHandler(this.unsubscribeCustomButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.subscribeCustomButton);
            this.groupBox1.Controls.Add(this.customTopicTextBox);
            this.groupBox1.Controls.Add(this.unsubscribeCustomButton);
            this.groupBox1.Location = new System.Drawing.Point(141, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 78);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "&Custom Topic";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 473);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clientIdTextBox);
            this.Controls.Add(this.firehoseButton);
            this.Controls.Add(this.topicsCheckListBox);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.publishButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button publishButton;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.CheckedListBox topicsCheckListBox;
        private System.Windows.Forms.Button firehoseButton;
        private System.Windows.Forms.TextBox clientIdTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button subscribeCustomButton;
        private System.Windows.Forms.TextBox customTopicTextBox;
        private System.Windows.Forms.Button unsubscribeCustomButton;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

