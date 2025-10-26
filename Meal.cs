using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitDiaryApp
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MealType MealType { get; set; }
        public string Description { get; set; }
        public double Protein { get; set; } // in grmas
        public double Carbs { get; set; } // in grams
        public double Fat { get; set; } // in grams
        public double Calories { get; set; } // kcal
        public DateOnly MealDate { get; set; }

        public const double PROTEIN_TO_CALORIES = 4;
        public const double CARBS_TO_CALORIES = 4;
        public const double FATS_TO_CALORIES = 9;

        public Meal(int id, string name, MealType mealType, string description, double protein, double carbs, double fats, DateOnly mealDate)
        {
            this.Id = id;
            this.Name = name;
            this.MealType = mealType;
            this.Description = description;
            this.Protein = protein;
            this.Carbs = carbs;
            this.Fat = fats;
            this.MealDate = mealDate;

            this.calculateMealCalories();
        }

        public void calculateMealCalories()
        {
            this.Calories = this.calculateCaloriesFromCarbs() + this.calculateCaloriesFromFats() + calculateCaloriesFromProtein();
        }

        private  double calculateCaloriesFromProtein()
        {
            return PROTEIN_TO_CALORIES * this.Protein;
        }

        private double calculateCaloriesFromCarbs()
        {
            return CARBS_TO_CALORIES * this.Carbs;
        }

        private double calculateCaloriesFromFats()
        {
            return FATS_TO_CALORIES * this.Fat;
        }

        public override string? ToString()
        {
            return $"Name:          {this.Name}\n" +
                   $"Meal:          {this.MealType}\n" +
                   $"Date:          {this.MealDate.ToString()}\n" +        
                   $"Description:   {this.Description}\n" +
                   $"Carbs:         {this.calculateCaloriesFromCarbs()} kCal\n" +
                   $"Proteins:      {this.calculateCaloriesFromProtein()} kCal\n" +
                   $"Fat:           {this.calculateCaloriesFromFats()} kCal\n";
        }
    }
}
