using System;

namespace Tools
{
    public class UserInterface
    {
        public static void Output(string _Text, ConsoleColor _Color)
        {
            Console.ResetColor();
            Console.BackgroundColor = _Color;
            Console.WriteLine(_Text);
        }

        public static void Output(string _Text)
        {
            Console.WriteLine(_Text);
        }

        public static string Input()
        {
            return Console.ReadLine();
        }

        public static string Input(string _Text)
        {
            Output(_Text);
            return Console.ReadLine();
        }

        public static string Input(string _Text, ConsoleColor _Color)
        {
            Output(_Text, _Color);
            return Console.ReadLine();
        }

        public static void Key()
        {
            Output("Нажмите любую клавишу...", ConsoleColor.Green);
            Console.ReadKey();
            Console.ResetColor();
        }
        public static void Key(ConsoleKey _Key)
        { 
            Output($"Нажмите клавишу {_Key.ToString()}...", ConsoleColor.Green);
            ConsoleKeyInfo _keyInfo = new ConsoleKeyInfo();
            while(_keyInfo.Key != _Key)
            {
                _keyInfo = Console.ReadKey();
            }
            Console.ResetColor();
        }

        public static void Clear()
        {
            Console.Clear();
        }
    }
}
