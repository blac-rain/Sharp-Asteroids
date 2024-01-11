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

        public Enemy(int x = 0, int y = 0)
        {
            posX = x;
            posY = y;
        }

        public void Update()
        {
            if (Game.goLeft)
            {
                posX -= 3;
            }
            else if (Game.goRight)
            {
                posX += 3;
            }
            else if (Game.goDown)
            {
                posY += 3;
            }
            else if (Game.goUp)
            {
                posY -= 3;
            }
            else
            {
                posY += 3;
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
