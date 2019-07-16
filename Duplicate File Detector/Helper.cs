using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicate_File_Detector
{
    public class DataItem
    {
        //To store there checkSum
        private string _checkSum;
        public string CheckSum
        {
            get
            {
                return _checkSum;
            }
            set
            {
                _checkSum = value;
            }

        }

        // to store all duplicate files path
        private List<string> _filesPath;
        public List<string> FilesPath
        {
            get
            {
                return _filesPath;
            }
            set
            {
                _filesPath = value;
            }

        }

        private int _count;
        public int Count
        {
            get { return _count;  }
            set { _count = value; }
        }

        // Constructor
        public DataItem()
        {
            FilesPath = new List<string>();
        }

    }

    public class Helper
    {
    }
}
