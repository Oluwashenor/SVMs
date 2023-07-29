// See https://aka.ms/new-console-template for more information
using HashGenerator;
using System.Security.Cryptography;
using System.Text.Json;

Console.WriteLine("Hello, World!");


string configpath = @"C:\Users\Shenor\Downloads\Download SVM\SVM_-1769111514\SVM\VirtualMachine\";
string configfile = "config.json";
string configFilePath = Path.Combine(configpath, configfile);
string configFileContent = File.ReadAllText(configFilePath);
AllowedAssembly assemblyConfigs = JsonSerializer.Deserialize<AllowedAssembly>(configFileContent);

List<AssemblyConfig> assemblyFiles = assemblyConfigs.AllowedAssemblies;

foreach (var item in assemblyFiles)
{
    string appPath = @"C:\Users\Shenor\Downloads\Download SVM\SVM_-1769111514\SVM\bin\Debug\";
    string filePath = Path.Combine(appPath, item.AssemblyName);
    if (File.Exists(filePath))
    {
        Console.WriteLine("File Exist");
        byte[] data = File.ReadAllBytes(filePath);
        using SHA256 sha256 = SHA256.Create();
        byte[] hashValue = sha256.ComputeHash(data);
        string fineTunedHash = BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        item.Hash = fineTunedHash;
    }
    else
    {
        Console.WriteLine("File Not Found {0}");
    }
}

var writeToFile = File.WriteAllTextAsync(configFilePath, JsonSerializer.Serialize(assemblyConfigs));