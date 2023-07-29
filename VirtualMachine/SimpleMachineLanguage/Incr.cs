﻿namespace SVM.SimpleMachineLanguage;

/// <summary>
/// Implements the SML Incr  instruction
/// Increments the integer value stored on top of the stack, 
/// leaving the result on the stack
/// </summary>
public class Incr : BaseInstruction
{
    #region TASK 3 - TO BE IMPLEMENTED BY THE STUDENT
    #endregion

    #region Constants
    protected const string NonStringValue = "The value is a non string value ( at [line {0}] {1})";
    #endregion


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
                throw new SvmRuntimeException("Invalid type found");
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
