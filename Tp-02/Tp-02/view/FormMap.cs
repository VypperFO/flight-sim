namespace Tp_02.view
{
    public partial class FormMap : Form
    {
        public FormGenerator formGenerator;
        private Point start;
        public FormMap()
        {
            InitializeComponent();
        }

        private void ClickSelection(EventArgs e)
        {

            MouseEventArgs meStart = (MouseEventArgs)e;
            Point coordinates = meStart.Location;
            start = coordinates;
            label1.Text = start.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ClickSelection(e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormMap_Load(object sender, EventArgs e)
        {

        }
    }
}
