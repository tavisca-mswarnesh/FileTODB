using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface IDatabaseFactory
    {
        void WriteToDatabase(string fileLoction);
    }

}
