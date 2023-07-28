namespace SVM.VirtualMachine;

using SVM.SimpleMachineLanguage;
using System.Globalization;

#region Using directives
using System.Reflection;
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
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                Type classType = assembly.GetTypes().FirstOrDefault(a=>a.Name.Equals(opcode, StringComparison.OrdinalIgnoreCase));  
                if (classType != null)
                {
                    object instance = Activator.CreateInstance(classType);
                    instruction = (IInstruction)instance;
                    break;
                }
            }
            if (instruction == null)
            {
                throw new SvmRuntimeException("No matching instruction found for opcode: " + opcode);
            }
        }
        catch (Exception ex) { }
        #endregion
        return instruction;
    }

    internal static IInstruction CompileInstruction(string opcode, params string[] operands)
    {
        IInstructionWithOperand instruction = null;

        #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
        try
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                Type classType = assembly.GetTypes().FirstOrDefault(a => a.Name.Equals(opcode, StringComparison.OrdinalIgnoreCase));
                if (classType != null)
                {
                    object instance = Activator.CreateInstance(classType);
                    instruction = (IInstructionWithOperand)instance;
                    instruction.Operands = operands;
                    //instruction.VirtualMachine = null;
                    break;
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
    #endregion
}
