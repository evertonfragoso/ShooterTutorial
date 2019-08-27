//using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterTutorial.GameObjects.Buttons
{
    public class BaseButton
    {
        protected GraphicsDevice _device;
        protected ContentManager _content;
        protected SpriteBatch _spriteBatch;

        protected static SpriteFont _spriteFont;
        protected static Vector2 _buttonSize;
        protected static string _buttonText;
        protected static Color _frontColor;
        protected static Color _backColor;

        private static Vector2 _buttonPosition;

        public Vector2 ButtonPosition
        {
            get => _buttonPosition;
            set => _buttonPosition = value;
        }

        public BaseButton(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch)
        {
            _device = device;
            _content = content;
            _spriteBatch = spriteBatch;
        }

        public virtual bool Initialize()
        {
            _spriteFont = _content.Load<SpriteFont>("Graphics\\gameFont");
            return true;
        }

        /// <summary>
        /// Return true if a player clicks the button with mouse
        ///</summary>
        public static bool IsHoverButton()
        {
            Point mousePosition = Mouse.GetState().Position;
            Vector2 spriteFontMeasure = _spriteFont.MeasureString(_buttonText);
            float buttonWidth = _buttonPosition.X + spriteFontMeasure.X;
            float buttonHeight = _buttonPosition.Y + spriteFontMeasure.Y;

            return
                mousePosition.X < buttonWidth &&
                mousePosition.X > _buttonPosition.X &&
                mousePosition.Y < buttonHeight &&
                mousePosition.Y > _buttonPosition.Y;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw()
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            Vector2 origin = Vector2.Zero;
            float layerDepth = 1f;
            float rotation = 0;
            float scale = 1f;

            // Button outline
            if (IsHoverButton())
            {
                _spriteBatch.DrawString(_spriteFont, _buttonText,
                    ButtonPosition + new Vector2( 1 * scale,  1 * scale),
                    _backColor, rotation, origin, scale, spriteEffects,
                    layerDepth);
                _spriteBatch.DrawString(_spriteFont, _buttonText,
                    ButtonPosition + new Vector2(-1 * scale,  1 * scale),
                    _backColor, rotation, origin, scale, spriteEffects,
                    layerDepth);
                _spriteBatch.DrawString(_spriteFont, _buttonText,
                    ButtonPosition + new Vector2(-1 * scale, -1 * scale),
                    _backColor, rotation, origin, scale, spriteEffects,
                    layerDepth);
                _spriteBatch.DrawString(_spriteFont, _buttonText,
                    ButtonPosition + new Vector2( 1 * scale, -1 * scale),
                    _backColor, rotation, origin, scale, spriteEffects,
                    layerDepth);
            }

            _spriteBatch.DrawString(_spriteFont, _buttonText, ButtonPosition, _frontColor);
        }

        // END
    }
}
