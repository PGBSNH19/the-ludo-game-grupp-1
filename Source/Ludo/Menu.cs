using System;
using System.Collections.Generic;
using System.Text;
using EngineClasses;

namespace Ludo
{
    public class Menu
    {
        private readonly string[] MainMenu = { "new game", "load game", "exit" };
        private readonly string[] EnterPlayersMenu = { "2", "3", "4" };

        public string ShowMainMenu()
        {
            return ShowMenu("Ludo Game", MainMenu);
        }

        public string ShowNewGameMenu()
        {
            return ShowMenu("PlayerSelect", EnterPlayersMenu);
        }

        private string ShowMenu(string prompt, string[] options)
        {
            Console.Clear();
            Console.WriteLine(prompt);

            int selected = 0;

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                // If this is not the first iteration, move the cursor to the first line of the menu.
                if (key != null)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = Console.CursorTop - options.Length;
                }

                // Print all the options, highlighting the selected one.
                for (int i = 0; i < options.Length; i++)
                {
                    var option = options[i];
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("- " + option);
                    Console.ResetColor();
                }

                // Read another key and adjust the selected value before looping to repeat all of this.
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Length - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }
            }

            Console.CursorVisible = true;
            return options[selected];
        }
    }
}
