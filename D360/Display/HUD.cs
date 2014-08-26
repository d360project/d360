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
        Texture2D controllerNotFoundTexture;

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

            using (FileStream stream = new FileStream(@"Content\ControllerNotFound.png", FileMode.Open))
            {
                controllerNotFoundTexture = Texture2D.FromStream(dev, stream);
            }

            // Init basic effect
            effect = new BasicEffect(dev);

            spriteBatch = new SpriteBatch(dev);
        }

        public void Draw(ControllerState state)
        {
            dev.Clear(new Microsoft.Xna.Framework.Color(0, 0, 0, 0.0f));

            spriteBatch.Begin();

            Microsoft.Xna.Framework.Rectangle targetRect;

            if (!state.connected)
            {
                targetRect = new Microsoft.Xna.Framework.Rectangle((screenWidth / 2) - (controllerNotFoundTexture.Width / 2), (screenHeight / 2) - (controllerNotFoundTexture.Height / 2), controllerNotFoundTexture.Width, controllerNotFoundTexture.Height);
                spriteBatch.Draw(controllerNotFoundTexture, targetRect, Microsoft.Xna.Framework.Color.White);
            }

            else
            {


                if ((state.targetingReticulePosition.X != state.centerPosition.X) && (state.targetingReticulePosition.Y != state.centerPosition.Y))
                {
                    int x = (int)(((state.targetingReticulePosition.X) / 65535.0f) * screenWidth) - 16;
                    int y = (int)(((state.targetingReticulePosition.Y) / 65535.0f) * screenHeight) - 16;
                    targetRect = new Microsoft.Xna.Framework.Rectangle(x, y, 32, 32);

                    spriteBatch.Draw(targetTexture, targetRect, new Microsoft.Xna.Framework.Color(1.0f, 1.0f, 1.0f, 0.5f));
                }

                if (state.inputMode == InputMode.Pointer)
                {
                    spriteBatch.Draw(pointerModeTexture, new Microsoft.Xna.Framework.Rectangle(screenWidth - 128, screenHeight - 64, 128, 64), Microsoft.Xna.Framework.Color.White);
                }
                else
                {
                    spriteBatch.Draw(moveModeTexture, new Microsoft.Xna.Framework.Rectangle(screenWidth - 128, screenHeight - 64, 128, 64), Microsoft.Xna.Framework.Color.White);
                }

            }
            spriteBatch.End();

            /*
            

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
