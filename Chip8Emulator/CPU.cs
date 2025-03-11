using SFML.Graphics;

public class CPU
{
    public static uint WIDTH = 64;
    public static uint HEIGHT = 32;
    private byte[] memory;
    public ushort pc;
    public ushort index_register;
    public Stack<ushort> stk;
    private byte delay_timer;
    private byte sound_timer;
    public byte[] registers;
    
    public Image screen;
    
    public CPU(byte[] instructions)
    {
        memory = new byte[4096]; //initialize memory
        Buffer.BlockCopy(instructions, 0, memory, 0x200, instructions.Length);
        pc = 0x200;
        index_register = 0;
        stk = new Stack<ushort>();
        delay_timer = 0;
        sound_timer = 0;
        registers = new byte[16];
        
        screen = new Image(WIDTH, HEIGHT);
        
        InitializeFont();
    }

    public void ExecuteCycle()
    {
        byte[] currentInstruction = {memory[pc], memory[pc + 1]};
        byte nibble1 = (byte) (currentInstruction[0] >> 4);
        byte nibble2 = (byte) (currentInstruction[0] & 0x0F);
        byte nibble3 = (byte) (currentInstruction[1] >> 4);
        byte nibble4 = (byte) (currentInstruction[1] & 0x0F);
        ushort instruction = BitConverter.ToUInt16(new [] {currentInstruction[1], currentInstruction[0]}, 0);
        
        switch (nibble1)
        {
            case 0x0:
                switch (instruction)
                {
                    case 0x00E0:
                        screen = new Image(WIDTH, HEIGHT);
                        pc += 2;
                        break;
                }
                break;
            case 0x1:
                pc = GetValueFromNNN(nibble2, nibble3, nibble4);
                break;
            case 0x6:
                registers[nibble2] = currentInstruction[1];
                pc += 2;
                break;
            case 0x7:
                registers[nibble2] += currentInstruction[1];
                pc += 2;
                break;
            case 0xA:
                index_register = GetValueFromNNN(nibble2, nibble3, nibble4);
                pc += 2;
                break;
            case 0xD:
                byte x_coord = (byte) (registers[nibble2] % WIDTH);
                byte y_coord = (byte) (registers[nibble3] % HEIGHT);
                registers[0xF] = 0;
                
                byte spriteHeight = nibble4;
                int byteCounter = 0;
                
                for (int y = y_coord; y < spriteHeight + y_coord && y < HEIGHT; y++)
                {
                    byte currentByte = memory[byteCounter + index_register];
                    int bitCounter = 0;
                    for (int x = x_coord; x < x_coord + 8 && x < WIDTH; x++)
                    {
                        bool bit = (currentByte & (1 << 7 - bitCounter)) != 0;
                        bool screenBit = screen.GetPixel((uint) x, (uint) y).Equals(Color.White);
                    
                        if (bit == screenBit && bit)
                        {
                            registers[0xF] = 1;
                        }
                        
                        bit ^= screenBit;
                        if (bit)
                        {
                            screen.SetPixel((uint) x, (uint) y, Color.White);
                        }
                        else
                        {
                            screen.SetPixel((uint) x, (uint) y, Color.Black);
                        }
                        bitCounter++;
                    }
                    byteCounter++;
                }
                pc += 2;
                break;
        }
    }

    private ushort GetValueFromNNN(byte nibble2, byte nibble3, byte nibble4)
    {
        ushort address = 0;
        address |= (ushort) (nibble2 << 8);
        address |= (ushort) (nibble3 << 4);
        address |= nibble4;
        return address;
    }

    private void InitializeFont()
    {
        //0
        memory[0x50] = 0xF0;
        memory[0x51] = 0x90;
        memory[0x52] = 0x90;
        memory[0x53] = 0x90;
        memory[0x54] = 0xF0;
        //1
        memory[0x55] = 0x20;
        memory[0x56] = 0x60;
        memory[0x57] = 0x20;
        memory[0x58] = 0x20;
        memory[0x59] = 0x70;
        //2
        memory[0x5A] = 0xF0;
        memory[0x5B] = 0x10;
        memory[0x5C] = 0xF0;
        memory[0x5D] = 0x80;
        memory[0x5E] = 0xF0;
        //3
        memory[0x5F] = 0xF0;
        memory[0x60] = 0x10;
        memory[0x61] = 0xF0;
        memory[0x62] = 0x10;
        memory[0x63] = 0xF0;
        //4
        memory[0x64] = 0x90;
        memory[0x65] = 0x90;
        memory[0x66] = 0xF0;
        memory[0x67] = 0x10;
        memory[0x68] = 0x10;
        //5
        memory[0x69] = 0xF0;
        memory[0x6A] = 0x80;
        memory[0x6B] = 0xF0;
        memory[0x6C] = 0x10;
        memory[0x6D] = 0xF0;
        //6
        memory[0x6E] = 0xF0;
        memory[0x6F] = 0x80;
        memory[0x70] = 0xF0;
        memory[0x71] = 0x90;
        memory[0x72] = 0xF0;
        //7
        memory[0x73] = 0xF0;
        memory[0x74] = 0x10;
        memory[0x75] = 0x20;
        memory[0x76] = 0x40;
        memory[0x77] = 0x40;
        //8
        memory[0x78] = 0xF0;
        memory[0x79] = 0x90;
        memory[0x7A] = 0xF0;
        memory[0x7B] = 0x90;
        memory[0x7C] = 0xF0;
        //9
        memory[0x7D] = 0xF0;
        memory[0x7E] = 0x90;
        memory[0x7F] = 0xF0;
        memory[0x80] = 0x10;
        memory[0x81] = 0xF0;
        //A
        memory[0x82] = 0xF0;
        memory[0x83] = 0x90;
        memory[0x84] = 0xF0;
        memory[0x85] = 0x90;
        memory[0x86] = 0x90;
        //B
        memory[0x87] = 0xE0;
        memory[0x88] = 0x90;
        memory[0x89] = 0xE0;
        memory[0x8A] = 0x90;
        memory[0x8B] = 0xE0;
        //C
        memory[0x8C] = 0xF0;
        memory[0x8D] = 0x80;
        memory[0x8E] = 0x80;
        memory[0x8F] = 0x80;
        memory[0x90] = 0xF0;
        //D
        memory[0x91] = 0xE0;
        memory[0x92] = 0x90;
        memory[0x93] = 0x90;
        memory[0x94] = 0x90;
        memory[0x95] = 0xE0;
        //E
        memory[0x96] = 0xF0;
        memory[0x97] = 0x80;
        memory[0x98] = 0xF0;
        memory[0x99] = 0x80;
        memory[0x9A] = 0xF0;
        //F
        memory[0x9B] = 0xF0;
        memory[0x9C] = 0x80;
        memory[0x9D] = 0xF0;
        memory[0x9E] = 0x80;
        memory[0x9F] = 0x80;
    }
}
