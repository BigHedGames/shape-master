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
        protected readonly double DIAGONAL_FACTOR = 1.0 / Math.Sqrt(2.0);

        #endregion

        #region Fields

        // determines position of the character
        protected Rectangle positionRectangle;

        // movement boundaries for screen
        protected int boundaryTop = 0;
        protected int boundaryBottom;
        protected int boundaryLeft = 0;
        protected int boundaryRight;

        // create shape object
        Shape shape;
        Eyes eyes;
        Mouth mouth;

        // character movement status to pass to sub-sprites
        protected MovementStatus movementStatus;

        // create sprite type object
        protected SpriteType currentSpriteType;

        // Collision detection support
        bool collisionOccurred = false;

        #endregion

        #region Properties

        // Velocity (publicly available for potential power-ups/other external modifications
        public int Velocity { get; protected set; }

        // Declare property to hold status of player's shape
        public ShapeStatus CharShapeStatus { get; protected set; }

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
        public Character(ContentManager contentManager, SpriteType spriteType,int x, int y, int width, 
            int height, int windowWidth, int windowHeight)
        {
            // define the position rectangle
            positionRectangle = new Rectangle(x - width / 2, y - height / 2, width, height);

            // set boundaries for characters
            boundaryBottom = windowHeight;
            boundaryRight = windowWidth;

            // create the shape object
            shape = new Shape(contentManager, spriteType, width);
            eyes = new Eyes(contentManager, spriteType, width);
            mouth = new Mouth(contentManager, spriteType, width);

            // set the sprite type
            currentSpriteType = spriteType;
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
        public void Update(GameTime gameTime)
        {
            // update the sub-sprites
            shape.Update(positionRectangle, CharShapeStatus, movementStatus, gameTime);
            eyes.Update(positionRectangle, CharShapeStatus, movementStatus, gameTime);
            mouth.Update(positionRectangle, CharShapeStatus, movementStatus, gameTime);
        }

        /// <summary>
        /// Checks to see if 2 sprites have collided
        /// </summary>
        public void checkForCollisions()
        {
            // determine if collision has occurred
            if ()
            {
                collisionOccurred = true;
            }
        }

        #endregion

        #region Private Methods
        
        #endregion
    }
}
