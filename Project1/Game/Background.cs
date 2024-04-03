using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project1
{
    internal class Background : DrawableGameComponent
    {
        //variable decarations
        private SpriteBatch spriteBatch;
        private Vector2 backgroundPosition1, backgroundPosition2;
        private Vector2 foregroundPosition;
        private float backgroundSpeed;
        private float foregroundSpeed;
        private Texture2D backgroundSprite;
        private Texture2D foregroundSprite;



        // constructor for the background cs class for scrolling effect
        public Background(Game game, SpriteBatch spriteBatch, Texture2D backgroundSprite, Texture2D foregroundSprite, float backgroundSpeed, float foregroundSpeed)
            : base(game)
        {
            //declarations
            this.spriteBatch = spriteBatch;
            this.backgroundSprite = backgroundSprite;
            this.foregroundSprite = foregroundSprite;
            this.backgroundPosition1 = Vector2.Zero;
            this.backgroundPosition2 = new Vector2(backgroundSprite.Width, 0);
            int screenHeight = game.GraphicsDevice.Viewport.Height;       
            this.foregroundPosition = new Vector2(0, screenHeight - foregroundSprite.Height);
            this.backgroundSpeed = backgroundSpeed;
            this.foregroundSpeed = foregroundSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            // scroll the background
            backgroundPosition1.X -= backgroundSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            backgroundPosition2.X -= backgroundSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // check if the background has completely moved off screen
            if (backgroundPosition1.X + backgroundSprite.Width <= 0)
            {
                backgroundPosition1.X = backgroundPosition2.X + backgroundSprite.Width;
            }
            if (backgroundPosition2.X + backgroundSprite.Width <= 0)
            {
                backgroundPosition2.X = backgroundPosition1.X + backgroundSprite.Width;
            }

            // scroll the foreground at a different speed for scroll effect
            foregroundPosition.X -= foregroundSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // repeat the foreground background
            if (foregroundPosition.X <= -foregroundSprite.Width)
            {
                foregroundPosition.X += foregroundSprite.Width;
            }

            base.Update(gameTime);
        }



        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //get the screen width
            int screenWidth = spriteBatch.GraphicsDevice.Viewport.Width;
            // calculate how many times to draw the foreground texture to fill the screen width
            int numberOfBackgroundTiles = (int)Math.Ceiling((float)screenWidth / backgroundSprite.Width) + 1;
            // draw the background tiles
            for (int i = 0; i < numberOfBackgroundTiles; i++)
            {
                spriteBatch.Draw(backgroundSprite, new Vector2(backgroundPosition1.X + (i * backgroundSprite.Width), backgroundPosition1.Y), Color.White);
                spriteBatch.Draw(backgroundSprite, new Vector2(backgroundPosition2.X + (i * backgroundSprite.Width), backgroundPosition2.Y), Color.White);
            }
            //calculate how many times to draw the foreground to fill the screen width
            int numberOfForegroundTiles = (int)Math.Ceiling((float)screenWidth / foregroundSprite.Width) + 1;

            // draw the foreground tiles
            for (int i = 0; i < numberOfForegroundTiles; i++)
            {
                spriteBatch.Draw(foregroundSprite, new Vector2(foregroundPosition.X + (i * foregroundSprite.Width), foregroundPosition.Y), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
