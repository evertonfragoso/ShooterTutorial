//using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.GameObjects.Buttons
{
    public class GameMenuButton
    {
        public BaseButton Button;

        public static Vector2 Position;
        public static string Text;

        public void Initialize(BaseButton baseButton, Vector2 position)
        {
            Button = baseButton;
            Position = position;
            Text = "MENU";
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
