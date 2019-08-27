using System;

namespace ShooterTutorial
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new ShooterTutorialGame())
                game.Run();
        }
    }
}
