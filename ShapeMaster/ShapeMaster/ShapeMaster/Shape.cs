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
    class Shape : SpriteBase
    {
        #region Constants

        // base width for each image in the sprite strip
        const int BASE_WIDTH = 128;

        // animation update time (in ms)
        const int MOVEMENT_UPDATE_TIME = 100;

        #endregion

        #region Fields

        // Animation rectangle
        Rectangle sourceRectangle;

        // Declare variables to hold shape art
        Texture2D shapeCircle;
        Texture2D shapeSquare;
        Texture2D shapeStar;
        Texture2D shapeTriangle;

        // class timer and animation support
        int movementTimer;
        int iteration = 0;
        int maxIterations;

        #endregion

        #region Constructor

        /// <summary>
        /// Shape constructor.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        public Shape(ContentManager contentManager, int spriteWidth)
            : base(contentManager, spriteWidth)
        {
            // load sprites
            LoadContent(contentManager);

            // create the source rectangle for the sprite strip
            sourceRectangle = new Rectangle(0, 0, BASE_WIDTH, BASE_WIDTH);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draw the Mouth.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Shapeshifting draw logic
            if (charShapeStatus == ShapeStatus.Circle)
            {
                spriteBatch.Draw(shapeCircle, drawRectangle, sourceRectangle, Color.Green);
            }

            if (charShapeStatus == ShapeStatus.Square)
            {
                spriteBatch.Draw(shapeSquare, drawRectangle, sourceRectangle, Color.Green);
            }

            if (charShapeStatus == ShapeStatus.Star)
            {
                spriteBatch.Draw(shapeStar, drawRectangle, sourceRectangle, Color.Green);
            }

            if (charShapeStatus == ShapeStatus.Triangle)
            {
                spriteBatch.Draw(shapeTriangle, drawRectangle, sourceRectangle, Color.Green);
            }
        }

        /// <summary>
        /// Method to update this sub-sprite (including animations)
        /// </summary>
        /// <param name="drawRec">The draw rectangle (for the position for movement).</param>
        /// <param name="shapeStatus">The shape to draw.</param>
        public void Update(Rectangle drawRec, ShapeStatus shapeStatus, MovementStatus movementStatus, GameTime gameTime)
        {
            // update and animate the base
            base.Update(drawRec, shapeStatus, gameTime);
            base.Animate(gameTime);

            // run animations
            Animate(movementStatus, gameTime);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to load the content into the content manager.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        private void LoadContent(ContentManager contentManager)
        {
            // load all textures
            shapeCircle = contentManager.Load<Texture2D>("CIRCLEZ");
            shapeSquare = contentManager.Load<Texture2D>("SQUAREZ");
            shapeStar = contentManager.Load<Texture2D>("STARZ");
            shapeTriangle = contentManager.Load<Texture2D>("TRIANGLEZ");

            // set the maximum iterations before starting over at 0 in the sprite strip
            maxIterations = shapeCircle.Width / BASE_WIDTH;

            // double check
            if (shapeSquare.Width != shapeCircle.Width ||
                shapeStar.Width != shapeCircle.Width ||
                shapeTriangle.Width != shapeCircle.Width)
            {
                throw new Exception("The sprite strip image widths are different in the Shape class.");
            }
        }

        /// <summary>
        /// Animate the shape (wiggle when moving, shape shift animation).
        /// </summary>
        /// <param name="movementStatus">The direction the shape is moving.</param>
        /// <param name="gameTime">For the ellapsed game time.</param>
        private void Animate(MovementStatus movementStatus, GameTime gameTime)
        {
            // Movement Animation
            movementTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (movementTimer > MOVEMENT_UPDATE_TIME)
            {
                // reset the timer
                movementTimer = 0;

                // increment the frame
                iteration++;
                if (iteration >= maxIterations)
                {
                    iteration = 0;
                }

            }

            // check the movement state and update source rectangle
            if (movementStatus == MovementStatus.Stationary)
            {
                sourceRectangle.X = 0;
            }
            else
            {
                sourceRectangle.X = iteration * BASE_WIDTH;
            }

        }

        #endregion
    }
}
