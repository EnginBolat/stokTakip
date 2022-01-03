using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    class baglantiSinifi
    {
        public string baglantiAdresi = System.IO.File.ReadAllText(@"C:\baglanti.txt");
    }
}
