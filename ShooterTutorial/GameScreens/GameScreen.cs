using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Media;

using ShooterTutorial.GameObjects;

namespace ShooterTutorial.GameScreens
{
    public class GameScreen : BaseScreen
    {
        public const string SCREEN_NAME = "gameScreen";

        // A movement speed for the player
        private const float PlayerMoveSpeed = 8;

        // Represents the player
        private static Player _player;

        // Image used to display the static background
        private static Texture2D _mainBackground;
        private static Rectangle _rectBackground;
        private static ParallaxingBackground _bgLayer1;
        private static ParallaxingBackground _bgLayer2;
        private const float Scale = 1f;

        // Keyboard states used to determine key presses
        private static KeyboardState _currentKeyboardState;
        private static KeyboardState _prevKeyboardState;

        // Gamepad states used to determine button presses
        private static GamePadState _currentGamePadState;
        private static GamePadState _prevGamePadState;

        // Mouse states used to track Mouse button press
        private static MouseState _currentMouseState;
        private static MouseState _prevMouseState;

        // Lasers
        private static Texture2D laserTexture;
        private static List<Laser> laserBeams;

        // The rate at which the laser can be fired
        private static TimeSpan laserSpawnTime;
        private static TimeSpan prevLaserSpawnTime;

        // Enemies
        private static Texture2D enemyTexture;
        private static List<Enemy> enemies;

        // The rate at which the enemies appear
        private static TimeSpan enemySpawnTime;
        private static TimeSpan prevSpawnTime;

        // Explosions
        private static List<Explosion> explosions;
        private static Texture2D explosionTexture;

        // BGM
        //private static Song gameMusic;

        // Laser sound and instance
        private static SoundEffect laserSound;
        private static SoundEffectInstance laserSoundInstance;

        // Explosion sound and instance
        private static SoundEffect explosionSound;
        private static SoundEffectInstance explosionSoundInstance;

        // A random number generator
        private static Random random;

        public GameScreen(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch)
            : base(device, content, spriteBatch, SCREEN_NAME)
        {
        }

        public override bool Initialize()
        {
            ShooterTutorialGame.MouseVisibility = false;

            // Initialize the player class
            _player = new Player();

            // Background
            _rectBackground = new Rectangle(0, 0, _device.Viewport.Width,
                _device.Viewport.Height);
            _bgLayer1 = new ParallaxingBackground();
            _bgLayer2 = new ParallaxingBackground();

            // Initialize the enemies list
            enemies = new List<Enemy>();

            // Set the time keepers to zero
            prevSpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns
            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            // Initialize the laser
            laserBeams = new List<Laser>();
            const float SECONDS_IN_MINUTE = 60f;
            const float RATE_OF_FIRE = 200f;
            laserSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
            prevLaserSpawnTime = TimeSpan.Zero;

            // Initialize the explosion sheet
            explosions = new List<Explosion>();

            // Initialize our random number generator
            random = new Random();

            // Load the player resources
            Rectangle titleSafeArea = _device.Viewport.TitleSafeArea;
            Vector2 playerPosition = new Vector2(titleSafeArea.X,
                                    titleSafeArea.Y + (titleSafeArea.Height / 2));

            Animation playerAnimation = new Animation();
            Texture2D playerTexture = _content.Load<Texture2D>("Graphics\\shipAnimation");
            playerAnimation.Initialize(playerTexture, playerPosition,
                115, 69, 8, 30, Color.White, Scale, true);

            _player.Initialize(playerAnimation, playerPosition);

            // Load the parallaxing background
            _bgLayer1.Initialize(_content, "Graphics\\bgLayer1",
                _device.Viewport.Width, _device.Viewport.Height, -1);
            _bgLayer2.Initialize(_content, "Graphics\\bgLayer2",
                _device.Viewport.Width, _device.Viewport.Height, -2);

            _mainBackground = _content.Load<Texture2D>("Graphics\\mainBackground");

            // Load the laser texture
            laserTexture = _content.Load<Texture2D>("Graphics\\laser");

            // Enemy texture
            enemyTexture = _content.Load<Texture2D>("Graphics\\mineAnimation");

            // Explosion texture
            explosionTexture = _content.Load<Texture2D>("Graphics\\explosion");

            // Load laser sound effect and create the effect instance
            laserSound = _content.Load<SoundEffect>("Sounds\\laserFire");
            laserSoundInstance = laserSound.CreateInstance();

            // Load explosion sound effect and create the effect instance
            explosionSound = _content.Load<SoundEffect>("Sounds\\explosion");
            explosionSoundInstance = explosionSound.CreateInstance();

            // Load the BGM
            //gameMusic = _content.Load<Song>("Sounds\\gameMusic");

            //MediaPlayer.Play(gameMusic);

            return base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            //MediaPlayer.Stop();
            laserSoundInstance.Dispose();
            explosionSoundInstance.Dispose();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Save the previous state of the keyboard and game pad so we can determine single key/button presses
            _prevGamePadState = _currentGamePadState;
            _prevKeyboardState = _currentKeyboardState;

            // Read the current state of the keyboard and gamepad and store it
            _currentKeyboardState = Keyboard.GetState();
            _currentGamePadState = GamePad.GetState(PlayerIndex.One);

            // Get Mouse states
            _prevMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            // Update the player
            UpdatePlayer(gameTime);

            // Update the parallaxing background
            _bgLayer1.Update(gameTime);
            _bgLayer2.Update(gameTime);

            // Update the enemies
            UpdateEnemies(gameTime);

            // Update laser beams
            UpdateLasers(gameTime);

            // Update explosions
            UpdateExplosions(gameTime);

            // Update the collision
            UpdateCollision();

            // Check if ESC is pressed and go to GameOver screen
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ScreenManager.GotoScreen(GameOverScreen.SCREEN_NAME);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw the Main Background Texture
            _spriteBatch.Draw(_mainBackground, _rectBackground, Color.White);

            // Draw the moving background
            _bgLayer1.Draw(_spriteBatch);
            _bgLayer2.Draw(_spriteBatch);

            // Draw the Player
            _player.Draw(_spriteBatch);

            // Draw the Lasers
            for (int l = 0; l < laserBeams.Count; l++)
            {
                laserBeams[l].Draw(_spriteBatch);
            }

            // Draw the Enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(_spriteBatch);
            }

