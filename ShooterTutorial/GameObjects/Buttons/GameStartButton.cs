//using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.GameObjects.Buttons
{
    public class GameStartButton
    {
        public BaseButton Button;

        public string Text;

        public GameStartButton()
        {
            Text = "NEW GAME";
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
