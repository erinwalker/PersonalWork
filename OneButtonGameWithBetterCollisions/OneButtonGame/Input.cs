using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneButtonGame
{
    class Input : Microsoft.Xna.Framework.GameComponent
    {
        KeyboardState currentState;
        KeyboardState previousState;

        public Input(Game game) : base(game)
        {
            currentState = Keyboard.GetState();
            previousState = currentState;
        }

        public override void Update(GameTime gameTime)
        {
            previousState = currentState;
            currentState = Keyboard.GetState();

            base.Update(gameTime);
        }

        public bool IsKeyDown(Keys key)
        {
            return (currentState.IsKeyDown(key));
        }

        public bool IsHoldingKey(Keys key)
        {
            return (currentState.IsKeyDown(key) && previousState.IsKeyDown(key));
        }

        public bool WasKeyPressed(Keys key)
        {
            return (currentState.IsKeyDown(key) && previousState.IsKeyUp(key));
        }

        public bool HasReleasedKey(Keys key)
        {
            return (currentState.IsKeyUp(key) && previousState.IsKeyDown(key));
        }
    }
}
