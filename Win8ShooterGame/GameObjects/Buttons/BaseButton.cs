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

        private static Vector2 buttonPosition;

        public Vector2 ButtonPosition
        {
            get => buttonPosition;
            set => buttonPosition = value;
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
            float buttonWidth = buttonPosition.X + _spriteFont.MeasureString(_buttonText).X;
            float buttonHeight = buttonPosition.Y + _spriteFont.MeasureString(_buttonText).Y;

            return
                mousePosition.X < buttonWidth &&
                mousePosition.X > buttonPosition.X &&
                mousePosition.Y < buttonHeight &&
                mousePosition.Y > buttonPosition.Y;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw()
        {
        }

        // END
    }
}
