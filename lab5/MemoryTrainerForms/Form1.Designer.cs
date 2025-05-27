using System.Windows.Forms;

namespace MemoryTrainerForms
{
    partial class Form1
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
            glControl1 = new OpenTK.GLControl.GLControl();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            panel1 = new Panel();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // glControl1
            // 
            glControl1.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            glControl1.APIVersion = new Version(3, 3, 0, 0);
            glControl1.Dock = DockStyle.Fill;
            glControl1.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            glControl1.IsEventDriven = true;
            glControl1.Location = new Point(0, 0);
            glControl1.Name = "glControl1";
            glControl1.Profile = OpenTK.Windowing.Common.ContextProfile.Compatability;
            glControl1.SharedContext = null;
            glControl1.Size = new Size(1200, 900);
            glControl1.TabIndex = 0;
            glControl1.Load += GLControlLoad;
            glControl1.Paint += GLControlRender;
            glControl1.MouseDown += GLControlMouseDown;
            glControl1.Resize += GLControlResize;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.Font = new Font("Papyrus", 15F);
            button1.Location = new Point(540, 296);
            button1.Name = "button1";
            button1.Size = new Size(97, 56);
            button1.TabIndex = 3;
            button1.Tag = "3x4";
            button1.Text = "3x4";
            button1.UseVisualStyleBackColor = true;
            button1.Click += GameModeButton_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.Font = new Font("Papyrus", 15F);
            button2.Location = new Point(540, 404);
            button2.Name = "button2";
            button2.Size = new Size(97, 56);
            button2.TabIndex = 4;
            button2.Tag = "4x4";
            button2.Text = "4x4";
            button2.UseVisualStyleBackColor = true;
            button2.Click += GameModeButton_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.None;
            button3.Font = new Font("Papyrus", 15F);
            button3.Location = new Point(540, 514);
            button3.Name = "button3";
            button3.Size = new Size(97, 56);
            button3.TabIndex = 5;
            button3.Tag = "4x5";
            button3.Text = "4x5";
            button3.UseVisualStyleBackColor = true;
            button3.Click += GameModeButton_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
            panel1.Location = new Point(0, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1200, 897);
            panel1.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Papyrus", 15F);
            label2.Location = new Point(479, 209);
            label2.Name = "label2";
            label2.Size = new Size(236, 39);
            label2.TabIndex = 8;
            label2.Text = "Select a game mode:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Papyrus", 15F);
            label1.Location = new Point(415, 120);
            label1.Name = "label1";
            label1.Size = new Size(379, 39);
            label1.TabIndex = 7;
            label1.Text = "Welcome to Memory Trainer 3D!";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1200, 900);
            Controls.Add(panel1);
            Controls.Add(glControl1);
            Name = "Form1";
            Text = "Memory Trainer 3D";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private OpenTK.GLControl.GLControl glControl1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Panel panel1;
        private Label label2;
        private Label label1;
    }
}
