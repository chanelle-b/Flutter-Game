using System.Linq;
using Project1.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project1
{
    public class Game1 : Game
    {
        public GameScene Gameplay => gameplay;
        public GameScene CreditScene => _creditScene;

        public GraphicsDeviceManager GraphicsManager { get; private set; }
        public SpriteBatch _spriteBatch;
        private StartScene startScene;
        private GameScene currentScene; // Track the current scene
        private GamePlay gameplay;
        public HelpScene HelpScene;
        public HighScoreScene HighScoreScene;
        public CreditScene _creditScene;

        public XmlSerializer<HighScore> serializer; // serializer for saveing and loading highscore
        public HighScore score; // a component that holds the highscore value
        public int GlobalScore { get; set; } // Global score accessible from any scene

        public KeyboardState oldKeyboardState;

        public Game1()
        {
            GraphicsManager = new GraphicsDeviceManager(this); // Initialize _graphics here
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            // The below settings should be in Game1, not here
             GraphicsManager.PreferredBackBufferWidth = 1280;
             GraphicsManager.PreferredBackBufferHeight = 720;
             GraphicsManager.ApplyChanges();
        }

        protected override void Initialize()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialize scenes
            startScene = new StartScene(this);
            gameplay = new GamePlay(this); // Initialize the gameplay scene
            HelpScene = new HelpScene(this);
            _creditScene = new CreditScene(this);
            HighScoreScene = new HighScoreScene(this);

            // initialize score and serializer
            score = new HighScore();
            serializer = new XmlSerializer<HighScore>();
            serializer.type = typeof(HighScore);
            score = serializer.Load("highScore");
            HighScoreScene.highestTime = score.highScoreCounter;




            // Start with the start scene
            currentScene = startScene;
            Components.Add(startScene);

            base.Initialize();
        }

        public void ChangeScene(GameScene newScene)
        {
            //save the change in score with each scene change
            serializer.Save("highScore", score);
            //Load new scores
            score = serializer.Load("highScore");
            // Disable and remove the current scene
            if (currentScene != null)
            {
                currentScene.Enabled = false;
                currentScene.Visible = false;
                Components.Remove(currentScene);
            }

            // Add the new scene
            currentScene = newScene;
            Components.Add(newScene);

            // Enable the new scene
            newScene.Enabled = true;
            newScene.Visible = true;


        }
        public void UpdateScore()
        {
            //check if the current score is more or less than the saved score
            if(GlobalScore> score.highScoreCounter)
            {
                score.highScoreCounter = GlobalScore;
                HighScoreScene.highestTime = score.highScoreCounter;
            }
            else
            {
                HighScoreScene.highestTime = score.highScoreCounter;
            }

            //check if the current level is higher or lower than the saved level
            if(gameplay.currentLevel > score.highscoreLevel)
            {
                score.highscoreLevel = gameplay.currentLevel;
                HighScoreScene.highestLevel = score.highscoreLevel;
            }
            else
            {
                HighScoreScene.highestLevel = score.highscoreLevel;
            }
            
        }

        protected override void LoadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            //get the value of the global score
            GlobalScore = gameplay.currentScore;
            // Global check for Escape key to return to the StartScene
            if (newKeyboardState.IsKeyDown(Keys.Escape) && !oldKeyboardState.IsKeyDown(Keys.Escape))
            {
                if (currentScene != startScene) // Check if the current scene is not already the StartScene
                {
                    UpdateScore();
                    ChangeScene(startScene);
                }
            }
            
            // Update the current scene
            currentScene?.Update(gameTime);

            oldKeyboardState = newKeyboardState; // Update the old keyboard state

            base.Update(gameTime);
        }




        protected override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }
    }
}
