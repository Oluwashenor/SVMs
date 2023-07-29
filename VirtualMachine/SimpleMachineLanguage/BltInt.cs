using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVM.SimpleMachineLanguage
{
    public class BltInt : BaseInstructionWithOperand
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
            if (op1 is int)
            {
                if (opValue > (int)op1)
                {
                    string branch_location = this.Operands[1].ToString();
                    //LabelMap eventLabel = this.VirtualMachine.LabelMaps.FirstOrDefault(x => x.label == branch_location);
                    //this.VirtualMachine.ProgramCounter = eventLabel.postion;
                }
                else
                {
                    throw new SvmRuntimeException("Value is less than top stack value");
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
