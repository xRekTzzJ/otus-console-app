namespace otus_console_app;

public static class Command
{
    public const string Start = "/start";
    public const string Help = "/help";
    public const string Info = "/info";
    public const string Exit = "/exit";
    public const string Echo = "/echo";
    public const string Clear = "/clear";
}

internal static class Program
{
    public static void Main()
    {
        var name = "";
        PrintWelcomeMessage(name);
        Handler(ref name);
    }

    private static void PrintWelcomeMessage(string? name)
    {
        var isNameSet = !string.IsNullOrEmpty(name);
        Console.WriteLine($"\n{(isNameSet ? name : "Привет")}, доступные команды:" +
                          $"\n{Command.Start} - начать использование приложения." +
                          $"\n{Command.Help} - краткая справочная информация." +
                          $"\n{Command.Info} - информация о приложении." +
                          $"\n{Command.Exit} - завершить работу." +
                          $"{(isNameSet ? $"\n{Command.Echo} <текст> - повторяет введённый текст." : "")}" +
                          $"\n{Command.Clear} - очистить консоль.");
    }

    private static string InputHandler()
    {
        return Console.ReadLine()?.Trim() ?? "";
    }

    private static string HandleStart()
    {
        Console.WriteLine("Введите своё имя:");
        return InputHandler();
    }

    private static void HandleHelp()
    {
        Console.WriteLine(
            $"Справка:\n1. {Command.Start} - введите имя.\n" +
            $"2. {Command.Help} - показать эту справку.\n" +
            $"3. {Command.Info} - версия программы и дата.\n" +
            $"4. {Command.Echo} <текст> - повторяет текст (доступно после /start).\n" +
            $"5. {Command.Clear} - очистка консоли.\n" +
            $"6. {Command.Exit} - выход из программы.");
    }

    private static void HandleInfo()
    {
        Console.WriteLine($"Версия программы: 0.0.1.\nДата создания программы {new DateTime(2025, 8, 17, 17, 32, 0)}");
    }

    private static void HandleClear()
    {
        Console.Clear();
    }

    private static void HandleEcho(string name, string input)
    {
        var parts = input.Split(' ', 2);
        if (parts.Length < 2)
            Console.WriteLine($"{name}, вы не указали текст для /echo.");
        else
            Console.WriteLine($"{name}: {parts[1]}");
    }

    private static void Handler(ref string? name)
    {
        while (true)
        {
            var input = InputHandler();

            switch (input.Split(' ')[0].ToLower())
            {
                case Command.Start:
                    name = HandleStart();
                    break;
                case Command.Help:
                    HandleHelp();
                    break;
                case Command.Info:
                    HandleInfo();
                    break;
                case Command.Clear:
                    HandleClear();
                    break;
                case Command.Exit:
                    return;
                case var cmd when cmd == Command.Echo && !string.IsNullOrEmpty(name):
                    HandleEcho(name, input);
                    break;
                case var cmd when cmd == Command.Echo && string.IsNullOrEmpty(name):
                    Console.WriteLine("Сначала введите имя командой /start");
                    break;
                default:
                    Console.WriteLine("Некорректная команда. Попробуйте снова.");
                    break;
            }
        }
    }
}