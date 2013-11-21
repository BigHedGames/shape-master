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

        // character movement status to pass to sub-sprites
        MovementStatus movementStatus;

        #endregion

        #region Properties

        // Velocity (publicly available for potential power-ups/other external modifications
        public int Velocity { get; private set; }

        // Declare property to hold status of player's shape
        public ShapeStatus CharShapeStatus { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Character constructor.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="x">The center x position.</param>
        /// <param name="y">The center y position.</param>
        /// <param name="width">The width of the sprite.</param>
        /// <param name="height">The height of the sprite.</param>
        /// <param name="windowWidth">The window width (needed for the boundaries).</param>
        /// <param name="windowHeight">The window height (needed for the boundaries).</param>
        public Character(ContentManager contentManager, int x, int y, int width, int height, int windowWidth, int windowHeight)
        {
            // define the position rectangle
            positionRectangle = new Rectangle(x - width / 2, y - height / 2, width, height);

            // set the velocity
            Velocity = INITIAL_VELOCITY;

            // set boundaries for characters
            boundaryBottom = windowHeight;
            boundaryRight = windowWidth;

            // create the shape object
            shape = new Shape(contentManager, width);
            eyes = new Eyes(contentManager, width);
            mouth = new Mouth(contentManager, width);

            CharShapeStatus = ShapeStatus.Circle;
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
        public void Update(KeyboardState keyboardState, GameTime gameTime)
        {
            // move the sprite
            Move(keyboardState);

            // Set shape status of character
            ShapeShift(keyboardState);

            // update the sub-sprites
            shape.Update(positionRectangle, CharShapeStatus, movementStatus, gameTime);
            eyes.Update(positionRectangle, CharShapeStatus, movementStatus, gameTime);
            mouth.Update(positionRectangle, CharShapeStatus, movementStatus, gameTime);
        }

        #endregion

        #region Private Methods
            /// <summary>
        /// Move the sprite based on keyboard input.
        /// </summary>
        /// <param name="keyboardState">The keyboard state which provides the keyboard input.</param>
        private void Move(KeyboardState keyboardState)
        {
            // logic for movement in diagonals
            if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Up))
            {
                movementStatus = MovementStatus.NorthEast;
                positionRectangle.Y -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                positionRectangle.X += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Down))
            {
                movementStatus = MovementStatus.SouthEast;
                positionRectangle.Y += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                positionRectangle.X += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Up))
            {
                movementStatus = MovementStatus.NorthWest;
                positionRectangle.Y -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                positionRectangle.X -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Down))
            {
                movementStatus = MovementStatus.SouthWest;
                positionRectangle.Y += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                positionRectangle.X -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            // logic for movement in standard four directions
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    movementStatus = MovementStatus.North;
                    positionRectangle.Y -= Velocity;
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    movementStatus = MovementStatus.South;
                    positionRectangle.Y += Velocity;
                }

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    movementStatus = MovementStatus.West;
                    positionRectangle.X -= Velocity;
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    movementStatus = MovementStatus.East;
                    positionRectangle.X += Velocity;
                }
            }

            // check for stationary status
            if (!keyboardState.IsKeyDown(Keys.Up) && !keyboardState.IsKeyDown(Keys.Down) &&
                !keyboardState.IsKeyDown(Keys.Left) && !keyboardState.IsKeyDown(Keys.Right))
            {
                movementStatus = MovementStatus.Stationary;
            }

            // If the sprite has moved passed the boundaries, put it back.
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

        private void ShapeShift(KeyboardState keyboardState)
        {

            // set key definitions to change shape
            if (keyboardState.IsKeyDown(Keys.A))
            {
                CharShapeStatus = ShapeStatus.Circle;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                CharShapeStatus = ShapeStatus.Square;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                CharShapeStatus = ShapeStatus.Star;
            }

            if (keyboardState.IsKeyDown(Keys.F))
            {
                CharShapeStatus = ShapeStatus.Triangle;
            }
        }

        #endregion
    }
}
