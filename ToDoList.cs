namespace TO_DO_list;

public static class ToDoList
{
    private static readonly List<string?> _toDoList = [];

    private static void Main()
    {
        var isExiting = true;
        StartupMessage();

        while (isExiting)
        {
            switch (GetAction())
            {
                case OptionType.ShowAll:
                    HandleShowAll();
                    break;
                case OptionType.Add:
                    HandleAddTask();
                    break;

                case OptionType.Remove:
                    HandleRemoveTask();
                    break;
                default:
                    SimulateLoadingDots("Shutting down application");
                    isExiting = false;
                    break;
            }
        }

        Console.WriteLine("Thank you for using TO-DO list app!");
        Console.ReadKey();
    }

    private static void StartupMessage()
    {
        SimulateLoadingDots("Loading application");

        Console.WriteLine("================ Welcome To TO-DO List application ================");
        Console.WriteLine();
        Console.WriteLine();

        Console.Write("Write your first task to get started: ");
        var firstAddedTask = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(firstAddedTask))
        {
            Console.Write("Please type something: ");
            firstAddedTask = Console.ReadLine();
        }

        _toDoList.Add(firstAddedTask);
        Console.WriteLine($"First task successfully added!: 1. {firstAddedTask}");
        Console.WriteLine();
    }

    private static OptionType GetAction()
    {
        Console.WriteLine("Choose an action:");
        Console.WriteLine();
        Console.WriteLine("1. View Tasks");
        Console.WriteLine("2. Add Task");
        Console.WriteLine("3. Remove Task");
        Console.WriteLine("4. Quit Application");
        Console.Write("(Write 1, 2, 3 or 4): ");

        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out var index) && Enum.IsDefined(typeof(OptionType), index))
            {
                Console.WriteLine();
                return (OptionType)index;
            }
            Console.Write("Please enter a valid input (1, 2, 3, 4): ");
        }
    }

    private static void HandleShowAll()
    {
        SimulateLoadingDots("Fetching tasks");

        Console.WriteLine();
        Console.WriteLine("========== Your TO-DO list: ==========");
        Console.WriteLine();

        if (_toDoList.Count == 0)
        {
            Console.WriteLine("TO-DO list is empty! Try adding some tasks by entering '2'");
        }
        else
        {
            for (var i = 0; i < _toDoList.Count; i++)                 // be very careful to not add an equality (<=) in the condition because that creates an out of range index (last index is 1 smaller than the count)
            {
                Console.WriteLine($"{i + 1}. {_toDoList[i]}");      // the displayed index should be program index + 1, because C# starts indexing from 0!
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    private static void HandleAddTask()
    {
        Console.Write("Write the task you want to add: ");
        var AddedTask = Console.ReadLine();
        while (AddedTask?.Length == 0)                                      // prevent empty task
        {
            Console.Write("Please write something: ");
            AddedTask = Console.ReadLine();
        }

        _toDoList.Add(AddedTask);
        SimulateLoadingDots("Adding task");
        Console.WriteLine($"Task successfully added!: {_toDoList.Count}. {AddedTask}");
        Console.WriteLine();
    }

    private static void HandleRemoveTask()
    {
        Console.Write("Which task do you want to remove? (Write its number): ");

        var input = Console.ReadLine();
        int displayedIndex;

        while (!int.TryParse(input, out displayedIndex) || displayedIndex < 1 || displayedIndex > _toDoList.Count)                 // if input is not an integer -> asks you to input again (the ! makes the false true, and initializes the loop)
        {                                                                                                                           // if input is integer -> outputs it under the name displayedIndex
            Console.Write("Input is invalid or an element with that number does not exist (Enter a NUMBER within list range!): ");  // if displayed index < 1 or outside list range then asks to input again
            input = Console.ReadLine();
        }

        var programIndex = displayedIndex - 1;                                                                                      // program index calculated from the user inputted index, hence displayedIndex < 1
                                                                                                                                    // means program index < 0, which is impossible

        Console.Write($"Are you sure you want to remove task #{displayedIndex}? (Y/N): ");                                          // confirm before deleting
        var confirmRemove = Console.ReadLine()?.ToUpper();                                                                        // with .ToUpper() even if you input "y" or "n" you still get "Y" or "Y"
                                                                                                                                  // (case sensitive conditions)
        while (confirmRemove is not "Y" and not "N")
        {
            Console.Write("Please enter a valid answer (Y/N): ");
            confirmRemove = Console.ReadLine()?.ToUpper();
        }

        if (confirmRemove == "Y")
        {
            // write remove statement before removing to avoid errors
            SimulateLoadingDots("Removing task");
            Console.WriteLine($"Removed task #{displayedIndex}. {_toDoList[programIndex]}");   // we use displayedIndex to show the user and programIndex when dealing with list index
            _toDoList.RemoveAt(programIndex);
        }
        else                                               // since we ruled out any other characters, this else block only applies when we have "N"
        {
            Console.WriteLine("Task removal cancelled");
        }

    }


    private static void SimulateLoadingDots(string message = "Loading", int dots = 5, int delay = 150)
    {
        Console.Write(message);

        for (var i = 0; i < dots; i++)
        {
            Thread.Sleep(delay);
            Console.Write(".");
        }

        Console.WriteLine();
        Console.WriteLine();
    }
}