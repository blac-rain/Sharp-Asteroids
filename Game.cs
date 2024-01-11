using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SharpAsteroids
{
    public class Game
    {
        List<Bullet> bullets = new List<Bullet>(32);
        List<Enemy> enemies = new List<Enemy>(16);
        Player player;
        float enemyTimer;
        int score = 0;
        int death = 0;
        public bool win;
        public bool loose;
        public bool end;
        public static bool goUp;
        public static bool goDown;
        public static bool goLeft;
        public static bool goRight;

        public Game()
        {
            player = new Player(bullets);
            Bullet.LeftScreen += OnLeftScreen;
            player.BulletSpawn += OnBulletSpawn;
        }

        public void Unsubscribe()
        {
            Bullet.LeftScreen -= OnLeftScreen;
            player.BulletSpawn -= OnBulletSpawn;
        }

        public void OnLeftScreen(Bullet bullet)
        {
            bullets.Remove(bullet);
        }

        public void OnBulletSpawn(float x, float y, Vector2 dir)
        {
            Bullet b = new Bullet((int)x, (int)y, dir);
            bullets.Add(b);
        }

        public void Update()
        {
            enemyTimer += Raylib.GetFrameTime();
            if (enemyTimer >= 3f)
            {
                enemyTimer = 0f;
                int randX = Raylib.GetRandomValue(0, 800);
                if (randX > 650)
                {
                    goRight = true;
                }
                if (randX < 150)
                {
                    goLeft = true;
                }
                int randY = Raylib.GetRandomValue(0, 480);
                if (randY < 150)
                {
                    goDown = true;
                }
                if (randY > 330)
                {
                    goUp = true;
                }
                Enemy enemy = new Enemy(randX, randY);
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
                        score++;
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
            Raylib.DrawText("POINTS: " + score, 5, 5, 20, Color.WHITE);
            Raylib.DrawText("DEATH:  " + death, 5, 30, 20, Color.WHITE);
            //Raylib.DrawText("Game Time: " + GetTime()-startTime, 5, 55, 20, Color.WHITE);
        }
    }
}
