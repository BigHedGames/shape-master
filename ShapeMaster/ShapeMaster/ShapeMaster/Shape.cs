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
        Rectangle drawRectangle;
        Rectangle sourceRectangle;
        Texture2D spriteTexture;

        #endregion

        #region Constructor
        public Shape(ContentManager contentManager, string spriteName, int x, int y, int width, int height)
        {
            LoadContent(contentManager, spriteName);

            drawRectangle = new Rectangle(x - width / 2, y - height / 2, width, height);
        }

        #endregion

        #region Private Methods
        private void LoadContent(ContentManager contentManager, string spriteName)
        {
            spriteTexture = contentManager.Load<Texture2D>(spriteName);
        }

        #endregion

        #region Public Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, drawRectangle, Color.Green);
        }

        public void SetPosition(Rectangle rectangle)
        {
            drawRectangle = rectangle;
        }

        #endregion
    }
}
