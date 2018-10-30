using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FifaAutoClicker
{
    public class Controller
    {
        #region Properties

        public MouseActions MouseActions { get; }

        public int CurrentMinPrice { get; set; }

        #endregion Properties

        #region Constructors

        public Controller()
        {
            MouseActions = new MouseActions();
            CurrentMinPrice = 200;
        }

        #endregion Constructors

        #region Public Methods

        public void DoButtonCalibration()
        {
            Console.WriteLine("Place your mouse pointer where the Search button is. Then press enter.");
            Console.ReadLine();
            MouseActions.SearchCoordinates = System.Windows.Forms.Cursor.Position;
            Console.WriteLine($"Your Search Coordinates are: X = {MouseActions.SearchCoordinates.X} | Y = {MouseActions.SearchCoordinates.Y}");

            Console.WriteLine("\nPlace your mouse pointer where the increase min price button is. Then press enter.");
            Console.ReadLine();
            MouseActions.IncreaseMinCoords = System.Windows.Forms.Cursor.Position;
            Console.WriteLine($"Your Increase Minimum Coordinates are: X = {MouseActions.IncreaseMinCoords.X} | Y = {MouseActions.IncreaseMinCoords.Y}");

            Console.WriteLine("\nPlace your mouse pointer where the decrease min price button is. Then press enter.");
            Console.ReadLine();
            MouseActions.DecreaseMinCoords = System.Windows.Forms.Cursor.Position;
            Console.WriteLine($"Your Decrease Minimum Coordinates are: X = {MouseActions.DecreaseMinCoords.X} | Y = {MouseActions.DecreaseMinCoords.Y}");

            Console.WriteLine("\nPlace your mouse pointer where the back button is. Then press enter.");
            Console.ReadLine();
            MouseActions.BackCoordinates = System.Windows.Forms.Cursor.Position;
            Console.WriteLine($"Your Back Button Coordinates are: X = {MouseActions.BackCoordinates.X} | Y = {MouseActions.BackCoordinates.Y}");

            Console.WriteLine("\nPlace your mouse pointer where the buy now button is. Then press enter.");
            Console.ReadLine();
            MouseActions.BuyCoordinates = System.Windows.Forms.Cursor.Position;
            Console.WriteLine($"Your Buy Coordinates are: X = {MouseActions.BuyCoordinates.X} | Y = {MouseActions.BuyCoordinates.Y}");

            Console.WriteLine("\nPlace your mouse pointer where the confirm purchase button is. Then press enter.");
            Console.ReadLine();
            MouseActions.ConfirmCoordinates = System.Windows.Forms.Cursor.Position;
            Console.WriteLine($"Your Confirm Coordinates are: X = {MouseActions.ConfirmCoordinates.X} | Y = {MouseActions.ConfirmCoordinates.Y}\n\nPlace these in the app.config unless you want to keep doing this shit.");
        }

        public void LoadCalibrationSettings()
        {
            try
            {
                var buyX = Convert.ToInt32(ConfigurationManager.AppSettings["Buy-X"]);
                var buyY = Convert.ToInt32(ConfigurationManager.AppSettings["Buy-Y"]);
                var confirmX = Convert.ToInt32(ConfigurationManager.AppSettings["Confirm-X"]);
                var confirmY = Convert.ToInt32(ConfigurationManager.AppSettings["Confirm-Y"]);

                MouseActions.BuyCoordinates = new System.Drawing.Point(buyX, buyY);
                MouseActions.ConfirmCoordinates = new System.Drawing.Point(confirmX, confirmY);
            }
            catch (FormatException fex)
            {
                Console.WriteLine($"You messed up the app.config, moron. Exception: {fex.Message}. Press enter to exit.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public void ValidateCoordinatesLoaded()
        {
            if (!MouseActions.BuyCoordinates.IsEmpty && !MouseActions.ConfirmCoordinates.IsEmpty)
            {
                Console.WriteLine("Coordinates are loaded.");
            }
            else
            {
                Console.WriteLine("You fucked it all up. Press enter to exit.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public void Run()
        {
            bool decreasing = false;

            while (CurrentMinPrice >= 200 && CurrentMinPrice <= 650)
            {
                if (!decreasing && CurrentMinPrice < 650)
                {
                    MouseActions.PerformClick(Enums.ButtonTypes.IncreaseMin);
                    CurrentMinPrice += 50;
                }

                if (CurrentMinPrice == 650)
                {
                    decreasing = true;
                }

                if (decreasing)
                {
                    MouseActions.PerformClick(Enums.ButtonTypes.DecreaseMin);
                    CurrentMinPrice -= 50;
                }

                if (CurrentMinPrice == 200)
                {
                    decreasing = false;
                }

                MouseActions.PerformClick(Enums.ButtonTypes.Search);

                MouseActions.BringConsoleToFront();

                Console.WriteLine("Press 1 to Buy. Press 0 to Search Again.");
                var response = Convert.ToInt32(Console.ReadLine());

                if (response == 1)
                {
                    MouseActions.PerformClick(Enums.ButtonTypes.BuyPlusConfirm);
                }
                else
                {
                    MouseActions.PerformClick(Enums.ButtonTypes.BackButton);
                }
            }
        }

        #endregion Public Methods
    }
}
