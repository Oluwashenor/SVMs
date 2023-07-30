// See https://aka.ms/new-console-template for more information
using HashGenerator;
using System.Security.Cryptography;
using System.Text.Json;

namespace HashGenerator;

class Program
{
    static void Main(string[] args)
    {

        string solutionRootPath = string.Empty;
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        for (int i = 0; i < 6; i++)
        {
            DirectoryInfo parentDirectory = Directory.GetParent(currentDirectory);

            if (parentDirectory == null || parentDirectory.Name.Equals("SVM", StringComparison.OrdinalIgnoreCase))
            {
                solutionRootPath = parentDirectory.FullName;
                break;
            }
            currentDirectory = parentDirectory.FullName;
        }

        string configpath = $"{solutionRootPath}\\VirtualMachine\\";
        string configfile = "config.json";
        string configFilePath = Path.Combine(configpath, configfile);
        string configFileContent = File.ReadAllText(configFilePath);

        AllowedAssembly assemblyConfigs = JsonSerializer.Deserialize<AllowedAssembly>(configFileContent);

        List<AssemblyConfig> assemblyFiles = assemblyConfigs.AllowedAssemblies;

        foreach (var item in assemblyFiles)
        {
            string appPath = $"{solutionRootPath}\\bin\\Debug\\";
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
    }
}
