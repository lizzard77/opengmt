using System.Text.Json;

namespace OpenGMT.DB
{
    public class FileDB
    {
        public static string DataDir;

        public List<Scene> Scenes { get => load<List<Scene>>(); }
        public List<Creature> Creatures { get => load<List<Creature>>(); }
        public List<Map> Maps { get => load<List<Map>>(); }
        public Session Session { get => load<Session>(); }

        public void Upsert<T>(T record)
        {
            var list = load<List<T>>();
            var existing = list.Find(x => getId(x) == getId(record));
            if (existing != null)
            {
                list.Remove(existing);
            }
            list.Add(record);
            save(list);
        }

        public void UpsertSingleton<T>(T record)
        {
            save(record);
        }

        public void DeleteSingleton<T>()
        {
            save(default(T));
        }

        public void Delete<T>(long id)
        {
            var list = load<List<T>>();
            var existing = list.Find(x => getId(x) == id);
            if (existing != null)
            {
                list.Remove(existing);
            }
            save(list);
        }

        private long getId<T>(T record)
        {
            return (long)typeof(T).GetProperty("Id").GetValue(record);
        }

        private string getFileName<T>()
        {
            Type t = typeof(T);
            if (t.IsGenericType)
                t = t.GetGenericArguments()[0];
            return t.Name.ToLower() + ".json";
        }

        private T load<T>(string fileName = "") 
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = getFileName<T>();
            string dataFile = Path.Combine(DataDir, fileName);

            if (Directory.Exists(DataDir) && System.IO.File.Exists(dataFile))
            {
                string data = System.IO.File.ReadAllText(dataFile);
                return JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return default(T);
        }

        public void save<T>(T data, string fileName = "")
        {
            if (!Directory.Exists(DataDir))
                Directory.CreateDirectory(DataDir);

            if (string.IsNullOrEmpty(fileName))
                fileName = getFileName<T>();

            string dataFile = Path.Combine(DataDir, fileName);
            System.IO.File.WriteAllText(dataFile, JsonSerializer.Serialize(data));
        }
    }
}