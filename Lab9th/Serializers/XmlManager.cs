using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LastLangVersion.Serializers
{
    public class XmlManager : Serial
    {
        public override void Write<T>(T obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                new XmlSerializer(typeof(T)).Serialize(fs, obj);
            }
        }
        public override T Read<T>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(fs);
            }
        }
    }
}
