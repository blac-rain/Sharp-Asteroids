using Raylib_cs;
using System.Numerics;

namespace SharpAsteroids
{
    public class Enemy
    {
        public Vector2 Pos => new Vector2(posX, posY);
        public Rectangle Rec => new Rectangle(posX, posY, sizeX, sizeY);
        public int SizeX => sizeX;

        int sizeX = 15;
        int sizeY = 15;
        int posX;
        int posY;
        int speed = 3;
        bool goLeft;

        public Enemy(bool ml, int x = 0, int y = 0)
        {
            posX = x;
            posY = y;
            goLeft = ml;
        }

        public void Update()
        {
            if (goLeft)
            {
                posX -= speed;
            }
            else
            {
                posX += speed;
            }
        }
        public void Draw()
        {
            Raylib.DrawRectangle(posX, posY, sizeX, sizeY, Color.ORANGE);
            //when hitbox as circle:
            //Raylib.DrawRectangle(posX - sizeX/2, posY - sizeY/2, sizeX, sizeY, Color.ORANGE);
        }
    }
}
