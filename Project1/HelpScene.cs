using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Scenes
{
    public class HelpScene : GameScene
    {
        SpriteBatch spriteBatch;
        private Texture2D helpImage;

        public HelpScene(Game game) : base(game)
        {
            Game1 game1 = game as Game1;
            spriteBatch = game1._spriteBatch;
            helpImage = game1.Content.Load<Texture2D>("helpscene");


        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Draw(helpImage, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

}
