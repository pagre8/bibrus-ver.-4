using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bibrusik
{
    internal static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Logowanie login = new Logowanie();

            Application.Run(login);
            if (login.id == 0)  return; 

            Menu menu = new Menu(login.id);
            Application.Run(menu);

            

        }
    }
}
