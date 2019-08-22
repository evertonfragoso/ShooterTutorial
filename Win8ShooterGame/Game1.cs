using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using ShooterTutorial.GameObjects;

namespace ShooterTutorial
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // A movement speed for the player
        private const float PlayerMoveSpeed = 8;

        // Represents the player
        private Player _player;

        // Image used to display the static background
        private Texture2D _mainBackground;
        private Rectangle _rectBackground;
        private ParallaxingBackground _bgLayer1;
        private ParallaxingBackground _bgLayer2;
        const float Scale = 1f;

        // Keyboard states used to determine key presses
        private KeyboardState _currentKeyboardState;
        private KeyboardState _prevKeyboardState;

        // Gamepad states used to determine button presses
        private GamePadState _currentGamePadState;
        private GamePadState _prevGamePadState;

        // Mouse states used to track Mouse button press
        private MouseState _currentMouseState;
        private MouseState _prevMouseState;

        // Lasers
        private Texture2D laserTexture;
        private List<Laser> laserBeams;

        // The rate at which the laser can be fired
        private TimeSpan laserSpawnTime;
        private TimeSpan prevLaserSpawnTime;

        // Enemies
        private Texture2D enemyTexture;
        private List<Enemy> enemies;

        // The rate at which the enemies appear
        private TimeSpan enemySpawnTime;
        private TimeSpan prevSpawnTime;

        // Explosions
        private List<Explosion> explosions;
        private Texture2D explosionTexture;

        // A random number generator
        private Random random;

        public Game1()
        {
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Initialize the player class
            _player = new Player();

            // Background
            _rectBackground = new Rectangle(0, 0, GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height);
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

            // Enable the FreeDrag gesture
            TouchPanel.EnabledGestures = GestureType.FreeDrag;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Load the player resources

            Rectangle titleSafeArea = GraphicsDevice.Viewport.TitleSafeArea;
            Vector2 playerPosition = new Vector2(titleSafeArea.X,
                titleSafeArea.Y + titleSafeArea.Height / 2);

            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("Graphics\\shipAnimation");
            playerAnimation.Initialize(playerTexture, playerPosition,
                115, 69, 8, 30, Color.White, Scale, true);

            _player.Initialize(playerAnimation, playerPosition);

            // Load the parallaxing background
            _bgLayer1.Initialize(Content, "Graphics\\bgLayer1",
                GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);
            _bgLayer2.Initialize(Content, "Graphics\\bgLayer2",
                GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);

            _mainBackground = Content.Load<Texture2D>("Graphics\\mainbackground");

            // Load the laser texture
            laserTexture = Content.Load<Texture2D>("Graphics\\laser");

            // Enemy texture
            enemyTexture = Content.Load<Texture2D>("Graphics\\mineAnimation");

            // Explosion texture
            explosionTexture = Content.Load<Texture2D>("Graphics\\explosion");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // Start drawing
            spriteBatch.Begin();

            // Draw the Main Background Texture
            spriteBatch.Draw(_mainBackground, _rectBackground, Color.White);

            // Draw the moving background
            _bgLayer1.Draw(spriteBatch);
            _bgLayer2.Draw(spriteBatch);

            // Draw the Player
            _player.Draw(spriteBatch);

            // Draw the Lasers
            for(int l = 0; l < laserBeams.Count; l++)
            {
                laserBeams[l].Draw(spriteBatch);
            }

            // Draw the Enemies
            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }

            // Draw the explosions
            for(int e = 0; e < explosions.Count; e++)
            {
                explosions[e].Draw(spriteBatch);
            }

            // Stop drawing
            spriteBatch.End();

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
                GraphicsDevice.Viewport.Height - (_player.Height * Scale));
            _player.Position.X = MathHelper.Clamp(_player.Position.X, 0,
                GraphicsDevice.Viewport.Width - (_player.Width * Scale));
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
                GraphicsDevice.Viewport.Width + enemyTexture.Width / 2,
                random.Next(100, GraphicsDevice.Viewport.Height - 100)
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
            if(gameTime.TotalGameTime - prevLaserSpawnTime > laserSpawnTime)
            {
                prevLaserSpawnTime = gameTime.TotalGameTime;

                // Add the laser to the list
                AddLaser();
            }
        }

        private void UpdateLasers(GameTime gameTime)
        {
            for (int i = 0; i < laserBeams.Count; i++)
            {
                laserBeams[i].Update(gameTime);

                // Remove the beam when its deactivated or is at
                // the end of the screen
                if(laserBeams[i].Active == false ||
                    laserBeams[i].Position.X > GraphicsDevice.Viewport.Width)
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

            /* TODO: add code to create a laser. */
            //laserSoundInstance.Play();
        }

        private void AddExplosion(Vector2 enemyPosition)
        {
            Animation explosionAnimation = new Animation();

            explosionAnimation.Initialize(explosionTexture, enemyPosition,
                                    134, 134, 12, 30, Color.White, 1.0f, true);

            Explosion explosion = new Explosion();
            explosion.Initialize(explosionAnimation, enemyPosition);

            explosions.Add(explosion);
        }

        public void UpdateExplosions(GameTime gameTime)
        {
            for(int e = 0; e < explosions.Count; e++)
            {
                explosions[e].Update(gameTime);
                if(explosions[e].Active == false)
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
            playerRectangle = new Rectangle((int)_player.Position.X,
                (int)_player.Position.Y,
                _player.Width,
                _player.Height);

            // Do the collision between the player and the enemies
            for (int e = 0; e < enemies.Count; e++)
            {
                enemyRectangle = new Rectangle((int)enemies[e].Position.X,
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
                    }
                }

                // Detect if this enemy collide with any laser shots
                for (int lb = 0; lb < laserBeams.Count; lb++)
                {
                    laserRectangle = new Rectangle((int)laserBeams[lb].Position.X,
                        (int)laserBeams[lb].Position.Y,
                        laserBeams[lb].Width,
                        laserBeams[lb].Height);

                    // Test the bounds of the laser and the enemy
                    if(laserRectangle.Intersects(enemyRectangle))
                    {
                        // Apply the damage to the enemy
                        enemies[e].Health -= laserBeams[lb].Damage;

                        // Record the kill if the enemy is killed
                        if(enemies[e].Health <= 0)
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
    }
}
