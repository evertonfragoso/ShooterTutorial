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
    public class GameOverScreen : BaseScreen
    {
        private Texture2D backgroundTexture;
        private Rectangle destinationRectangle;
        //private Song menuMusic;

        private static MenuButton _menuButton;

        //private int _screen_height;
        //private int _screen_width;

        private const string SCREEN_NAME = "gameOverScreen";

        public GameOverScreen() : base(null, null, null)
        {
            Name = SCREEN_NAME;
        }

        public GameOverScreen(GraphicsDevice device, ContentManager content,
            SpriteBatch spriteBatch) : base(device, content, spriteBatch)
        {
            Name = SCREEN_NAME;
        }

        public override bool Initialize()
        {
            ShooterTutorialGame.MouseVisibility = true;

            backgroundTexture = _content.Load<Texture2D>("Graphics\\endMenu");
            destinationRectangle = new Rectangle(0, 0,
                backgroundTexture.Width, _device.Viewport.Height);

            SpriteFont spriteFont = _content.Load<SpriteFont>("Graphics\\gameFont");

            _menuButton = new MenuButton(spriteFont);
            _menuButton.Initialize("MENU", new Vector2(360, 300));

            //menuMusic = _content.Load<Song>("Sounds\\menuMusic");
            //MediaPlayer.Play(menuMusic);

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
            if (_menuButton.Clicked())
                ScreenManager.GotoScreen(new MenuScreen().Name);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(backgroundTexture, destinationRectangle, Color.White);
            _menuButton.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
