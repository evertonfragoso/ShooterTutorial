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
        private KeyboardState _previousKeyboardState;

        // Gamepad states used to determine button presses
        private GamePadState _currentGamePadState;
        private GamePadState _prevGamePadState;

        // Mouse states used to track Mouse button press
        private MouseState _currentMouseState;
        private MouseState _prevMouseState;

        // Enemies
        private Texture2D enemyTexture;
        private List<Enemy> enemies;

        // The rate at which the enemies appear
        private TimeSpan enemySpawnTime;
        private TimeSpan prevSpawnTime;

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
            _rectBackground = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _bgLayer1 = new ParallaxingBackground();
            _bgLayer2 = new ParallaxingBackground();

            // Initialize the enemies list
            enemies = new List<Enemy>();

            // Set the time keepers to zero
            prevSpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns
            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

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
            Vector2 playerPosition = new Vector2(titleSafeArea.X, titleSafeArea.Y + titleSafeArea.Height / 2);

            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("Graphics\\shipAnimation");
            playerAnimation.Initialize(playerTexture, playerPosition, 115, 69, 8, 30, Color.White, Scale, true);

            _player.Initialize(playerAnimation, playerPosition);

            // Load the parallaxing background
            _bgLayer1.Initialize(Content, "Graphics/bgLayer1", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);
            _bgLayer2.Initialize(Content, "Graphics/bgLayer2", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);

            _mainBackground = Content.Load<Texture2D>("Graphics/mainbackground");

            // Enemy texture
            enemyTexture = Content.Load<Texture2D>("Graphics/mineAnimation");
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
            _previousKeyboardState = _currentKeyboardState;

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

            // Draw the Enemies
            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
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
            Vector2 mousePosition = new Vector2(_currentMouseState.X, _currentMouseState.Y);

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

            // Make sure that the player does not go out of bounds
            _player.Position.Y = MathHelper.Clamp(_player.Position.Y, 0,
                GraphicsDevice.Viewport.Height - (_player.Height * Scale));
            _player.Position.X = MathHelper.Clamp(_player.Position.X, 0,
                GraphicsDevice.Viewport.Width - (_player.Width * Scale));
        }

        private bool IsUpKey()
        {
            return (_currentKeyboardState.IsKeyDown(Keys.Up) ||
                    _currentKeyboardState.IsKeyDown(Keys.W) ||
                    ButtonState.Pressed == _currentGamePadState.DPad.Up);
        }

        private bool IsDownKey()
        {
            return (_currentKeyboardState.IsKeyDown(Keys.Down) ||
                    _currentKeyboardState.IsKeyDown(Keys.S) ||
                    ButtonState.Pressed == _currentGamePadState.DPad.Down);
        }

        private bool IsLeftKey()
        {
            return (_currentKeyboardState.IsKeyDown(Keys.Left) ||
                    _currentKeyboardState.IsKeyDown(Keys.A) ||
                    ButtonState.Pressed == _currentGamePadState.DPad.Left);
        }

        private bool IsRightKey()
        {
            return (_currentKeyboardState.IsKeyDown(Keys.Right) ||
                    _currentKeyboardState.IsKeyDown(Keys.D) ||
                    ButtonState.Pressed == _currentGamePadState.DPad.Right);
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
    }
}
