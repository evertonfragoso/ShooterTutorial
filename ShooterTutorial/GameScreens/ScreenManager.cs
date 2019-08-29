/************************************************************************/
/* Author : David Amador 
 * Web:      http://www.david-amador.com
 * Twitter : http://www.twitter.com/DJ_Link                             
 * 
 * You can use this for whatever you want. If you want to give me some
 * credit for it that's cool but not mandatory
/************************************************************************/

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameScreens
{
    /// <summary>
    /// Screen Manager
    /// Keeps a list of available screens so you can switch between them, 
    /// ie. jumping from the start screen to the game screen
    /// </summary>
    public static class ScreenManager
    {
        // Private members
        private static readonly List<BaseScreen> _screens = new List<BaseScreen>();
        private static bool _started;
        private static BaseScreen _previous;

        // Public members
        public static BaseScreen ActiveScreen;

        /// <summary>
        /// Add new Screen
        /// </summary>
        /// <param name="screen">New screen, name must be unique</param>
        public static void AddScreen(BaseScreen screen)
        {
            for (int i = 0; i < _screens.Count; i++)
                if (_screens[i].Name == screen.Name)
                    return;

            _screens.Add(screen);
        }

        public static int GetTotalScreens() => _screens.Count;

        public static BaseScreen GetScreen(int idx) => _screens[idx];

        /// <summary>
        /// Go to screen
        /// </summary>
        /// <param name="destinationScreen">Screen name</param>
        public static void GotoScreen(BaseScreen destinationScreen)
        {
            foreach (BaseScreen screen in _screens)
                if (screen.Name == destinationScreen.Name)
                {
                    // Shutdown previous screen
                    _previous = ActiveScreen;
                    if (ActiveScreen != null) ActiveScreen.UnloadContent();

                    // Initialize new screen
                    ActiveScreen = screen;

                    if (_started) ActiveScreen.Initialize();

                    return;
                }
        }

        /// <summary>
        /// Initialize screen if it exists
        /// </summary>
        public static void Initialize()
        {
            _started = true;
            if (ActiveScreen != null) ActiveScreen.Initialize();
        }

        /// <summary>
        /// Falls back to previous selected screen if any
        /// </summary>
        public static void GoBack()
        {
            if (_previous != null)
            {
                GotoScreen(_previous);
                return;
            }
        }

        /// <summary>
        /// Loads screen content
        /// </summary>
        public static void LoadContent()
        {
            if (_started == false) return;
            if (ActiveScreen != null) ActiveScreen.LoadContent();
        }

        public static void UnloadContent()
        {
            if (_started == false) return;
            if (ActiveScreen != null) ActiveScreen.UnloadContent();
        }

        /// <summary>
        /// Updates Active Screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public static void Update(GameTime gameTime)
        {
            if (_started == false) return;
            if (ActiveScreen != null) ActiveScreen.Update(gameTime);
        }

        /// <summary>
        /// Draws the screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public static void Draw(GameTime gameTime)
        {
            if (_started == false) return;
            if (ActiveScreen != null) ActiveScreen.Draw(gameTime);
        }
    }
}
