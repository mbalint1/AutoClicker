using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FifaAutoClicker
{
    public class MouseActions
    {
        #region Properties

        public System.Drawing.Point BuyCoordinates { get; set; }

        public System.Drawing.Point ConfirmCoordinates { get; set; }

        public System.Drawing.Point BackCoordinates { get; set; }

        public System.Drawing.Point IncreaseMinCoords { get; set; }

        public System.Drawing.Point DecreaseMinCoords { get; set; }

        public System.Drawing.Point SearchCoordinates { get; set; }

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;

        public const int MOUSEEVENTF_LEFTUP = 0x04;

        #endregion Properties

        #region UnmanagedCode

        [System.Runtime.InteropServices.DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GetConsoleWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        #endregion UnmanagedCode

        #region Public Methods

        public void PerformClick(Enums.ButtonTypes buttonType)
        {
            switch (buttonType)
            {
                case Enums.ButtonTypes.BuyPlusConfirm:
                    Console.WriteLine("Clicking Buy Now Button + Confirm Button");
                    DoClickAtPosition(BuyCoordinates.X, BuyCoordinates.Y);
                    DoClickAtPosition(ConfirmCoordinates.X, ConfirmCoordinates.Y);
                    System.Threading.Thread.Sleep(300); // In case of slow connections
                    DoClickAtPosition(ConfirmCoordinates.X, ConfirmCoordinates.Y);
                    Console.WriteLine("If purchase was successful list it immediately from same screen. Back button will be pressed in 12 seconds..");
                    System.Threading.Thread.Sleep(10000); // To give time to sell it
                    break;

                case Enums.ButtonTypes.Search:
                    System.Threading.Thread.Sleep(1500);
                    Console.WriteLine("Clicking Search Button");
                    DoClickAtPosition(SearchCoordinates.X, SearchCoordinates.Y);
                    break;

                case Enums.ButtonTypes.IncreaseMin:
                    System.Threading.Thread.Sleep(1500);
                    Console.WriteLine("Clicking Increase Min Button");
                    DoClickAtPosition(IncreaseMinCoords.X, IncreaseMinCoords.Y);
                    break;

                case Enums.ButtonTypes.DecreaseMin:
                    System.Threading.Thread.Sleep(1500);
                    Console.WriteLine("Clicking Decrease Min Button");
                    DoClickAtPosition(DecreaseMinCoords.X, DecreaseMinCoords.Y);
                    break;

                case Enums.ButtonTypes.BackButton:
                    Console.WriteLine("Clicking Back Button");
                    DoClickAtPosition(BackCoordinates.X, BackCoordinates.Y);
                    break;
            }
        }

        public void BringConsoleToFront()
        {
            SetForegroundWindow(GetConsoleWindow());
        }

        #endregion Public Methods

        #region Private Methods

        private void DoClickAtPosition(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        #endregion Private Methods
    }
}
