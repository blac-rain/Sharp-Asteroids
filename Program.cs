using Raylib_cs;

namespace SharpAsteroids;

//assets and sounds under CC0 (https://creativecommons.org/publicdomain/zero/1.0/) by https://www.kenney.nl

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