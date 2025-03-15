using System.Windows.Forms;
using System.Drawing;
using SpaceSim;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace SolarSystemApp
{
    public class SpaceSimControl : Control
    {
        public string SelectedObject = "The Sun";
        List<SpaceObject> solarSystem = new List<SpaceObject>
            {
                new SpaceObject("Sun", "yellow"),
                new SpaceObject("Mercury", "gray"),
                new SpaceObject("Venus", "orange"),
                new SpaceObject("Earth", "blue"),
                new SpaceObject("Mars", "red"),
                new SpaceObject("Jupiter", "brown"),
                new SpaceObject("Saturn", "yellow"),
                new SpaceObject("Uranus", "blue"),
                new SpaceObject("Neptune", "blue")
            };

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawPlantes(e.Graphics, 0);
        }

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
                if (sun != null) DrawPlanet(g, sun, t);
                if (mercury != null) DrawPlanet(g, mercury, t);
                if (venus != null) DrawPlanet(g, venus, t);
                if (earth != null) DrawPlanet(g, earth, t);
                if (mars != null) DrawPlanet(g, mars, t);
                if (jupiter != null) DrawPlanet(g, jupiter, t);
                if (saturn != null) DrawPlanet(g, saturn, t);
                if (uranus != null) DrawPlanet(g, uranus, t);
                if (neptune != null) DrawPlanet(g, neptune, t);
            }
            else
            {
                var selected = solarSystem.Find(obj => obj.Name == SelectedObject);
                if (selected != null)
                {
                    var moons = solarSystem.OfType<Moon>().Where(moon => moon.OrbObject == selected);
                    DrawPlanet(g, selected, t);
                    foreach (var moon in moons)
                    {
                        DrawPlanet(g, moon, t);
                    }
                }
            }
        }

        private void DrawPlanet(Graphics g, SpaceObject obj, double t)
        {
            Brush color = new SolidBrush(Color.FromName(obj.GetColor()));

            double centerX = 960;
            double centerY = 540;
            (double x, double y, double r) = calcRelativePos(obj, t);

            g.FillEllipse(color, (float)x + (float)centerX, (float)y + (float)centerY, (float)r, (float)r);
            color.Dispose();
        }

        private (double x, double y, double r) calcRelativePos(SpaceObject obj, double t)
        {
            (double xT, double yT) = obj.CalcPos(t);
            double x = xT * CalculateDistanceScale();
            double y = yT * CalculateDistanceScale();
            double r = obj.ObjRadius * CalculateSizeScale();
            return (x, y, r);
        }

        private double CalculateSizeScale()
        {
            var sun = solarSystem.Find(obj => obj.Name == "The Sun");
            if (sun != null)
            {
                double maxObjectRadius = sun.ObjRadius;
                return 50 / maxObjectRadius;
            }
            return 1;
        }

        private double CalculateDistanceScale()
        {
            var neptune = solarSystem.Find(obj => obj.Name == "Neptune");
            if (neptune != null)
            {
                double maxOrbitalRadius = neptune.OrbRadius;
                return 960 / maxOrbitalRadius;
            }
            return 1;
        }
    }
}