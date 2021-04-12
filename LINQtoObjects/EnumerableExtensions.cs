using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LINQtoObjects
{
    public static class EnumerableExtensions
    {
        public static void WriteToFile<T>(this IEnumerable<T> collection, 
            string path, 
            bool printPretty = false)
        {
            var options = new JsonSerializerOptions { WriteIndented = printPretty };

            string jsonString = JsonSerializer.Serialize(collection, options);

            File.WriteAllText(path, jsonString);
        }
    }
}
