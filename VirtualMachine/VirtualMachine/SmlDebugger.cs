using SVM.DebuggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SVM.VirtualMachine
{
    public class SmlDebugger : IDebugger
    {
        private Form1 debuggerForm;
        public IVirtualMachine VirtualMachine { get; set; }
        public void Break(IDebugFrame debugFrame)
        {
            int selected = debugFrame.LineNumber;
            List<string> instructions = new List<string>();
            List<string> stackValues = new List<string>();
            foreach (var stackItem in debugFrame.StackValues.ToArray())
            {
                stackValues.Add(stackItem.ToString());
            }
            int counter = 0;
            foreach (var item in debugFrame.CodeFrame)
            {
                instructions.Add(item.ToString());
                counter++;
            }
            debuggerForm = new Form1(instructions, selected, stackValues);
            debuggerForm.ShowDialog();
        }
    }
}
