using Raylib_cs;
using System.Numerics;

namespace SharpAsteroids;



class Program
{
    public static void Main()
    {
        Raylib.InitWindow(800, 480, "Game 01");
        Raylib.SetTargetFPS(60);

        Game game = new Game();

        while (!Raylib.WindowShouldClose())
        {
            game.Update();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            game.Draw();
            Raylib.EndDrawing();
        }
        game.Unsubscribe();

        Raylib.CloseWindow();
    }
}