namespace Tp_02_02
{
    public partial class FormSimulator : Form
    {
        public FormSimulator()
        {
            InitializeComponent();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            button2.Enabled = false;
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}