using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ShapeMaster
{
    class Character
    {
        #region Constants
        const int INITIAL_VELOCITY = 5;
        readonly double DIAGONAL_FACTOR = 1.0 / Math.Sqrt(2.0);
        #endregion

        #region Fields
        Rectangle drawRectangle;
        Texture2D spriteTexture;

        // movement boundaries for screen
        int boundaryTop = 0;
        int boundaryBottom;
        int boundaryLeft = 0;
        int boundaryRight;

        #endregion

        #region Properties
        public int Velocity { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Player class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Character(ContentManager contentManager, string spriteName, int x, int y, int width, int height, int windowWidth, int windowHeight)
        {
            LoadContent(contentManager, spriteName);

            drawRectangle = new Rectangle(x - spriteTexture.Width / 2, y - spriteTexture.Height / 2, width, height);

            Velocity = INITIAL_VELOCITY;

            // set boundaries for characters
            boundaryBottom = windowHeight;
            boundaryRight = windowWidth;
        }
        #endregion

        #region Public Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, drawRectangle, Color.White);
        }

        public void Move(KeyboardState keyboardState)
        {
            
            // logic for movement in diagonals
            if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Up))
            {
                drawRectangle.Y -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                drawRectangle.X += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }
            else if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Down))
            {
                drawRectangle.Y += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                drawRectangle.X += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Up))
            {
                drawRectangle.Y -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                drawRectangle.X -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }
            else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Down))
            {
                drawRectangle.Y += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                drawRectangle.X -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            // logic for movement in standard four directions
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    drawRectangle.Y -= Velocity;
                }
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    drawRectangle.Y += Velocity;
                }
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    drawRectangle.X -= Velocity;
                }
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    drawRectangle.X += Velocity;
                }
            }

            // set movement boundaries
            if (drawRectangle.X < boundaryLeft)
            {
                drawRectangle.X = boundaryLeft;
            }
            if (drawRectangle.Y < boundaryTop)
            {
                drawRectangle.Y = boundaryTop;
            }
            if (drawRectangle.X + drawRectangle.Width > boundaryRight)
            {
                drawRectangle.X = boundaryRight - drawRectangle.Width;
            }
            if (drawRectangle.Y + drawRectangle.Height > boundaryBottom)
            {
                drawRectangle.Y = boundaryBottom - drawRectangle.Height;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Loads the sprite
        /// </summary>
        /// <param name="contentManager"></param>
        /// <param name="spriteName"></param>
        private void LoadContent(ContentManager contentManager, string spriteName)
        {
            spriteTexture = contentManager.Load<Texture2D>(spriteName);
        }
        #endregion
    }
}
