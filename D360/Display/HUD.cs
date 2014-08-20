using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D360
{
    public class HUD
    {
        public int screenWidth;
        public int screenHeight;

        GraphicsDevice dev = null;
        BasicEffect effect = null;

        SpriteBatch spriteBatch = null;

        Texture2D targetTexture;
        Texture2D moveModeTexture;
        Texture2D pointerModeTexture;

        public HUD(IntPtr windowHandle)
        {
            // Create device presentation parameters
            PresentationParameters p = new PresentationParameters();
            p.IsFullScreen = false;
            p.DeviceWindowHandle = windowHandle;
            p.BackBufferFormat = SurfaceFormat.Vector4;
            p.PresentationInterval = PresentInterval.One;

            // Create XNA graphics device
            dev = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, p);

            using (FileStream stream = new FileStream(@"Content\Target.png", FileMode.Open))
            {
                targetTexture = Texture2D.FromStream(dev, stream);
            }

            using (FileStream stream = new FileStream(@"Content\Move.png", FileMode.Open))
            {
                moveModeTexture = Texture2D.FromStream(dev, stream);
            }

            using (FileStream stream = new FileStream(@"Content\Pointer.png", FileMode.Open))
            {
                pointerModeTexture = Texture2D.FromStream(dev, stream);
            }

            // Init basic effect
            effect = new BasicEffect(dev);

            spriteBatch = new SpriteBatch(dev);
        }

        public void Draw(ControllerState state)
        {
            dev.Clear(new Microsoft.Xna.Framework.Color(0, 0, 0, 0.0f));

            

            spriteBatch.Begin();

            if (state.inputMode == Bindings.InputMode.Pointer)
            {
                spriteBatch.Draw(pointerModeTexture, new Microsoft.Xna.Framework.Rectangle(screenWidth - 256, screenHeight - 128, 128, 64), Microsoft.Xna.Framework.Color.White);
            }
            else
            {
                spriteBatch.Draw(moveModeTexture, new Microsoft.Xna.Framework.Rectangle(screenWidth - 256, screenHeight - 128, 128, 64), Microsoft.Xna.Framework.Color.White);
            }
            
            spriteBatch.End();

            /*
            int x = (int)(((centerX + rightStickXValue) / 65535.0f) * screenWidth) - 16;
            int y = (int)(((centerY + rightStickYValue) / 65535.0f) * screenHeight) - 16;
            Microsoft.Xna.Framework.Rectangle targetRect = new Microsoft.Xna.Framework.Rectangle(x, y, 32, 32);

            spriteBatch.Begin();

            if ((rightStickXValue != 0) && (rightStickYValue != 0))
            {
                spriteBatch.Draw(targetTexture, targetRect, new Microsoft.Xna.Framework.Color(1.0f, 1.0f, 1.0f, 0.5f));
            }

            //spriteBatch.Draw(moveModeTexture, new Microsoft.Xna.Framework.Rectangle(screenWidth - 256, screenHeight - 128, 128, 64), Microsoft.Xna.Framework.Color.White);

            if (mouse_absolute) 
            {
                spriteBatch.Draw(moveModeTexture, new Microsoft.Xna.Framework.Rectangle(screenWidth - 256, screenHeight - 128, 128, 64), Microsoft.Xna.Framework.Color.White);
            }
            else
            {
                spriteBatch.Draw(pointerModeTexture, new Microsoft.Xna.Framework.Rectangle(screenWidth - 256, screenHeight - 128, 128, 64), Microsoft.Xna.Framework.Color.White);
            }


            spriteBatch.End();

             */
            // Present the device contents into form
            dev.Present();
        }
    }
}
