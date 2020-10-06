using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneTranslate
{
    class Dictionary
    {
        private string file;


        /// <summary> Initalises a new instance of Dictionary </summary>
        public Dictionary() 
        {
            this.file = @".\..\..\files\Dictionary.txt";
        }


        public void Read()
        {
            string normal, slang, swear;

            using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
            {
                List<string> list = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    slang = line;
                    normal = reader.ReadLine();
                    swear = reader.ReadLine();
                    reader.ReadLine(); // Spacing line
                }

                Console.WriteLine("Done");
            }
        }
    }
}