            // Draw the explosions
            for (int e = 0; e < explosions.Count; e++)
            {
                explosions[e].Draw(_spriteBatch);
            }

            base.Draw(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            _player.Update(gameTime);

            // Windows 8 Touch Gestures for MonoGame
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.FreeDrag)
                    _player.Position += gesture.Delta;
            }

            // Get Mouse State then Capture the Button type and Respond Button Press
            Vector2 mousePosition = new Vector2(_currentMouseState.X,
                                                _currentMouseState.Y);

            if (_currentMouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 posDelta = mousePosition - _player.Position;
                posDelta.Normalize();
                posDelta *= PlayerMoveSpeed;
                _player.Position += posDelta;
            }

            // Get Thumbstick Controls
            _player.Position.X += _currentGamePadState.ThumbSticks.Left.X * PlayerMoveSpeed;
            _player.Position.Y -= _currentGamePadState.ThumbSticks.Left.Y * PlayerMoveSpeed;

            // Use the Keyboard / Dpad
            if (IsLeftKey()) _player.Position.X -= PlayerMoveSpeed;
            if (IsRightKey()) _player.Position.X += PlayerMoveSpeed;
            if (IsUpKey()) _player.Position.Y -= PlayerMoveSpeed;
            if (IsDownKey()) _player.Position.Y += PlayerMoveSpeed;

            // Fire laser
            if (IsFireKey()) FireLaser(gameTime);

