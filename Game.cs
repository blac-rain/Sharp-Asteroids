using Raylib_cs;
using System.Numerics;

namespace SharpAsteroids
{
    public class Game
    {
        List<Bullet> bullets = new List<Bullet>(32);
        List<Enemy> enemies = new List<Enemy>(16);
        Player player;
        double startTime;
        float enemyTimer;
        int score = 0;
        int death = 10;
        bool win;
        bool lose;
        int axis;
        Font font;
        int fontSize1 = 25;
        Sound laserSound = Raylib.LoadSound("resources/sfx_laser2.mp3");

        public Game(Font f, Texture2D playerTexture)
        {
            player = new Player(bullets, playerTexture);
            Bullet.LeftScreen += OnLeftScreen;
            player.BulletSpawn += OnBulletSpawn;
            startTime = Raylib.GetTime();
            this.font = f;
        }

        public void Unsubscribe()
        {
            Bullet.LeftScreen -= OnLeftScreen;
            player.BulletSpawn -= OnBulletSpawn;
            Raylib.UnloadSound(laserSound);
        }

        public void OnLeftScreen(Bullet bullet)
        {
            bullets.Remove(bullet);
        }

        public void OnBulletSpawn(float x, float y, Vector2 dir)
        {
            Bullet b = new Bullet((int)x, (int)y, dir);
            bullets.Add(b);
            Raylib.PlaySound(laserSound);
        }

        public void Update()
        {
            enemyTimer += Raylib.GetFrameTime();
            if (enemyTimer >= 2f)
            {
                enemyTimer = 0f;
                Random random1 = new Random();
                Random random2 = new Random();
                bool randPos = random1.Next(2) == 0; //random where enemy spawns
                bool randAxis = random2.Next(2) == 0; //random on which axis the enemy moves
                int randX = Raylib.GetRandomValue(10, 790);
                int randY = Raylib.GetRandomValue(10, 470);
                int posX;
                int posY;
                if (randAxis)
                {
                    if (randPos)
                    {
                        posX = 10;
                        axis = 1;
                        posY = randY;
                    }
                    else
                    {
                        posX = 790;
                        axis = 0;
                        posY = randY;
                    }
                }
                else
                {
                    if (randPos)
                    {
                        posY = 10;
                        axis = 3;
                        posX = randX;
                    }
                    else
                    {
                        posY = 470;
                        axis = 2;
                        posX = randX;
                    }
                }
                Enemy enemy = new Enemy(axis, posX, posY);
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
                    death--;
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

            //win and lose
            if (score >= 10) { win = true; }
            else if (death <= 0) { lose = true; } //else if to win even when you die at same moment

            if (win || lose)
            {
                for (int b = bullets.Count - 1; b >= 0; b--)
                {
                    bullets.RemoveAt(b);
                }
                for (int e = enemies.Count - 1; e >= 0; e--)
                {
                    enemies.RemoveAt(e);
                }
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
            double elapsedTime = Raylib.GetTime() - startTime;
            int seconds = (int)elapsedTime % 60;
            int minutes = (int)elapsedTime / 60;

            string msgP = "POINTS: " + score.ToString("00");
            string msgD = "DEATH:  " + death.ToString("00");
            string msgT = $"{minutes:00}:{seconds:00}";
            Raylib.DrawTextEx(font, msgP, new(5, 5), fontSize1, 0, Color.GREEN);
            Raylib.DrawTextEx(font, msgD, new(5, 25), fontSize1, 0, Color.PURPLE);
            Raylib.DrawTextEx(font, msgT, new(5, Raylib.GetScreenHeight()-fontSize1), fontSize1, 0, Color.WHITE);

            if (win)
            {
                string msg = "Congratulations!\nYou Win!";
                Raylib.DrawTextEx(font, msg, new(Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2), fontSize1*2, 0, Color.GREEN);
            }
            if (lose)
            {
                string msg = "Game Over!\nYou Lost!";
                Raylib.DrawTextEx(font, msg, new(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2), fontSize1 * 2, 0, Color.RED);
            }
        }
    }
}
