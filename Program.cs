using System;

namespace EksamensProjekt2021
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameWorld())
                game.Run();
        }
    }
}
