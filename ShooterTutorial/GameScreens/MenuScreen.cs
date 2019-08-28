//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

using ShooterTutorial.GameObjects;

namespace ShooterTutorial.GameScreens
{
    public class MenuScreen : BaseScreen
    {
        private Texture2D backgroundTexture;
        private Rectangle backgroundRectangle;

        private static MenuButton _startButton;
        private static MenuButton _exitButton;

        //private Song menuMusic;

        //private int _screen_height;
        //private int _screen_width;

        private const string SCREEN_NAME = "menuScreen";

        public MenuScreen() : base(null, null, null)
        {
            Name = SCREEN_NAME;
        }

        public MenuScreen(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch)
            : base(device, content, spriteBatch)
        {
            Name = SCREEN_NAME;
        }

        public override bool Initialize()
        {
            backgroundTexture = _content.Load<Texture2D>("Graphics\\mainMenu");
            backgroundRectangle = new Rectangle(0, 0,
                backgroundTexture.Width, _device.Viewport.Height);

            SpriteFont spriteFont = _content.Load<SpriteFont>("Graphics\\gameFont");

            _startButton = new MenuButton(spriteFont);
            _startButton.Initialize("NEW GAME", new Vector2(340, 300));

            _exitButton = new MenuButton(spriteFont);
            _exitButton.Initialize("EXIT", new Vector2(380, 350));

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
            if (_startButton.Clicked())
                ScreenManager.GotoScreen(new GameScreen().Name);

            if (_exitButton.Clicked() || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                QuitGame();
                return;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            _startButton.Draw(_spriteBatch);
            _exitButton.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        // END
    }
}
