namespace SVM.DebuggerUI
{
    public partial class Form1 : Form
    {

        public Form1(List<string> instructions, int selected, List<string> stackValues)
        {
            InitializeComponent();
            foreach (var item in instructions)
            {
                instructionsBox.Items.Add(item);
            }
            instructionsBox.SetSelected(selected, true);
            listBox2.Items.Clear();
            foreach (var item in stackValues)
            {
                listBox2.Items.Add(item);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}