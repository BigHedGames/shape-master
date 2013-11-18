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

        // velocity related constants
        const int INITIAL_VELOCITY = 5;
        readonly double DIAGONAL_FACTOR = 1.0 / Math.Sqrt(2.0);

        #endregion

        #region Fields

        // determines position of the character
        Rectangle positionRectangle;

        // movement boundaries for screen
        int boundaryTop = 0;
        int boundaryBottom;
        int boundaryLeft = 0;
        int boundaryRight;

        // create shape object
        Shape shape;
        Eyes eyes;
        Mouth mouth;

        #endregion

        #region Properties

        // Velocity (publicly available for potential power-ups/other external modifications
        public int Velocity { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Character constructor.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="shapeSpriteName">The shape sprite name.</param>
        /// <param name="eyesSpriteName">The eyes sprite name.</param>
        /// <param name="mouthSpriteName">The mouth sprite name.</param>
        /// <param name="x">The center x position.</param>
        /// <param name="y">The center y position.</param>
        /// <param name="width">The width of the sprite.</param>
        /// <param name="height">The height of the sprite.</param>
        /// <param name="windowWidth">The window width (needed for the boundaries).</param>
        /// <param name="windowHeight">The window height (needed for the boundaries).</param>
        public Character(ContentManager contentManager, string shapeSpriteName, string eyesSpriteName, string mouthSpriteName, 
            int x, int y, int width, int height, int windowWidth, int windowHeight)
        {
            // define the position rectangle
            positionRectangle = new Rectangle(x - width / 2, y - height / 2, width, height);

            // set the velocity
            Velocity = INITIAL_VELOCITY;

            // set boundaries for characters
            boundaryBottom = windowHeight;
            boundaryRight = windowWidth;

            // create the shape object
            shape = new Shape(contentManager, shapeSpriteName);
            eyes = new Eyes(contentManager, eyesSpriteName);
            mouth = new Mouth(contentManager, mouthSpriteName);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draw the sprites.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the shape first, then the eyes and mouth.
            shape.Draw(spriteBatch);
            eyes.Draw(spriteBatch);
            mouth.Draw(spriteBatch);
        }

        /// <summary>
        /// Update the sprites.
        /// </summary>
        /// <param name="keyboardState">The keyboard state which provides keyboard input.</param>
        public void Update(KeyboardState keyboardState)
        {
            // move the sprite
            Move(keyboardState);

            // set the position of the sub-sprites
            shape.SetPosition(positionRectangle);
            eyes.SetPosition(positionRectangle);
            mouth.SetPosition(positionRectangle);
        }

        /// <summary>
        /// Move the sprite based on keyboard input.
        /// </summary>
        /// <param name="keyboardState">The keyboard state which provides the keyboard input.</param>
        public void Move(KeyboardState keyboardState)
        {
            // logic for movement in diagonals
            if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Up))
            {
                positionRectangle.Y -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                positionRectangle.X += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Down))
            {
                positionRectangle.Y += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                positionRectangle.X += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Up))
            {
                positionRectangle.Y -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                positionRectangle.X -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Down))
            {
                positionRectangle.Y += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                positionRectangle.X -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            // logic for movement in standard four directions
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    positionRectangle.Y -= Velocity;
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    positionRectangle.Y += Velocity;
                }

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    positionRectangle.X -= Velocity;
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    positionRectangle.X += Velocity;
                }
            }

            // If the sprite has mooved passed the boundaries, put it back.
            if (positionRectangle.X < boundaryLeft)
            {
                positionRectangle.X = boundaryLeft;
            }
            if (positionRectangle.Y < boundaryTop)
            {
                positionRectangle.Y = boundaryTop;
            }
            if (positionRectangle.X + positionRectangle.Width > boundaryRight)
            {
                positionRectangle.X = boundaryRight - positionRectangle.Width;
            }
            if (positionRectangle.Y + positionRectangle.Height > boundaryBottom)
            {
                positionRectangle.Y = boundaryBottom - positionRectangle.Height;
            }
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
