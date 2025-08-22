using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public sealed class JSONParser
{
    public string ReadFile(string path)
    {
        string text;
        if (File.Exists(path))
        {
            FileStream file = File.Open(path, FileMode.Open);
            using (StreamReader strreader = new StreamReader(file))
            {
                text = strreader.ReadToEnd();
            }
            JsonTextReader reader = new JsonTextReader(new StringReader(text));
            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    return reader.Value.ToString();
                }
            }
        }
        else
        {
            throw new Exception($"File not found, please make file 'token.txt' in {path} with your token. \n" +
                                "For Example:\n{\n \"Token\": \"ur token\" \n}");
        }
        return null;
    }
    public void CreateFile(string path, string token)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        if (File.Exists(path))
            return;
        using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                
                writer.WritePropertyName("Token");
                writer.WriteValue(token);

                writer.WriteEndObject();
            }

        File.WriteAllText(path, sb.ToString());
    }
}