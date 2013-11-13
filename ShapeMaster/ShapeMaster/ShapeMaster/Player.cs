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
    class Player
    {
        #region Constants
        const int INITIAL_VELOCITY = 5;
        #endregion

        #region Fields
        Rectangle drawRectangle;
        Texture2D spriteTexture;
        #endregion

        #region Properties
        public int Velocity { get; set; }
        #endregion

        /// <summary>
        /// Player class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Player(ContentManager contentManager, string spriteName, int x, int y, int width, int height)
        {
            LoadContent(contentManager, spriteName);

            drawRectangle = new Rectangle(x - spriteTexture.Width / 2, y - spriteTexture.Height / 2, width, height);

            Velocity = INITIAL_VELOCITY;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, drawRectangle, Color.White);
        }

        public void Move(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                drawRectangle.Y -= Velocity;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                drawRectangle.Y += Velocity;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                drawRectangle.X -= Velocity;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                drawRectangle.X += Velocity;
            }
        }


        #region Private Methods
        /// <summary>
        /// Loads the sprite
        /// </summary>
        /// <param name="contentManager"></param>
        /// <param name="spriteName"></param>
        private void LoadContent(ContentManager contentManager, string spriteName)
        {
            spriteTexture = contentManager.Load<Texture2D>(spriteName);
        }
        #endregion
    }
}
