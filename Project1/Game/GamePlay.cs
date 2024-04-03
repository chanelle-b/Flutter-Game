
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Project1
{
    public static class MySounds
    {
        public static SoundEffect damageSound;
        public static Song natureSound;
    }

    public class GamePlay : GameScene
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Game1 game1; // Add a field to store the Game1 instance

        
        private Texture2D backgroundSprite;
        private Texture2D foregroundSprite;
        private SpriteFont timerFont;
        private Texture2D fairySprite;
        private Texture2D flyUp;
        private Texture2D flyDown;
        private Texture2D flyLeft;
        private Texture2D flyRight;
        private Texture2D fullHeart;
        private Texture2D emptyHeart;
        private Texture2D daisyObstacle;

        // Instances
        private Background movingBackground;
        private Player player = new Player();
        private DaisyControls daisyController = new DaisyControls();

        //public counters for current score,level
        public int currentScore;
        public int currentLevel;
        public GamePlay(Game1 game) : base(game) 
        {
            game1 = game;
            _graphics = game1.GraphicsManager; 

            player.Position = Player.defaultPosition;
        }



        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // backgrounds loaded into project
            backgroundSprite = game1.Content.Load<Texture2D>("friendlyForest2");
            foregroundSprite = game1.Content.Load<Texture2D>("friendlyForest2");
            //fonts
            timerFont = game1.Content.Load<SpriteFont>("timerFont");
            //player sprites
            fairySprite = game1.Content.Load<Texture2D>("Player/fairySprite");
            flyUp = game1.Content.Load<Texture2D>("Player/flyUP");
            flyDown = game1.Content.Load<Texture2D>("Player/flyDown");
            flyLeft = game1.Content.Load<Texture2D>("Player/flyLeft");
            flyRight = game1.Content.Load<Texture2D>("Player/flyRight");
            //player sprite animations
            player.playerAnimations[0] = new SpriteAnimation(flyUp, 5, 6);
            player.playerAnimations[1] = new SpriteAnimation(flyDown, 5, 6);
            player.playerAnimations[2] = new SpriteAnimation(flyLeft, 5, 6);
            player.playerAnimations[3] = new SpriteAnimation(flyRight, 5, 6);
            player.anim = player.playerAnimations[0];
            //sound effects
            MySounds.damageSound = game1.Content.Load<SoundEffect>("SoundEffects/blip");
            MySounds.natureSound = game1.Content.Load<Song>("SoundEffects/nature");
            //environment sprites
            fullHeart = game1.Content.Load<Texture2D>("fullHeart");
            emptyHeart = game1.Content.Load<Texture2D>("emptyHeart");
            daisyObstacle = game1.Content.Load<Texture2D>("daisyObstacle");

            // set speed for the background and foreground
            float backgroundSpeed = 150f;
            float foregroundSpeed = 150f;

            // now pass the sprites to the background class
            movingBackground = new Background(this.game1, _spriteBatch, backgroundSprite, foregroundSprite, backgroundSpeed, foregroundSpeed);
        }

        public override void Update(GameTime gameTime)
        {
            // check if the Back button on a gamepad or Escape key on a keyboard is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                game1.Exit();
            }

            // checks if the game is not in progress and enter key is pressed
            if (!daisyController.InGame)
            {
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    //if enter key is pressed, start the game and reset everything including position, health, etc.
                    MediaPlayer.Play(MySounds.natureSound);
                    MediaPlayer.IsRepeating = true;
                    daisyController.InGame = true;
                    player.Position = Player.defaultPosition;
                    player.ResetHealth();
                    daisyController.Daisies.Clear();
                    currentLevel = 0;
                }
            }

            // when in game, update the player state and game controller state
            if (daisyController.InGame)
            {
                currentScore = (int)daisyController.TotalTime;
                currentLevel = (int)daisyController.TotalTime / 20;
                player.Update(gameTime);
                daisyController.controllerUpdate(gameTime);

                // collisions with daisies
                for (int i = 0; i < daisyController.Daisies.Count; i++)
                {
                    Daisy currentDaisy = daisyController.Daisies[i];
                    currentDaisy.DaisyUpdate(gameTime);

                    // check if the player collides with a daisy
                    if (Vector2.Distance(currentDaisy.HitboxCenter, player.Position) < currentDaisy.Radius + player.radius)
                    {
                        //play the damage sound and envoke the "TakeDamage" method
                        MySounds.damageSound.Play();
                        player.TakeDamage();

                        // remove the daisy that the player collided with
                        daisyController.Daisies.RemoveAt(i);
                        i--;

                        //--GAME OVER LOGIC HERE--//
                        // if the players health is less than or equal to 0, end the game and reset 
                        //assets
                        if (player.Health <= 0)
                        {

                            MediaPlayer.IsRepeating = false;
                            MediaPlayer.Stop();
                            daisyController.InGame = false;
                            player.Position = Player.defaultPosition;
                            daisyController.Daisies.Clear();
                            daisyController.TotalTimer = 0;
                            break;
                        }
                    }
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //background movement from background.cs class
            movingBackground.Draw(gameTime);
            movingBackground.Update(gameTime);
            _spriteBatch.Begin();
            //animating the player

            player.anim.Draw(_spriteBatch);
            //drawing daisies
            for (int i = 0; i < daisyController.Daisies.Count; i++)
            {
                _spriteBatch.Draw(daisyObstacle, new Vector2(daisyController.Daisies[i].Position.X - daisyController.Daisies[i].Radius, daisyController.Daisies[i].Position.Y - daisyController.Daisies[i].Radius), Color.White);
            }
            //if inGame variable is false, display "press enter to begin" message
            if (daisyController.InGame == false)
            {
                string menuMessage = "Press enter to begin";
                Vector2 sizeOfText = timerFont.MeasureString(menuMessage);
                int halfWidth = _graphics.PreferredBackBufferWidth / 2;
                _spriteBatch.DrawString(timerFont, menuMessage, new Vector2(halfWidth - sizeOfText.X / 2, 200), Color.HotPink);
            }
            //if player health reaches 0 game over scene displays
            if (player.Health <= 0)
            {
                //TEMPORARY GAME OVER MESSAGES HERE??(MAY DELETE AND REPLACE WITH GAMEOVER SCENE IN UPDATE METHOD
                GraphicsDevice.Clear(Color.White);
                string endMessage = "Game Over! Press Escape to go back to menu";
                Vector2 sizeOfText = timerFont.MeasureString(endMessage);
                _spriteBatch.DrawString(timerFont, endMessage, new Vector2(300, 500), Color.Black);
            }
            // draw hearts for the health bar
            Vector2 heartPosition = new Vector2(10, 37);
            float scale = 0.050f; 

            //draw and replace full hearts with empty hearts as the player loses health
            for (int i = 0; i < 3; i++)
            {
                Texture2D currentHeart = i < player.Health ? fullHeart : emptyHeart;
                Vector2 origin = new Vector2(0, 0); //positioning in the top corner
                _spriteBatch.Draw(currentHeart, heartPosition, null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
                //scaling the hearts
                heartPosition.X += (currentHeart.Width * scale) + 10;
            }

            //drawing timer and level
            _spriteBatch.DrawString(timerFont, "Time: " + Math.Floor(daisyController.TotalTime).ToString(), new Vector2(3, 3), Color.HotPink);
            _spriteBatch.DrawString(timerFont, "Level: " + currentLevel.ToString(), new Vector2(3, 68), Color.Gold);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}