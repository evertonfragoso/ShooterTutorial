//using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.GameObjects.Buttons
{
    public class GameMenuButton
    {
        public BaseButton Button;

        public static string Text;

        public void Initialize(BaseButton baseButton)
        {
            Button = baseButton;
            Text = "MENU";
        }

        public void Update(GameTime gameTime)
        {
            Button.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Button.Draw(spriteBatch);
        }
    }
}
