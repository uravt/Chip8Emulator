using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Chip8Emulator;

class Program
{
    static RenderWindow window;
    public static bool[] keysPressed = new bool[16];
    
    static void Main(string[] args)
    {
        CPU cpu = new CPU(File.ReadAllBytes("/Users/uravtanna/Coding/CSharp/Chip8Emulator/Chip8Emulator/TestRoms/4-flags.ch8"));
        window = new RenderWindow(new VideoMode(1200, 600), "Hello World!");
        window.SetVisible(true);
        window.Closed += OnClosed;
        window.KeyReleased += OnReleased;
        window.KeyPressed += OnPressed;
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
        Console.WriteLine(args.Code);
        switch (args.Code)
        {
            case X:
                keysPressed[0] = true;
                break;
            case Num1:
                keysPressed[1] = true;
                break;
            case Num2:
                keysPressed[2] = true;
                break;
            case Num3:
                keysPressed[3] = true;
                break;
            case Q:
                keysPressed[4] = true;
                break;
            case W:
                keysPressed[5] = true;
                break;
            case E:
                keysPressed[6] = true;
                break;
            case A:
                keysPressed[7] = true;
                break;
            case S:
                keysPressed[8] = true;
                break;
            case D:
                keysPressed[9] = true;
                break;
            case Z:
                keysPressed[10] = true;
                break;
            case C:
                keysPressed[11] = true;
                break;
            case Num4:
                keysPressed[12] = true;
                break;
            case R:
                keysPressed[13] = true;
                break;
            case F:
                keysPressed[14] = true;
                break;
            case V:
                keysPressed[15] = true;
                break;
        }
    }

    private static void OnReleased(object? sender, KeyEventArgs args)
    {
        switch (args.Code)
        {
            case X:
                keysPressed[0] = false;
                break;
            case Num1:
                keysPressed[1] = false;
                break;
            case Num2:
                keysPressed[2] = false;
                break;
            case Num3:
                keysPressed[3] = false;
                break;
            case Q:
                keysPressed[4] = false;
                break;
            case W:
                keysPressed[5] = false;
                break;
            case E:
                keysPressed[6] = false;
                break;
            case A:
                keysPressed[7] = false;
                break;
            case S:
                keysPressed[8] = false;
                break;
            case D:
                keysPressed[9] = false;
                break;
            case Z:
                keysPressed[10] = false;
                break;
            case C:
                keysPressed[11] = false;
                break;
            case Num4:
                keysPressed[12] = false;
                break;
            case R:
                keysPressed[13] = false;
                break;
            case F:
                keysPressed[14] = false;
                break;
            case V:
                keysPressed[15] = false;
                break;
        }
    }
}



