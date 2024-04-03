using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    internal class Daisy
    {
        //variable declarations
        private Vector2 position = new Vector2(600, 300);
        private int speed;
        private int radius = 60;
        private int hitboxCenter = -55; 

        //getters and setters to access private variables
        public Vector2 HitboxCenter
        {
            get { return new Vector2(Position.X, Position.Y + hitboxCenter); }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; } 
        }

        public int Speed
        {
            get { return speed; }
            private set { speed = value; } 
        }

        public int Radius
        {
            get { return radius; }
        }
        
        //logic for generating daisies in the game at random and setting their speed
        public Daisy(int newSpeed)
        {
            Speed = newSpeed;
            Random rand = new Random();
            Position = new Vector2(1380, rand.Next(0, 721));
        }

        //daisy update .....
        public void DaisyUpdate(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= speed * dt; 
        }
    }
}
