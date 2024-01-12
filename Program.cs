using Raylib_cs;

namespace SharpAsteroids;

//assets and sounds by https://www.kenney.nl under CC0 license
//font Fira Code https://github.com/tonsky/FiraCode under OFL-1.1 license

class Program
{
    public static void Main()
    {        
        Raylib.InitWindow(800, 480, "Game 01");        
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();

        Texture2D playerTexture = Raylib.LoadTexture("resources/ship_red-small.png");
        Font fontFC = Raylib.LoadFont("resources/FiraCode-Regular.otf");
        Font fontFCB = Raylib.LoadFont("resources/FiraCode-Bold.otf");
        Player player = new(null, playerTexture);
        Raylib.SetMasterVolume(0.5f);

        Game game = new(fontFCB, playerTexture);
        
        //console error messages for texture and fonts
        if (!Raylib.IsTextureReady(playerTexture))
        { 
            Console.WriteLine("Texture loading failed!"); 
        }
        if (!Raylib.IsFontReady(fontFC))
        {
            Console.WriteLine("Fira Code loading failed!");
        }
        if (!Raylib.IsFontReady(fontFCB))
        {
            Console.WriteLine("Fira Code Bold loading failed!");
        }

        while (!Raylib.WindowShouldClose())
        {
            game.Update();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            game.Draw();
            Raylib.EndDrawing();
        }
        game.Unsubscribe();
        Raylib.UnloadTexture(playerTexture);
        Raylib.UnloadFont(fontFC);
        Raylib.UnloadFont(fontFCB);

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }
}