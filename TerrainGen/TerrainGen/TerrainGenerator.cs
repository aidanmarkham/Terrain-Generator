using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerrainGen
{
    class TerrainGenerator
    {
        // generates terrain on a 100x100 map
        int[,] terrain = new int[100, 100]; // 2D array for the map
        Random rand = new Random(); // Random object
        double ROUGH_SCALE = 5; //Scale of the object - steepness of the mountain
        int ITERATIONS = 255; // distance from the peak
        public void generateRandom() // Random generation, fills the map with random data.
        {
            for (int i = 0; i < terrain.GetLength(0); i++)
            {
                for (int j = 0; j < terrain.GetLength(1); j++)
                {
                    terrain[i, j] = rand.Next(0, 256);
                }
            }
        }
        public void generateMountain(int seedMode) // generates mountains on the map
        {
            if (seedMode == 0) // Random Peaks
            {
                for (int i = 0; i < rand.Next(5, 20); i++)
                {
                    terrain[rand.Next(3, 97), rand.Next(3, 97)] = 255;
                }
            }
            if (seedMode == 1) // Chain Peaks
            {
                int startx = rand.Next(25, 75);
                int starty = rand.Next(25, 75);
                terrain[startx, starty] = 255;
                for (int b = 0; b < 500; b++)
                {
                    int direction = rand.Next(0, 8);
                    Console.WriteLine(direction);
                    if (direction == 0)
                    {
                        startx -= 1;
                        starty -= 1;
                    }
                    else if (direction == 1)
                    {
                        startx += 1;
                        starty -= 1;

                    }
                    else if (direction == 2)
                    {
                        startx += 1;
                        starty += 1;
                    }
                    else if (direction == 3)
                    {
                        startx -= 1;
                        starty += 1;
                    }
                    else if (direction == 4)
                    {
                        
                        starty -= 1;
                    }
                    else if (direction == 5)
                    {
                        
                        starty += 1;

                    }
                    else if (direction == 6)
                    {
                        startx += 1;
                        
                    }
                    else if (direction == 7)
                    {
                        startx -= 1;
                        
                    }
                    if (startx > 0 && startx < 99 && starty > 0 && starty < 99)
                    {
                        terrain[startx, starty] = 255;
                    }
                }
            }

            if (seedMode == 2) // Random Seed
            {

                for (int i = 0; i < terrain.GetLength(0); i++)
                {
                    for (int j = 0; j < terrain.GetLength(1); j++)
                    {
                        terrain[i, j] = rand.Next(0, 256);
                    }
                }
            }   

                for (int l = 0; l < ITERATIONS; l++) // iterates the mountaingen code, makes it so that there's slopes
                {

                    for (int i = 3; i < terrain.GetLength(0) - 3; i++)
                    {
                        for (int j = 3; j < terrain.GetLength(1) - 3; j++)
                        {
                            if (terrain[i, j] == (-1 * ((l + 1) - 256)))
                            {
                                if (terrain[i + 1, j] < terrain[i, j])
                                {
                                    terrain[i + 1, j] = (int)(terrain[i, j] - ROUGH_SCALE);
                                }
                                if (terrain[i - 1, j] < terrain[i, j])
                                {
                                    terrain[i - 1, j] = (int)(terrain[i, j] - ROUGH_SCALE);
                                }
                                if (terrain[i, j - 1] < terrain[i, j])
                                {
                                    terrain[i, j - 1] = (int)(terrain[i, j] - ROUGH_SCALE);
                                }
                                if (terrain[i, j + 1] < terrain[i, j])
                                {
                                    terrain[i, j + 1] = (int)(terrain[i, j] - ROUGH_SCALE);
                                }
                            }
                        }
                    }
                }
        }

        public int[,] getArray()
        {
            return terrain;
        }
    }
}
