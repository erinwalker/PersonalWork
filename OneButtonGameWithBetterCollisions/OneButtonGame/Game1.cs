using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace OneButtonGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Dog dog;
        Input input;
        List<Bomb> bombs;
        float spawnCounter;
        int i, bombCounter;
        string text;
        Vector2 textPosition;
        Color color;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 300;
            graphics.PreferredBackBufferWidth = 800;
        }

        protected override void Initialize()
        {
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font");
            input = new Input(this);
            this.Components.Add(input);
            bombs = new List<Bomb>();
            dog = new Dog(this, input, bombs);
            this.Components.Add(dog);
            i = 0;
            bombCounter = 0;
            text = "Bombs Passed: " + bombCounter;
            textPosition = new Vector2(10, 10);
            color = Color.Black;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            //spriteBatch.Dispose();
            //dog.Dispose();
            //input.Dispose();
            //this.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            text = "Bombs Passed: " + bombCounter;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Console.WriteLine((float)gameTime.TotalGameTime.TotalSeconds);
            spawnCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Checks for collison and pauses and closes
            if (dog.collision == true)
            {
                System.Threading.Thread.Sleep(2000);
                this.Exit();
            }

            //Spawns new bombs every 2.3 seconds
            if(spawnCounter >= SpawnTime(gameTime))
            {
                spawnCounter = 0;
                bombs.Add(new Bomb(this));
                this.Components.Add(bombs[i]);
                i++;
            }

            //Checks if bomb is past edge and adds to bomb count
            foreach (Bomb bomb in bombs)
            {
                if (bomb.location.X < 0 && bomb.pastEdge == false)
                {
                    bombCounter++;
                    bomb.pastEdge = true;
                    //Console.WriteLine(bombCounter);
                    //bombs.Remove(bomb);
                }
            }

            base.Update(gameTime);
        }

        //Changes intervl of spawn time as the bombs speed up
        float SpawnTime(GameTime gameTime)
        {
            float spawnTime = 2.3f;
            float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;
            if (totalTime > 20)
            {
                spawnTime = 2.1f;
                if (totalTime > 35)
                {
                    spawnTime = 1.9f;
                }
                if (totalTime > 50)
                {
                    spawnTime = 1.7f;
                }
                if (totalTime > 65)
                {
                    spawnTime = 1.5f;
                }
                if (totalTime > 80)
                {
                    spawnTime = 1.3f;
                }
                if (totalTime > 95)
                {
                    spawnTime = 1.2f;
                }
                if (totalTime > 110)
                {
                    spawnTime = 1.05f;
                }
            }

            return spawnTime;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            if (dog.collision == true)
            {
                color = Color.White;
                GraphicsDevice.Clear(Color.Firebrick);
                text = "       You Died!\nBombs Passed: " + bombCounter;           
                textPosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - 40, GraphicsDevice.Viewport.Height / 2);
            }

            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, textPosition, color);
            spriteBatch.DrawString(font, "Time: " + (int)gameTime.TotalGameTime.TotalSeconds + " seconds", new Vector2 (10, 40), color);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
