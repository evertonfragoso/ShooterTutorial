// using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Win8ShooterGame;

namespace Shooter
{
    class Player
    {
        // Static Texture representing the player
        //public Texture2D PlayerTexture;

        // Animation representing the player
        public Animation PlayerAnimation;

        // Position of the Player relative to the upper left side of the screen
        public Vector2 Position;

        // State of the player
        public bool Active;

        // Amount of hit points that player has
        public int Health;

        // Get the width of the player ship
        public int Width
        {
            //get { return PlayerTexture.Width; }       // Static Player
            get { return PlayerAnimation.FrameWidth; }  // Animated Player
        }

        // Get the height of the player ship
        public int Height
        {
            //get { return PlayerTexture.Height; }          // Static Player
            get { return PlayerAnimation.FrameHeight; }     // Animated Player

        }

        //public void Initialize(Texture2D texture, Vector2 position)   // Static Player
        public void Initialize(Animation animation, Vector2 position)   // Animated Player
        {
            //PlayerTexture = texture;      // Static Player
            PlayerAnimation = animation;    // Animated Player

            // Set the starting position of the player around the middle of
            // the screen and to the back
            Position = position;

            // Set the player to be active
            Active = true;

            // Set the player health
            Health = 100;
        }

        // Update the player animation
        //public void Update()                  // Static Player
        public void Update(GameTime gameTime)   // Animated Player
        {
            // Animated Player
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch);                                  // Animated Player
            //spriteBatch.Draw(PlayerTexture, Position, null, Color.White, 0f,  // Static Player
                //Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
