﻿/************************************************************************/
/* Author : David Amador 
 * Web:      http://www.david-amador.com
 * Twitter : http://www.twitter.com/DJ_Link                             
 * 
 * You can use this for whatever you want. If you want to give me some
 * credit for it that's cool but not mandatory
/************************************************************************/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.GameScreens
{
    public class BaseScreen
    {
        protected GraphicsDevice _device;
        protected ContentManager _content;
        protected SpriteBatch _spriteBatch;

        protected string SCREEN_NAME;

        public string Name {
            get => SCREEN_NAME;
            set => SCREEN_NAME = value;
        }

        protected virtual void SetScreenName() => throw new System.NotImplementedException();

        /// <summary>
        /// Screen Constructor
        /// </summary>
        public BaseScreen(GraphicsDevice device, ContentManager content,
            SpriteBatch spriteBatch)
        {
            _device = device;
            _content = content;
            _spriteBatch = spriteBatch;

            SetScreenName();
        }

        /// <summary>
        /// Virtual Function that's called when entering a Screen
        /// override it and add your own initialization code
        /// </summary>
        /// <returns></returns>
        public virtual bool Initialize() => true;

        /// <summary>
        /// Virtual Function that's called when exiting a Screen
        /// override it and add your own shutdown code
        /// </summary>
        /// <returns></returns>
        public virtual void LoadContent() { }

        /// <summary>
        /// Override it to have access to elapsed time
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime) { }

        public virtual void UnloadContent() { }
    }
}
