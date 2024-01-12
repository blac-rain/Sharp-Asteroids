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
        int axis;

        public Enemy(int a, int x = 0, int y = 0 )
        {
            posX = x;
            posY = y;
            axis = a;
        }

        public void Update()
        {
            switch (axis) {
                case 0: posX -= speed; break; //move left
                case 1: posY -= speed; break; //move down
                case 2: posX += speed; break; //move right
                case 3: posY += speed; break; //move up
            }
        }
        public void Draw()
        {
            Raylib.DrawRectangle(posX, posY, sizeX, sizeY, Color.ORANGE);
            //if hitbox = circle:
            //Raylib.DrawRectangle(posX - sizeX/2, posY - sizeY/2, sizeX, sizeY, Color.ORANGE);
        }
    }
}
