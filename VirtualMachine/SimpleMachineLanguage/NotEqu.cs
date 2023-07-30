using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVM.SimpleMachineLanguage
{
    public class NotEqu : BaseInstructionWithOperand
    {
        #region IInstruction Members

        public override void Run()
        {
            
            var op1 = VirtualMachine.Stack.Pop();
            var op2 = VirtualMachine.Stack.Pop();
            VirtualMachine.Stack.Push(op1);
            if (op1.GetType().Name != op2.GetType().Name)
            {
                    string branch_location = this.Operands[1].ToString();
                    SvmVirtualMachine svmVm = (SvmVirtualMachine)this.VirtualMachine;
                    LabelMap eventLabel = svmVm.LabelMaps.FirstOrDefault(x => x.label == branch_location);
                    svmVm.ProgramCounter = eventLabel.postion - 1;
            }
            else
            {
                throw new SvmRuntimeException("Invalid type found");
            }

        }

        #endregion
    }

}
