using System;
using System.Collections.Generic;
using System.Text;

namespace Core.TerrainGenerator
{
    class SineGenerator : TerrainGenerator
    {
        //Offset is roughly the mid point of the terrain.
        //So if offset = screenheight/2, then the sine waves
        //will form roughly around the center of the screen.
        // Value in number of pixels
        public double Offset { get; set; }

        //Amplitude is the height of individual sine waves.
        //value in number of pixels
        public double Amplitude { get; set; }

        //Lower values of Flatness means more steeper sine waves, and vice versa.
        //It essentially shows how 'narrow' each wave is
        //High amplitude + high flatness means that each wave would be higher but slope would change gently.
        //High amplitude + low flatness means that each wave would be higher but slope would change rapidly.
        // value in number of pixels.
        public double Flatness { get; set; }


        public override IEnumerable<double> Generate()
        {
            for (double x = 0; ; x += StepSize)
            {
                Random randomizer = new Random();
                double rand1 = randomizer.NextDouble() + 1;
                double rand2 = randomizer.NextDouble() + 2;
                double rand3 = randomizer.NextDouble() + 3;

                double y = Amplitude / rand1 * Math.Sin((float)x / Flatness * rand1 + rand1);
                y += Amplitude / rand2 * Math.Sin((float)x / Flatness * rand2 + rand2);
                y += Amplitude / rand3 * Math.Sin((float)x / Flatness * rand3 + rand3);
                y += Offset;
                yield return y;
            }
        }
    }
}
