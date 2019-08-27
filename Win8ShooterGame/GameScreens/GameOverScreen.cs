//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace ShooterTutorial.GameScreens
{
    public class GameOverScreen : BaseScreen
    {
        private Texture2D texture;
        private Rectangle destinationRectangle;
        //private Song menuMusic;

        //private int _screen_height;
        //private int _screen_width;

        public const string SCREEN_NAME = "gameOverScreen";

        public GameOverScreen(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch)
            : base(device, content, spriteBatch, SCREEN_NAME)
        {
        }

        public override bool Initialize()
        {
            ShooterTutorialGame.MouseVisibility = true;

            texture = _content.Load<Texture2D>("Graphics\\endMenu");
            destinationRectangle = new Rectangle(0, 0,
                                        texture.Width, _device.Viewport.Height);

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

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(texture, destinationRectangle, Color.White);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            /* Improve this shit yikes - make this a button to click */
            // Go to Menu Screen when pressing M (for Menu)
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                ScreenManager.GotoScreen(MenuScreen.SCREEN_NAME);
            }
            base.Update(gameTime);
        }
    }
}
