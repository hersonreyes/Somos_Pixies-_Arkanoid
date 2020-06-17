using Microsoft.Win32.SafeHandles;

namespace Arkanaoid_poo
{
    public static class GameData
    {
        public static bool gameStarted = false;
        public static double ticksCount = 0;
        public static int dirX = 16, dirY = -dirX, hearts= 3, score = 0;

        public static void InitializeGame()
        {
            gameStarted = false;
            hearts = 3;
            score = 0;
        }
        
    }
}