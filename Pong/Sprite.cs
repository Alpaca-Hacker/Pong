using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    abstract public class Sprite
    {
       protected readonly Texture2D texture;
       public Vector2 Location;
       protected Vector2 delta = Vector2.Zero;
       protected Rectangle screenBounds;
        
       public int Height 
       {
           get { return texture.Height; }  
       }

       public int Width
       {
           get { return texture.Width; }
       }

       public Rectangle BoundingBox
       {
           get
           {
               return new Rectangle((int)Location.X, (int)Location.Y, Width, Height);
           }
       }

        public Sprite(Texture2D texture, Vector2 location, Rectangle screenBounds)
        {
            this.texture = texture;
            this.Location = location;
            this.screenBounds = screenBounds;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Location, Color.White);
        }

        public virtual void Update(GameTime gameTime, GameObjects gameObjects)
        {
            Location += delta;
            delta = Vector2.Zero;
            CheckBounds();
        }



        protected abstract void CheckBounds();
     
    }
}
