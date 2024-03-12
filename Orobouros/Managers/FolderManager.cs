using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orobouros.Managers
{
    public static class FolderManager
    {
        /// <summary>
        /// Creates a folder if it does not exist already.
        /// </summary>
        /// <param name="path"></param>
        public static void VerifyFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}