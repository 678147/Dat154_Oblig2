using SpaceSim;

namespace SolarSystemApp
{
    public partial class Form1 : Form
    {
        private List<SpaceObject> solarSystem;

        public Form1(List<SpaceObject> solarSystem)
        {
            this.solarSystem = solarSystem;
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1?.SelectedItem != null)
            {
                string selectedObject = comboBox1.SelectedItem.ToString();
                if (spaceSimControl != null)
                {
                    spaceSimControl.SelectedObject = selectedObject;
                    spaceSimControl.Invalidate();
                }
            }
        }

        private void InitializeComponent()
        {
            this.comboBox1 = new ComboBox();
            this.spaceSimControl = new SpaceSimControl(solarSystem);
            this.SuspendLayout();

            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
                "The Sun",
                "Mercury",
                "Venus",
                "Earth",
                "Mars",
                "Jupiter",
                "Saturn",
                "Uranus",
                "Neptune"});
            this.comboBox1.Location = new Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(151, 28);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += this.comboBox1_SelectedIndexChanged;

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
