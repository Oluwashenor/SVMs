using SVM.VirtualMachine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SML_Extensions
{
        public class DisplayImage : BaseInstruction
        {
            public override void Run()
            {
                // Check if there's an image on the stack
                if (VirtualMachine.Stack.Count == 0 || !(VirtualMachine.Stack.Peek() is byte[] imageData))
                {
                    throw new SvmRuntimeException("No image found on the stack.");
                }

                try
                {
                    // Create a MemoryStream from the image data byte array
                    using (var ms = new MemoryStream(imageData))
                    {
                        // Load the image from the MemoryStream
                        using (var image = Image.FromStream(ms))
                        {
                            // Display the image in a Windows Forms dialog
                            using (var imageForm = new Form())
                            {
                                imageForm.Text = "Displayed Image";
                                imageForm.ClientSize = new Size(image.Width, image.Height);
                                imageForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                                imageForm.StartPosition = FormStartPosition.CenterScreen;
                                imageForm.BackgroundImage = image;

                                // Show the image dialog
                                Application.Run(imageForm);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new SvmRuntimeException("Error displaying image: " + ex.Message);
                }
            }
        }
    }
