using System;
using System.Collections.ObjectModel;
using System.Data;
using SpaceSim;


namespace SpaceSim
{
    public class SpaceObject
    {
        public String Name { get; protected set; }
        public double OrbRadius { get; set; }
        public double OrbPeriod { get; set; }
        public double ObjRadius { get; set; }
        public double RotPeriod { get; set; }
        public double Width { get; set; }
        public String Color { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        private double currentAngle = 0;

        private Planet _orbObject;
        public Planet OrbObject
        {
            get { return _orbObject; }
            set { _orbObject = value; }
        }

        public SpaceObject(
            String name,
            String color
            )
        {
            Name = name;
            Color = color;
        }
        public void SubscribeToTick(EventController controller)
        {
            controller.DoTick += UpdatePosition;
        }
        private void UpdatePosition(object sender, EventArgs e)
        {
            double angleIncrement = (2 * Math.PI) / OrbPeriod;
            currentAngle += angleIncrement;
            if (currentAngle >= 2 * Math.PI)
            {
                currentAngle -= 2 * Math.PI;
            }

            X = OrbRadius * Math.Cos(currentAngle);
            Y = OrbRadius * Math.Sin(currentAngle);
        }
        public virtual void Draw()
        {
            Console.WriteLine(Name);
        }
        public virtual (double X, double Y) CalcPos(double time)
        {
            double angle = 360 * time / OrbPeriod * (Math.PI / 180);
            double x = OrbRadius * Math.Cos(angle);
            double y = OrbRadius * Math.Sin(angle);
            if (OrbObject != null)
            {
                double angle2 = 360 * time / OrbObject.OrbPeriod * (Math.PI / 180);
                double x2 = OrbObject.OrbRadius * Math.Cos(angle2);
                double y2 = OrbObject.OrbRadius * Math.Sin(angle2);

                return (x + x2, y + y2);
            }
            return (x, y);
        }
        public string GetName() => Name;
        public double GetOrbRadius() => OrbRadius;
        public double GetOrbPeriod() => OrbPeriod;
        public double GetObjRadius() => ObjRadius;
        public double GetRotPeriod() => RotPeriod;
        public double GetWidth() => Width;
        public string GetColor() => Color;
        public Planet GetOrbObject() => OrbObject;
    }

    public class Star : SpaceObject
    {
        public Star(
            String name,
            String color,
            double orbRadius,
            double objRadius,
            double rotPeriod
            ) : base(
                name,
                color
                )
        {
            OrbRadius = orbRadius;
            ObjRadius = objRadius;
            RotPeriod = rotPeriod;
            ObjRadius = objRadius;
        }
        public override void Draw()
        {
            Console.Write("Star  : ");
            base.Draw();
        }
    }

    public class Planet : SpaceObject
    {
        public Planet(
            String name,
            String color,
            double orbRadius,
            double objRadius,
            double orbPeriod,
            double rotPeriod
            ) : base(
                name,
                color
                )
        {
            OrbPeriod = orbPeriod;
            OrbRadius = orbRadius;
            ObjRadius = objRadius;
            RotPeriod = rotPeriod;
            X = OrbRadius;
            Y = 0;
        }
        public override void Draw()
        {
            Console.Write("Planet: ");
            base.Draw();
        }
    }

    public class Moon : Planet
    {
        public Moon(
            String name,
            String color,
            double orbRadius,
            double orbPeriod,
            double objRadius,
            double rotPeriod,
            Planet orbObject
            ) : base(
                name,
                color,
                orbRadius,
                objRadius,
                orbPeriod,
                rotPeriod

                )
        {
            OrbObject = orbObject;
            X = OrbRadius + orbObject.X;
            Y = orbObject.Y;
        }
        public override void Draw()
        {
            Console.Write("Moon  : ");
            base.Draw();
        }
    }

    public class AsteroidBelt : SpaceObject
    {
        public AsteroidBelt(
            String name,
            String color,
            double orbRadius,
            double objRadius
            ) : base(
                name,
                color
                )
        {
            OrbRadius = orbRadius;
            ObjRadius = objRadius;
        }
        public override void Draw()
        {
            Console.Write("AsteroidBelt: ");
            base.Draw();
        }
    }
}