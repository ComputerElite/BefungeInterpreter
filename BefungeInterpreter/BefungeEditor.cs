using System.ComponentModel.Design;

namespace BefungeInterpreter;

public class BefungeEditor
{
    public int playFieldHeight = 500;
    public int playFieldWidth = 500;
    public char[,] playfield;
    public int x = 0;
    public int y = 0;
    public int cursorX = 0;
    public int cursorY = 0;

    public int consoleWidth
    {
        get
        {
            return Console.WindowWidth;
        }
    }
    public int consoleHeight
    {
        get
        {
            return Console.WindowHeight;
        }
    }

    public int yPlayfieldSpace => consoleHeight - 4;

    public int xPlayfieldSpace => consoleWidth - 4;

    public void StartEdit()
    {
        while (true)
        {
            DrawCode();
            Input();
        }
    }

    private void Input()
    {
        ConsoleKeyInfo i = Console.ReadKey(true);
        if (i.Modifiers.HasFlag(ConsoleModifiers.Shift))
        {
            switch (i.Key)
            {
                case ConsoleKey.R:
                    Run();
                    break;
                case ConsoleKey.E:
                    Select();
                    break;
            }
        }
        else
        {
            switch (i.Key)
            {
                case ConsoleKey.DownArrow:
                    cursorY++;
                    AdjustCamera();
                    break;
                case ConsoleKey.UpArrow:
                    cursorY--;
                    AdjustCamera();
                    break;
                case ConsoleKey.RightArrow:
                    cursorX++;
                    AdjustCamera();
                    break;
                case ConsoleKey.LeftArrow:
                    cursorX--;
                    AdjustCamera();
                    break;
                case ConsoleKey.PageDown:
                    y += yPlayfieldSpace / 2;
                    cursorY += yPlayfieldSpace / 2;
                    AdjustCamera();
                    break;
                case ConsoleKey.PageUp:
                    y -= yPlayfieldSpace / 2;
                    cursorY -= yPlayfieldSpace / 2;
                    AdjustCamera();
                    break;
                default:
                    playfield[cursorX, cursorY] = i.KeyChar;
                    break;
            }
        }
    }

    bool select = false;
    int selectionWidth = 0;
    int selectionHeight = 0;
    int selectionX = 0;
    int selectionY = 0;
    
    
    private void Select()
    {
        select = true;
        selectionX = cursorX;
        selectionY = cursorY;
        selectionWidth = 0;
        selectionHeight = 0;
        while (select)
        {
            ConsoleKeyInfo i = Console.ReadKey(true);
            switch (i.Key)
            {
                case ConsoleKey.DownArrow:
                    selectionHeight++;
                    break;
                case ConsoleKey.UpArrow:
                    selectionHeight--;
                    break;
                case ConsoleKey.RightArrow:
                    selectionWidth++;
                    break;
                case ConsoleKey.LeftArrow:
                    selectionWidth--;
                    break;
                case ConsoleKey.R:
                    select = false;
                    break;
                case ConsoleKey.Enter:
                    select = false;
                    MoveSelection();
                    break;
            }
            DrawCode();
        }
        select = false;
    }

    bool movingSelection = false;
    private void MoveSelection()
    {
        movingSelection = true;
        while (movingSelection)
        {
            DrawCode();
            ConsoleKeyInfo i = Console.ReadKey(true);
            switch (i.Key)
            {
                case ConsoleKey.RightArrow:
                    MoveSelectionOnX(1);
                    break;
                case ConsoleKey.LeftArrow:
                    MoveSelectionOnX(-1);
                    break;
                case ConsoleKey.UpArrow:
                    MoveSelectionOnY(-1);
                    break;
                case ConsoleKey.DownArrow:
                    MoveSelectionOnY(1);
                    break;
                case ConsoleKey.Enter:
                    movingSelection = false;
                    break;
            }
        }
        movingSelection = false;
    }

    private void MoveSelectionOnY(int dy)
    {
        if(selectionY - dy < 0) return;
        char[,] oldPlayfield = GetPlayFieldCopy();
        for (int i = 0; i < selectionWidth + 1; i++)
        {
            for (int j = 0; j < selectionHeight + 1; j++)
            {
                playfield[selectionX + i, selectionY + j] = ' ';
            }
        }
        for (int i = 0; i < selectionWidth + 1; i++)
        {
            for (int j = 0; j < selectionHeight + 1; j++)
            {
                playfield[selectionX + i, selectionY + j + dy] = oldPlayfield[selectionX + i, selectionY + j];
            }
        }
        selectionY += dy;
    }

