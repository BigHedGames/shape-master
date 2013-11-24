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
        #region Constants

        // base width for each image in the sprite strip
        const int BASE_WIDTH = 128;

        #endregion

        #region Fields

        // Animation rectangle
        Rectangle sourceRectangle;

        // Declare variables to hold mouth art
        Texture2D mouth;

        // hash table holding the various possible eyes
        Dictionary<string, Texture2D> loadedMouth = new Dictionary<string, Texture2D>();

        #endregion

        #region Constructor

        /// <summary>
        /// Mouth constructor.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        public Mouth(ContentManager contentManager, SpriteType spriteType, int spriteWidth)
            : base(contentManager, spriteType, spriteWidth)
        {
            // load the sprite image
            LoadContent(contentManager);

            // define the animation strip rectangle
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
            spriteBatch.Draw(mouth, drawRectangle, sourceRectangle, Color.White);
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

        /// <summary>
        /// Mouth animation.
        /// </summary>
        /// <param name="movementStatus">Says whether or not this character is moving.</param>
        /// <param name="gameTime">The game time used to update the animation.</param>
        private void Animate(MovementStatus movementStatus, GameTime gameTime)
        {
            SetSprite();

            if (CurrentSpriteType == SpriteType.CHARly)
            {
                if (movementStatus != MovementStatus.Stationary)
                {
                    sourceRectangle.X = 0;
                }
                else
                {
                    sourceRectangle.X = BASE_WIDTH;
                }
            }

            else
            {
                sourceRectangle.X = 0;
            }
        }


        /// <summary>
        /// Set the sprite type
        /// </summary>
        private void SetSprite()
        {
            SetPrefix();
            mouth = loadedMouth[currentPrefix + "MOUTH"];
        }

        /// <summary>
        /// Method to load the content into the content manager.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        private void LoadContent(ContentManager contentManager)
        {
            // loop over all prefixes
            foreach (string prefix in characterPrefixes)
            {
                // set the asset and dictionary strings
                string imageStr = prefix + "MOUTH";
                string dictionaryStr = imageStr;
                if (prefix.Equals("CHAR_")) imageStr += "Z";

                // load the content and insert into the dictionary
                loadedMouth[dictionaryStr] = contentManager.Load<Texture2D>(imageStr);
            }
        }

        #endregion
    }
}
