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

        public void generateRandomPeaks() // generates mountains on the map
        {
            
            for (int i = 0; i < rand.Next(5, 20); i++)
            {
                terrain[rand.Next(3, 97), rand.Next(3, 97)] = 255;
            }
            
        }

        public void generateChain() 
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

            public void generateRandomData()
            {

                for (int i = 0; i < terrain.GetLength(0); i++)
                {
                    for (int j = 0; j < terrain.GetLength(1); j++)
                    {
                        terrain[i, j] = rand.Next(0, 256);
                    }
                }
            }   




        public int[,] getArray()
        {
            return terrain;
        }
    }
}
