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
    class NPCharacter : Character
    {
        #region Constants

        // velocity related constants
        const int INITIAL_VELOCITY = 3;

        #endregion

        #region Fields
        // Object to hold randon number
        Random randNum;

        // movement support
        Vector2 velocityVector;

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
        public NPCharacter(ContentManager contentManager, int x, int y, int width, int height, int windowWidth, int windowHeight, Random rand)
            : base(contentManager, SpriteType.Mad, x, y, width, height, windowWidth, windowHeight)
        {
            // set the velocity
            Velocity = INITIAL_VELOCITY;

            // set the randNum reference
            randNum = rand;

            // set initial shape for enemy to random shape based off of number of total shapes in ShapeStatus enum
            int maxShapes = Enum.GetNames(typeof(ShapeStatus)).Length;
            CharShapeStatus = (ShapeStatus)randNum.Next(maxShapes);

            // set initial random location
            positionRectangle.X = randNum.Next(width / 2, (windowWidth - (width / 2)) + 1);
            positionRectangle.Y = randNum.Next(height / 2, (windowHeight - (height / 2)) + 1);

            // set random direction & speed
            velocityVector = new Vector2(0, 0);
            while (velocityVector.X == 0 && velocityVector.Y == 0)
            {
                velocityVector.X = randNum.Next(-INITIAL_VELOCITY, INITIAL_VELOCITY);
                velocityVector.Y = randNum.Next(-INITIAL_VELOCITY, INITIAL_VELOCITY);
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Update the sprites.
        /// </summary>
        /// <param name="keyboardState">The keyboard state which provides keyboard input.</param>
        public new void Update(GameTime gameTime)
        {
            // move the sprite
            Move();

            // Update base sprites
            base.Update(gameTime);

            // Quick & dirty set movement status
            movementStatus = MovementStatus.North;
        }

        #endregion

        #region Private Methods
            /// <summary>
        /// Move the sprite based on keyboard input.
        /// </summary>
        /// <param name="keyboardState">The keyboard state which provides the keyboard input.</param>
        private void Move()
        {
            // make npc move
            positionRectangle.X += (int)velocityVector.X;
            positionRectangle.Y += (int)velocityVector.Y;

            // If the sprite has moved passed the boundaries, put it back.
            if (positionRectangle.X < boundaryLeft)
            {
                velocityVector.X *= -1;
            }
            if (positionRectangle.Y < boundaryTop)
            {
                velocityVector.Y *= -1;
            }
            if (positionRectangle.X + positionRectangle.Width > boundaryRight)
            {
                velocityVector.X *= -1;
            }
            if (positionRectangle.Y + positionRectangle.Height > boundaryBottom)
            {
                velocityVector.Y *= -1;
            }
        }
        #endregion
    }
}
