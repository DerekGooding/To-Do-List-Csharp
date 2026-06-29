namespace TO_DO_list;

public static class ToDoList
{
    private static readonly List<string?> _toDoList = [];                    // very important to use lists because you can add/remove elements
    private static void Main()
    {
        var running = true;                                            // jump into the app immediately
        int action;                                                     // declare to avoid loop condition errors, don't assign yet
        SimulateLoadingDots("Loading application");

        Console.WriteLine("================ Welcome To TO-DO List application ================");
        Console.WriteLine();                                                                              // welcome screen outside of loop, so it only shows at the start
        Console.WriteLine();

        while (running)
        {
            if (_toDoList.Count == 0)
            {
                Console.Write("Write your first task to get started: ");         // need to have a first task, logical because then actions 1 and 3 don't make any sense.
                var firstAddedTask = Console.ReadLine();                      // think of it as a startup screen

                // prevent no input :
                while (firstAddedTask?.Length == 0) // pressing enter without typing anything gives empty string, not null!
                {
                    Console.Write("Please type something: ");
                    firstAddedTask = Console.ReadLine();
                }

                // added a confirmation message because it felt weird to add a task without confirmation on user side
                _toDoList.Add(firstAddedTask);
                Console.WriteLine($"First task successfully added!: 1. {firstAddedTask}");
                Console.WriteLine();
            }

            Console.WriteLine("Choose an action:");
            Console.WriteLine();                                    // empty line to make it more aesthetically pleasing
            Console.WriteLine("1. View Tasks");
            Console.WriteLine("2. Add Task");
            Console.WriteLine("3. Remove Task");
            Console.WriteLine("4. Quit Application");
            Console.Write("(Write 1, 2, 3 or 4): ");                // use Console.Write() here to enter the input in the same line (looks cleaner)

            // ensure no other input other than 1 2 3 4 can be entered
            while (!int.TryParse(Console.ReadLine(), out action) || action < 1 || action > 4)             // if input is not an integer -> asks you to input again        (the ! before the false statement makes it true)
            {                                                                                             // if it is integer but it's <1 or >4 -> asks you to write again
                Console.Write("Please enter a valid input (1, 2, 3, 4): ");
            }

            Console.WriteLine();

            switch (action)
            {
                case 1:
                    HandleShowAll();
                    break;
                case 2:
                    HandleAddTask();
                    break;

                case 3:
                    HandleRemoveTask();
                    break;
                default:
                    SimulateLoadingDots("Shutting down application");
                    running = false;
                    break;
            }
        }

        Console.WriteLine("Thank you for using TO-DO list app!");

        Console.ReadKey();
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
        int displayedIndex;                 // the user inputs the index they see on screen, so 1 bigger than the program index
                                            // declare but don't assign. We need it as a placeholder if the input is valid (see down)

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


    private static void SimulateLoadingDots(string message = "Loading", int dots = 5, int delay = 150)  // the assigned values serve as default values
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