using Moq;
using SVM.SimpleMachineLanguage;
using SVM.VirtualMachine;
using System.Collections;

namespace SVM.UnitTest
{

    public class TestVirtualMachine : IVirtualMachine
    {
        public Stack Stack { get; private set; }
        public int ProgramCounter { get; private set; }

        public TestVirtualMachine()
        {
            // Initialize the stack with test data for your specific tests
            Stack = new Stack();
            Stack.Push(42); // For example, pushing a test value onto the stack
        }
    }



    [TestClass]
    public class SmlInstructionTests
    {
        [TestMethod]
        public void IncrInstruction_Run_ShouldIncrementValueOnTopOfStack()
        {
            var sample = new TestVirtualMachine();
            var instruction = new Incr();
            instruction.VirtualMachine = sample;

            instruction.Run();
        }

        [TestMethod]
        public void DecrInstruction_Run_ShouldDecrementValueOnTopOfStack()
        {
            var sample = new TestVirtualMachine();
            var instruction = new Decr();
            instruction.VirtualMachine = sample;

            instruction.Run();
            sample.Stack.Peek();
        }
    }
}