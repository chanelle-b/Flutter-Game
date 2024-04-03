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
    internal class DaisyControls
    {
        //variable declarations for the DaisyController class
        private List<Daisy> daisies = new List<Daisy>();
        private double timer = 2;
        private double maxTime = 2;
        private int nextSpeed = 240;
        private bool inGame = false;
        private double totalTime = 0;

        //getters and setters to access private variables 
        public double TotalTime
        {
            get { return totalTime; }
            set { totalTime = value; }
        }

        public List<Daisy> Daisies
        {
            get { return daisies; }
            
        }
        public bool InGame
        {
            get { return inGame; }
            set { inGame = value; }
        }
        public double TotalTimer
        {
            get { return totalTime; }
            set { totalTime = value; }
        }

        //daisy generation
        public void controllerUpdate(GameTime gameTime)
        {
            //if timer is true timer will count down, if not true timer will not count down and no daisies will be generated
            //if enter is pressed ingame variable will become true 
            if (inGame)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                totalTime += gameTime.ElapsedGameTime.TotalSeconds;
                
            }
            else
            {
                KeyboardState kstate = Keyboard.GetState();
                if (kstate.IsKeyDown(Keys.Enter))
                {
                    //when enter is pressed everything is reset
                    inGame = true;
                    totalTime = 0;
                    timer = 2;
                    maxTime = 2;
                    nextSpeed = 240;
                }

            }
            //when the timer reaches 0 
            if (timer <= 0)
            {
                daisies.Add(new Daisy(nextSpeed));
                timer = maxTime;

                if (maxTime > 0.5)
                {
                    maxTime -= 0.1;
                }
                if (nextSpeed > 720)
                {
                    nextSpeed += 4;
                }
            }
        }
    }
}
