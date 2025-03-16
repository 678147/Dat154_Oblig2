namespace SolarSystemApp
{
    public partial class Form1 : Form
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox comboBox1;
        private SpaceSimContol simContol;
        private CheckBox checkBoxShowInfo;
        private CheckBox checkBoxShowLabels;
        

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
