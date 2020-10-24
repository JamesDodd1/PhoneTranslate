using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneTranslate.Crud
{
    public class FileFactory
    {
        public static Translate GetFile(string type)
        {
            Translate file = null;

            switch (type)
            {
                case "Dictionary":
                    file = new Translate(@"Dictionary");
                    break;

                case "SwearWords":
                    file = new Translate(@"SwearWords");
                    break;

                default:
                    break;
            }

            return file;
        }
    }
}
