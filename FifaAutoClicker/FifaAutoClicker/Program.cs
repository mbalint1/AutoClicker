using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FifaAutoClicker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var controller = new Controller();
                Console.WriteLine("Do you need to calibrate your button coordinates? Y/N");
                var response = Console.ReadLine();

                if (response.Trim().ToUpper() == "Y")
                {
                    controller.DoButtonCalibration();
                }
                if (response?.Trim().ToUpper() == "N")
                {
                    controller.LoadCalibrationSettings();
                }

                controller.ValidateCoordinatesLoaded();

                Console.WriteLine("The app will now continuously search for your selected player. To purchase Press 1, To exit Press 0.");

                Console.WriteLine("Get ready the search will begin in 15 seconds..");
                System.Threading.Thread.Sleep(15000);

                controller.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"You really fucked up. Message: {ex.Message}. Press enter to exit.");
                Console.ReadLine();
            }
        }
    }
}
