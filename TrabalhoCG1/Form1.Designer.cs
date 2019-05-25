namespace TrabalhoCG1
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Linha");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Retangulo");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Triangulo");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Circulo");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Formas", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_Bresh = new System.Windows.Forms.RadioButton();
            this.rb_DDA = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_Circulo = new System.Windows.Forms.RadioButton();
            this.rb_Triangulo = new System.Windows.Forms.RadioButton();
            this.rb_Retangulo = new System.Windows.Forms.RadioButton();
            this.rb_Reta = new System.Windows.Forms.RadioButton();
            this.Canvas = new System.Windows.Forms.Panel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(929, 560);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Canvas, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.treeView1, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 43);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(923, 514);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.button3);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(94, 508);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_Bresh);
            this.groupBox1.Controls.Add(this.rb_DDA);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(91, 70);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algoritmo";
            // 
            // rb_Bresh
            // 
            this.rb_Bresh.AutoSize = true;
            this.rb_Bresh.Checked = true;
            this.rb_Bresh.Location = new System.Drawing.Point(7, 44);
            this.rb_Bresh.Name = "rb_Bresh";
            this.rb_Bresh.Size = new System.Drawing.Size(52, 17);
            this.rb_Bresh.TabIndex = 1;
            this.rb_Bresh.TabStop = true;
            this.rb_Bresh.Text = "Bresh";
            this.rb_Bresh.UseVisualStyleBackColor = true;
            // 
            // rb_DDA
            // 
            this.rb_DDA.AutoSize = true;
            this.rb_DDA.Location = new System.Drawing.Point(7, 20);
            this.rb_DDA.Name = "rb_DDA";
            this.rb_DDA.Size = new System.Drawing.Size(48, 17);
            this.rb_DDA.TabIndex = 0;
            this.rb_DDA.TabStop = true;
            this.rb_DDA.Text = "DDA";
            this.rb_DDA.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Linha Bress";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cores";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_Circulo);
            this.groupBox2.Controls.Add(this.rb_Triangulo);
            this.groupBox2.Controls.Add(this.rb_Retangulo);
            this.groupBox2.Controls.Add(this.rb_Reta);
            this.groupBox2.Location = new System.Drawing.Point(3, 166);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(91, 140);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Formas";
            // 
            // rb_Circulo
            // 
            this.rb_Circulo.AutoSize = true;
            this.rb_Circulo.Location = new System.Drawing.Point(7, 92);
            this.rb_Circulo.Name = "rb_Circulo";
            this.rb_Circulo.Size = new System.Drawing.Size(57, 17);
            this.rb_Circulo.TabIndex = 3;
            this.rb_Circulo.Text = "Circulo";
            this.rb_Circulo.UseVisualStyleBackColor = true;
            // 
            // rb_Triangulo
            // 
            this.rb_Triangulo.AutoSize = true;
            this.rb_Triangulo.Location = new System.Drawing.Point(7, 68);
            this.rb_Triangulo.Name = "rb_Triangulo";
            this.rb_Triangulo.Size = new System.Drawing.Size(69, 17);
            this.rb_Triangulo.TabIndex = 2;
            this.rb_Triangulo.Text = "Triângulo";
            this.rb_Triangulo.UseVisualStyleBackColor = true;
            // 
            // rb_Retangulo
            // 
            this.rb_Retangulo.AutoSize = true;
            this.rb_Retangulo.Location = new System.Drawing.Point(7, 44);
            this.rb_Retangulo.Name = "rb_Retangulo";
            this.rb_Retangulo.Size = new System.Drawing.Size(74, 17);
            this.rb_Retangulo.TabIndex = 1;
            this.rb_Retangulo.Text = "Retângulo";
            this.rb_Retangulo.UseVisualStyleBackColor = true;
            // 
            // rb_Reta
            // 
            this.rb_Reta.AutoSize = true;
            this.rb_Reta.Checked = true;
            this.rb_Reta.Location = new System.Drawing.Point(7, 20);
            this.rb_Reta.Name = "rb_Reta";
            this.rb_Reta.Size = new System.Drawing.Size(48, 17);
            this.rb_Reta.TabIndex = 0;
            this.rb_Reta.TabStop = true;
            this.rb_Reta.Text = "Reta";
            this.rb_Reta.UseVisualStyleBackColor = true;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(103, 3);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(617, 508);
            this.Canvas.TabIndex = 1;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseClick);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 108);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Limpar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(726, 3);
            this.treeView1.Name = "treeView1";
            treeNode1.ForeColor = System.Drawing.Color.Red;
            treeNode1.Name = "NLinha";
            treeNode1.Text = "Linha";
            treeNode2.ForeColor = System.Drawing.Color.Lime;
            treeNode2.Name = "NRetangulo";
            treeNode2.Text = "Retangulo";
            treeNode3.ForeColor = System.Drawing.Color.Blue;
            treeNode3.Name = "NTriangulo";
            treeNode3.Text = "Triangulo";
            treeNode4.ForeColor = System.Drawing.Color.Fuchsia;
            treeNode4.Name = "NCirculo";
            treeNode4.Text = "Circulo";
            treeNode5.Name = "NFormas";
            treeNode5.Text = "Formas";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.treeView1.Size = new System.Drawing.Size(194, 508);
            this.treeView1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 560);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel Canvas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_Bresh;
        private System.Windows.Forms.RadioButton rb_DDA;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rb_Circulo;
        private System.Windows.Forms.RadioButton rb_Triangulo;
        private System.Windows.Forms.RadioButton rb_Retangulo;
        private System.Windows.Forms.RadioButton rb_Reta;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TreeView treeView1;
    }
}

