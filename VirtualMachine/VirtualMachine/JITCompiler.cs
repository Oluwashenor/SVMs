namespace SVM.VirtualMachine;

using HashGenerator;
using SVM.SimpleMachineLanguage;
using System.Globalization;

#region Using directives
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
#endregion

/// <summary>
/// Utility class which generates compiles a textual representation
/// of an SML instruction into an executable instruction instance
/// </summary>
internal static class JITCompiler
{
    #region Constants
    #endregion

    #region Fields
    #endregion

    #region Constructors
    #endregion

    #region Properties
    #endregion

    #region Public methods
    #endregion

    #region Non-public methods
    internal static IInstruction CompileInstruction(string opcode)
    {
        IInstruction instruction = null;

        #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
        try
        {
            string svmExecutableDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string[] assemblyFiles = Directory.GetFiles(svmExecutableDirectory, "*.dll");
            
            foreach (string assemblyFile in assemblyFiles)
            {
                bool matched = VerifyAssembly(assemblyFile);
                if (!matched)
                {
                    throw new SvmRuntimeException("Invalid Hash Values : please update the config file" + opcode);
                }
                try
                {
                    Assembly assembly = Assembly.LoadFrom(assemblyFile);
                    Type classType = assembly.GetTypes()
                        .FirstOrDefault(a => a.Name.Equals(opcode, StringComparison.OrdinalIgnoreCase) &&
                                             typeof(IInstruction).IsAssignableFrom(a) && !a.IsAbstract);

                    if (classType != null)
                    {
                        object instance = Activator.CreateInstance(classType);
                        instruction = (IInstruction)instance;
                        break;
                    }
                }
                catch (Exception)
                {
                        throw new SvmRuntimeException("Something went wrong " + opcode);
                    }
            }

            if (instruction == null)
            {
                throw new SvmRuntimeException("No matching instruction found for opcode: " + opcode);
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during the process
        }
        #endregion
        return instruction;
    }

    internal static IInstruction CompileInstruction(string opcode, params string[] operands)
    {
        IInstructionWithOperand instruction = null;

        #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
        try
        {
            string svmExecutableDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string[] assemblyFiles = Directory.GetFiles(svmExecutableDirectory, "*.dll");
           

            foreach (string assemblyFile in assemblyFiles)
            {
                bool matched = VerifyAssembly(assemblyFile);
                if (!matched)
                {
                    throw new SvmRuntimeException("Invalid Hash Values : please update the config file" + opcode);
                }
                try
                {
                    Assembly assembly = Assembly.LoadFrom(assemblyFile);
                    Type classType = assembly.GetTypes()
                        .FirstOrDefault(a => a.Name.Equals(opcode, StringComparison.OrdinalIgnoreCase) &&
                                             typeof(IInstruction).IsAssignableFrom(a) && !a.IsAbstract);
                    if (classType != null)
                    {
                        object instance = Activator.CreateInstance(classType);
                        instruction = (IInstructionWithOperand)instance;
                        instruction.Operands = operands;
                        break;
                    }
                }
                catch (Exception)
                {
                    // Handle any exceptions related to loading assemblies or finding types
                }
            }
            if (instruction == null)
            {
                throw new ArgumentException("No matching instruction found for opcode: " + opcode);
            }


        }
        catch (Exception ex) { }
        #endregion
        return instruction;
    }



    public static bool VerifyAssembly(string assembly)
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
        string dllPath = AppDomain.CurrentDomain.BaseDirectory;
        string assemblyFileName = Path.GetFileName(assembly);
        string assemblyFile = Path.Combine(dllPath, assemblyFileName);
        string configfile = "config.json";
        string filePath = Path.Combine(configpath, configfile);
        if (File.Exists(filePath))
        {
            string configFileContent = File.ReadAllText(filePath);
            AllowedAssembly assemblyConfigs = JsonSerializer.Deserialize<AllowedAssembly>(configFileContent);
            var currentAssembly = assemblyConfigs.AllowedAssemblies.FirstOrDefault(x => x.AssemblyName == assemblyFileName);
            if (currentAssembly == null) return false;
            byte[] data = File.ReadAllBytes(assemblyFile);
            var dllExist = File.Exists(assemblyFile);
            using SHA256 sha256 = SHA256.Create();
            byte[] hashValue = sha256.ComputeHash(data);
            string hashedValue = BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            if(hashedValue == currentAssembly.Hash)return true;
            return false;
        }
        else
        {
            Console.WriteLine("File Not Found");
            return false;
        }
    }
    #endregion
}
