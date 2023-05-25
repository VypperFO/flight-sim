namespace Tp_02_02
{
    partial class FormSimulator
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
            panel1 = new Panel();
            label4 = new Label();
            label1 = new Label();
            openFileDialog1 = new OpenFileDialog();
            button1 = new Button();
            button2 = new Button();
            listBox1 = new ListBox();
            label2 = new Label();
            label3 = new Label();
            listBox2 = new ListBox();
            label5 = new Label();
            listBox3 = new ListBox();
            button3 = new Button();
            button4 = new Button();
            label6 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.Fortnite_Map_Season_1__2_;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(label4);
            panel1.Location = new Point(14, 228);
            panel1.Name = "panel1";
            panel1.Size = new Size(600, 600);
            panel1.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ActiveCaption;
            label4.Location = new Point(510, 558);
            label4.Name = "label4";
            label4.Size = new Size(0, 15);
            label4.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 1;
            label1.Text = "Simulation";
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "XML files (*.XML)|*.XML|All files (*.*)|*.*";
            // 
            // button1
            // 
            button1.Location = new Point(14, 27);
            button1.Name = "button1";
            button1.Size = new Size(456, 23);
            button1.TabIndex = 2;
            button1.Text = "Selectionner fichier";
            button1.UseVisualStyleBackColor = true;
            button1.Click += SelectFileBtn_click;
            // 
            // button2
            // 
            button2.Location = new Point(487, 27);
            button2.Name = "button2";
            button2.Size = new Size(127, 23);
            button2.TabIndex = 3;
            button2.Text = "Démarrer";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Play_click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(14, 91);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(120, 109);
            listBox1.TabIndex = 4;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(14, 73);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 5;
            label2.Text = "Aéroport";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(152, 73);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 7;
            label3.Text = "Client";
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(152, 91);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(318, 109);
            listBox2.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(487, 73);
            label5.Name = "label5";
            label5.Size = new Size(44, 15);
            label5.TabIndex = 9;
            label5.Text = "Avions";
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            listBox3.ItemHeight = 15;
            listBox3.Location = new Point(487, 91);
            listBox3.Name = "listBox3";
            listBox3.Size = new Size(127, 109);
            listBox3.TabIndex = 8;
            // 
            // button3
            // 
            button3.Location = new Point(421, 56);
            button3.Name = "button3";
            button3.Size = new Size(49, 23);
            button3.TabIndex = 10;
            button3.Text = "|>";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(366, 56);
            button4.Name = "button4";
            button4.Size = new Size(49, 23);
            button4.TabIndex = 11;
            button4.Text = "<|";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(14, 210);
            label6.Name = "label6";
            label6.Size = new Size(0, 15);
            label6.TabIndex = 12;
            // 
            // FormSimulator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(626, 840);
            Controls.Add(label6);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label5);
            Controls.Add(listBox3);
            Controls.Add(label3);
            Controls.Add(listBox2);
            Controls.Add(label2);
            Controls.Add(listBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FormSimulator";
            Text = "Simulateur de scénario";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private OpenFileDialog openFileDialog1;
        private Button button1;
        private Button button2;
        private ListBox listBox1;
        private Label label2;
        private Label label3;
        private ListBox listBox2;
        private Label label5;
        private ListBox listBox3;
        private Button button3;
        private Button button4;
        private Label label4;
        private Label label6;
    }
}