using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TurtleGamesStudio.Utilities.SaveServices
{
    public class BinaryFormatterSaver : ISaveService
    {
        public bool HasContainer(string key)
        {
            return File.Exists(key);
        }

        public T Load<T>(string key)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            object data;

            using (FileStream fileStream = new FileStream(key, FileMode.Open))
            {
                data = binaryFormatter.Deserialize(fileStream);
            }

            return (T)data;
        }

        public void Save<T>(string key, T data)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fileStream = new FileStream(key, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, data);
            }
        }
    }
}
