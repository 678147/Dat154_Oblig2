namespace SolarSystemApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeComboBox();
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

        private void InitializeComboBox()
        {
            comboBox1 = new ComboBox();
            comboBox1.Items.AddRange(new object[] { "The Sun", "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" });
            comboBox1.SelectedIndex = 0; 
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            comboBox1.Location = new Point(10, 10);
            comboBox1.Size = new Size(150, 30);
            this.Controls.Add(comboBox1);
        }
    }
}
