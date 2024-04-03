using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Project1
{
    public class MonoComponent : DrawableGameComponent
    {
        private SpriteBatch sb;
        private SpriteFont regularFont, hilightFont;

        private List<string> menuItems;
        public int SelectedIndex { get; set; }
        private Vector2 position;
        private Color regularColor = Color.White;
        private Color hilightColor = Color.Red;
        private KeyboardState oldState;


        public MonoComponent(Game game, SpriteBatch sb, SpriteFont regularFont, SpriteFont hilightFont, string[] menus) : base(game)
        {
            this.sb = sb;
            this.regularFont = regularFont;
            this.hilightFont = hilightFont;
            menuItems = menus.ToList();
            position = new Vector2(Game.GraphicsDevice.Viewport.Width / 19,
                                           Game.GraphicsDevice.Viewport.Height / 18);

            position.X += 495;

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                SelectedIndex++;
                if (SelectedIndex >= menuItems.Count) SelectedIndex = 0; // Use Count for lists
            }
            else if (newState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                SelectedIndex--;
                if (SelectedIndex < 0) SelectedIndex = menuItems.Count - 1; // Use Count for lists
            }
            else if (newState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
            {
                switch (SelectedIndex)
                {
                    case 0:

                        break;
                    case 1: // Help
                            // Logic for showing Help scene
                        break;
                    case 2: // High Score
                            // Logic for showing High Score scene
                        break;
                    case 3: // Credit
                            // Logic for showing Credit scene
                        break;
                    case 4: // Quit
                        Game.Exit();
                        break;
                }
            }

            oldState = newState;
            base.Update(gameTime);
        }






        public override void Draw(GameTime gameTime)
        {
            Vector2 temPos = position;
            sb.Begin();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (SelectedIndex == i)
                {
                    sb.DrawString(hilightFont, menuItems[i], temPos, hilightColor);
                    temPos.Y += hilightFont.LineSpacing;
                }
                else
                {
                    sb.DrawString(regularFont, menuItems[i], temPos, regularColor);
                    temPos.Y += regularFont.LineSpacing;
                }
            }
            sb.End();


            base.Draw(gameTime);
        }
    }
}