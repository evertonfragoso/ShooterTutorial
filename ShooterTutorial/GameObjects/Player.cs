//using System;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.GameObjects
{
    class Player
    {
        // Player Score
        public Score Score;

        // Animation representing the player
        public Animation PlayerAnimation;

        // Position of the Player relative to the upper left side of the screen
        public Vector2 Position;

        // State of the player
        public bool Active;

        // Amount of hit points that player has
        public int Health;

        // Amount of lives the player has
        public int Lives;

        // Get the width of the player ship
        public int Width
        {
            get { return PlayerAnimation.FrameWidth; }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return PlayerAnimation.FrameHeight; }

        }

        public void Initialize(Animation animation, Vector2 position)
        {
            Score = new Score();

            PlayerAnimation = animation;

            // Set the starting position of the player around the middle of
            // the screen and to the back
            Position = position;

            // Set the player to be active
            Active = true;

            // Set the player health
            Health = 100;

            // Set the player lives
            Lives = 3;
        }

        // Update the player animation
        public void Update(GameTime gameTime)
        {
            // Animated Player
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch);
        }

        public void Die()
        {
            Lives--;

            if (Lives <= 0)
            {
                Active = false;
                return;
            }

            Health = 100;
        }
    }
}
