using Raylib_cs;
using System.Numerics;

namespace SharpAsteroids
{
    public class Player
    {
        List<Bullet> bullets = new List<Bullet>();
        float angle;
        float rotateSpeed = 3f;
        Vector2 startDir = new Vector2(0f, -1f);
        Vector2 dir = new Vector2();
        public event Action<float, float, Vector2>? BulletSpawn;
        Texture2D playerTexture;

        int sizeX = 11;
        int sizeY = 37;
        float posX = Raylib.GetRenderWidth() / 2;
        float posY = (Raylib.GetRenderHeight() / 3) * 2;

        public Rectangle Rec
        {
            get
            {
                // centered hitbox, wings are not part of hitbox
                int centerX = (int)(posX + (playerTexture.Width - sizeX) * 0.5f);
                int centerY = (int)(posY + (playerTexture.Height - sizeY) * 0.5f);
                return new Rectangle(centerX, centerY, sizeX, sizeY);
            }
        }

        public Player(List<Bullet> b, Texture2D t)
        {
            this.bullets = b;
            BulletSpawn += (x, y, dir) => { };
            this.playerTexture = t;
        }

        public void Update()
        {
            //shoot
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                //centered bullet spawn
                float centerX = posX + (playerTexture.Width - sizeX) * 0.5f;
                float centerY = posY + (playerTexture.Height - sizeY) * 0.5f;
                BulletSpawn?.Invoke(centerX, centerY, dir);
            }

            //movement
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
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
                angle -= rotateSpeed;
                dir = Raymath.Vector2Rotate(startDir, Raylib.DEG2RAD * angle);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                angle += rotateSpeed;
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
            Raylib.DrawTextureEx(playerTexture, new(posX, posY), angle, 1, Color.WHITE);
        }
    }
}
