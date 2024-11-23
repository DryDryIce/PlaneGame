namespace PlaneGame
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.btnShoot = new System.Windows.Forms.Button();
            this.btnGenerateGraph = new System.Windows.Forms.Button();
            this.btnBuildPlanes = new System.Windows.Forms.Button();
            this.btnStartMovement = new System.Windows.Forms.Button();
            this.btnPauseMovement = new System.Windows.Forms.Button();
            this.btnRestartGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstOutput
            // 
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.ItemHeight = 20;
            this.lstOutput.Location = new System.Drawing.Point(810, 8);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(178, 244);
            this.lstOutput.TabIndex = 0;
            this.lstOutput.SelectedIndexChanged += new System.EventHandler(this.lstOutput_SelectedIndexChanged);
            // 
            // btnShoot
            // 
            this.btnShoot.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnShoot.Location = new System.Drawing.Point(810, 707);
            this.btnShoot.Name = "btnShoot";
            this.btnShoot.Size = new System.Drawing.Size(178, 81);
            this.btnShoot.TabIndex = 1;
            this.btnShoot.Text = "Disparar";
            this.btnShoot.UseVisualStyleBackColor = false;
            this.btnShoot.Click += new System.EventHandler(this.btnShoot_Click);
            // 
            // btnGenerateGraph
            // 
            this.btnGenerateGraph.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnGenerateGraph.Location = new System.Drawing.Point(810, 359);
            this.btnGenerateGraph.Name = "btnGenerateGraph";
            this.btnGenerateGraph.Size = new System.Drawing.Size(178, 81);
            this.btnGenerateGraph.TabIndex = 2;
            this.btnGenerateGraph.Text = "Generar Grafo";
            this.btnGenerateGraph.UseVisualStyleBackColor = false;
            this.btnGenerateGraph.Click += new System.EventHandler(this.btnGenerateGraph_Click);
            // 
            // btnBuildPlanes
            // 
            this.btnBuildPlanes.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnBuildPlanes.Location = new System.Drawing.Point(810, 446);
            this.btnBuildPlanes.Name = "btnBuildPlanes";
            this.btnBuildPlanes.Size = new System.Drawing.Size(178, 81);
            this.btnBuildPlanes.TabIndex = 3;
            this.btnBuildPlanes.Text = "Construir Aviones";
            this.btnBuildPlanes.UseVisualStyleBackColor = false;
            this.btnBuildPlanes.Click += new System.EventHandler(this.btnBuildPlanes_Click);
            // 
            // btnStartMovement
            // 
            this.btnStartMovement.BackColor = System.Drawing.Color.IndianRed;
            this.btnStartMovement.Location = new System.Drawing.Point(810, 533);
            this.btnStartMovement.Name = "btnStartMovement";
            this.btnStartMovement.Size = new System.Drawing.Size(178, 81);
            this.btnStartMovement.TabIndex = 4;
            this.btnStartMovement.Text = "Iniciar";
            this.btnStartMovement.UseVisualStyleBackColor = false;
            this.btnStartMovement.Click += new System.EventHandler(this.btnStartMovement_Click);
            // 
            // btnPauseMovement
            // 
            this.btnPauseMovement.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnPauseMovement.Location = new System.Drawing.Point(810, 620);
            this.btnPauseMovement.Name = "btnPauseMovement";
            this.btnPauseMovement.Size = new System.Drawing.Size(178, 81);
            this.btnPauseMovement.TabIndex = 5;
            this.btnPauseMovement.Text = "Pausar";
            this.btnPauseMovement.UseVisualStyleBackColor = false;
            this.btnPauseMovement.Click += new System.EventHandler(this.btnPauseMovement_Click);
            // 
            // btnRestartGame
            // 
            this.btnRestartGame.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnRestartGame.Location = new System.Drawing.Point(810, 272);
            this.btnRestartGame.Name = "btnRestartGame";
            this.btnRestartGame.Size = new System.Drawing.Size(178, 81);
            this.btnRestartGame.TabIndex = 6;
            this.btnRestartGame.Text = "Reiniciar";
            this.btnRestartGame.UseVisualStyleBackColor = false;
            this.btnRestartGame.Click += new System.EventHandler(this.btnRestartGame_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 800);
            this.Controls.Add(this.btnRestartGame);
            this.Controls.Add(this.btnPauseMovement);
            this.Controls.Add(this.btnStartMovement);
            this.Controls.Add(this.btnBuildPlanes);
            this.Controls.Add(this.btnGenerateGraph);
            this.Controls.Add(this.btnShoot);
            this.Controls.Add(this.lstOutput);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Plane Game";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.Button btnShoot;
        private System.Windows.Forms.Button btnGenerateGraph;
        private System.Windows.Forms.Button btnBuildPlanes;
        private System.Windows.Forms.Button btnStartMovement;
        private System.Windows.Forms.Button btnPauseMovement;
        private System.Windows.Forms.Button btnRestartGame;
    }
}

