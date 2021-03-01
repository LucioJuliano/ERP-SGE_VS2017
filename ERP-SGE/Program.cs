using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace ERP_SGE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Deletando os Arquivos temporario da Bematech
            FileInfo ArqBematech = new FileInfo("BemaConfig.xml");
            if (ArqBematech.Exists)
                ArqBematech.Delete();
            ArqBematech = null;
            ArqBematech = new FileInfo("cupomFiscal.bin");
            if (ArqBematech.Exists)
                ArqBematech.Delete();
            //------------------------------------------
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TelaPrincipal());
        }
    }
}
