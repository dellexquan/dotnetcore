using System.IO;

using (var httpClient = new HttpClient())
{
    var task = httpClient.GetStringAsync("https://www.baidu.com");


    var destFilePath = "web.txt";
    var content = "hello async and await";
    await File.WriteAllTextAsync(destFilePath, content);
    string content2 = await File.ReadAllTextAsync(destFilePath);
    Console.WriteLine(content2);
    var html = await task;
    Console.WriteLine(html);
}