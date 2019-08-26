/************************************************************************/
/* Author : David Amador 
 * Web:      http://www.david-amador.com
 * Twitter : http://www.twitter.com/DJ_Link                             
 * 
 * You can use this for whatever you want. If you want to give me some
 * credit for it that's cool but not mandatory
/************************************************************************/

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ShooterTutorial.GameScreens
{
    public class BaseScreen
    {
        protected GraphicsDevice _device;
        protected ContentManager _content;
        protected SpriteBatch _spriteBatch;

        /// <summary>
        /// Screen Constructor
        /// </summary>
        /// <param name="name">Must be unique since when you use ScreenManager is per name</param>
        public BaseScreen(GraphicsDevice device, ContentManager content, string name)
        {
            Name = name;
            _device = device;
            _content = content;
            _spriteBatch = new SpriteBatch(_device);
        }

        public string Name;

        /// <summary>
        /// Virtual Function that's called when entering a Screen
        /// override it and add your own initialization code
        /// </summary>
        /// <returns></returns>
        public virtual bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// Virtual Function that's called when exiting a Screen
        /// override it and add your own shutdown code
        /// </summary>
        /// <returns></returns>
        public virtual void LoadContent()
        {
        }

        /// <summary>
        /// Override it to have access to elapsed time
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
            _spriteBatch.End();
        }

        public virtual void UnloadContent()
        {
        }
    }
}
