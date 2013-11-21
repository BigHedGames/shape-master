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
    /// <summary>
    /// Base class of all sub-sprites, basically just handles the drawRectangle
    /// and it's animation while shape shifting.
    /// </summary>
    class SpriteBase
    {
        #region Constants

        // animation update time (in ms)
        const int SHIFTING_UPDATE_TIME = 100;

        #endregion

        #region Fields

        // Draw rectangle, animation rectangle, and sprite image
        protected Rectangle drawRectangle;

        // Declare variable to hold shape status
        ShapeStatus charShapeStatusHold;
        protected ShapeStatus charShapeStatus;
        bool runShiftingAnimation = false;

        // class timer and animation support
        int shiftingTimer;
        readonly int normalWidth;

        #endregion

        #region Constructor

        /// <summary>
        /// Shape constructor.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        public SpriteBase(ContentManager contentManager, int spriteWidth)
        {
            // set the stable size of the draw rectangle 
            // (so that we can shrink and re-grow during a shape-shift)
            normalWidth = spriteWidth;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to update this sub-sprite (including animations)
        /// </summary>
        /// <param name="drawRec">The draw rectangle (for the position for movement).</param>
        /// <param name="shapeStatus">The shape to draw.</param>
        public void Update(Rectangle drawRec, ShapeStatus shapeStatus, GameTime gameTime)
        {
            // updating some drawRectangle position
            drawRectangle = drawRec;
            charShapeStatusHold = shapeStatus;
            if (!runShiftingAnimation && charShapeStatusHold != charShapeStatus)
            {
                runShiftingAnimation = true;
                shiftingTimer = 0;
            }

            // run animations
            Animate(gameTime);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Animate the shape (wiggle when moving, shape shift animation).
        /// </summary>
        /// <param name="movementStatus">The direction the shape is moving.</param>
        /// <param name="gameTime">For the ellapsed game time.</param>
        protected void Animate(GameTime gameTime)
        {
            // Check if a shape shift should occurr or if one is in progress.
            if (runShiftingAnimation)
            {
                // update the gametime
                shiftingTimer += gameTime.ElapsedGameTime.Milliseconds;

                // if the actual shape status is different than the hold, shrink
                if (charShapeStatus != charShapeStatusHold)
                {
                    int newWidth = (int)((1 - (double)shiftingTimer / (double)SHIFTING_UPDATE_TIME) * normalWidth);

                    if (newWidth < 0)
                    {
                        newWidth = 0;
                        charShapeStatus = charShapeStatusHold;
                        shiftingTimer = 0;
                    }

                    int shift = (int)((float)(drawRectangle.Width - newWidth) / 2.0);
                    drawRectangle.Width = newWidth;
                    drawRectangle.Height = newWidth;
                    drawRectangle.X += shift;
                    drawRectangle.Y += shift;
                }
                else
                {
                    int newWidth = (int)((double)shiftingTimer / (double)SHIFTING_UPDATE_TIME * normalWidth);

                    if (newWidth > normalWidth)
                    {
                        newWidth = normalWidth;
                        runShiftingAnimation = false;
                    }

                    int shift = (int)((float)(newWidth - drawRectangle.Width) / 2.0);
                    drawRectangle.Width = newWidth;
                    drawRectangle.Height = newWidth;
                    drawRectangle.X -= shift;
                    drawRectangle.Y -= shift;
                }
            }
        }

        #endregion
    }
}
