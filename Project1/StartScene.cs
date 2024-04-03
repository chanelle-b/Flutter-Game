using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Project1
{
    public class StartScene : GameScene
    {
        public bool IsStartGameSelected
        {
            get { return selectedIndex == 0; } // Assuming 0 is the index for "Start game"
        }
        public bool IsHelpSelected
        {
            get { return selectedIndex == 1; } // Assuming 1 is the index for "Help"
        }
        public bool IsHighScoreSelected
        {
            get { return selectedIndex == 2; } // Assuming 2 is the index for "High Score"
        }
        public bool IsCreditSelected
        {
            get { return selectedIndex == 3; } // Assuming 1 is the index for "Help"
        }

        private int selectedIndex = 0;  // To keep track of the selected menu item
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        private Texture2D backgroundTexture;
        private MonoComponent menu;
        private KeyboardState oldState; // Class member for storing the old keyboard state
        Game1 g;

        string[] menuItems = { "Start game", "Help", "High Score", "Credit", "Quit" };

        public MonoComponent Menu { get => menu; set => menu = value; }

        public StartScene(Game1 game) : base(game)
        {
            g = game;
            oldState = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundTexture = g.Content.Load<Texture2D>("Images/background_Texture4");


            SpriteFont regularFont = g.Content.Load<SpriteFont>("fonts/RegularFont");
            SpriteFont hilightFont = g.Content.Load<SpriteFont>("fonts/HilightFont");
            Menu = new MonoComponent(g, spriteBatch, regularFont, hilightFont, menuItems);
            Components.Add(Menu);

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            // Navigate menu items
            if (newKeyboardState.IsKeyDown(Keys.Up) && !oldState.IsKeyDown(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = menuItems.Length - 1; // Wrap to last item
                }
            }
            else if (newKeyboardState.IsKeyDown(Keys.Down) && !oldState.IsKeyDown(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex >= menuItems.Length)
                {
                    selectedIndex = 0; // Wrap to first item
                }
            }

            // Check if Enter key is pressed and was not pressed in the previous frame
            if (newKeyboardState.IsKeyDown(Keys.Enter) && !oldState.IsKeyDown(Keys.Enter))
            {
                if (IsStartGameSelected) // Check if "Start game" is selected
                {
                    // Change to the gameplay scene
                    ((Game1)Game).ChangeScene(((Game1)Game).Gameplay);
                }
                else if (IsHelpSelected)
                {
                    // Change to the help scene
                    ((Game1)Game).ChangeScene(((Game1)Game).HelpScene); // Assuming you have a HelpScene instance in Game1
                }
                else if (IsHighScoreSelected)
                {
                    // Change to the highscore scene
                    ((Game1)Game).ChangeScene(((Game1)Game).HighScoreScene); // Assuming you have a HighScoreScene instance in Game1
                }
                else if (IsCreditSelected)
                {
                    ((Game1)Game).ChangeScene(((Game1)Game).CreditScene);
                }
            }

            oldState = newKeyboardState; // Update the old keyboard state

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (backgroundTexture != null)
            {
                spriteBatch.Draw(backgroundTexture, GraphicsDevice.Viewport.Bounds, Color.White);
            }
           
            spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}