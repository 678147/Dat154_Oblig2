using System.Windows.Forms;
using System.Drawing;
using SpaceSim;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Text;
using System.Xml.Serialization;

namespace SolarSystemApp
{
    public class SpaceSimControl : Control
    {
        public string SelectedObject { get; set; }
        List<SpaceObject> solarSystem;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawPlantes(e.Graphics, 0);
        }
        readonly Brush yellow = new SolidBrush(Color.FromName("yellow"));
      
        readonly Brush gray = new SolidBrush(Color.Gray);
        readonly Brush brown = new SolidBrush(Color.Brown);
        readonly Brush blue = new SolidBrush(Color.Blue);
        readonly Brush red = new SolidBrush(Color.Red);
        readonly Brush orange = new SolidBrush(Color.Orange);
        readonly Brush white = new SolidBrush(Color.White);
        
        private void DrawPlantes(Graphics g, double t)
        {
            var sun = solarSystem.Find(obj => obj.Name == "Sun");
            var mercury = solarSystem.Find(obj => obj.Name == "Mercury");
            var venus = solarSystem.Find(obj => obj.Name == "Venus");
            var earth = solarSystem.Find(obj => obj.Name == "Earth");
            var mars = solarSystem.Find(obj => obj.Name == "Mars");
            var jupiter = solarSystem.Find(obj => obj.Name == "Jupiter");
            var saturn = solarSystem.Find(obj => obj.Name == "Saturn");
            var uranus = solarSystem.Find(obj => obj.Name == "Uranus");
            var neptune = solarSystem.Find(obj => obj.Name == "Neptune");
            if (SelectedObject == "The Sun")
            {
                DrawPlanet(g, sun, t);
                DrawPlanet(g, mercury, t);
                DrawPlanet(g, venus, t);
                DrawPlanet(g, earth, t);
                DrawPlanet(g, mars, t);
                DrawPlanet(g, jupiter, t);
                DrawPlanet(g, saturn, t);
                DrawPlanet(g, uranus, t);
                DrawPlanet(g, neptune, t);
            }else if (SelectedObject == "Mercury")
            {
                DrawPlanet(g, mercury, t);

            };
        private void DrawPlanet(Graphics g, SpaceObject obj, double t)
        {
            Brush color = new SolidBrush(Color.FromName(obj.GetColor()));
            
            double centerX = 960;
            double centerY = 540;
            (double x, double y, double r) = calcRelativePos(obj, t);


            g.FillEllipse(color, (float)x + (float)centerX, (float)y+ (float) centerY, (float)r, (float)r);
            color.Dispose();
        }
        private (double x, double y, double r) calcRelativePos(SpaceObject obj, double t) 
        {
            (double xT, double yT ) = obj.CalcPos(t);
            double x = xT * CalculateDistanceScale();
            double y = yT * CalculateDistanceScale();
            double r = obj.ObjRadius * CalculateSizeScale();
            return (x, y, r);
        }
        private double CalculateSizeScale()
        {
            
            var sun = solarSystem.Find(obj => obj.Name == "Sun");
            double maxObjectRadius = sun.ObjRadius;

            // Scale to fit within a reasonable size on the screen (e.g., 50 pixels)
            return 50 / maxObjectRadius;
        }
        private double CalculateDistanceScale()
        {
            // Assuming the maximum orbital radius is that of Neptune
            var neptune = solarSystem.Find(obj => obj.Name == "Neptune");
            double maxOrbitalRadius = neptune.OrbRadius;

            // Scale to fit within half the width of the screen (960 pixels)
            return 960 / maxOrbitalRadius;
        }
    }
}