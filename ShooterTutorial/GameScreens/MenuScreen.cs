//using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

using ShooterTutorial.GameObjects.Buttons;

namespace ShooterTutorial.GameScreens
{
    public class MenuScreen : BaseScreen
    {
        private Texture2D menuTexture;
        private Rectangle backgroundRectangle;

        private static GameStartButton _startButton;
        private static GameExitButton _exitButton;

        //private Song menuMusic;

        //private int _screen_height;
        //private int _screen_width;

        public const string SCREEN_NAME = "menuScreen";

        public MenuScreen(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch)
            : base(device, content, spriteBatch, SCREEN_NAME)
        {
        }

        public override bool Initialize()
        {
            menuTexture = _content.Load<Texture2D>("Graphics\\mainMenu");
            backgroundRectangle = new Rectangle(0, 0,
                menuTexture.Width, _device.Viewport.Height);

            SpriteFont spriteFont = _content.Load<SpriteFont>("Graphics\\gameFont");

            BaseButton startBaseButton = new BaseButton();
            BaseButton exitBaseButton = new BaseButton();
            _startButton = new GameStartButton();
            _exitButton = new GameExitButton();

            Vector2 startButtonPosition = new Vector2(340, 300);
            Vector2 exitButtonPosition = new Vector2(380, 350);

            startBaseButton.Initialize(spriteFont, _startButton.Text, startButtonPosition);
            exitBaseButton.Initialize(spriteFont, _exitButton.Text, exitButtonPosition);

            _startButton.Initialize(startBaseButton, startButtonPosition);
            _exitButton.Initialize(exitBaseButton, exitButtonPosition);

            //MediaPlayer.Play(menuMusic);
            //menuMusic = _content.Load<Song>("Sounds\\menuMusic");

            return base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            //MediaPlayer.Stop();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //if (_startButton.IsHoverButton() &&
            //    ButtonState.Pressed == Mouse.GetState().LeftButton)
            //{
            //    ScreenManager.GotoScreen(GameScreen.SCREEN_NAME);
            //}

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(menuTexture, backgroundRectangle, Color.White);
            _startButton.Draw(_spriteBatch);
            _exitButton.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        // END
    }
}
