using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LastLangVersion.Serializers
{
    public class JsonManager : Serial
    {
        public override void Write<T>(T obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, obj);
            }
        }
        public override T Read<T>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<T>(fs);
            }
        }
    }
}
