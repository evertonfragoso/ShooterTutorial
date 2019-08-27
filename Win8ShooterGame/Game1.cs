﻿//using System;
//using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using ShooterTutorial.GameScreens;

namespace ShooterTutorial
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private const int DEFAULT_SCREEN_WIDTH = 800;
        private const int DEFAULT_SCREEN_HEIGHT = 480;

        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            IsMouseVisible = true;

            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = DEFAULT_SCREEN_WIDTH,
                PreferredBackBufferHeight = DEFAULT_SCREEN_HEIGHT
            };

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
            //System.Diagnostics.Debug.WriteLine("graphics {0}", graphics);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialize screen manager and add screens to it
            ScreenManager.AddScreen(new MenuScreen(GraphicsDevice, Content, spriteBatch));
            ScreenManager.AddScreen(new GameScreen(GraphicsDevice, Content, spriteBatch));
            ScreenManager.AddScreen(new GameOverScreen(GraphicsDevice, Content, spriteBatch));

            // Set the active screen to the game menu
            ScreenManager.GotoScreen(MenuScreen.SCREEN_NAME);
            //ScreenManager.GotoScreen(GameScreen.SCREEN_NAME);

            ScreenManager.Initialize();

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
            // Have the active screen initialize itself
            ScreenManager.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            ScreenManager.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            /* TODO: Update ESC to pause and open in-game menu instead of close */
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ScreenManager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            // Draw the active screen
            ScreenManager.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        // END
    }
}
