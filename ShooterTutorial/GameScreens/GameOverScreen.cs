using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Media;

using ShooterTutorial.GameObjects;

namespace ShooterTutorial.GameScreens
{
    public class GameOverScreen : BaseScreen
    {
        private Texture2D backgroundTexture;
        private Rectangle backgroundRectangle;
        //private Song menuMusic;

        private static MenuButton _menuButton;

        private const string MENU_BUTTON_TEXT = "MENU";

        protected override void SetScreenName() => SCREEN_NAME = "gameOverScreen";

        public GameOverScreen() : base(null, null, null) { }

        public GameOverScreen(GraphicsDevice device, ContentManager content,
            SpriteBatch spriteBatch) : base(device, content, spriteBatch) { }

        public override bool Initialize()
        {
            ShooterTutorialGame.MouseVisibility = true;

            backgroundTexture = _content.Load<Texture2D>("Graphics\\endMenu");
            backgroundRectangle = new Rectangle(0, 0,
                backgroundTexture.Width, _device.Viewport.Height);

            SpriteFont spriteFont = _content.Load<SpriteFont>("Graphics\\gameFont");

            _menuButton = new MenuButton(spriteFont);
            _menuButton.Initialize(MENU_BUTTON_TEXT, new Vector2(360,
                _device.Viewport.Height - spriteFont.MeasureString(MENU_BUTTON_TEXT).Y - 10));

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
                ScreenManager.GotoScreen(new MenuScreen());

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            _menuButton.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
