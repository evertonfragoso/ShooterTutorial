//using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.GameObjects.Buttons
{
    public class GameExitButton
    {
        public BaseButton Button;

        public string Text;

        public GameExitButton()
        {
            Text = "EXIT";
        }

        public void Initialize(BaseButton baseButton)
        {
            Button = baseButton;
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
