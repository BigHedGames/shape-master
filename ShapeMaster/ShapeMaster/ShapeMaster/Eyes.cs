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
    class Eyes
    {
        #region Fields

        // Draw rectangle, animation rectangle, and sprite image
        Rectangle drawRectangle;
        //Rectangle sourceRectangle;

        // Declare variables to hold eye art
        Texture2D charEyes;
        Texture2D madEyes;
        Texture2D saveEyes;

        #endregion

        #region Constructor

        /// <summary>
        /// Eyes constructor.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        public Eyes(ContentManager contentManager)
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
            spriteBatch.Draw(charEyes, drawRectangle, Color.White);
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
            charEyes = contentManager.Load<Texture2D>("CHAR_EYEZ");
            madEyes = contentManager.Load<Texture2D>("MAD_EYEZ");
            saveEyes = contentManager.Load<Texture2D>("SAVED_EYEZ");
        }

        #endregion
    }
}
