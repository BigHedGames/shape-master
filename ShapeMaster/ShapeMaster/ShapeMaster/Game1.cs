using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

// This is our game.
namespace ShapeMaster
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Constants

        // declare window resolution constants
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;

        // declare character width & height constants
        const int CHARACTER_WIDTH = 64;
        const int CHARACTER_HEIGHT = 64;

        #endregion

        #region Fields

        // graphics support
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // main character sprite support
        Character player;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Game1()
        {
            // graphics support.
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // make mouse visible
            IsMouseVisible = true;

            // set resolution to defined constants
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // initialize the XNA framework.
            base.Initialize();
        }
        #endregion

        #region LoadContent
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load characters
            player = new Character(Content, WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2, CHARACTER_WIDTH,
                CHARACTER_HEIGHT, WINDOW_WIDTH, WINDOW_HEIGHT);

        }
        #endregion

        #region UnloadContent
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        #region Update
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Get keyboard state to move player sprite
            KeyboardState keyboardState = Keyboard.GetState();

            // update the player
            player.Update(keyboardState);

            // update the game time.
            base.Update(gameTime);
        }
        #endregion

        #region Draw
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Black background
            GraphicsDevice.Clear(Color.Black);

            // begin the sprite batch
            spriteBatch.Begin();

            // draw the player
            player.Draw(spriteBatch);

            // end the sprite batch
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}
