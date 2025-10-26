namespace FitDiaryApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu mainMenu = new Menu(new MealService());

            bool appIsRunning = true;

            while (appIsRunning)
            {
                appIsRunning = mainMenu.Run();
            }
        }
    }
}