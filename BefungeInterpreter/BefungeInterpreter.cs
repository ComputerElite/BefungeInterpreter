using System.Diagnostics;
using System.Net.Sockets;

namespace BefungeInterpreter;

public class BefungeInterpreter
{
    public string code = "";
    public char[,] playfield = new char[500, 500];
    public int x = 0;
    public int y = 0;
    public int dx = 1;
    public int dy = 0;

    /// <summary>
    /// Converts the code into a playfield
    /// </summary>
    

    private int a = 0;
    private int b = 0;
    private bool asciiModeOn = false;

    public List<int> stack = new List<int>();
    public void Execute()
    {
        char command;
        // While command is not @ (end program)
        while ((command = playfield[x,y]) != '@')
        {
            // Save values of a and b
            if (stack.Count >= 1) a = stack[^1];
            else a = 0;
            if (stack.Count >= 2) b = stack[^2];
            else b = 0;
            // If ascii mode is on, push the ascii value of the command onto the stack
            if (asciiModeOn && command != '"')
            {
                stack.Add((int)command);
            }
            else
            {
                // Only do commands when ascii mode is off
                switch (command)
                {
                    // Push numbers onto the stack
                    case '0':
                        stack.Add(0);
                        break;
                    case '1':
                        stack.Add(1);
                        break;
                    case '2':
                        stack.Add(2);
                        break;
                    case '3':
                        stack.Add(3);
                        break;
                    case '4':
                        stack.Add(4);
                        break;
                    case '5':
                        stack.Add(5);
                        break;
                    case '6':
                        stack.Add(6);
                        break;
                    case '7':
                        stack.Add(7);
                        break;
                    case '8':
                        stack.Add(8);
                        break;
                    case '9':
                        stack.Add(9);
                        break;
                    case '+':
                        Add();
                        break;
                    case '-':
                        Subtract();
                        break;
                    case '*':
                        Multiply();
                        break;
                    case '/':
                        IntegerDivide();
                        break;
                    case '%':
                        Modulo();
                        break;
                    case '!':
                        LogicalNot();
                        break;
                    case '`':
                        GreaterThan();
                        break;
                    case '>':
                        dx = 1;
                        dy = 0;
                        break;
                    case '<':
                        dx = -1;
                        dy = 0;
                        break;
                    case '^':
                        dx = 0;
                        dy = -1;
                        break;
                    case 'v':
                        dx = 0;
                        dy = 1;
                        break;
                    case '?':
                        MoveRandomly();
                        break;
                    case '_':
                        IfRightLeft();
                        break;
                    case '|':
                        IfUpDown();
                        break;
                    case '"':
                        asciiModeOn = !asciiModeOn;
                        break;
                    case ':':
                        // Duplicate value on top of stack
                        stack.Add(a);
                        break;
                    case '\\':
                        SwapAB();
                        break;
                    case '$':
                        // Pop and discard
                        RemoveAFromStack();
                        break;
                    case '.':
                        PopAndOutputIntFollowedBySpace();
                        break;
                    case ',' :
                        PopAndOutputChar();
                        break;
                    case '#':
                        // Skip next cell
                        Move();
                        break;
                    case 'p':
                        Put();
                        break;
                    case 'g':
                        Get();
                        break;
                    case '&':
                        InputNumber();
                        break;
                    case '~':
                        InputChar();
                        break;
                }
            }

            Move();
        }
        Console.WriteLine("PROGRAM END");
    }

    private void InputChar()
    {
        stack.Add(Console.ReadKey(true).KeyChar);
    }

    private void InputNumber()
    {
        stack.Add(Convert.ToInt32(Console.ReadKey(true).KeyChar.ToString()));
    }

    private void Get()
    {
        // A "get" call (a way to retrieve data in storage). Pop y and x, then push ASCII value of the character at that position in the program
        int y = a;
        int x = b;
        stack.RemoveAt(stack.Count - 1);
        stack.RemoveAt(stack.Count - 1);
        stack.Add(playfield[x, y]);
        playfield[x, y] = ' ';
    }

    private void Put()
    {
        // A "put" call (a way to store a value for later use). Pop y, x, and v, then change the character at (x,y) in the program to the character with ASCII value v
        int y = a;
        int x = b;
        int v = stack[^3];
        stack.RemoveAt(stack.Count - 1);
        stack.RemoveAt(stack.Count - 1);
        stack.RemoveAt(stack.Count - 1);
        playfield[x, y] = (char)v;
    }

    private void Move()
    {
        x += dx;
        y += dy;
    }

    private void PopAndOutputChar()
    {
        RemoveAFromStack();
        Console.Write((char)a);
    }

    private void PopAndOutputIntFollowedBySpace()
    {
        RemoveAFromStack();
        Console.Write(a + " ");
    }

    private void SwapAB()
    {
        stack[^1] = b;
        stack[^2] = a;
    }

    private void IfUpDown()
    {
        // Pop a value; move right if value=0, left otherwise
        RemoveAFromStack();
        if (a == 0)
        {
            dx = 0;
            dy = 1;
        }
        else
        {
            dx = 0;
            dy = -1;
        }
    }
    
    private void IfRightLeft()
    {
        // Pop a value; move right if value=0, left otherwise
        RemoveAFromStack();
        if (a == 0)
        {
            dx = 1;
            dy = 0;
        }
        else
        {
            dx = -1;
            dy = 0;
        }
    }

    private void MoveRandomly()
    {
        switch (Random.Shared.Next(0, 4))
        {
            case 0:
                dx = 1;
                dy = 0;
                break;
            case 1:
                dx = -1;
                dy = 0;
                break;
            case 2:
                dx = 0;
                dy = 1;
                break;
            case 3:
                dx = 0;
                dy = -1;
                break;
        }
    }

    private void GreaterThan()
    {
        // Greater than: Pop a and b, then push 1 if b>a, otherwise zero.
        RemoveAAndBFromStack();
        stack.Add(b > a ? 1 : 0);
    }

    private void LogicalNot()
    {
        RemoveAFromStack();
        stack.Add(a == 0 ? 1 : 0);
    }

    private void Modulo()
    {
        // Modulo: Pop a and b, then push the remainder of the integer division of b/a.
        RemoveAAndBFromStack();
        stack.Add(b % a);
    }

    void RemoveAFromStack()
    {
        stack.RemoveAt(stack.Count - 1);
    }

    private void RemoveAAndBFromStack()
    {
        stack.RemoveAt(stack.Count - 1);
        stack.RemoveAt(stack.Count - 1);
    }

    private void IntegerDivide()
    {
        //	Integer division: Pop a and b, then push b/a, rounded towards 0.
        RemoveAAndBFromStack();
        stack.Add(b / a);
    }

    private void Multiply()
    {
        // Multiplication: pop a and b, then push a * b
        RemoveAAndBFromStack();
        stack.Add(a * b);
    }

    private void Subtract()
    {
        // Subtraction: pop a and b, then push b - a
        RemoveAAndBFromStack();
        stack.Add(b - a);
    }

    private void Add()
    {
        // Addition: pop a and b, then push a + b
        RemoveAAndBFromStack();
        stack.Add(a + b);
    }
}