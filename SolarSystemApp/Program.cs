using System;
using SpaceSim;
using System.Windows.Forms;

namespace SolarSystemApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Star sun = new Star("Sun", "Yellow", 0, 696342, 24.5);
            Planet mercury = new Planet("Mercury", "gray", 57909227, 2439.5, 87.97, 176);
            Planet venus = new Planet("Venus", "brown", 108209475, 6052, 224.7, 243);
            Planet terra = new Planet("Earth", "blue", 149598262, 6378, 365.25, 1);
            Moon luna = new Moon("the Moon", "gray", 384400, 29.5, 1737.5, 27.3, terra);
            Planet mars = new Planet("Mars", "red", 227943824, 10648.5, 686.98, 1.04);
            Moon phobos = new Moon("Phobos", "gray", 9376, 0.32, 34, 85, mars);
            Moon deimos = new Moon("Deimos", "gray", 23458, 1.2624, 19.5, 0, mars);
            Planet jupiter = new Planet("Jupiter", "orange", 778340821, 71492, 4331.57, 0.41);
            Moon io = new Moon("Io", "yellow", 421700, 1821.6, 364.3, 1.77, jupiter);
            Moon europa = new Moon("Europa", "white", 671034, 1560.8, 85.2, 3.55, jupiter);
            Moon ganymede = new Moon("Ganymede", "gray", 1070412, 2634.1, 171.7, 7.15, jupiter);
            Moon callisto = new Moon("Callisto", "gray", 1882709, 2410.3, 400.5, 16.69, jupiter);
            Planet saturn = new Planet("Saturn", "yellow", 1426666422, 60268, 10759.22, 0.45);
            Moon titan = new Moon("Titan", "orange", 1221870, 2574.7, 15.95, 16, saturn);
            Moon rhea = new Moon("Rhea", "gray", 527040, 763.8, 4.5, 4.5, saturn);
            Moon enceladus = new Moon("Enceladus", "white", 238020, 252.1, 1.4, 1.4, saturn);
            Planet uranus = new Planet("Uranus", "blue", 2870658186, 25559, 30685, 0.72);
            Moon oberon = new Moon("Oberon", "gray", 583520, 761.4, 13.5, 13.5, uranus);
            Moon titania = new Moon("Titania", "gray", 435910, 788.9, 8.7, 8.7, uranus);
            Moon umbriel = new Moon("Umbriel", "gray", 266300, 584.7, 4.1, 4.1, uranus);
            Planet neptune = new Planet("Neptune", "blue", 4498396441, 24764, 60190, 0.67);
            Moon triton = new Moon("Triton", "blue", 354759, 1353.4, 5.9, 5.9, neptune);
            List<SpaceObject> solarSystem = new List<SpaceObject>
            {
                sun,
                mercury,
                venus,
                terra,luna,
                mars, phobos, deimos,
                jupiter, io, europa, ganymede, callisto,
                saturn, titan, rhea, enceladus,
                uranus, oberon, titania, umbriel,
                neptune, triton
            };
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(solarSystem));
        }
    }
}
