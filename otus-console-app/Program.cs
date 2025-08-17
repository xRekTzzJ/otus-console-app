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
        StartApp();
        return;

        void StartApp(string? name = "")
        {
            var isNameNotNull = !string.IsNullOrEmpty(name);

            Console.WriteLine($"\n{(isNameNotNull ? name : "Привет")}, Доступные комманды: " +
                              $"\n{Command.Start} - начать использование приложения." +
                              $"\n{Command.Help} - краткая справочная информация о том, как пользоваться программой." +
                              $"\n{Command.Info} - информация о приложении." +
                              $"\n{Command.Exit} - завершить работу приложения." +
                              $"{(isNameNotNull ? $"\n{Command.Echo} - вернуть введенный текст." : "")}");

            Handler(name);
        }

        string InputHandler(bool needTransformToLowerCase = false)
        {
            var input = Console.ReadLine()?.Trim();

            if (needTransformToLowerCase) input = input?.ToLower();

            return input ?? "";
        }

        string HandleStart()
        {
            Console.WriteLine("Введите свое имя");
            return InputHandler();
        }

        void HandleHelp()
        {
            Console.WriteLine(
                $"Справочная информация:\n\n1. Введите команду {Command.Start}, чтобы начать работу и указать своё имя.\n   После этого программа будет обращаться к вам по имени.\n2. Команда {Command.Help} выводит эту справку.\n3. Команда {Command.Info} показывает версию программы и дату её создания.\n4. После {Command.Start} становится доступна команда /echo <текст>,\n   которая повторяет введённый вами текст.\n5. Чтобы очистить консоль введите {Command.Clear}. \n6. Чтобы выйти из программы, введите {Command.Exit}.\n");
        }

        void HandleInfo()
        {
            Console.WriteLine(
                $"Версия программы: 0.0.1.\nДата создания программы {new DateTime(2025, 8, 17, 17, 32, 0)}");
        }

        void HandleClear()
        {
            Console.Clear();
        }

        void HandleEcho(string? name, string? input)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Сначала введите имя командой /start");
                return;
            }

            var parts = input?.Split(' ', 2);
            Console.WriteLine(parts?.Length > 1 ? $"{name}: {parts[1]}" : $"{name}: (пусто)");
        }


        void Handler(string? name = "")
        {
            while (true)
            {
                var input = InputHandler(true);

                switch (input)
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
                    case Command.Exit:
                        return;
                    case Command.Clear:
                        HandleClear();
                        break;
                    case var _ when input.StartsWith(Command.Echo):
                        HandleEcho(name, input);
                        break;
                    default:
                        Console.WriteLine("Некорректный запрос. Попробуйте снова");
                        break;
                }

                StartApp(name);
            }
        }
    }
}