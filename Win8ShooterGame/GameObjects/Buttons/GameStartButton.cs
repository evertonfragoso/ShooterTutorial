//using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.GameObjects.Buttons
{
    public class GameStartButton : BaseButton
    {
        public GameStartButton(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch)
            : base(device, content, spriteBatch)
        {
        }

        public override bool Initialize()
        {
            _buttonText = "START";
            //_frontColor = Color.White;
            _frontColor = new Color(255, 255, 255, 0);
            _backColor = new Color(255, 0, 0, 255);

            return base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
