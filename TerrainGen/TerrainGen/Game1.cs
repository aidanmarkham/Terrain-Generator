using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace TerrainGen
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private MouseState oldState;
        private KeyboardState oldKeyboardState;

        Texture2D terrain; // Texture to hold the map
        
        TerrainGenerator ter;
        TerrainRenderer terRend;
        Random rand;
        int scale;
        int size;
       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            
        }
        protected override void Initialize()
        {


            base.Initialize();

            scale = 500;
            size = 1;
            ter = new TerrainGenerator(scale);

            rand = new Random();



            terrain = TerrainRenderer.shadowColor(GraphicsDevice, scale, scale, ter.generatePerlin(0, size), size);
            
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            

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
                int randomSeed = rand.Next(0, Int32.MaxValue);
                size += 1;
                terrain = TerrainRenderer.shadowColor(GraphicsDevice, scale, scale, ter.generatePerlin(0, size), size);

 
                
            }
            if (keyboardstate.IsKeyDown(Keys.Up) && oldKeyboardState.IsKeyUp(Keys.Up))
            {
                int width = GraphicsDevice.PresentationParameters.BackBufferWidth;
                int height = GraphicsDevice.PresentationParameters.BackBufferHeight;

                

                //Copy to texture
                
                //Get a date for file name
                DateTime date = DateTime.Now; //Get the date for the file name
                Stream stream = File.Create(date.ToString("MM-dd-yy H;mm;ss") + ".png"); 

                //Save as PNG
                terrain.SaveAsPng(stream, terrain.Width, terrain.Height);
                stream.Dispose();
                



            }


            oldState = newState;
            oldKeyboardState = keyboardstate;
            // TODO: Add your update logic here
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            //spriteBatch.Draw(terrain1, new Rectangle(0, 0, 250, 250), Color.White);
            //spriteBatch.Draw(terrain2, new Rectangle(0, 250, 250, 250), Color.White);
            //spriteBatch.Draw(terrain4, new Rectangle(250, 0, 250, 250), Color.White);
            spriteBatch.Draw(terrain, new Rectangle(0, 0, 500, 500), Color.White);
            spriteBatch.End();
 

        }
        
        
    }
}
