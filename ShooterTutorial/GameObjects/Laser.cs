﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.GameObjects
{
    public class Laser
    {
        // animation the represents the laser animation.
        public Animation LaserAnimation;

        // the speed the laser travels
        private readonly float laserMoveSpeed = 30f;

        // position of the laser
        public Vector2 Position;

        // The damage the laser deals.
        public int Damage;

        // set the laser to active
        public bool Active;

        // Laser beams range.
        //public int Range;

        // the width of the laser image.
        public int Width => LaserAnimation.FrameWidth;

        // the height of the laser image.
        public int Height => LaserAnimation.FrameHeight;

        public void Initialize(Animation animation, Vector2 position)
        {
            LaserAnimation = animation;
            Position = position;
            Damage = 10;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            Position.X += laserMoveSpeed;
            LaserAnimation.Position = Position;
            LaserAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LaserAnimation.Draw(spriteBatch);
        }
    }
}
