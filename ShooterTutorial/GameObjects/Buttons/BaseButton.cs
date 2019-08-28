//using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterTutorial.GameObjects.Buttons
{
    public class BaseButton
    {
        private static SpriteFont _spriteFont;

        public Vector2 Position;
        public string Text;

        public Color FrontColor;
        public Color BackColor;

        public bool Active;

        public void Initialize(SpriteFont spriteFont, string text, Vector2 position)
        {
            _spriteFont = spriteFont;

            Text = text;
            Position = position;
            FrontColor = new Color(255, 255, 255, 0);
            BackColor = new Color(255, 0, 0, 255);

            Active = true;
        }

        public void Initialize(SpriteFont spriteFont, string text, Vector2 position,
                                Color frontColor, Color backColor)
        {
            _spriteFont = spriteFont;

            Text = text;
            Position = position;
            FrontColor = frontColor;
            BackColor = backColor;

            Active = true;
        }

        /// <summary>
        /// Return true if a player clicks the button with mouse
        ///</summary>
        //public bool IsHoverButton()
        //{
        //    Point mousePosition = Mouse.GetState().Position;
        //    Vector2 spriteFontMeasure = _spriteFont.MeasureString(Text);
        //    float buttonWidth = Position.X + spriteFontMeasure.X;
        //    float buttonHeight = Position.Y + spriteFontMeasure.Y;

        //    return
        //        mousePosition.X < buttonWidth &&
        //        mousePosition.X > Position.X &&
        //        mousePosition.Y < buttonHeight &&
        //        mousePosition.Y > Position.Y;
        //}

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                //SpriteEffects spriteEffects = SpriteEffects.None;
                //Vector2 origin = Vector2.Zero;
                //float layerDepth = 1f;
                //float rotation = 0;
                //float scale = 1f;

                //// Button outline
                //if (IsHoverButton())
                //{
                //    spriteBatch.DrawString(_spriteFont, Text,
                //        Position + new Vector2(1 * scale, 1 * scale),
                //        BackColor, rotation, origin, scale, spriteEffects,
                //        layerDepth);
                //    spriteBatch.DrawString(_spriteFont, Text,
                //        Position + new Vector2(-1 * scale, 1 * scale),
                //        BackColor, rotation, origin, scale, spriteEffects,
                //        layerDepth);
                //    spriteBatch.DrawString(_spriteFont, Text,
                //        Position + new Vector2(-1 * scale, -1 * scale),
                //        BackColor, rotation, origin, scale, spriteEffects,
                //        layerDepth);
                //    spriteBatch.DrawString(_spriteFont, Text,
                //        Position + new Vector2(1 * scale, -1 * scale),
                //        BackColor, rotation, origin, scale, spriteEffects,
                //        layerDepth);
                //}

                spriteBatch.DrawString(_spriteFont, Text, Position, FrontColor);
            }
        }

        // END
    }
}