            // Make sure that the player does not go out of bounds
            _player.Position.Y = MathHelper.Clamp(_player.Position.Y, 0,
                _device.Viewport.Height - (_player.Height * Scale));
            _player.Position.X = MathHelper.Clamp(_player.Position.X, 0,
                _device.Viewport.Width - (_player.Width * Scale));
        }

        private bool IsUpKey()
        {
            return _currentKeyboardState.IsKeyDown(Keys.Up) ||
                    _currentKeyboardState.IsKeyDown(Keys.W) ||
                    ButtonState.Pressed == _currentGamePadState.DPad.Up;
        }

        private bool IsDownKey()
        {
            return _currentKeyboardState.IsKeyDown(Keys.Down) ||
                    _currentKeyboardState.IsKeyDown(Keys.S) ||
                    ButtonState.Pressed == _currentGamePadState.DPad.Down;
        }

        private bool IsLeftKey()
        {
            return _currentKeyboardState.IsKeyDown(Keys.Left) ||
                    _currentKeyboardState.IsKeyDown(Keys.A) ||
                    ButtonState.Pressed == _currentGamePadState.DPad.Left;
        }

        private bool IsRightKey()
        {
            return _currentKeyboardState.IsKeyDown(Keys.Right) ||
                    _currentKeyboardState.IsKeyDown(Keys.D) ||
                    ButtonState.Pressed == _currentGamePadState.DPad.Right;
        }

        private bool IsFireKey()
        {
            return _currentKeyboardState.IsKeyDown(Keys.Space) ||
                    (ButtonState.Pressed == _currentMouseState.RightButton) ||
                    (ButtonState.Pressed == _currentGamePadState.Buttons.X);
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            // Spawn a new enemy enemy every 1.5 seconds
            if (gameTime.TotalGameTime - prevSpawnTime > enemySpawnTime)
            {
                prevSpawnTime = gameTime.TotalGameTime;

                // Add an Enemy
                AddEnemy();
            }

            // Update the enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime);
                if (enemies[i].Active == false)
                {
                    enemies.RemoveAt(i);
                }
            }
        }

        private void AddEnemy()
        {
            // Create the animation
            Animation enemyAnimation = new Animation();

            // Initialize the animation with the correct animation information
            enemyAnimation.Initialize(enemyTexture, Vector2.Zero,
                                        47, 61, 8, 30, Color.White, 1f, true);

            // Randomly generate the position of the enemy
            Vector2 position = new Vector2(
                _device.Viewport.Width + enemyTexture.Width / 2,
                random.Next(100, _device.Viewport.Height - 100)
            );

            // Create an enemy
            Enemy enemy = new Enemy();

            // Initialize the enemy
            enemy.Initialize(enemyAnimation, position);

            // Add the enemy to the active enemies list
            enemies.Add(enemy);
        }

        private void FireLaser(GameTime gameTime)
        {
            // The rate of lasers fired
            if (gameTime.TotalGameTime - prevLaserSpawnTime > laserSpawnTime)
            {
                prevLaserSpawnTime = gameTime.TotalGameTime;

                // Add the laser to the list
                AddLaser();

                // Play laser sound
                laserSoundInstance.Play();
            }
        }

        private void UpdateLasers(GameTime gameTime)
        {
            for (int i = 0; i < laserBeams.Count; i++)
            {
                laserBeams[i].Update(gameTime);

                // Remove the beam when its deactivated or is at
                // the end of the screen
                if (laserBeams[i].Active == false ||
                    laserBeams[i].Position.X > _device.Viewport.Width)
                {
                    laserBeams.RemoveAt(i);
                }
            }

            /* TODO: Range var has never been used */
        }

        private void AddLaser()
        {
            Animation laserAnimation = new Animation();

            // Initialize the laser animation
            laserAnimation.Initialize(laserTexture, _player.Position,
                                        46, 16, 1, 30, Color.White, 1f, true);

            Laser laser = new Laser();

            // Get the starting position of the laser
            Vector2 laserPosition = _player.Position;
            laserPosition.Y += 37;
            laserPosition.X += 70;

            // initialize the laser
            laser.Initialize(laserAnimation, laserPosition);
            laserBeams.Add(laser);
        }

        private void AddExplosion(Vector2 enemyPosition)
        {
            Animation explosionAnimation = new Animation();
            Vector2 position = new Vector2(enemyPosition.X * 0.85f, enemyPosition.Y * 0.9f);

            explosionAnimation.Initialize(explosionTexture, position,
                                    134, 134, 12, 30, Color.White, 1.0f, true);

            Explosion explosion = new Explosion();
            explosion.Initialize(explosionAnimation, enemyPosition);

            explosions.Add(explosion);

            // Play the explosion sound
            explosionSound.Play();
        }

        public void UpdateExplosions(GameTime gameTime)
        {
            for (int e = 0; e < explosions.Count; e++)
            {
                explosions[e].Update(gameTime);
                if (explosions[e].Active == false)
                {
                    explosions.RemoveAt(e);
                }
            }
        }

        private void UpdateCollision()
        {
            // Use the Rectangle's built-in intersect function to
            // determine if two objects are overlapping
            Rectangle playerRectangle;
            Rectangle enemyRectangle;
            Rectangle laserRectangle;

            // Only create the rectangle once for the player
            playerRectangle = new Rectangle(
                (int)_player.Position.X,
                (int)_player.Position.Y,
                _player.Width,
                _player.Height);

            // Do the collision between the player and the enemies
            for (int e = 0; e < enemies.Count; e++)
            {
                enemyRectangle = new Rectangle(
                    (int)enemies[e].Position.X,
                    (int)enemies[e].Position.Y,
                    enemies[e].Width,
                    enemies[e].Height);

                // Determine if the two objects collided with each other
                if (playerRectangle.Intersects(enemyRectangle))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    _player.Health -= enemies[e].Damage;

                    // Add explosion where the enemy was
                    AddExplosion(enemies[e].Position);

                    // Since the enemy collided with the player destroy it
                    enemies[e].Health = 0;
                    enemies[e].Active = false;

                    // If the player health is less than zero we died
                    if (_player.Health <= 0)
                    {
                        _player.Active = false;

                        /* TODO: Add transition or fade out */
                        ScreenManager.GotoScreen(GameOverScreen.SCREEN_NAME);
                    }
                }

                // Detect if this enemy collide with any laser shots
                for (int lb = 0; lb < laserBeams.Count; lb++)
                {
                    laserRectangle = new Rectangle(
                        (int)laserBeams[lb].Position.X,
                        (int)laserBeams[lb].Position.Y,
                        laserBeams[lb].Width,
                        laserBeams[lb].Height);

                    // Test the bounds of the laser and the enemy
                    if (laserRectangle.Intersects(enemyRectangle))
                    {
                        // Apply the damage to the enemy
                        enemies[e].Health -= laserBeams[lb].Damage;

                        // Record the kill if the enemy is killed
                        if (enemies[e].Health <= 0)
                        {
                            // Add explosion where the enemy was
                            AddExplosion(enemies[e].Position);

                            /* TODO: record the kill */
                            //myGame.Stage.EnemiesKilled++;

                            laserBeams[lb].Active = false;
                            enemies[e].Active = false;

                            /* TODO: Record the score */
                            //myGame.Score += e.Value;
                        }
                    }
                }
            }
        }

        // END
    }
}
