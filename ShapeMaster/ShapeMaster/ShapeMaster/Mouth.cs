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
    class Mouth : SpriteBase
    {
        #region Fields

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
        public Mouth(ContentManager contentManager, int spriteWidth)
            : base(contentManager, spriteWidth)
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
            //Animate(movementStatus, gameTime);
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
