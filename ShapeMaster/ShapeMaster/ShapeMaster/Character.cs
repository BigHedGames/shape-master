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
        const int INITIAL_VELOCITY = 5;
        readonly double DIAGONAL_FACTOR = 1.0 / Math.Sqrt(2.0);
        #endregion

        #region Fields
        Rectangle positionRectangle;

        // movement boundaries for screen
        int boundaryTop = 0;
        int boundaryBottom;
        int boundaryLeft = 0;
        int boundaryRight;

        // create shape object
        Shape shape;
        Eyes eyes;
        Mouth mouth;

        #endregion

        #region Properties
        public int Velocity { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Player class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Character(ContentManager contentManager, string shapeSpriteName, string eyesSpriteName, string mouthSpriteName, int x, int y, 
            int width, int height, int windowWidth, int windowHeight)
        {
            positionRectangle = new Rectangle(x - width / 2, y - height / 2, width, height);

            Velocity = INITIAL_VELOCITY;

            // set boundaries for characters
            boundaryBottom = windowHeight;
            boundaryRight = windowWidth;

            // create the shape object
            shape = new Shape(contentManager, shapeSpriteName, positionRectangle.X, 
                positionRectangle.Y, positionRectangle.Width, positionRectangle.Height);

            eyes = new Eyes(contentManager, eyesSpriteName, positionRectangle.X,
                positionRectangle.Y, positionRectangle.Width, positionRectangle.Height);

            mouth = new Mouth(contentManager, mouthSpriteName, positionRectangle.X,
                positionRectangle.Y, positionRectangle.Width, positionRectangle.Height);
        }
        #endregion

        #region Public Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            shape.SetPosition(positionRectangle);
            shape.Draw(spriteBatch);
            eyes.SetPosition(positionRectangle);
            eyes.Draw(spriteBatch);
            mouth.SetPosition(positionRectangle);
            mouth.Draw(spriteBatch);
        }

        public void Move(KeyboardState keyboardState)
        {
            
            // logic for movement in diagonals
            if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Up))
            {
                positionRectangle.Y -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                positionRectangle.X += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Down))
            {
                positionRectangle.Y += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                positionRectangle.X += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Up))
            {
                positionRectangle.Y -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                positionRectangle.X -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Down))
            {
                positionRectangle.Y += (int)Math.Round(Velocity * DIAGONAL_FACTOR);
                positionRectangle.X -= (int)Math.Round(Velocity * DIAGONAL_FACTOR);
            }

            // logic for movement in standard four directions
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    positionRectangle.Y -= Velocity;
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    positionRectangle.Y += Velocity;
                }

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    positionRectangle.X -= Velocity;
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    positionRectangle.X += Velocity;
                }
            }

            // set movement boundaries
            if (positionRectangle.X < boundaryLeft)
            {
                positionRectangle.X = boundaryLeft;
            }
            if (positionRectangle.Y < boundaryTop)
            {
                positionRectangle.Y = boundaryTop;
            }
            if (positionRectangle.X + positionRectangle.Width > boundaryRight)
            {
                positionRectangle.X = boundaryRight - positionRectangle.Width;
            }
            if (positionRectangle.Y + positionRectangle.Height > boundaryBottom)
            {
                positionRectangle.Y = boundaryBottom - positionRectangle.Height;
            }
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
