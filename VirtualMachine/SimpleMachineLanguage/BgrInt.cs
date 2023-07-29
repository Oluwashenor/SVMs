using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVM.SimpleMachineLanguage
{
    public class BgrInt : BaseInstructionWithOperand
    {
        #region IInstruction Members

        public override void Run()
        {
            int opValue;
            if (!Int32.TryParse(this.Operands[0].ToString(), out opValue))
            {
                throw new SvmRuntimeException(String.Format(BaseInstruction.OperandOfWrongTypeMessage,
                                                this.ToString(), VirtualMachine.ProgramCounter));
            }
            var op1 = VirtualMachine.Stack.Pop();
            VirtualMachine.Stack.Push(op1);
            if (op1 is int)
            {
                if (opValue > (int)op1)
                {
                    string branch_location = this.Operands[1].ToString();
                    SvmVirtualMachine svmVm = (SvmVirtualMachine)this.VirtualMachine;
                    LabelMap eventLabel = svmVm.LabelMaps.FirstOrDefault(x => x.label == branch_location);
                    svmVm.ProgramCounter = eventLabel.postion - 1;
                }
            }
            else
            {
                throw new SvmRuntimeException("Invalid type found");
            }

        }

        #endregion
    }

}
