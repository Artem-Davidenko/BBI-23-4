using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastLangVersion.Serializers
{
    public abstract class Serial
    {
        public abstract void Write<T>(T obj, string filePath);
        public abstract T Read<T>(string filePath);
    }
}
