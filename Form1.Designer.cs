namespace SolarSystemApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox comboBox1;
        private SpaceSimControl spaceSimControl;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.spaceSimControl = new SpaceSimControl();
            this.SuspendLayout();
            
            this.spaceSimControl.Location = new Point(12, 50);
            this.spaceSimControl.Name = "spaceSimControl";
            this.spaceSimControl.Size = new Size(1920, 1080);
            this.spaceSimControl.TabIndex = 1;
            
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1980, 1080);
            this.Controls.Add(this.spaceSimControl);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.Text = "Solar System Simulator";
            this.ResumeLayout(false);
        }
    }
}
