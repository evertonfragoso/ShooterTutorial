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
            return base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw()
        {
            //Color frontColor = Color.White;
            Color frontColor = new Color(255, 255, 255, 0);
            Color backColor = new Color(255, 0, 0, 255);
            Vector2 origin = Vector2.Zero;
            float scale = 1f;

            if (IsHoverButton())
            {
                _spriteBatch.DrawString(_spriteFont, _buttonText,
                    ButtonPosition + new Vector2( 1 * scale,  1 * scale),
                    backColor, 0, origin, scale, SpriteEffects.None, 1f);
                _spriteBatch.DrawString(_spriteFont, _buttonText,
                    ButtonPosition + new Vector2(-1 * scale,  1 * scale),
                    backColor, 0, origin, scale, SpriteEffects.None, 1f);
                _spriteBatch.DrawString(_spriteFont, _buttonText,
                    ButtonPosition + new Vector2(-1 * scale, -1 * scale),
                    backColor, 0, origin, scale, SpriteEffects.None, 1f);
                _spriteBatch.DrawString(_spriteFont, _buttonText,
                    ButtonPosition + new Vector2( 1 * scale, -1 * scale),
                    backColor, 0, origin, scale, SpriteEffects.None, 1f);
            }

            //System.Diagnostics.Debug.WriteLine(ButtonPosition);
            _spriteBatch.DrawString(_spriteFont, _buttonText, ButtonPosition, frontColor);

            base.Draw();
        }
    }
}
