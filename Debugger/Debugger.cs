﻿using SVM.DebuggerUI;
using SVM.VirtualMachine.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Debugger : IDebugger
{
    #region TASK 5 - TO BE IMPLEMENTED BY THE STUDENT
    private Form1 debuggerForm;
    public IVirtualMachine VirtualMachine { get; set; }
    public void Break(IDebugFrame debugFrame)
    {
        debuggerForm = new Form1();
    }
    #endregion
}


