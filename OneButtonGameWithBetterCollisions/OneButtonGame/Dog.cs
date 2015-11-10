using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneButtonGame
{
    class Dog : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D dog;
        Texture2D explosion;
        Vector2 location;
        SpriteBatch spriteBatch;
        Vector2 gravityDir, dogDir;
        float height, gravityAccel;
        int jumpHeight;
        private Input input;
        private List<Bomb> bombs;
        bool isOnGround;
        public Color[] textureData;
        public Rectangle rectangle;
        public bool collision;

        public Dog(Game game) : base(game)
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            dog = this.Game.Content.Load<Texture2D>("dog");
            height = this.Game.GraphicsDevice.Viewport.Height - 75;
            location = new Vector2(30, height);
            gravityDir = new Vector2(0, 1);
            gravityAccel = 15.0f;
            dogDir = new Vector2(0, 0);
            jumpHeight = -500;
            textureData = new Color[dog.Width * dog.Height];
            dog.GetData(textureData);
            rectangle = new Rectangle((int)(location.X), (int)(location.Y), 75, 75);
            collision = false;
            explosion = this.Game.Content.Load<Texture2D>("explosion");
        }

        public Dog(Game1 game, Input input, List<Bomb> bombs) : this(game)
        {
            this.input = input;
            this.bombs = bombs;
        }

        public override void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            rectangle = new Rectangle((int)(location.X), (int)(location.Y), 75, 75);

            location = location + ((dogDir * (time / 1000)));
            dogDir = dogDir + (gravityDir * gravityAccel);

            //Checks if dog is below ground level and sets it back on ground
            if(location.Y > height)
            {
                location.Y = height;
                dogDir.Y = 0;
                isOnGround = true;
            }

            //Checks if dog is on ground so it only can jump once
            if (isOnGround)
            {
                if (input.WasKeyPressed(Keys.Space))
                {
                    dogDir = dogDir + new Vector2(0, jumpHeight);
                    isOnGround = false;
                    //Console.WriteLine("jump");
                }
            }

            //Checks if dog is colliding with a bomb and returns true if it is
            foreach (Bomb bomb in bombs)
            {
                if (Collision(rectangle, textureData, bomb.rectangle, bomb.textureData))
                //if(rectangle.Intersects(bomb.rectangle))
                {
                    Console.WriteLine("Collision");
                    collision = true;
                }   
            }

 	        base.Update(gameTime);
        }

        //Per pixel collision detection
        static bool Collision(Rectangle dogRect, Color[] dogData, Rectangle bombRect, Color[] bombData)
        {
            int top = Math.Max(dogRect.Top, bombRect.Top);
            int bottom = Math.Min(dogRect.Bottom, bombRect.Bottom);
            int left = Math.Max(dogRect.Left, bombRect.Left);
            int right = Math.Min(dogRect.Right, bombRect.Right);

            for (int y = top; y < bottom; y++)
                for (int x = left; x < right; x++)
                {
                    Color color1 = dogData[(x - dogRect.Left) + (y - dogRect.Top) * (dogRect.Width-18)];
                    Color color2 = bombData[(x - bombRect.Left) + (y - bombRect.Top) * (bombRect.Width-18)];

                    if (color1.A != 0 && color2.A != 0)
                    {
                        return true;
                    }
                }

            return false;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //Changes image to explosion if collision occurs
            if(collision)
            {
                spriteBatch.Draw(explosion, rectangle, Color.White);
            }
            else
            {
                spriteBatch.Draw(dog, rectangle, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
