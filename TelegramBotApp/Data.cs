namespace ConsoleApp1;

public class Data
{
    public string name;
    public string description;
    public List<Image> image;
}

public class Serializer
{
    public string DatabasePath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory, "data.json"
    );
    public List<Data> dataList = new();

    public void Serialize()
    {
        var jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat
        };

        string jsonData = JsonConvert.SerializeObject(
            _userCache.ToDictionary(x => x.Key, x => x.Value), 
            jsonSettings
        );

        Directory.CreateDirectory(Path.GetDirectoryName(DatabasePath));
        File.WriteAllTextAsync(DatabasePath, jsonData);
    }
    public Data DeSerialize()
    {
        if (!File.Exists(DatabasePath))
            return;

        string jsonData = await File.ReadAllTextAsync(DatabasePath);
        var loadedData = JsonConvert.DeserializeObject<Dictionary<long, UserData>>(jsonData);

        return loadedData;
    }
}
