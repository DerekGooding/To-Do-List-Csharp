namespace ToDoList
{
    class ToDoList
    {
        static void Main(string[] args)
        {

            bool running = true;                                            // jump into the app immediately
            int action;                                                     // declare to avoid loop condition errors, don't assign yet
            List<string> ToDoList = new List<string>();                    // very important to use lists because you can add/remove elements

            LoadingDots("Loading application");

            Console.WriteLine("================ Welcome To TO-DO List application ================");
            Console.WriteLine();                                                                              // welcome screen outside of loop, so it only shows at the start
            Console.WriteLine();

            while (running)
            {

                if (ToDoList.Count == 0)
                {
                    Console.Write("Write your first task to get started: ");         // need to have a first task, logical because then actions 1 and 3 don't make any sense.
                    String firstAddedTask = Console.ReadLine();                      // think of it as a startup screen


                    // prevent no input :
                    while (firstAddedTask == "") // pressing enter without typing anything gives empty string, not null!
                    {
                        Console.Write("Please type something: ");
                        firstAddedTask = Console.ReadLine();
                    }

                    // added a confirmation message because it felt weird to add a task without confirmation on user side
                    ToDoList.Add(firstAddedTask);
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
                    case 1:  // SHOW ALL TASKS

                        LoadingDots("Fetching tasks");

                        Console.WriteLine();
                        Console.WriteLine("========== Your TO-DO list: ==========");
                        Console.WriteLine();

                        if (ToDoList.Count == 0)
                        {
                            Console.WriteLine("TO-DO list is empty! Try adding some tasks by entering '2'");
                        }
                        else
                        {
                            for (int i = 0; i < ToDoList.Count; i++)                 // be very careful to not add an equality (<=) in the condition because that creates an out of range index (last index is 1 smaller than the count)
                            {
                                Console.WriteLine($"{(i + 1)}. {ToDoList[i]}");      // the displayed index should be program index + 1, because C# starts indexing from 0!
                            }
                            Console.WriteLine();
                            Console.WriteLine();
                        }

                        break;


                    case 2:  // ADD A TASK

                        Console.Write("Write the task you want to add: ");
                        String AddedTask = Console.ReadLine();
                        while (AddedTask == "")                                      // prevent empty task
                        {
                            Console.Write("Please write something: ");
                            AddedTask = Console.ReadLine();
                        }

                        ToDoList.Add(AddedTask);
                        LoadingDots("Adding task");
                        Console.WriteLine($"Task successfully added!: {ToDoList.Count}. {AddedTask}");
                        Console.WriteLine();                                                                                // empty lines to prevent visual clunking
                        break;

                    case 3:  // REMOVE A TASK

                        Console.Write("Which task do you want to remove? (Write its number): ");

                        String input = Console.ReadLine();
                        int displayedIndex;                 // the user inputs the index they see on screen, so 1 bigger than the program index
                                                            // declare but don't assign. We need it as a placeholder if the input is valid (see down)

                        while (!int.TryParse(input, out displayedIndex) || displayedIndex < 1 || displayedIndex > ToDoList.Count)                 // if input is not an integer -> asks you to input again (the ! makes the false true, and initializes the loop) 
                        {                                                                                                                           // if input is integer -> outputs it under the name displayedIndex
                            Console.Write("Input is invalid or an element with that number does not exist (Enter a NUMBER within list range!): ");  // if displayed index < 1 or outside list range then asks to input again
                            input = Console.ReadLine();
                        }

                        int programIndex = displayedIndex - 1;                                                                                      // program index calculated from the user inputted index, hence displayedIndex < 1
                                                                                                                                                    // means program index < 0, which is impossible


                        Console.Write($"Are you sure you want to remove task #{displayedIndex}? (Y/N): ");                                          // confirm before deleting
                        String confirmRemove = Console.ReadLine().ToUpper();                                                                        // with .ToUpper() even if you input "y" or "n" you still get "Y" or "Y"
                                                                                                                                                    // (case sensitive conditions)
                        while (confirmRemove != "Y" && confirmRemove != "N")
                        {
                            Console.Write("Please enter a valid answer (Y/N): ");
                            confirmRemove = Console.ReadLine().ToUpper();
                        }

                        if (confirmRemove == "Y")
                        {
                            // write remove statement before removing to avoid errors
                            LoadingDots("Removing task");
                            Console.WriteLine($"Removed task #{displayedIndex}. {ToDoList[programIndex]}");   // we use displayedIndex to show the user and programIndex when dealing with list index
                            ToDoList.RemoveAt(programIndex);
                        }
                        else                                               // since we ruled out any other characters, this else block only applies when we have "N"
                        {
                            Console.WriteLine("Task removal cancelled");
                        }

                        break;

                    case 4:  // QUIT

                        LoadingDots("Shutting down application");
                        running = false;                                   // just make it false to break out of the big while loop on line 21
                        break;

                        // no need for default case, because we ruled out any other inputs except 1 2 3 4
                }

            }

            Console.WriteLine("Thank you for using TO-DO list app!");


            Console.ReadKey();
        }


        // method to simulate "loading"
        static void LoadingDots(string message = "Loading", int dots = 5, int delay = 150)  // the assigned values serve as default values
        {
            Console.Write(message);

            for (int i = 0; i < dots; i++)
            {
                Thread.Sleep(delay);
                Console.Write(".");
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}