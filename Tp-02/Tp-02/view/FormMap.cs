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
            formGenerator.setCoords();
            Close();
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

        private string convertCoordsToGPS(string coords)
        {
            float x = start.X;
            float y = start.Y;
            float newX = Math.Abs(300 - x);
            float newY = Math.Abs(300 - y);
            float tempAngleX;
            int FinalAngleX;
            int finalMinutesX;
            int finalSecondsX;
            double tempMinutesX;
            double tempSecondsX;
            float tempAngleY;
            int FinalAngleY;
            int finalMinutesY;
            int finalSecondsY;
            double tempMinutesY;
            double tempSecondsY;
            if (x <= 300 && y <= 300)
            {
                tempAngleX = ((newX * 180) / 300);
                FinalAngleX = (int)tempAngleX;

                tempMinutesX = tempAngleX - Math.Truncate(tempAngleX);
                tempMinutesX = tempMinutesX * 60;
                finalMinutesX = (int)tempMinutesX;

                tempSecondsX = tempMinutesX - Math.Truncate(tempMinutesX);
                tempSecondsX = tempSecondsX * 60;
                finalSecondsX = (int)tempSecondsX;

                tempAngleY = ((newY * 90) / 300);
                FinalAngleY = (int)tempAngleY;

                tempMinutesY = tempAngleY - Math.Truncate(tempAngleY);
                tempMinutesY = tempMinutesY * 60;
                finalMinutesY = (int)tempMinutesY;

                tempSecondsY = tempMinutesY - Math.Truncate(tempMinutesY);
                tempSecondsY = tempSecondsY * 60;
                finalSecondsY = (int)tempSecondsY;

                return "-" + FinalAngleX.ToString() + "° " + finalMinutesX.ToString() + "' " + finalSecondsX.ToString() + "\" " + "O , " + FinalAngleY.ToString() + "° " + finalMinutesY.ToString() + "' " + finalSecondsY.ToString() + "\" " + "N";

            }
            else if (x >= 300 && y <= 300)
            {
                tempAngleX = ((newX * 180) / 300);
                FinalAngleX = (int)tempAngleX;

                tempMinutesX = tempAngleX - Math.Truncate(tempAngleX);
                tempMinutesX = tempMinutesX * 60;
                finalMinutesX = (int)tempMinutesX;

                tempSecondsX = tempMinutesX - Math.Truncate(tempMinutesX);
                tempSecondsX = tempSecondsX * 60;
                finalSecondsX = (int)tempSecondsX;

                tempAngleY = ((newY * 90) / 300);
                FinalAngleY = (int)tempAngleY;

                tempMinutesY = tempAngleY - Math.Truncate(tempAngleY);
                tempMinutesY = tempMinutesY * 60;
                finalMinutesY = (int)tempMinutesY;

                tempSecondsY = tempMinutesY - Math.Truncate(tempMinutesY);
                tempSecondsY = tempSecondsY * 60;
                finalSecondsY = (int)tempSecondsY;

                return FinalAngleX.ToString() + "° " + finalMinutesX.ToString() + "' " + finalSecondsX.ToString() + "\" " + "E , " + FinalAngleY.ToString() + "° " + finalMinutesY.ToString() + "' " + finalSecondsY.ToString() + "\" " + "N";

            }
            else if (x <= 300 && y >= 300)
            {
                tempAngleX = ((newX * 180) / 300);
                FinalAngleX = (int)tempAngleX;

                tempMinutesX = tempAngleX - Math.Truncate(tempAngleX);
                tempMinutesX = tempMinutesX * 60;
                finalMinutesX = (int)tempMinutesX;

                tempSecondsX = tempMinutesX - Math.Truncate(tempMinutesX);
                tempSecondsX = tempSecondsX * 60;
                finalSecondsX = (int)tempSecondsX;

                tempAngleY = ((newY * 90) / 300);
                FinalAngleY = (int)tempAngleY;

                tempMinutesY = tempAngleY - Math.Truncate(tempAngleY);
                tempMinutesY = tempMinutesY * 60;
                finalMinutesY = (int)tempMinutesY;

                tempSecondsY = tempMinutesY - Math.Truncate(tempMinutesY);
                tempSecondsY = tempSecondsY * 60;
                finalSecondsY = (int)tempSecondsY;

                return "-" + FinalAngleX.ToString() + "° " + finalMinutesX.ToString() + "' " + finalSecondsX.ToString() + "\" " + "O , -" + FinalAngleY.ToString() + "° " + finalMinutesY.ToString() + "' " + finalSecondsY.ToString() + "\" " + "S";
            }
            else if (x >= 300 && y >= 300)
            {
                tempAngleX = ((newX * 180) / 300);
                FinalAngleX = (int)tempAngleX;

                tempMinutesX = tempAngleX - Math.Truncate(tempAngleX);
                tempMinutesX = tempMinutesX * 60;
                finalMinutesX = (int)tempMinutesX;

                tempSecondsX = tempMinutesX - Math.Truncate(tempMinutesX);
                tempSecondsX = tempSecondsX * 60;
                finalSecondsX = (int)tempSecondsX;

                tempAngleY = ((newY * 90) / 300);
                FinalAngleY = (int)tempAngleY;

                tempMinutesY = tempAngleY - Math.Truncate(tempAngleY);
                tempMinutesY = tempMinutesY * 60;
                finalMinutesY = (int)tempMinutesY;

                tempSecondsY = tempMinutesY - Math.Truncate(tempMinutesY);
                tempSecondsY = tempSecondsY * 60;
                finalSecondsY = (int)tempSecondsY;

                return FinalAngleX.ToString() + "° " + finalMinutesX.ToString() + "' " + finalSecondsX.ToString() + "\" " + "E , -" + FinalAngleY.ToString() + "° " + finalMinutesY.ToString() + "' " + finalSecondsY.ToString() + "\" " + "S";
            }
            return "Try again";
        }

        public string getCoords()
        {
            return convertCoordsToGPS(label1.Text);
        }
    }
}
