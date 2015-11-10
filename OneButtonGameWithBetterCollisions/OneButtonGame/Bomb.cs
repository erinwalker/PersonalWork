using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneButtonGame
{
    class Bomb : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D bomb;
        public Vector2 location;
        SpriteBatch spriteBatch;
        public Vector2 bombDir;
        public Color[] textureData;
        public Rectangle rectangle;
        public bool pastEdge;
        float totalTime;
        const int SIZE = 40;

        public Bomb(Game game) : base(game)
        {
            bomb = this.Game.Content.Load<Texture2D>("Bomb");
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            location = new Vector2(this.Game.GraphicsDevice.Viewport.Width + SIZE, this.Game.GraphicsDevice.Viewport.Height - SIZE);
            bombDir = new Vector2(-120, 0);
            textureData = new Color[bomb.Width * bomb.Height];
            bomb.GetData(textureData);
            rectangle = new Rectangle((int)(location.X), (int)(location.Y), SIZE, SIZE);
            pastEdge = false;
        }

        public override void Update(GameTime gameTime)
        {
            totalTime = (float)gameTime.TotalGameTime.TotalSeconds;
            rectangle = new Rectangle((int)(location.X), (int)(location.Y), SIZE, SIZE);
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            location = location + ((bombDir * (time / 1000)));

            BombSpeed();

            base.Update(gameTime);
        }

        //Changes the speed of the bombs over time
        void BombSpeed()
        {
            if (totalTime > 20 && location.X < this.Game.GraphicsDevice.Viewport.Width + SIZE)
            {
                bombDir = new Vector2(-150, 0);
                if (totalTime > 35)
                {
                    bombDir = new Vector2(-170, 0);
                }
                if (totalTime > 50)
                {
                    bombDir = new Vector2(-190, 0);
                }
                if (totalTime > 65)
                {
                    bombDir = new Vector2(-210, 0);
                }
                if (totalTime > 80)
                {
                    bombDir = new Vector2(-230, 0);
                }
                if (totalTime > 95)
                {
                    bombDir = new Vector2(-250, 0);
                }
                if (totalTime > 110)
                {
                    bombDir = new Vector2(-270, 0);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bomb, rectangle, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
