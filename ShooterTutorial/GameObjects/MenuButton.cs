using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShooterTutorial.GameObjects
{
    public class MenuButton
    {
        private static SpriteFont _spriteFont;

        public Vector2 Position;
        public string Text;

        public Color FrontColor;
        public Color BackColor;

        public bool Active;

        public MenuButton(SpriteFont spriteFont)
        {
            _spriteFont = spriteFont;
        }

        public void Initialize(string text, Vector2 position)
        {
            Color _white = new Color(255, 255, 255, 0);
            Color _red = new Color(255, 0, 0, 255);
            Initialize(text, position, _white, _red);
        }

        public virtual void Initialize(string text, Vector2 position,
            Color frontColor, Color backColor)
        {
            Text = text;
            Position = position;
            FrontColor = frontColor;
            BackColor = backColor;
            Active = true;
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            float scale = 1f;
            float rotation = 0;
            float layerDepth = 1f;
            Vector2 origin = Vector2.Zero;
            SpriteEffects effects = SpriteEffects.None;

            Draw(spriteBatch, rotation, origin, scale, effects, layerDepth);
        }

        public virtual void Draw(SpriteBatch spriteBatch, float rotation,
            Vector2 origin, float scale, SpriteEffects effects,
            float layerDepth)
        {
            // Button outline
            if (IsMouseHoverButton())
            {
                spriteBatch.DrawString(_spriteFont, Text,
                    Position + new Vector2(1 * scale, 1 * scale),
                    BackColor, rotation, origin, scale, effects,
                    layerDepth);
                spriteBatch.DrawString(_spriteFont, Text,
                    Position + new Vector2(-1 * scale, 1 * scale),
                    BackColor, rotation, origin, scale, effects,
                    layerDepth);
                spriteBatch.DrawString(_spriteFont, Text,
                    Position + new Vector2(-1 * scale, -1 * scale),
                    BackColor, rotation, origin, scale, effects,
                    layerDepth);
                spriteBatch.DrawString(_spriteFont, Text,
                    Position + new Vector2(1 * scale, -1 * scale),
                    BackColor, rotation, origin, scale, effects,
                    layerDepth);
            }

            spriteBatch.DrawString(_spriteFont, Text, Position, FrontColor,
                rotation, origin, scale, effects, layerDepth);
        }

        /// <summary>
        /// Mouse clicked the button
        /// </summary>
        public bool Clicked()
        {
            return IsMouseHoverButton() &&
                ButtonState.Pressed == Mouse.GetState().LeftButton;
        }

        /// <summary>
        /// Return true if a player clicks the button with mouse
        /// </summary>
        private bool IsMouseHoverButton()
        {
            Point mousePosition = Mouse.GetState().Position;
            Vector2 spriteFontMeasure = _spriteFont.MeasureString(Text);
            float buttonWidth = Position.X + spriteFontMeasure.X;
            float buttonHeight = Position.Y + spriteFontMeasure.Y;

            return
                mousePosition.X < buttonWidth &&
                mousePosition.X > Position.X &&
                mousePosition.Y < buttonHeight &&
                mousePosition.Y > Position.Y;
        }

        // END
    }
}
