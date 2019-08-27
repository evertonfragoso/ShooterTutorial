//using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.GameObjects.Buttons
{
    public class GameStartButton
    {
        public BaseButton Button;

        public Vector2 Position;
        public string Text;

        public GameStartButton()
        {
            Text = "NEW GAME";
        }

        public void Initialize(BaseButton baseButton, Vector2 position)
        {
            Button = baseButton;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
            Button.Position = Position;
            Button.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Button.Draw(spriteBatch);
        }
    }
}
