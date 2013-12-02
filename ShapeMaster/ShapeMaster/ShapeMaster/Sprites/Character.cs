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
    class Character
    {
        #region Constants

        // velocity related constants
        protected readonly double DIAGONAL_FACTOR = 1.0 / Math.Sqrt(2.0);

        #endregion

        #region Fields

        // determines position of the character
        protected Vector2 positionVector;
        protected Rectangle positionRectangle;

        // movement boundaries for screen
        protected int boundaryTop = 0;
        protected int boundaryBottom;
        protected int boundaryLeft = 0;
        protected int boundaryRight;

        // create shape object
        Shape shape;
        Eyes eyes;
        Mouth mouth;

        // character movement status to pass to sub-sprites
        protected MovementStatus movementStatus;

        #endregion

        #region Properties

        // Velocity (publicly available for potential power-ups/other external modifications
        public int Velocity { get; protected set; }

        // Declare property to hold status of player's shape
        public ShapeStatus CharShapeStatus { get; protected set; }

        // create sprite type object
        public SpriteType CurrentSpriteType { get; protected set; }

        // create collision rectangle functionality
        public Rectangle CollisionRectangle
        {
            get
            {
                return positionRectangle;
            }
        }

        // Get shape texture
        public Texture2D Texture
        {
            get
            {
                return shape.Texture;
            }
        }

        // Active/not active support
        public bool IsActive { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Character constructor.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="x">The center x position.</param>
        /// <param name="y">The center y position.</param>
        /// <param name="width">The width of the sprite.</param>
        /// <param name="height">The height of the sprite.</param>
        /// <param name="windowWidth">The window width (needed for the boundaries).</param>
        /// <param name="windowHeight">The window height (needed for the boundaries).</param>
        public Character(ContentManager contentManager, SpriteType spriteType, int x, int y, int width,
            int height, int windowWidth, int windowHeight)
        {
            // define the position vector and rectangle
            positionVector = new Vector2(x - width / 2, y - width / 2);
            positionRectangle = new Rectangle(x - width / 2, y - height / 2, width, height);

            // set boundaries for characters
            boundaryBottom = windowHeight;
            boundaryRight = windowWidth;

            // create the shape object
            shape = new Shape(contentManager, spriteType, width);
            eyes = new Eyes(contentManager, spriteType, width);
            mouth = new Mouth(contentManager, spriteType, width);

            // set the sprite type
            CurrentSpriteType = spriteType;

            // Active characters
            IsActive = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draw the sprites.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the shape first, then the eyes and mouth.
            shape.Draw(spriteBatch);
            eyes.Draw(spriteBatch);
            mouth.Draw(spriteBatch);
        }

        /// <summary>
        /// Update the sprites.
        /// </summary>
        /// <param name="keyboardState">The keyboard state which provides keyboard input.</param>
        public void Update(GameTime gameTime)
        {
            // update the sub-sprites
            positionRectangle.X = (int)(positionVector.X);
            positionRectangle.Y = (int)(positionVector.Y);
            shape.Update(positionRectangle, CharShapeStatus, movementStatus, gameTime);
            eyes.Update(positionRectangle, CharShapeStatus, movementStatus, gameTime);
            mouth.Update(positionRectangle, CharShapeStatus, movementStatus, gameTime);

            // set the sprite type
            shape.CurrentSpriteType = this.CurrentSpriteType;
            eyes.CurrentSpriteType = this.CurrentSpriteType;
            mouth.CurrentSpriteType = this.CurrentSpriteType;
        }

        /// <summary>
        /// Checks to see if 2 sprites have collided
        /// </summary>
        public bool CheckForCollisions(Character characterToCheck)
        {
            // Determine if collision has occurred between 2 sprite rectangles
            if (CollisionRectangle.Intersects(characterToCheck.CollisionRectangle))
            {
                // Check for pixel-perfect collision
                if (PixelPerfect(this, characterToCheck))
                {
                    // Check for collision type (between sprites)
                    if (this.CurrentSpriteType == SpriteType.CHARly ||
                        characterToCheck.CurrentSpriteType == SpriteType.CHARly)
                    {
                        // Check for if the sprite collision involves a "mad" character
                        if (this.CurrentSpriteType == SpriteType.Mad ||
                            characterToCheck.CurrentSpriteType == SpriteType.Mad)
                        {
                            return true;
                        }
                        // Check for if the sprite collision involves a "saved" character
                        if (this.CurrentSpriteType == SpriteType.Saved)
                        {
                            this.IsActive = false;
                        }
                        if (characterToCheck.CurrentSpriteType == SpriteType.Saved)
                        {
                            characterToCheck.IsActive = false;
                        }
                    }
                }
            }

            return false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check for pixel-perfect collisions
        /// </summary>
        /// <param name="rectangle0"></param>
        /// <param name="rectangle1"></param>
        /// <returns></returns>
        private bool PixelPerfect(Character charA, Character charB)
        {
            // Get Color data of each Texture
            Color[] bitsA = new Color[charA.Texture.Width * charA.Texture.Height];
            charA.Texture.GetData(bitsA);
            Color[] bitsB = new Color[charB.Texture.Width * charB.Texture.Height];
            charB.Texture.GetData(bitsB);

            // Calculate the intersecting rectangle
            int x1 = Math.Max(charA.CollisionRectangle.X, charB.CollisionRectangle.X);
            int x2 = Math.Min(charA.CollisionRectangle.X + charA.CollisionRectangle.Width, charB.CollisionRectangle.X + charB.CollisionRectangle.Width);

            int y1 = Math.Max(charA.CollisionRectangle.Y, charB.CollisionRectangle.Y);
            int y2 = Math.Min(charA.CollisionRectangle.Y + charA.CollisionRectangle.Height, charB.CollisionRectangle.Y + charB.CollisionRectangle.Height);

            // Calculate image scaling factors
            int scaleFactorXA = charA.Texture.Height / charA.CollisionRectangle.Width; // FIXME LATER!!!
            int scaleFactorXB = charB.Texture.Height / charB.CollisionRectangle.Width; // FIXME LATER!!!
            int scaleFactorYA = charA.Texture.Height / charA.CollisionRectangle.Height;
            int scaleFactorYB = charB.Texture.Height / charB.CollisionRectangle.Height;


            // For each single pixel in the intersecting rectangle
            for (int y = y1; y < y2; ++y)
            {
                for (int x = x1; x < x2; ++x)
                {
                    // Get the color from each texture
                    Color a = bitsA[scaleFactorXA * (x - charA.CollisionRectangle.X) + scaleFactorYA * (y - charA.CollisionRectangle.Y) * charA.Texture.Width];
                    Color b = bitsB[scaleFactorXB * (x - charB.CollisionRectangle.X) + scaleFactorYB * (y - charB.CollisionRectangle.Y) * charB.Texture.Width];

                    if (a.A != 0 && b.A != 0) // If both colors are not transparent (the alpha channel is not 0), then there is a collision
                    {
                        return true;
                    }
                }
            }
            // If no collision occurred by now, we're clear.
            return false;
        }

        #endregion
    }
}
