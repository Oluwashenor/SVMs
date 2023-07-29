using SVM.VirtualMachine;
using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace SML_Extensions
{
    public class LoadImage : BaseInstructionWithOperand
    {
        public override void Run()
        {
            // Get the operand value (the file path to the image)
            if (Operands[0].GetType() != typeof(string))
            {
                throw new SvmRuntimeException(String.Format(BaseInstruction.OperandOfWrongTypeMessage,
                                            this.ToString(), VirtualMachine.ProgramCounter));
            }

            string imagePath = Operands[0].ToString();
            imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);
           
            // Load the image from the file path
            try
            {
                using (Bitmap image = new Bitmap(imagePath))
                {
                    // Convert the image to a byte array
                    byte[] imageData;
                    using (var ms = new System.IO.MemoryStream())
                    {
                        image.Save(ms, ImageFormat.Jpeg);
                        imageData = ms.ToArray();
                    }

                    // Push the byte array (image data) onto the stack
                    VirtualMachine.Stack.Push(imageData);
                }
            }
            catch (Exception ex)
            {
                throw new SvmRuntimeException("Error loading image: " + ex.Message);
            }
        }

    }
}
