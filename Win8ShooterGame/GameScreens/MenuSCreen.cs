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

            _startButton = new GameStartButton(_device, _content, _spriteBatch);
            _startButton.ButtonPosition = new Vector2(340, 300);
            _startButton.Initialize();

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
            if (BaseButton.IsHoverButton() &&
                ButtonState.Pressed == Mouse.GetState().LeftButton)
            {
                ScreenManager.GotoScreen(GameScreen.SCREEN_NAME);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(menuTexture, backgroundRectangle, Color.White);
            _startButton.Draw();

            base.Draw(gameTime);
        }

        // END
    }
}
