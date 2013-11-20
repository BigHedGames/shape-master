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
    class Mouth
    {
        #region Fields

        // Draw rectangle, animation rectangle, and sprite image
        Rectangle drawRectangle;
        //Rectangle sourceRectangle;

        // Declare variables to hold mouth art
        Texture2D charMouth;
        Texture2D madMouth;
        Texture2D saveMouth;

        #endregion

        #region Constructor

        /// <summary>
        /// Mouth constructor.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        public Mouth(ContentManager contentManager)
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
            spriteBatch.Draw(charMouth, drawRectangle, Color.White);
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
        private void LoadContent(ContentManager contentManager)
        {
            charMouth = contentManager.Load<Texture2D>("CHAR_MOUTH");
            madMouth = contentManager.Load<Texture2D>("MAD_MOUTH");
            saveMouth = contentManager.Load<Texture2D>("SAVED_MOUTH");
        }

        #endregion
    }
}