    private void MoveSelectionOnX(int dx)
    {
        if(selectionX - dx < 0) return;
        char[,] oldPlayfield = GetPlayFieldCopy();
        for (int i = 0; i < selectionWidth + 1; i++)
        {
            for (int j = 0; j < selectionHeight + 1; j++)
            {
                playfield[selectionX + i, selectionY + j] = ' ';
            }
        }
        for (int i = 0; i < selectionWidth + 1; i++)
        {
            for (int j = 0; j < selectionHeight + 1; j++)
            {
                playfield[selectionX + i + dx, selectionY + j] = oldPlayfield[selectionX + i, selectionY + j];
            }
        }
        selectionX += dx;
    }

    private char[,] GetPlayFieldCopy()
    {
        char[,] copy = new char[playFieldWidth, playFieldHeight];
        for (int i = 0; i < playFieldWidth; i++)
        {
            for(int j = 0; j < playFieldHeight; j++)
            {
                copy[i, j] = playfield[i, j];
            }
        }

        return copy;
    }


    private void Run()
    {
        BefungeInterpreter i = new BefungeInterpreter();
        i.playfield = playfield;
        Console.Clear();
        i.Execute();
        Console.WriteLine("\nPress enter to return to editor");
        Console.ReadLine();
    }

    private void AdjustCamera()
    {
        if (cursorX < 0) cursorX = 0;
        if (cursorY < 0) cursorY = 0;
        if(cursorX < x) x = cursorX;
        if(cursorY < y) y = cursorY;
        if(cursorX > x + xPlayfieldSpace) x = cursorX - xPlayfieldSpace;
        if(cursorY > y + yPlayfieldSpace) y = cursorY - yPlayfieldSpace;
        if (y < 0) y = 0;
        if (x < 0) x = 0;
    }

    public void DrawCode()
    {
        Console.Clear();
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < consoleHeight - 3; i++)
        {
            int codeLineIndex = i + y;
            Console.Write(codeLineIndex.ToString().PadLeft(3) + " ");
            for (int j = 0; j < xPlayfieldSpace; j++)
            {
                int codeX = x + j;
                if (InSelection(codeX, codeLineIndex))
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write(playfield[codeX, codeLineIndex]);
            }
            Console.WriteLine();
        }
        
        Console.SetCursorPosition(0, consoleHeight - 2);
        if (select || movingSelection)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write(select ? "SELECTING" : "MOVING");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" ");
        }
        Console.WriteLine("camX: " + x + " camY: " + y + " cursorX: " + cursorX + " cursorY: " + cursorY);

        int consoleX;
        int consoleY;
        PlayfieldCoordToConsoleCoord(cursorX, cursorY, out consoleX, out consoleY);
        Console.CursorVisible = true;
        Console.SetCursorPosition(consoleX, consoleY);
    }

    private bool InSelection(int codeX, int codeLineIndex)
    {
        if (!select && !movingSelection) return false;
        if (codeX >= selectionX && codeX <= selectionX + selectionWidth && codeLineIndex >= selectionY && codeLineIndex <= selectionY + selectionHeight) return true;
        return false;
    }

    public void PlayfieldCoordToConsoleCoord(int playfieldX, int playfieldY, out int consoleX, out int consoleY)
    {
        int newY = playfieldY - y;
        int newX = playfieldX - x;
        newX += 4;
        consoleX = newX;
        consoleY = newY;
    }

    public void LoadCode(string code)
    {
        playfield = new char[playFieldWidth, playFieldHeight];
        List<string> lines = code.Split('\n').ToList();
        for (int i = 0; i < playFieldHeight; i++)
        {
            for (int j = 0; j < playFieldWidth; j++)
            {
                // If the position is present in the code set the playfield to that. Otherwise set it to a space
                if (i < lines.Count && j < lines[i].Length)
                {
                    playfield[j, i] = lines[i][j];
                }
                else
                {
                    playfield[j, i] = ' ';
                }
            }
        }
    }
    
    public void LoadFile(string location)
    {
        LoadCode(File.ReadAllText(location));
    }
}