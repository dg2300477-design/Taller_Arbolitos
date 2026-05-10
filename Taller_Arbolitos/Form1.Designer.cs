namespace Taller_Arbolitos
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
            titulo = new Label();
            InsertButton = new Button();
            RemoveButton = new Button();
            ContainsButton = new Button();
            CountButton = new Button();
            HeightButton = new Button();
            ClearButton = new Button();
            OrderButton = new Button();
            SuspendLayout();
            // 
            // titulo
            // 
            titulo.AutoSize = true;
            titulo.Location = new Point(288, 20);
            titulo.Name = "titulo";
            titulo.Size = new Size(143, 25);
            titulo.TabIndex = 0;
            titulo.Text = "Eliga una opcion";
            // 
            // InsertButton
            // 
            InsertButton.Location = new Point(188, 67);
            InsertButton.Name = "InsertButton";
            InsertButton.Size = new Size(112, 34);
            InsertButton.TabIndex = 1;
            InsertButton.Text = "Insertar";
            InsertButton.UseVisualStyleBackColor = true;
            InsertButton.Click += button1_Click;
            // 
            // RemoveButton
            // 
            RemoveButton.Location = new Point(188, 135);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(112, 34);
            RemoveButton.TabIndex = 2;
            RemoveButton.Text = "eliminar";
            RemoveButton.UseVisualStyleBackColor = true;
            // 
            // ContainsButton
            // 
            ContainsButton.Location = new Point(188, 206);
            ContainsButton.Name = "ContainsButton";
            ContainsButton.Size = new Size(112, 34);
            ContainsButton.TabIndex = 3;
            ContainsButton.Text = "buscar";
            ContainsButton.UseVisualStyleBackColor = true;
            // 
            // CountButton
            // 
            CountButton.Location = new Point(407, 67);
            CountButton.Name = "CountButton";
            CountButton.Size = new Size(112, 34);
            CountButton.TabIndex = 4;
            CountButton.Text = "contar";
            CountButton.UseVisualStyleBackColor = true;
            // 
            // HeightButton
            // 
            HeightButton.Location = new Point(407, 135);
            HeightButton.Name = "HeightButton";
            HeightButton.Size = new Size(112, 34);
            HeightButton.TabIndex = 5;
            HeightButton.Text = "altura";
            HeightButton.UseVisualStyleBackColor = true;
            HeightButton.Click += button5_Click;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(407, 206);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(112, 34);
            ClearButton.TabIndex = 6;
            ClearButton.Text = "limpiar";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += button1_Click_1;
            // 
            // OrderButton
            // 
            OrderButton.Location = new Point(306, 275);
            OrderButton.Name = "OrderButton";
            OrderButton.Size = new Size(112, 34);
            OrderButton.TabIndex = 7;
            OrderButton.Text = "recorrer";
            OrderButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(OrderButton);
            Controls.Add(ClearButton);
            Controls.Add(HeightButton);
            Controls.Add(CountButton);
            Controls.Add(ContainsButton);
            Controls.Add(RemoveButton);
            Controls.Add(InsertButton);
            Controls.Add(titulo);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label titulo;
        private Button InsertButton;
        private Button RemoveButton;
        private Button ContainsButton;
        private Button CountButton;
        private Button HeightButton;
        private Button ClearButton;
        private Button OrderButton;
    }
}
