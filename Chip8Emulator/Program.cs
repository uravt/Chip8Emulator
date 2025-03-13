using System.Diagnostics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Chip8Emulator;

class Program
{
    static RenderWindow window;
    
    static void Main(string[] args)
    {
        window = new RenderWindow(new VideoMode(1200, 600), "Chip 8 Emulator");
        window.SetVisible(true);
        window.Closed += OnClosed;
        window.KeyPressed += OnPressed;
        
        CPU cpu = new CPU(File.ReadAllBytes("TestRoms/Pong (1 player).ch8"));
        
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
    
    private static void OnClosed(object? sender, EventArgs e)
    {
        window.Close();
    }
    
    private static void OnPressed(object? sender, KeyEventArgs args)
    {
        if (args.Code.Equals(Escape))
        {
            window.Close();
        }
    }
}



