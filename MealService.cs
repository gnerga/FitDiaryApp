using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FitDiaryApp
{
    public class MealService
    {
        public List<Meal> meals;

        public MealService()
        {
            meals = new List<Meal>();
        }

        public void AddNewMeal(Meal newMeal)
        {
            this.meals.Add(newMeal);
        }

        public bool RemoveMeal(Meal mealToRemove)
        {
            return meals.Remove(mealToRemove);
        }

        public Meal? GetMealById(int id)
        {
            foreach (Meal meal in meals)
            {
                if (meal.Id == id)
                {
                    return meal;
                }
            }

            return null;
        }

        public List<Meal> GetAllMeals()
        {
            return new List<Meal>(meals);
        }

        public List<Meal> GetMealsPerDate(DateOnly date)
        {
            List<Meal> mealsByDate = new List<Meal>();

            foreach (Meal meal in meals)
            {
                if (meal.MealDate.Equals(date))
                {
                    mealsByDate.Add(meal);
                }
            }

            return mealsByDate;
        }

        public List<Meal> GetMealsPerDateAndMealType(DateOnly date, MealType mealType)
        {
            List<Meal> mealsByDateAndType = new List<Meal>();

            foreach (Meal meal in meals)
            {
                if (meal.MealDate.Equals(date) && meal.MealType == mealType)
                {
                    mealsByDateAndType.Add(meal);
                }
            }

            return mealsByDateAndType;
        }
    }
}
