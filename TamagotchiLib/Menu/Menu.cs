﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace TamagotchiLib.Menu
{
    public class Menu
    {
        private int selectedOption;
        private string[] options;
        private string prompt;

        public Menu(string prompt, string[] options)
        {
            this.prompt = prompt;
            this.options = options;
            selectedOption = 0;
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                Console.WriteLine(prompt);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"{(i == selectedOption ? ">" : " ")} {options[i]}");
                }

                keyPressed = Console.ReadKey(true).Key;
                if (keyPressed == ConsoleKey.UpArrow) selectedOption--;
                else if (keyPressed == ConsoleKey.DownArrow) selectedOption++;
                selectedOption = (selectedOption + options.Length) % options.Length;
            } while (keyPressed != ConsoleKey.Enter);

            return selectedOption;
        }
    }
    //public class Menu
    //{
    //    private int SelectedOption;
    //    private string[] Options;
    //    private string Prompt;

    //    public Menu(string prompt, string[] options)
    //    {
    //        Prompt = prompt;
    //        Options = options;
    //        SelectedOption = 0;
    //    }

    //    public void DisplayOptions()
    //    {
    //        WriteLine(Prompt);
    //        for (int i = 0; i < Options.Length; i++)
    //        {
    //            string currentOption = Options[i];
    //            string prefix;

    //            if (i == SelectedOption)
    //            {
    //                prefix = "*";
    //                ForegroundColor = ConsoleColor.Black;
    //                BackgroundColor = ConsoleColor.White;
    //            }
    //            else
    //            {
    //                prefix = " ";
    //                ForegroundColor = ConsoleColor.White;
    //                BackgroundColor = ConsoleColor.Black;
    //            }

    //            WriteLine($"{prefix} << {currentOption} >>");
    //        }
    //        ResetColor();
    //    }

    //    public int Run()
    //    {
    //        ConsoleKey keyPressed;
    //        do
    //        {
    //            Clear();
    //            DisplayOptions();

    //            ConsoleKeyInfo keyInfo = ReadKey(true);
    //            keyPressed = keyInfo.Key;

    //            if (keyPressed == ConsoleKey.UpArrow)
    //            {
    //                SelectedOption--;
    //                if (SelectedOption == -1)
    //                {
    //                    SelectedOption = Options.Length - 1;
    //                }
    //            }
    //            else if (keyPressed == ConsoleKey.DownArrow)
    //            {
    //                SelectedOption++;
    //                if (SelectedOption == Options.Length)
    //                {
    //                    SelectedOption = 0;
    //                }
    //            }

    //        } while (keyPressed != ConsoleKey.Enter);

    //        return SelectedOption;
    //    }
    //}
}