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
    internal class Player
    {
        //variable declarations
        static public Vector2 defaultPosition = new Vector2(570, 300);
        //position of ship
        public Vector2 position = defaultPosition;
        public int Health { get; private set; } = 3;
        private const int MaxHealth = 3;
        public int speed = 220;
        public int radius = 50;

        public SpriteAnimation anim;
        public SpriteAnimation[] playerAnimations = new SpriteAnimation[4];

        //getters and setters to access private variables
        public Vector2 Position { get { return position; } set { position = value; } }

        public void setX(float newX)
        {
            position.X = newX;
        }
        public void setY(float newY)
        {
            position.Y = newY;
        }

        //method to detirmine damage taken by the player
        public void TakeDamage()
        {
            if (Health > 0)
            {
                Health--;
            }
        }
        //method to reset health to full
        public void ResetHealth()
        {
            Health = MaxHealth;
        }

        //update method for player movement
        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState(); 
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //movement detirmined by keystate
            //ex) if right arrow key is pressed, player will move right.

            if (kState.IsKeyDown(Keys.Right) && position.X < 1280)
            {
                position.X += speed * dt;
                //sprite animation, each animation is given a number in array
                //certain movements have a certain player animation
                anim = playerAnimations[3];
            }
            if (kState.IsKeyDown(Keys.Left) && position.X > 0)
            {
                position.X -= speed * dt;
                anim = playerAnimations[2];
            }
            if (kState.IsKeyDown(Keys.Down) && position.Y < 720)
            {
                position.Y += speed * dt;
                anim = playerAnimations[1];
            }
            if (kState.IsKeyDown(Keys.Up) && position.Y > 0)
            {
                position.Y -= speed * dt;
                anim = playerAnimations[0];
            }
            anim.Position = position;
            anim.Update(gameTime);
        }
    }
}
