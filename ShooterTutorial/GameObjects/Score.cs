//using System;

namespace ShooterTutorial.GameObjects
{
    public class Score
    {
        public int LevelOneScore;
        public int TotalScore;
        public int TotalEnemiesKilled;
        public int TotalBossesKilled;
        public int TotalStagesCleared;

        public Score()
        {
            LevelOneScore = 0;
            TotalScore = 0;
            TotalEnemiesKilled = 0;
            TotalBossesKilled = 0;
            TotalStagesCleared = 0;
        }
    }
}
