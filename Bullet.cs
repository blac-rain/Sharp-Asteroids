using Raylib_cs;
using System.Numerics;

namespace SharpAsteroids
{
    public class Bullet
    {
        public Vector2 Pos => new Vector2(posX, posY); //shorthand for Vector2 getter method
        public float Radius => radius;

        int posX;
        int posY;
        Vector2 dir;
        float radius = 5f;
        float speed = 3f;
        Color redSemi = new Color(230, 41, 55, 125);

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
            Raylib.DrawCircle((int)posX, (int)posY, radius, redSemi);
        }
    }
}
