//using System;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial
{
    public class Animation
    {
        // The image representing the collection of images used for animation
        public Texture2D spriteStrip;

        // The scale used to display the sprite strip
        public float Scale;

        // The time since we last updated the frame
        public int ElapsedTime;

        // The time we display a frame until the next one
        public int FrameTime;

        // The number of frames that the animation contains
        public int FrameCount;

        // The index of the current frame we are displaying
        public int CurrentFrame;

        // The color of the frame we will be displaying
        public Color Color;

        // The area of the image strip we want to display
        public Rectangle SourceRect = new Rectangle();

        // The area where we want to display the image strip in the game
        public Rectangle DestinationRect = new Rectangle();

        // Width of a given frame
        public int FrameWidth;

        // Height of a given frame
        public int FrameHeight;

        // The state of the Animation
        public bool Active;

        // Determines if the animation will keep playing or deactivate after
        // one run
        public bool Looping;

        // Width of a given frame
        public Vector2 Position;

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth,
                                int frameHeight, int frameCount, int frametime,
                                Color color, float scale, bool looping)
        {
            // Keep a local copy of the values passed in
            Color = color;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FrameCount = frameCount;
            FrameTime = frametime;
            Scale = scale;

            Looping = looping;
            Position = position;
            spriteStrip = texture;

            // Set the time to zero
            ElapsedTime = 0;
            CurrentFrame = 0;

            // Set the Animation to active by default
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            // Do not update the game if we are not active
            if (Active == false) return;

            // Update the elapsed time
            ElapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            // If the elapsed time is larger than the frame time we need to
            // switch frames
            if (ElapsedTime > FrameTime)
            {
                // Move to the next frame
                CurrentFrame++;

                // If the CurrentFrame is equal to FrameCount reset
                // CurrentFrame to zero
                if (CurrentFrame == FrameCount)
                {
                    CurrentFrame = 0;
                    // If we are not looping deactivate the animation
                    Active &= Looping != false;
                }

                // Reset the elapsed time to zero
                ElapsedTime = 0;
            }

            // Grab the correct frame in the image strip by multiplying
            // the CurrentFrame index by the Frame width
            SourceRect = new Rectangle(CurrentFrame * FrameWidth, 0,
                                        FrameWidth, FrameHeight);

            // Grab the correct frame in the image strip by multiplying
            // the CurrentFrame index by the frame width
            DestinationRect = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                //(int)Position.X - (int)(FrameWidth * scale) / 2,
                //(int)Position.Y - (int)(FrameHeight * scale) / 2,
                (int)(FrameWidth * Scale),
                (int)(FrameHeight * Scale)
            );
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Only draw the animation when we are active
            if (Active)
                spriteBatch.Draw(spriteStrip, DestinationRect, SourceRect, Color);
        }
    }
}
