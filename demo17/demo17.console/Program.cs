using System.Text.Json;

const string File_PATH = "test.json";

var strJson = await File.ReadAllTextAsync(File_PATH);

System.Console.WriteLine(strJson);

// var fs = File.OpenRead(File_PATH);

var fullResp = JsonSerializer.Deserialize<Dictionary<string, string>>(strJson) ?? new();

foreach (var kv in fullResp)
{
    Console.WriteLine(kv);
}