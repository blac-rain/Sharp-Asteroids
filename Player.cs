using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SharpAsteroids
{
    public class Player
    {
        List<Bullet> bullets = new List<Bullet>();
        public Rectangle Rec => new Rectangle(posX, posY, sizeX, sizeY);
        public int SizeX => sizeX;
        public int SizeY => sizeY;
        float angle;
        Vector2 startDir = new Vector2(0f, -1f);
        Vector2 dir = new Vector2();
        public event Action<float, float, Vector2>? BulletSpawn;

        int sizeX = 15;
        int sizeY = 30;
        float posX = 400;
        float posY = 360;

        public Player(List<Bullet> bullets)
        {
            this.bullets = bullets;
        }

        public void Update()
        {
            //shoot
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                BulletSpawn?.Invoke(posX, posY, dir);
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
            Rectangle rect2 = new Rectangle(posX, posY - 5, sizeX, sizeY / 4);
            Vector2 origin = new Vector2(sizeX / 2, sizeY / 2);
            Vector2 origin2 = new Vector2(sizeX, sizeY);
            Raylib.DrawRectanglePro(rect, origin, angle, Color.DARKBLUE);
            Raylib.DrawRectanglePro(rect2, origin, angle, Color.SKYBLUE);
            //for debugging
            //Raylib.DrawCircle((int)posX, (int)posY, 3f, Color.RED);
        }
    }
}
