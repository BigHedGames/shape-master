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
    class MainCharacter : Character
    {
        #region Constants

        // velocity related constants
        const int INITIAL_VELOCITY = 5;

        #endregion

        #region Fields

        #endregion

        #region Properties

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
        public MainCharacter(ContentManager contentManager,int x, int y, int width, int height, int windowWidth, int windowHeight)
            : base(contentManager, SpriteType.CHARly, x, y, width, height, windowWidth, windowHeight)
        {
            // set the velocity
            Velocity = INITIAL_VELOCITY;

            // set initial shape for CHARly
            CharShapeStatus = ShapeStatus.Circle;
        }

        #endregion

        #region Public Methods
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

            // Update base sub-sprites
            base.Update(gameTime);
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
                positionVector.Y -= (float)(Velocity * DIAGONAL_FACTOR);
                positionVector.X += (float)(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Down))
            {
                movementStatus = MovementStatus.SouthEast;
                positionVector.Y += (float)(Velocity * DIAGONAL_FACTOR);
                positionVector.X += (float)(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Up))
            {
                movementStatus = MovementStatus.NorthWest;
                positionVector.Y -= (float)(Velocity * DIAGONAL_FACTOR);
                positionVector.X -= (float)(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Down))
            {
                movementStatus = MovementStatus.SouthWest;
                positionVector.Y += (float)(Velocity * DIAGONAL_FACTOR);
                positionVector.X -= (float)(Velocity * DIAGONAL_FACTOR);
            }

            // logic for movement in standard four directions
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    movementStatus = MovementStatus.North;
                    positionVector.Y -= Velocity;
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    movementStatus = MovementStatus.South;
                    positionVector.Y += Velocity;
                }

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    movementStatus = MovementStatus.West;
                    positionVector.X -= Velocity;
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    movementStatus = MovementStatus.East;
                    positionVector.X += Velocity;
                }
            }

            // check for stationary status
            if (!keyboardState.IsKeyDown(Keys.Up) && !keyboardState.IsKeyDown(Keys.Down) &&
                !keyboardState.IsKeyDown(Keys.Left) && !keyboardState.IsKeyDown(Keys.Right))
            {
                movementStatus = MovementStatus.Stationary;
            }

            // If the sprite has moved passed the boundaries, put it back.
            if (positionVector.X < boundaryLeft)
            {
                positionVector.X = boundaryLeft;
            }
            if (positionVector.Y < boundaryTop)
            {
                positionVector.Y = boundaryTop;
            }
            if (positionVector.X + positionRectangle.Width > boundaryRight)
            {
                positionVector.X = boundaryRight - positionRectangle.Width;
            }
            if (positionVector.Y + positionRectangle.Height > boundaryBottom)
            {
                positionVector.Y = boundaryBottom - positionRectangle.Height;
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
