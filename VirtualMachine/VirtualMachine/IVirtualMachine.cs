public interface IVirtualMachine
{
    public int ProgramCounter { get; }
    Stack Stack { get; }
}