using System.Windows.Forms;
using System.Drawing;
using SpaceSim;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace SolarSystemApp
{
    public class SpaceSimContol : Control
    {
        public string SelectedObject = "The Sun";
        public bool ShowLabels = false;
        public bool ShowInfo = false;
        List<SpaceObject> solarSystem;
        private EventController eventController;

        public SpaceSimContol(List<SpaceObject> solarSystem, EventController eventController)
        {
            this.solarSystem = solarSystem;
            this.eventController = eventController;
            this.eventController.DoTick += OnTick;
        }

        private void OnTick(object? sender, EventArgs e)
        {
            Invalidate();
          
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            double elapsedTime = eventController.GetElapsedTime();
            System.Diagnostics.Debug.WriteLine("OnPaint");


            DrawPlantes(e.Graphics, elapsedTime);
            if (ShowInfo)
            {
                DrawInfoSquare(e.Graphics);
            }
        }

        private void DrawPlantes(Graphics g, double t)
        {
            System.Diagnostics.Debug.WriteLine("DrawPlantes");

            var selectedPlanet = solarSystem.Find(obj => obj.Name == SelectedObject);
            bool zoomMode = selectedPlanet != null && SelectedObject != "Sun";

            if (zoomMode)
            {
                DrawZoomedPlanet(g, selectedPlanet, t);
            }
            else
            {
                var planets = new List<SpaceObject>
            {
                solarSystem.Find(obj => obj.Name == "Sun"),
                solarSystem.Find(obj => obj.Name == "Mercury"),
                solarSystem.Find(obj => obj.Name == "Venus"),
                solarSystem.Find(obj => obj.Name == "Earth"),
                solarSystem.Find(obj => obj.Name == "Mars"),
                solarSystem.Find(obj => obj.Name == "Jupiter"),
                solarSystem.Find(obj => obj.Name == "Saturn"),
                solarSystem.Find(obj => obj.Name == "Uranus"),
                solarSystem.Find(obj => obj.Name == "Neptune")
            };

                int totalPlanets = planets.Count;
                double[] radii = new double[totalPlanets];
                for (int i = 0; i < totalPlanets; i++)
                {
                    if (planets[i] != null)
                    {
                        radii[i] = (planets[i].ObjRadius * CalculateSizeScale()) / 200;
                    }
                }

                for (int i = 0; i < totalPlanets; i++)
                {
                    if (planets[i] != null)
                    {
                        DrawPlanet(g, planets[i], t, i, totalPlanets, radii);
                    }
                }
            }
        }

        private void DrawZoomedPlanet(Graphics g, SpaceObject obj, double t)
        {
            Brush color = new SolidBrush(Color.FromName(obj.GetColor()));

            double centerX = 960;
            double centerY = 540;
            double planetRadius = 500;

            // Draw the planet
            g.FillEllipse(color, (float)(centerX - planetRadius / 2), (float)(centerY - planetRadius / 2), (float)planetRadius, (float)planetRadius);
            var moons = solarSystem.Where(m => m.OrbObject == obj).ToList();
            if (moons.Count > 0)
            {
                DrawMoons(g, centerX, centerY, planetRadius, moons);
            }

            color.Dispose();
        }

        private void DrawMoons(Graphics g, double centerX, double centerY, double planetRadius, List<SpaceObject> moons)
        {
            double moonOrbitRadius = planetRadius * 1.5;
            double angleStep = 2 * Math.PI / moons.Count;

            Font font = new Font("Arial", 12, FontStyle.Bold);
            Brush textColor = Brushes.Black;

            for (int i = 0; i < moons.Count; i++)
            {
                SpaceObject moon = moons[i];

                double moonSize = planetRadius / (moon.ObjRadius / 1.0);
                if (moonSize < 10)
                {
                    moonSize = 10;
                }

                double angle = i * angleStep;
                double moonX = centerX + moonOrbitRadius * Math.Cos(angle);
                double moonY = centerY + moonOrbitRadius * Math.Sin(angle);

                Brush moonColor = new SolidBrush(Color.FromName(moon.GetColor()));

                g.FillEllipse(moonColor, (float)(moonX - moonSize / 2), (float)(moonY - moonSize / 2), (float)moonSize, (float)moonSize);

                string moonName = moon.Name;
                SizeF textSize = g.MeasureString(moonName, font);
                float textX = (float)(moonX - textSize.Width / 2);
                float textY = (float)(moonY - moonSize / 2 - textSize.Height);
                g.DrawString(moonName, font, textColor, textX, textY);

                moonColor.Dispose();
            }
            font.Dispose();
        }


        private void DrawPlanet(Graphics g, SpaceObject obj, double t, int index, int totalPlanets, double[] radii)
        {
            Brush color = new SolidBrush(Color.FromName(obj.GetColor()));
            Font font = new Font("Arial", 8);
            Brush textColor = Brushes.Black;

            double centerY = Height / 2;
            (double x, double y, double r) = calcRelativePos(obj, t, index, totalPlanets, radii);

            System.Diagnostics.Debug.WriteLine($"Drawing: {obj.Name} at ({x}, {y}) with radius {r}");

            double maxSize = 200;
            double minSize = 10;
            if (r > maxSize)
            {
                r = maxSize;
            }
            else if (r < minSize)
            {
                r = minSize;
            }

            System.Diagnostics.Debug.WriteLine($"x: {x}, y: {y}, r: {r}");
            g.FillEllipse(color, (float)(x - r / 2), (float)(y + centerY - r / 2), (float)r, (float)r);

            if (ShowLabels)
            {
                string name = obj.Name;
                SizeF textSize = g.MeasureString(name, font);
                float textX = (float)(x - textSize.Width / 2);
                float textY = (float)(y + centerY - r / 2 - textSize.Height);
                g.DrawString(name, font, textColor, textX, textY);
            }

            color.Dispose();
            font.Dispose();
        }

        private void DrawInfoSquare(Graphics g)
        {
            var selectedObject = solarSystem.Find(obj => obj.Name == SelectedObject);
            if (selectedObject != null)
            {
                string info = $"Name: {selectedObject.Name}\n" +
                              $"Orbital Radius: {selectedObject.OrbRadius}\n" +
                              $"Orbital Period: {selectedObject.OrbPeriod}\n" +
                              $"Object Radius: {selectedObject.ObjRadius}\n" +
                              $"Rotation Period: {selectedObject.RotPeriod}";

                Font font = new Font("Arial", 10);
                Brush textColor = Brushes.Black;
                Brush backgroundColor = Brushes.White;
                Pen borderPen = new Pen(Color.Black);

                SizeF textSize = g.MeasureString(info, font);
                float padding = 10;
                float boxWidth = textSize.Width + padding * 2;
                float boxHeight = textSize.Height + padding * 2;
                float boxX = 1920 - boxWidth - 50;
                float boxY = 10;

                g.FillRectangle(backgroundColor, boxX, boxY, boxWidth, boxHeight);
                g.DrawRectangle(borderPen, boxX, boxY, boxWidth, boxHeight);
                g.DrawString(info, font, textColor, boxX + padding, boxY + padding);

                font.Dispose();
                borderPen.Dispose();
            }
        }

        private (double x, double y, double r) calcRelativePos(SpaceObject obj, double t, int index, int totalPlanets, double[] radii)
        {
            double scaleSize = CalculateSizeScale();
            double r = (obj.ObjRadius * scaleSize) / 1000;

            double centerX = 1980 / 2;
            double centerY = -250;

            if (obj.Name == "Sun")
            {
                return (centerX, centerY, r);
            }

            double maxOrbitalRadius = solarSystem.Max(p => p.OrbRadius);
            double maxScreenRadius = Math.Min(1920, 1080) * 0.3;
            double distanceScale = maxScreenRadius / maxOrbitalRadius / 4;
            double adjustedOrbRadius = obj.OrbRadius * distanceScale;
            double minSpacing = 20;
            double maxSpacing = 200;

            adjustedOrbRadius += Math.Min(adjustedOrbRadius, maxSpacing);
            adjustedOrbRadius += minSpacing * index;

            if (adjustedOrbRadius < (maxOrbitalRadius * 0.2))
            {
                adjustedOrbRadius *= 2.5;
            }

            double angle = (t / obj.OrbPeriod) * 2 * Math.PI;
            double x = centerX + adjustedOrbRadius * Math.Cos(angle);
            double y = centerY + adjustedOrbRadius * Math.Sin(angle);

            return (x, y, r);
        }

        private double CalculateSizeScale()
        {
            var sun = solarSystem.Find(obj => obj.Name == "The Sun");
            if (sun != null)
            {
                double maxObjectRadius = sun.ObjRadius;
                double maxScreenSize = Math.Min(1080, 1920) / 10;
                return maxScreenSize / maxObjectRadius;
            }
            return 1;
        }

        private double CalculateDistanceScale()
        {
            var neptune = solarSystem.Find(obj => obj.Name == "Neptune");
            if (neptune != null)
            {
                double maxOrbitalRadius = neptune.OrbRadius;
                double maxScreenRadius = 1080;
                return maxScreenRadius / maxOrbitalRadius;
            }
            return 1;
        }
    }
}