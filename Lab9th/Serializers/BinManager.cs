using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastLangVersion.Serializers
{
    public class BinManager : Serial
    {
        public override void Write<T>(T obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                Serializer.Serialize(fs, obj);
            }
        }
        public override T Read<T>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return Serializer.Deserialize<T>(fs);
            }
        }
    }
}
