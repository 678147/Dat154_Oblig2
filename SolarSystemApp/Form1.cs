using SpaceSim;

namespace SolarSystemApp
{
    public partial class Form1 : Form
    {
        private List<SpaceObject> solarSystem;
        private EventController eventController;
        private SpaceSimContol simControl;

        public Form1(List<SpaceObject> solarSystem, EventController eventController)
        {
            this.eventController = eventController;
            this.solarSystem = solarSystem;
            this.simControl = new SpaceSimContol(solarSystem, eventController);
            InitializeComponent();
            this.eventController.Start();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1?.SelectedItem != null)
            {
                string selectedObject = comboBox1.SelectedItem.ToString();
                if (simContol != null)
                {
                    simContol.SelectedObject = selectedObject;
                    simContol.Invalidate();
                }
            }
        }

        private void checkBoxShowInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (simControl != null)
            {
                simControl.ShowInfo = checkBoxShowInfo.Checked;
                simControl.Invalidate();
            }
        }

        private void checkBoxShowLabels_CheckedChanged(object sender, EventArgs e)
        {
            if (simContol != null)
            {
                simContol.ShowLabels = checkBoxShowLabels.Checked;
                simContol.Invalidate();
            }
        }

        private void InitializeComponent()
        {
            this.comboBox1 = new ComboBox();
            this.simContol = new SpaceSimContol(solarSystem, eventController);
            this.checkBoxShowInfo = new CheckBox();
            this.checkBoxShowLabels = new CheckBox();
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

            this.simContol.Location = new Point(12, 50);
            this.simContol.Name = "simContol";
            this.simContol.Size = new Size(1920, 1080);
            this.simContol.TabIndex = 1;

            this.checkBoxShowInfo.Location = new Point(300, 12);
            this.checkBoxShowInfo.Name = "checkBoxShowInfo";
            this.checkBoxShowInfo.Size = new Size(150, 28);
            this.checkBoxShowInfo.TabIndex = 2;
            this.checkBoxShowInfo.Text = "Show Info";
            this.checkBoxShowInfo.CheckedChanged += this.checkBoxShowInfo_CheckedChanged;

            this.checkBoxShowLabels.Location = new Point(180, 12);
            this.checkBoxShowLabels.Name = "checkBoxShowLabels";
            this.checkBoxShowLabels.Size = new Size(150, 28);
            this.checkBoxShowLabels.TabIndex = 3;
            this.checkBoxShowLabels.Text = "Show Labels";
            this.checkBoxShowLabels.CheckedChanged += this.checkBoxShowLabels_CheckedChanged;

            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1980, 1080);
            this.Controls.Add(this.simContol);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.checkBoxShowInfo);
            this.Controls.Add(this.checkBoxShowLabels); // Ensure this line is present
            this.Name = "Form1";
            this.Text = "Solar System Simulator";
            this.ResumeLayout(false);
        }
    }
}
