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
        Rectangle sourceRectangle;
        Texture2D spriteTexture;

        #endregion

        #region Constructor

        /// <summary>
        /// Shape constructor.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="spriteName">The name of the asset to start with.</param>
        public Shape(ContentManager contentManager, string spriteName)
        {
            LoadContent(contentManager, spriteName);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draw the Mouth.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, drawRectangle, Color.Green);
        }

        /// <summary>
        /// Set the mouth draw rectangle position.
        /// </summary>
        /// <param name="rectangle">The draw rectangle to set to.</param>
        public void SetPosition(Rectangle rectangle)
        {
            drawRectangle = rectangle;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to load the content into the content manager.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="spriteName">The asset class name.</param>
        private void LoadContent(ContentManager contentManager, string spriteName)
        {
            spriteTexture = contentManager.Load<Texture2D>(spriteName);
        }

        #endregion
    }
}
