using Raylib_cs;
using System.Collections.Generic;
using System;
using System.Numerics;

namespace GameTest;

//TODO Collision Check für Player und Enemies
//TODO Death Counter bei Berührung durch Enemy
//TODO simple Darstellung von Death Counter und Points
//TODO Enemies von allen Seiten
//TODO Darstellung von Player ändern, damit sichtbar ist, wo vorne ist -> evtl.neue Form?
//TODO Verlieren und Gewinnen einbauen -> benötigt eigenes Overlay und muss Game stoppen(evtl.neue Klasse "GameOverScreen"?), wichtig aber kann nicht einschätzen wie aufwändig das ist

//Design, wenig Aufwand(nicht wichtig):
//MAYBE andere Form für Enemy, mehr wie Asteroiden -> Bild nutzen?
//MAYBE gehen Bullets transparent? (sieht dann mehr aus wie Laser-Schüsse)

//Wahrscheinlich aufwändiger, nicht so wichtig und evtl. zu viel für 2 Tage:
//TODO Darstellung von Points und Death Counter nicht nur als DrawText, z.B.als eigene Leiste unten oder oben
//TODO Game Time darstellen
//TODO Fenster mit änderbarer Größe
//TODO Start Button -> neue Overlay oder Menü Klasse für solche Funktionen?
//TODO Pausen Funktion

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

        Raylib.CloseWindow();
    }
}

public class Game
{
    List<Bullet> bullets = new List<Bullet>(32);
    List<Enemy> enemies = new List<Enemy>(16);
    Player player;
    float enemyTimer;
    int points = 0;
    int death = 0;

    public Game()
    {
        player = new Player(bullets);
        Bullet.LeftScreen += OnLeftScreen;
    }

    public void OnLeftScreen(Bullet bullet)
    {
        bullets.Remove(bullet);
    }

    public void Update()
    {
        enemyTimer += Raylib.GetFrameTime();
        if (enemyTimer >= 3f)
        {
            enemyTimer = 0f;
            int randX = Raylib.GetRandomValue(0, 800);
            Enemy enemy = new Enemy(randX, -50);
            enemies.Add(enemy);
        }
        //collision check bullets and enemies
        for (int b = bullets.Count - 1; b >= 0; b--)
        {
            for (int e = enemies.Count - 1; e >= 0; e--)
            {
                if (Raylib.CheckCollisionCircleRec(bullets[b].Pos, bullets[b].Radius, enemies[e].Rec))
                {
                    bullets.RemoveAt(b);
                    enemies.RemoveAt(e);
                    points++;
                    break;
                }
            }
        }

        //collision check player
        for (int e = enemies.Count - 1; e >= 0; e--)
        {
            if (Raylib.CheckCollisionRecs(player.Rec, enemies[e].Rec))
            {
                enemies.RemoveAt(e);
                death++;
                break;
            }
        }

        player.Update();
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].Update();
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Update();
        }
    }

    public void Draw()
    {
        player.Draw();
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].Draw();
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Draw();
        }
        Raylib.DrawText("POINTS: " + points, 5, 5, 20, Color.WHITE);
        Raylib.DrawText("DEATH:  " + death, 5, 30, 20, Color.WHITE);
    }
}

public class Player
{
    List<Bullet> bullets = new List<Bullet>();
    public Vector2 Pos => new Vector2(posX, posY);
    public Rectangle Rec => new Rectangle(posX, posY, sizeX, sizeY);
    public int SizeX => sizeX;
    public int SizeY => sizeY;
    float angle;
    Vector2 startDir = new Vector2(0f, -1f);
    Vector2 dir = new Vector2();

    int sizeX = 15;
    int sizeY = 30;
    float posX = 400;
    float posY = 360;

    int moveSpeed = 3;


    public Player(List<Bullet> bullets)
    {
        this.bullets = bullets;
    }

    public void Update()
    {
        //shoot
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            Bullet b = new Bullet((int)posX, (int)posY, dir);
            bullets.Add(b);
        }

        //movement
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_UP))
        {
            //posY -= moveSpeed;
            posX += dir.X;
            posY += dir.Y;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            posX -= dir.X;
            posY -= dir.Y;
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            angle -= 3f;
            dir = Raymath.Vector2Rotate(startDir, Raylib.DEG2RAD * angle);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            angle += 3f;
            dir = Raymath.Vector2Rotate(startDir, Raylib.DEG2RAD * angle);
        }

        //if window border is reached, move to other side
        if (posY < 0) posY = 480;
        if (posY > 480) posY = 0;
        if (posX < 0) posX = 800;
        if (posX > 800) posX = 0;
    }

    public void Draw()
    {
        Rectangle rect = new Rectangle(posX, posY, sizeX, sizeY);
        Vector2 origin = new Vector2(sizeX / 2, sizeY / 2);
        Raylib.DrawRectanglePro(rect, origin, angle, Color.BLUE);
        //Raylib.DrawCircle((int)posX, (int)posY, 3f, Color.RED); //for debugging
    }
}

public class Bullet
{
    public Vector2 Pos => new Vector2(posX, posY); //shorthand for Vector2 getter method
    public float Radius => radius;

    int posX;
    int posY;
    Vector2 dir = new Vector2();
    float radius = 5f;
    float speed = 3f;

    public static event Action<Bullet>? LeftScreen;

    public Bullet(int x, int y, Vector2 v)
    {
        posX = x;
        posY = y;
        dir = v;
    }

    public void Update()
    {
        posX += (int)(dir.X * speed);
        posY += (int)(dir.Y * speed);

        // Destroy self offscreen
        if (posY < 0 || posX < 0 || posY > 480 || posX > 800)
        {
            LeftScreen?.Invoke(this);
        }
    }

    public void Draw()
    {
        Raylib.DrawCircle((int)posX, (int)posY, radius, Color.RED);
    }
}

public class Enemy
{
    public Vector2 Pos => new Vector2(posX, posY);
    public Rectangle Rec => new Rectangle(posX, posY, sizeX, sizeY);
    public int SizeX => sizeX;

    int sizeX = 15;
    int sizeY = 15;
    int posX;
    int posY;

    public Enemy(int x = 0, int y = 0)
    {
        posX = x;
        posY = y;
    }

    public void Update()
    {
        posY += 3;
    }
    public void Draw()
    {
        Raylib.DrawRectangle(posX, posY, sizeX, sizeY, Color.ORANGE);
        //when hitbox as circle:
        //Raylib.DrawRectangle(posX - sizeX/2, posY - sizeY/2, sizeX, sizeY, Color.ORANGE);
    }
}