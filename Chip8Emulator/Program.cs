using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Chip8Emulator
{
    class Program
    {
        static RenderWindow window;
        static void Main(string[] args)
        {
            CPU cpu = new CPU(File.ReadAllBytes("/Users/uravtanna/Downloads/IBMLogo.ch8"));
            window = new RenderWindow(new VideoMode(1200, 600), "Hello World!");
            window.SetVisible(true);
            window.Closed += OnClosed;
            while (window.IsOpen)
            {
                cpu.ExecuteCycle();
                Texture texture = new Texture(cpu.screen);
                Sprite spriteBuffer = new Sprite(texture);

                float windowWidth = window.Size.X;
                float windowHeight = window.Size.Y;
                float textureWidth = texture.Size.X;
                float textureHeight = texture.Size.Y;

                // Scale the sprite to fit the window width
                float scaleFactorX = windowWidth / textureWidth;
                float scaleFactorY = windowHeight / textureHeight; // Maintain aspect ratio

                spriteBuffer.Scale = new Vector2f(scaleFactorX, scaleFactorY);
                
                window.DispatchEvents();
                window.Draw(spriteBuffer);
                window.Display();
            }
            

        }
        
        private static void OnClosed(object sender, EventArgs e)
        {
            window.Close();
        }
    }
}


