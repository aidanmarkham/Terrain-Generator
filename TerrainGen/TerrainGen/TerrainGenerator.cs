using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerrainGen
{
    class TerrainGenerator
    {
        // generates terrain on a 100x100 map
        float[,] terrain;// 2D array for the map
        Random rand; // Random object
        
        public TerrainGenerator(int scale) {
            terrain = new float[scale, scale];
            rand = new Random();
        }

        public float[,] generateRandomPeaks() // generates mountains on the map
        {

            for (int i = 0; i < rand.Next(5, 20); i++)
            {
                terrain[rand.Next(3, 97), rand.Next(3, 97)] = 255;
            }
            return terrain;

        }


        public float[,] generatePerlin()
        {
            int randomSeed = rand.Next(0, 5000000);
            
            for (int i = 0; i < terrain.GetLength(0); i++)
            {
                for (int j = 0; j < terrain.GetLength(1); j++)
                {
                    float val = (float)(((NoiseGenerator.Noise(i + randomSeed, j + randomSeed) + 1) / 2) * 255);
                    if (val > 255)
                    {
                        val = 255;
                    }
                    terrain[i, j] = val;

                }
            }
            return terrain;
        }

        public float[,] generatePerlin(int seed, int size)
        {

            NoiseGenerator.Frequency = 0.015 / size;

            for (int i = 0; i < terrain.GetLength(0); i++)
            {
                for (int j = 0; j < terrain.GetLength(1); j++)
                {
                    float val = (float)(((NoiseGenerator.Noise(i + seed, j + seed) + 1) / 2) * 255);
                    if (val > 255)
                    {
                        val = 255;
                    }
                    terrain[i, j] = val;

                }
            }
            return terrain;
        }



        public float[,] generateRandomData()
        {

            for (int i = 0; i < terrain.GetLength(0); i++)
            {
                for (int j = 0; j < terrain.GetLength(1); j++)
                {
                    terrain[i, j] = rand.Next(0, 256);
                }
            }
            return terrain;
        }




        public float[,] getArray()
        {
            return terrain;
        }
    }
}
