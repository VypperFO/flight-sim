using System.Security;
using System.Text.RegularExpressions;
using Tp_02_02.controller;

namespace Tp_02_02
{
    public partial class FormSimulator : Form
    {
        private COTAI simulatorController;
        public FormSimulator(COTAI simulatorController)
        {
            InitializeComponent();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            button2.Enabled = false;
            this.simulatorController = simulatorController;
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            button1.Enabled = false;
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
        public string ConvertFromGPSToCoords(string gpsCoords)
        {
            string pattern = @"-?\d+";

            MatchCollection matches = Regex.Matches(gpsCoords, pattern);

            int[] numbers = new int[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                numbers[i] = int.Parse(matches[i].Value);
            }


            double longitudeDegrees = numbers[0];
            double longitudeMinutes = numbers[1];
            double longitudeSeconds = numbers[2];

            double latitudeDegrees = numbers[3];
            double latitudeMinutes = numbers[4];
            double latitudeSeconds = numbers[5];

            int mapWidth = 600;
            int mapHeight = 600;

            double latitude = ConvertToDecimalDegrees(latitudeDegrees, latitudeMinutes, latitudeSeconds);
            double longitude = ConvertToDecimalDegrees(longitudeDegrees, longitudeMinutes, longitudeSeconds);

            int x = (int)Math.Round(ConvertToX(longitude, mapWidth));
            int y = (int)Math.Round(ConvertToY(latitude, mapHeight));

            return $"{x},{y}";
        }

        private double ConvertToDecimalDegrees(double degrees, double minutes, double seconds)
        {
            double decimalDegrees = degrees + (minutes / 60) + (seconds / 3600);
            return decimalDegrees;
        }

        private double ConvertToX(double longitude, int mapWidth)
        {
            double x = (longitude + 180) * (mapWidth / 360.0);
            return x;
        }

        private double ConvertToY(double latitude, int mapHeight)
        {
            double y = (90 - latitude) * (mapHeight / 180.0);
            return y;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        public void PlaceOnMap(string gpsCoords, string name)
        {
            int x, y;
            try
            {
                string[] coords = ConvertFromGPSToCoords(gpsCoords).Split(",");
                Console.WriteLine(coords);
                x = Int32.Parse(coords[0]);
                y = Int32.Parse(coords[1]);
            }
            catch (Exception)
            {
                throw;
            }

            PictureBox pictureBox = new PictureBox();
            Image image = Properties.Resources.Battle_bus_icon;
            pictureBox.Image = image;

            pictureBox.Location = new Point(x, y);
            panel1.Controls.Add(pictureBox);

            Label label = new Label();
            label.Text = name;
            label.Font = new Font("Arial", 12);
            label.BackColor= Color.Transparent;

            int labelX = x; // y relative to the picturebox
            int labelY = y-10; // y relative to the picturebox
            label.Location = new Point(labelX, labelY);

            panel1.Controls.Add(label);
            label.BringToFront();
        }

        public void SetPlayBtnEnable(bool enabled)
        {
            button2.Enabled = enabled;
        }

        private void SelectFileBtn_click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    using (Stream str = openFileDialog1.OpenFile())
                    {
                        simulatorController.Load(filePath);
                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void Play_click(object sender, EventArgs e)
        {
            simulatorController.Play();
        }
    }
}