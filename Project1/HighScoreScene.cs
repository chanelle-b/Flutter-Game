using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project1
{
    public class HighScoreScene : GameScene
    {
        SpriteBatch spriteBatch;
        public SpriteFont font;
        public int highestTime;
        public int highestLevel;
        public HighScoreScene(Game game) : base(game)
        {
            Game1 game1 = game as Game1;
            spriteBatch = game1._spriteBatch;
            font = game1.Content.Load<SpriteFont>("fonts/RegularFont");
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.White);
            spriteBatch.DrawString(font,"Your highest Time: \n" +highestTime.ToString() + " Second", new Vector2(48, 64), Color.Gold);
            spriteBatch.DrawString(font,"Your highest Level: \n" +highestLevel.ToString() + "", new Vector2(48, 128), Color.Gold);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
