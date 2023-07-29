using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVM.SimpleMachineLanguage
{
    public class EquInt : BaseInstruction
    {
         #region IInstruction Members
    public override void Run()
    {
        try
        {
            if (VirtualMachine.Stack.Count < 1)
            {
                throw new SvmRuntimeException(String.Format(BaseInstruction.StackUnderflowMessage,
                                                this.ToString(), VirtualMachine.ProgramCounter));
            }
            var op1 = VirtualMachine.Stack.Pop();
            if (op1 is int)
            {
                VirtualMachine.Stack.Push((int)op1 + 1);
            }
            else
            {
                throw new SvmRuntimeException("Data on stack is not of type Int");
            }
        }
        catch (InvalidCastException)
        {
            throw new SvmRuntimeException(String.Format(BaseInstruction.OperandOfWrongTypeMessage,
                                            this.ToString(), VirtualMachine.ProgramCounter));
        }
    }

    #endregion

    }
}
