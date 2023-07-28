public interface IVirtualMachine
{
    int ProgramCounter { get; }
    Stack Stack { get; }
}