using MemoryTrainer.Models;
using MemoryTrainer.ViewModels;

namespace MemoryTrainer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var gameModel = new GameModel(4, 4);
            var gameViewModel = new GameViewModel(gameModel);

            Application.Run(new Form1());
        }
    }
}