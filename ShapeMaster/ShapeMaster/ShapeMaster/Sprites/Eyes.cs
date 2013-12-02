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

        // hash table holding the various possible eyes
        Dictionary<string, Texture2D> loadedEyes = new Dictionary<string,Texture2D>();

        // For flipping the sprites
        bool isFlipped = false;
        SpriteEffects flipper = SpriteEffects.FlipHorizontally;
        Vector2 spriteOrigin;

        // class timer and animation support
        int movementTimer;
        int iteration = 0;
        Dictionary<string, int> maxIterations = new Dictionary<string,int>();

        #endregion

        #region Constructor

        /// <summary>
        /// Eyes constructor.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        public Eyes(ContentManager contentManager, SpriteType spriteType, int spriteWidth)
            : base(contentManager, spriteType, spriteWidth)
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
                SetPrefix();
                drawEyes = loadedEyes[currentPrefix + "EYEZNORTH"];
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
                    if (iteration >= maxIterations[currentPrefix])
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
            // set the prefix based on type
            SetPrefix();

            // set the direction for the eyes
            SetDirection(movementStatus);
        }

        /// <summary>
        /// Set the direction the eyes should face.
        /// </summary>
        /// <param name="movementStatus">The direction the eyes should point.</param>
        private void SetDirection(MovementStatus movementStatus)
        {
            // direction
            isFlipped = false;
            if (movementStatus == MovementStatus.North)
            {
                drawEyes = loadedEyes[currentPrefix + "EYEZNORTH"];
            }
            if (movementStatus == MovementStatus.NorthEast)
            {
                drawEyes = loadedEyes[currentPrefix + "EYEZNE"];
            }
            if (movementStatus == MovementStatus.East)
            {
                drawEyes = loadedEyes[currentPrefix + "EYEZEAST"];
            }
            if (movementStatus == MovementStatus.SouthEast)
            {
                drawEyes = loadedEyes[currentPrefix + "EYEZSE"];
            }
            if (movementStatus == MovementStatus.South)
            {
                drawEyes = loadedEyes[currentPrefix + "EYEZSOUTH"];
            }
            if (movementStatus == MovementStatus.SouthWest)
            {
                isFlipped = true;
                drawEyes = loadedEyes[currentPrefix + "EYEZSE"];
            }
            if (movementStatus == MovementStatus.West)
            {
                isFlipped = true;
                drawEyes = loadedEyes[currentPrefix + "EYEZEAST"];
            }
            if (movementStatus == MovementStatus.NorthWest)
            {
                isFlipped = true;
                drawEyes = loadedEyes[currentPrefix + "EYEZNE"];
            }
        }

        /// <summary>
        /// Method to load the content into the content manager.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        private void LoadContent(ContentManager contentManager)
        {
            // load all the possible eyes
            List<string> directions = new List<string>() { "NORTH", "NE", "EAST", "SE", "SOUTH" };

            // loop over all prefixes
            foreach (string prefix in characterPrefixes)
            {
                // To test if the widths are the same for each string (needed for animations)
                int widthTest = 0;

                // loop over directions
                foreach (string dir in directions)
                {
                    // set the asset and dictionary strings
                    string imageStr = prefix + "EYEZ";
                    string dictionaryStr = imageStr + dir ;
                    if (prefix.Equals("CHAR_") || prefix.Equals("MAD_")) imageStr += dir;

                    // load the content and insert into the dictionary
                    loadedEyes[dictionaryStr] = contentManager.Load<Texture2D>(imageStr);

                    // Check that all sprites of the same character type are the same width
                    if (widthTest > 0)
                    {
                        if (loadedEyes[dictionaryStr].Width != widthTest)
                        {
                            throw new Exception("The sprite strip image widths are different in the Eyes class.");
                        }
                    }
                    else
                    {
                        widthTest = loadedEyes[prefix + "EYEZ" + dir].Width;
                        maxIterations.Add(prefix, widthTest / BASE_WIDTH);
                    }
                }
            }
        }

        #endregion
    }
}
