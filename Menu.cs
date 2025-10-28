using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitDiaryApp
{
    public class Menu
    {
        private MealService mealservice;

        public Menu(MealService mealService) 
        {
            this.mealservice = mealService;
        }

        public bool Run()
        {
            PrintMenu();

            int option = GetMenuOption();

            bool isProgramRunning = true;
            Console.WriteLine();
            switch (option)
            {
                case 1:
                    this.CreateMeal();
                    break;
                case 2:
                    this.RemoveMeal();
                    break;
                case 3:
                    this.GetMealsByDate();
                    break;
                case 4:
                    this.GetMealsByDateAndType();
                    break;
                case 5:
                    this.GetAllMeals();
                    break;
                case 0:
                    isProgramRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid menu option");
                    break;
            }
            Console.WriteLine();
            return isProgramRunning;
        }

        private void PrintMenu()
        {
            //Console.Clear();
            Console.WriteLine("Welcome to your diet dairy app !");
            Console.WriteLine("Options: ");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("1. Add meal");
            Console.WriteLine("2. Remove meal");
            Console.WriteLine("3. Find meals summary by day");
            Console.WriteLine("4. Find meal by type and date");
            Console.WriteLine("5. Display all meals");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("0. Exit");
        }

        private int GetMenuOption()
        {
            Console.Write("Enter value: ");
            int.TryParse(Console.ReadLine(), out int input);
            Console.WriteLine();
            return input;
        }

        private MealType SelectMealType()
        {
            Console.WriteLine("\tSelect a meal type:");

            // Display available options dynamically
            foreach (MealType type in Enum.GetValues(typeof(MealType)))
            {
                Console.WriteLine($"\t\t{(int)type}. {type}");
            }

            while (true)
            {
                Console.Write("\tEnter the number of your choice: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value) && Enum.IsDefined(typeof(MealType), value))
                {
                    return (MealType)value;
                }

                Console.WriteLine("\tInvalid choice. Please enter one of the listed numbers.");
            }
        }

        private string? GetStringValue(string inputValueName)
        {
            Console.Write($"\t{inputValueName}: ");
            return Console.ReadLine();
        }

        private int GetIdValue()
        {
            Console.Write("Enter meal ID: ");
            int input = -1;
            bool validType = false;

            while (!validType)
            {
                string? userInput = Console.ReadLine();
                validType = Int32.TryParse(userInput, out input) && input >= 1;

                if (!validType)
                {
                    Console.WriteLine("Index value should be equal or grated then 1");
                }
            }

            return input;
        }

        private double GetDoubleValue(string inputValueName)
        {
            Console.WriteLine($"\t{inputValueName}: ");
            double input = -1;

            bool validType = false;

            while (!validType)
            {
                string? userInput = Console.ReadLine();
                validType = double.TryParse(userInput, out input) && input >= 0;

                if (!validType)
                {
                    Console.WriteLine("Enter value must be greater or equals to zero");
                }
            }

            return input;
        }

        private DateOnly GetDateTime()
        {
            DateTime dateValue = DateTime.MinValue;

            bool validType = false;
            while (!validType)
            {
                Console.Write("Enter date in format dd-mm-yyyy: ");
                string? dateInput = Console.ReadLine();
                validType = DateTime.TryParse(dateInput, out dateValue);
            }

            return DateOnly.FromDateTime(dateValue);
        }

        private void DisplayMeals(List<Meal> meals)
        {
            foreach (Meal meal in meals)
            {
                Console.WriteLine($"Id: {meal.Id}\n{meal.ToString()}");
            }
        }

        public void CreateMeal()
        {
            int id;
            string name;
            string description;
            MealType mealType;
            double proteins;
            double carbs;
            double fat;

            Console.WriteLine("Enter information about meal you want to save in FitDairyApp!");

            id = this.GetIdValue();
            name = this.GetStringValue("NAME");
            description = this.GetStringValue("DESCRIPTION");
            mealType = this.SelectMealType();
            proteins = this.GetDoubleValue("PROTEINS");
            carbs = this.GetDoubleValue("CARBS");
            fat = this.GetDoubleValue("FAT");

            Meal newMeal = new Meal(id, name, mealType, description, proteins, carbs, fat, DateOnly.FromDateTime(DateTime.Now));

            this.mealservice.AddNewMeal(newMeal);

            Console.WriteLine($"The meal was saved:\n{newMeal.ToString()}");
        }

        public void RemoveMeal()
        {
            Console.WriteLine("Enter meal id you want to remove from FitDairyApp!");

            int id = this.GetIdValue();

            Meal? meal = this.mealservice.GetMealById(id);

            if (meal != null)
            {
                if (this.mealservice.RemoveMeal(meal))
                {
                    Console.WriteLine($"Meal with id {id} was removed");
                }
                else
                {
                    Console.WriteLine($"Meal with id {id} couldn't be removed");
                }
            }
            else
            {
                Console.WriteLine($"There is no meal with id {id}");
            }
        }

        public void GetMealsByDate()
        {
            Console.WriteLine("Enter date to find all meals saved in given day!");

            DateOnly date = this.GetDateTime();

            List<Meal> mealsByDate = this.mealservice.GetMealsPerDate(date);

            if (mealsByDate.Count != 0)
            {
                Console.WriteLine($"Meals from day: {date}");
                this.DisplayMeals(mealsByDate);
            }
            else
            {
                Console.WriteLine($"There is no saved meals from {date}");
            }
        }

        public void GetMealsByDateAndType()
        {
            Console.WriteLine("Enter date and meal type to find meals saved in given day!");

            DateOnly date = this.GetDateTime();
            MealType mealType = this.SelectMealType();

            List<Meal> mealsByDate = this.mealservice.GetMealsPerDateAndMealType(date, mealType);

            if (mealsByDate.Count != 0)
            {
                Console.WriteLine($"Meals from day: {date} with type: {mealType}");
                this.DisplayMeals(mealsByDate);
            }
            else
            {
                Console.WriteLine($"There is no saved meals from {date}");
            }
        }

        public void GetAllMeals()
        {
            Console.WriteLine("Display all meals saved in app: ");

            List<Meal> mealsByDate = this.mealservice.GetAllMeals();

            if (mealsByDate.Count != 0)
            {
                this.DisplayMeals(mealsByDate);
            }
            else
            {
                Console.WriteLine($"There is no saved meals");
            }
        }

    }
}
