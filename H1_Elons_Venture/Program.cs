using System.Drawing;
using System.Runtime.InteropServices;

namespace H1_Elons_Venture
{
    internal class Program
    {
        Car redCar = new Car();
        Car yellowCar = new Car();
        Car greenCar = new Car();

        Battery redBattery = new Battery();
        Battery yellowBattery = new Battery();
        Battery greenBattery = new Battery();

        Display Display = new Display();

        // These 2 variables get their value based on which car the user chooses
        Car userCar;
        Battery userBattery;

        #region View
        /// <summary>
        /// This view method is designed for normal outputs to the console.
        /// When it gets called, it gets a value in its parameter, which is the message that will be displayed in the console window.
        /// </summary>
        /// <param name="message"></param>
        void Message(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// This view method is designed to act like our car display.
        /// It outputs the current car's current battery capacity and the driven distance
        /// </summary>
        void ShowDisplay()
        {
            Console.WriteLine($"Total charge is {userBattery.Capacity}&");
            Console.WriteLine($"Total distance driven is {userCar.DistanceDriven}m");
        }

        #endregion

        #region Controller
        /// <summary>
        /// This Controller method provides values to their properties, in the car class
        /// </summary>
        void CreateCars()
        {
            redCar.Color = Color.Red;
            redCar.DistanceDriven = 0f;

            yellowCar.Color = Color.Yellow;
            yellowCar.DistanceDriven = 0f;

            greenCar.Color = Color.Green;
            greenCar.DistanceDriven = 0f;

            CreateBatteries();
        }

        /// <summary>
        /// This controller method provides values for the batteries.
        /// Each battery has the same specifications and will go down by 0.05% every meter.
        /// 0.05% every meter results to 1% every 20 meter, like the assignment specifies.
        /// </summary>
        void CreateBatteries()
        {
            redBattery.Capacity = 100f;
            redBattery.BatteryDrain = 0.05f;

            yellowBattery.Capacity = 100f;
            yellowBattery.BatteryDrain = 0.05f;

            greenBattery.Capacity = 100f;
            greenBattery.BatteryDrain = 0.05f;

            ChooseCar();
        }

        /// <summary>
        /// This is the controller method, in which the user decides which car they wanna use.
        /// </summary>
        void ChooseCar()
        {
            // Creates an infinite loop
            while (true)
            {
                // Outputs a message to the console
                Message("Choose a car, write 'red', 'yellow' or 'green'");

                // Gets the user input and forces it to lowercase 
                string input = Console.ReadLine().ToLower();

                // Checks which color the user wrote, and picks the appropriate car
                // If the user input was incorrect, the loop repeats
                if (input == "red")
                {
                    userCar = redCar;
                    userBattery = redBattery;
                    break;
                }
                else if (input == "yellow")
                {
                    userCar = yellowCar;
                    userBattery = yellowBattery;
                    break;
                }
                else if (input == "green")
                {
                    userCar = greenCar;
                    userBattery = greenBattery;
                    break;
                }
            }

            CreateDisplay();
        }

        /// <summary>
        /// This method creates our display, by giving the values of the user picked car and the appropriate battery.
        /// </summary>
        void CreateDisplay()
        {
            Display.DistanceDriven = userCar.DistanceDriven;
            Display.Capacity = userBattery.Capacity;

            DriveCar();
        }

        /// <summary>
        /// This method is where the user decides how far to drive their car.
        /// Once they have driven the car, they can take a break from RC cars, which quits the program.
        /// They can also drive the same car again, put the car to charge or change their car without charging.
        /// </summary>
        void DriveCar()
        {
            // Infinite loop, incase the user wants to use the same car again
            while(true)
            {
                // Outputs a message to the console, with this view method
                Message("How many meters do you wanna drive?");

                // Declares the input variable, in an earlier scope than its assigned a value, 
                float input;

                // Infinite loop, incase of user input errors
                while (true)
                {
                    Message("example: 43,2");

                    // Exception handling, incase of incorrect user input
                    try
                    {
                        // Assigns the user input to a string, because it will check if the input contains a dot instead of a comma.
                        // Example: Without this, 50.5 would become 505.
                        // This code prevents the dot, so the user is forced to write 50,5.
                        string inputStr = Console.ReadLine();

                        // Checks if the string contains a dot, if it does a message gets output and the loop repeats
                        if(inputStr.Contains("."))
                        {
                            Message("Use comma instead of full stops");
                            continue;
                        }

                        // Parses the string to the float "input", which was declare in an earlier scope
                        input = float.Parse(inputStr);

                        // Checks if the user can drive the specified distance, and outputs an error if its over the battery limit. Repeats loop.
                        if (userBattery.Capacity - input * userBattery.BatteryDrain < 0)
                        {
                            Message("You cant drive that much, with your charge");
                            continue;
                        }

                        // Breaks out of the loop because the input is valid
                        break;
                    }
                    catch
                    {
                        // If the user input string cannot be parsed to float, then this error gets outputted to the console
                        Message("Incorrect format, try again");
                    }
                }

                // This updates the user chosen car's driven distance, as well as battery capacity.
                userCar.DistanceDriven += input;
                userBattery.Capacity -= input * userBattery.BatteryDrain;

                // Outputs the driven distance and battery capacity, to the console
                ShowDisplay();

                // Asks the user if they wanna charge their car, and then calls method called "YesOrNo", which lets them decide
                Message("Do you wanna charge your car?");
                if (YesOrNo())
                {
                    // If the user is says yes to charging their car, then the DistanceDriven and capacity gets reset
                    userCar.DistanceDriven = 0;
                    userBattery.Capacity = 100;

                    // Asks the user if they wanna stop playing with RC cars, if yes then the application stops.
                    Message("Do you wanna take a break playing with RC cars?");
                    if (YesOrNo())
                    {
                        Environment.Exit(0);
                    }

                    // If the user wants to keep playing with RC, then they get to pick their next car, because their current one is charging
                    ChooseCar();
                    break;
                }
                else // If the user does not want to charge their car
                {
                    // Asks the user if they wanna change their car and then they get to answer in the "yesOrNo" method
                    Message("Do you wanna choose a different car?");
                    if (YesOrNo())
                    {
                        // If yes, then they choose a different car, else the loop repeats with their current car
                        ChooseCar();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// This method allows the user to answer yes or no, to multiple questions in the DriveCar method
        /// </summary>
        /// <returns></returns>
        bool YesOrNo()
        {
            // Declares a string variable for the user input
            string input = "";

            // Creates an infinite loop, which runs until the user writes either "y" or "n"
            // Once the user writes either "y" or "n", then it returns the boolean value, to the if statement in which the method is called from
            while (true)
            {
                Message("Write y/n");

                input = Console.ReadLine().ToLower();

                if (input == "y")
                {
                    return true;
                }
                else if (input == "n")
                {
                    return false;
                }
            }
        }

        #endregion

        /// <summary>
        /// This method is the entry point for the program. 
        /// It creates a new instance of Program, which it uses to call CreateCars, which is a non-static method.
        /// </summary>
        static void Main()
        {
            Program program = new Program();
            program.CreateCars();
        }
    }
}