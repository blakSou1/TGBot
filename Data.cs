using System.IO;
public class Data
{
    public string name;
    public string description = "Адрес: Поляничко, 9\n   🏢 Предлагаем вашему вниманию уютную квартиру для посуточной аренды от компании ГРИНХАУС, расположенную по адресу: Поляничко 9, в Дзержинском районе города Оренбург. Квартира находится на 19 этаже жилого комплекса 'Новая высота', что обеспечивает не только комфорт, но и великолепные виды на окружающие новостройки.\n\n    🛋 Уют и комфорт\n    Несмотря на свои небольшие размеры, квартира наполнена теплом и уютом. Здесь вы найдете комфортную мебель, которая создаст атмосферу домашнего уюта. Мы позаботились о том, чтобы ваше пребывание было максимально приятным и комфортным.\n\n    🍽 Бытовая техника\n    \"Квартира оснащена всей необходимой бытовой техникой, включая холодильник, плиту и стиральную машину. Это позволит вам готовить любимые блюда и чувствовать себя как дома, даже вдали от него.\n\n    🌐 Wi-Fi\n    Для вашего удобства квартира также оборудована высокоскоростным Wi-Fi, что позволит вам оставаться на связи и работать удаленно, если это необходимо.\n\n    🌆 Прекрасный вид\n    Окна квартиры выходят на живописные новостройки, что создает ощущение современности и динамики. Наслаждайтесь утренним кофе с видом на город или вечерними закатами, которые будут радовать вас каждый день.\n\n    ✨ Идеальный выбор для отдыха\n    Эта квартира станет отличным выбором как для краткосрочного, так и для длительного проживания. Мы гарантируем вам комфорт и уют в одном из самых развитых районов Оренбурга. Забронируйте свою идеальную квартиру уже сегодня!";
    
    private static string userName = $@"{Environment.UserDomainName}\{Environment.UserName}";

    public FileStream[] photos;
    

    private static FileStream LoadFile(string path, string nameImage)
    {
        if(Directory.Exists(path))
        {
            FileStream? fstream = null;
            fstream = new FileStream(@$"{path}\{nameImage}", FileMode.Open, FileAccess.ReadWrite);
            
            return fstream;
        }
        else
        {
            Directory.CreateDirectory(path);
            FileStream? fstream = null;
            fstream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            
            return fstream;
        }
    }

    public FileStream[] InitializeStream()
    {
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        Console.WriteLine(appDirectory);
        FileStream[] fstream =
        {
            LoadFile($@"{appDirectory}DataPhotos", "photo (2).jpg"),
            LoadFile($@"{appDirectory}DataPhotos", "photo (3).jpg"),
            LoadFile($@"{appDirectory}DataPhotos", "photo (4).jpg"),
            LoadFile($@"{appDirectory}DataPhotos", "photo.jpg")
        };
        return fstream;
    }

    public void ClearData(FileStream[] files)
    {
        foreach (var file in files)
        {
            file.Close();
        }
    }
}