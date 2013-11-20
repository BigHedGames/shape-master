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
    class Shape
    {
        #region Fields

        // Draw rectangle, animation rectangle, and sprite image
        Rectangle drawRectangle;
        //Rectangle sourceRectangle;

        // Declare variables to hold shape art
        Texture2D shapeCircle;
        Texture2D shapeSquare;
        Texture2D shapeStar;
        Texture2D shapeTriangle;

        // Declare variable to hold shape status
        ShapeStatus CharShapeStatus;

        #endregion

        #region Constructor

        /// <summary>
        /// Shape constructor.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        public Shape(ContentManager contentManager)
        {
            LoadContent(contentManager);
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
            if (CharShapeStatus == ShapeStatus.Circle)
            {
                spriteBatch.Draw(shapeCircle, drawRectangle, Color.Green);
            }

            if (CharShapeStatus == ShapeStatus.Square)
            {
                spriteBatch.Draw(shapeSquare, drawRectangle, Color.Green);
            }

            if (CharShapeStatus == ShapeStatus.Star)
            {
                spriteBatch.Draw(shapeStar, drawRectangle, Color.Green);
            }

            if (CharShapeStatus == ShapeStatus.Triangle)
            {
                spriteBatch.Draw(shapeTriangle, drawRectangle, Color.Green);
            }
        }

        /// <summary>
        /// Set the mouth draw rectangle position.
        /// </summary>
        /// <param name="rectangle">The draw rectangle to set to.</param>
        public void SetPosition(Rectangle rectangle)
        {
            drawRectangle = rectangle;
        }

        public void ChangeStatus(ShapeStatus shapeStatus)
        {
            CharShapeStatus = shapeStatus;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to load the content into the content manager.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        private void LoadContent(ContentManager contentManager)
        {
            shapeCircle = contentManager.Load<Texture2D>("CIRCLE");
            shapeSquare = contentManager.Load<Texture2D>("SQUARE");
            shapeStar = contentManager.Load<Texture2D>("STAR");
            shapeTriangle = contentManager.Load<Texture2D>("TRIANGLE");
        }

        #endregion
    }
}
