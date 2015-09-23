using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TerrainGen
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private MouseState oldState;
        private KeyboardState oldKeyboardState;
        Texture2D terrain; // Texture to hold the map
        int colors = 0;
        int genmode = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 282;
            graphics.PreferredBackBufferHeight = 282;
        }
        protected override void Initialize()
        {


            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            terrain = imagify(GraphicsDevice, 2, 0); // calls the method that gets the image from the terrain generator

        }
        protected override void UnloadContent()
        {
            terrain.Dispose();
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState newState = Mouse.GetState();
            KeyboardState keyboardstate = Keyboard.GetState();
            if (keyboardstate.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space))
            {
                terrain = imagify(GraphicsDevice, genmode, colors);
                

 
                //mode 0, nothing, 1, random, 2, mountains
                //colors 1, b&w, 0, mountains with peaks
                //peaks 0, random peaks, 1, chain peaks, 2, random screen seedS
            }
            if (keyboardstate.IsKeyDown(Keys.Up))
            {
                colors = 1;
            }
            if (keyboardstate.IsKeyDown(Keys.Down))
            {
                colors = 0;
            }
            if (keyboardstate.IsKeyDown(Keys.Left))
            {
                genmode = 0;
            }
            if (keyboardstate.IsKeyDown(Keys.Right))
            {
                genmode = 1;
            }

            oldState = newState;
            oldKeyboardState = keyboardstate;
            // TODO: Add your update logic here
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            spriteBatch.Draw(terrain, new Rectangle(-9, -9, 300, 300), Color.White); // Draws the map
            spriteBatch.End();
 

        }
        public static Texture2D imagify(GraphicsDevice graphics, int mode, int colors)
        {
            Texture2D terrain; //Init texture 2d
            TerrainGenerator ter = new TerrainGenerator(); // Create a generated terrain object
            if (mode == 0)
            {
                ter.generateRandomData(); //  generate random data
            }
            else if (mode == 1)
            {
                ter.generateChain(); // generate mountain data
            }

            terrain = new Texture2D(graphics, 100, 100); // set texture2d to be 100px by 100px

            List<Color> data = new List<Color>(); //Init a list to hold the color data

            //Loop through the terrain array and add the color values to the list for color data
            if (colors == 1) //Black and White renderer
            {
                int[,] terarray = ter.getArray();
                for (int i = 0; i < terarray.GetLength(0); i++)
                {
                    for (int j = 0; j < terarray.GetLength(1); j++)
                    {
                        data.Add(new Color(ter.getArray()[i, j], ter.getArray()[i, j], ter.getArray()[i, j], 255));
                    }
                }
            }
            if (colors == 0) // renderer that simply applies tints, meant to nicen up the view in top down mode. 
            {
                int[,] terarray = ter.getArray();
                for (int i = 0; i < terarray.GetLength(0); i++)
                {
                    for (int j = 0; j < terarray.GetLength(1); j++)
                    {
                        if (ter.getArray()[i, j] >= 230)
                        {
                            data.Add(new Color(ter.getArray()[i, j], ter.getArray()[i, j], ter.getArray()[i, j], 255));
                        }
                        if (ter.getArray()[i, j] >= 110 && ter.getArray()[i, j] < 230)
                        {
                            data.Add(new Color(ter.getArray()[i, j]/5, ter.getArray()[i, j], ter.getArray()[i, j]/5, 255));
                        }
                        if (ter.getArray()[i, j] >= 100 && ter.getArray()[i, j] < 110)
                        {
                            data.Add(new Color(ter.getArray()[i, j] + 100, ter.getArray()[i, j] + 100, ter.getArray()[i, j]/2 + 100, 255));
                        }
                        if (ter.getArray()[i, j] < 100)
                        {
                            data.Add(new Color(ter.getArray()[i, j]/5 + 50, ter.getArray()[i, j]/5 + 50, ter.getArray()[i, j] + 100, 255));
                        }
                    }
                } 
            }

            //turns the color data into an array
            Color[] dataArray = data.ToArray();

            //sets the terrain's texture to the color data from the array
            terrain.SetData(dataArray);

            //returns the image 
            return terrain;
        }
    }
}
