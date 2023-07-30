using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVM.VirtualMachine.Debug
{
    public class DebugFrame : IDebugFrame
    {
        public DebugFrame(IInstruction currentInstruction, List<IInstruction> codeFrame, int lineNumber, Stack stack)
        {
            CurrentInstruction = currentInstruction;
            CodeFrame = codeFrame;
            LineNumber = lineNumber;
            StackValues = stack;
        }

        public IInstruction CurrentInstruction { get; }

        public List<IInstruction> CodeFrame { get; }
        public int LineNumber { get; }
        public Stack StackValues { get; }
    }

}
