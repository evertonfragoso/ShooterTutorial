//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Media;

namespace ShooterTutorial.GameScreens
{
    public class MenuScreen : BaseScreen
    {
        private Texture2D texture;
        private Rectangle destinationRectangle;
        //private Song menuMusic;

        //private int _screen_height;
        //private int _screen_width;

        public const string SCREEN_NAME = "menuScreen";

        public MenuScreen(GraphicsDevice device, ContentManager content)
            : base(device, content, SCREEN_NAME)
        {
        }

        public override bool Initialize()
        {
            texture = _content.Load<Texture2D>("Graphics\\mainMenu");
            destinationRectangle = new Rectangle(0, 0,
                                        texture.Width, _device.Viewport.Height);

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

        public override void Draw(GameTime gameTime)
        {
            _device.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, destinationRectangle, Color.White);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            /* Improve this shit yikes - make this a button to click */
            // Go to Game Screen when pressing N (for New)
            if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                ScreenManager.GotoScreen(GameScreen.SCREEN_NAME);
            }
            base.Update(gameTime);
        }
    }
}
