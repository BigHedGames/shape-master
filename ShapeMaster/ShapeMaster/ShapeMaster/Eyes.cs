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
    class Eyes : SpriteBase
    {
        #region Constants

        // base width for each image in the sprite strip
        const int BASE_WIDTH = 128;

        // animation update time (in ms)
        const int MOVEMENT_UPDATE_TIME = 200;

        #endregion

        #region Fields

        // Animation rectangle
        Rectangle sourceRectangle;

        // Declare variables to hold eye art
        Texture2D drawEyes;
        Texture2D northEyes;
        Texture2D northEastEyes;
        Texture2D eastEyes;
        Texture2D southEastEyes;
        Texture2D southEyes;

        // For flipping the sprites
        bool isFlipped = false;
        SpriteEffects flipper = SpriteEffects.FlipHorizontally;
        Vector2 spriteOrigin;

        // class timer and animation support
        int movementTimer;
        int iteration = 0;
        int maxIterations;

        #endregion

        #region Constructor

        /// <summary>
        /// Eyes constructor.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        public Eyes(ContentManager contentManager, int spriteWidth)
            : base(contentManager, spriteWidth)
        {
            // load sprite content
            LoadContent(contentManager);

            // define the animation strip rectangle
            sourceRectangle = new Rectangle(0, 0, BASE_WIDTH, BASE_WIDTH);

            // needed for the flip effect
            spriteOrigin = new Vector2(0, 0);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draw the Mouth.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isFlipped)
            {
                spriteBatch.Draw(drawEyes, drawRectangle, sourceRectangle, Color.White, 0, spriteOrigin, flipper, 0);
            }
            else
            {
                spriteBatch.Draw(drawEyes, drawRectangle, sourceRectangle, Color.White);
            }
        }

        /// <summary>
        /// Updates this sub-sprite.
        /// </summary>
        /// <param name="drawRec">The rectangle to draw in.</param>
        /// <param name="shapeStatus">The shape that the full sprite should take.</param>
        /// <param name="movementStatus">The direction the sprite is moving.</param>
        /// <param name="gameTime">For animation timers.</param>
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

        private void Animate(MovementStatus movementStatus, GameTime gameTime)
        {
            // if it's not moving, just use first frame of north eyes
            if (movementStatus == MovementStatus.Stationary)
            {
                // Stationary eyes
                drawEyes = northEyes;
                sourceRectangle.X = 0;
            }
            else
            {
                // set the sub-sprite based on movement status
                SetSprite(movementStatus);

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
        }

        /// <summary>
        /// Takes the movement status and determines which eyes to use
        /// </summary>
        /// <param name="movementStatus"></param>
        private void SetSprite(MovementStatus movementStatus)
        {
            isFlipped = false;
            if (movementStatus == MovementStatus.North)
            {
                drawEyes = northEyes;
            }
            if (movementStatus == MovementStatus.NorthEast)
            {
                drawEyes = northEastEyes;
            }
            if (movementStatus == MovementStatus.East)
            {
                drawEyes = eastEyes;
            }
            if (movementStatus == MovementStatus.SouthEast)
            {
                drawEyes = southEastEyes;
            }
            if (movementStatus == MovementStatus.South)
            {
                drawEyes = southEyes;
            }
            if (movementStatus == MovementStatus.SouthWest)
            {
                isFlipped = true;
                drawEyes = southEastEyes;
            }
            if (movementStatus == MovementStatus.West)
            {
                isFlipped = true;
                drawEyes = eastEyes;
            }
            if (movementStatus == MovementStatus.NorthWest)
            {
                isFlipped = true;
                drawEyes = northEastEyes;
            }
        }

        /// <summary>
        /// Method to load the content into the content manager.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        private void LoadContent(ContentManager contentManager)
        {
            // load the sprites
            northEyes = contentManager.Load<Texture2D>("CHAR_EYEZNORTH");
            northEastEyes = contentManager.Load<Texture2D>("CHAR_EYEZNE");
            eastEyes = contentManager.Load<Texture2D>("CHAR_EYEZEAST");
            southEastEyes = contentManager.Load<Texture2D>("CHAR_EYEZSE");
            southEyes = contentManager.Load<Texture2D>("CHAR_EYEZSOUTH");

            // set the maximum iterations before starting over at 0 in the sprite strip
            maxIterations = northEyes.Width / BASE_WIDTH;

            if (northEastEyes.Width != northEyes.Width ||
                eastEyes.Width != northEyes.Width ||
                southEastEyes.Width != northEyes.Width ||
                southEastEyes.Width != northEyes.Width)
            {
                throw new Exception("The sprite strip image widths are different in the Eyes class.");
            }
        }

        #endregion
    }
}
