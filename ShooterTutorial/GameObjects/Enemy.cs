﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.GameObjects
{
    public class Enemy
    {
        // Animation representing the enemy
        public Animation EnemyAnimation;

        // The position of the enemy ship relative to the top left corner
        // of the screen
        public Vector2 Position;

        // The state of the Enemy Ship
        public bool Active;

        // The hit points of the enemy, if this goes to zero the enemy dies
        public int Health;

        // The amount of damage the enemy inflicts on the player ship
        public int Damage;

        // The amount of score the enemy will give to the player
        public int Value;

        // Get the width of the enemy ship
        public int Width => EnemyAnimation.FrameWidth;

        // Get the height of the enemy ship
        public int Height => EnemyAnimation.FrameHeight;

        // The speed at which the enemy moves
        private float _enemyMoveSpeed;

        public void Initialize(Animation animation, Vector2 position)
        {
            // Load enemy ship texture
            EnemyAnimation = animation;

            // Set the position of the enemy
            Position = position;

            // We initialize the enemy to be active so it will be updated
            // in the game
            Active = true;

            // Set the health of the enemy
            Health = 10;

            // Set the amount of damage the enemey can do
            Damage = 10;

            // Set how fast the enemy moves
            _enemyMoveSpeed = 6f;

            // Set the score value of the enemy
            Value = 100;
        }

        public void Update(GameTime gameTime)
        {
            // The enemy always moves to the left so decrement its x position
            Position.X -= _enemyMoveSpeed;

            // Update the position of the Animation
            EnemyAnimation.Position = Position;

            // Update Animation
            EnemyAnimation.Update(gameTime);

            // The enemy is past the screen or its health reaches 0 then
            // deactivate it
            // By setting the Active flag to false, the game will remove
            // this object from the game list
            Active &= (Position.X >= -Width) && (Health > 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the animation
            EnemyAnimation.Draw(spriteBatch);
        }
    }
}
