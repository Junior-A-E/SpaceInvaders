﻿using System;

namespace MonoGameInvaders
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new PlayState())
                game.Run();
        }
    }
}
