using System.Diagnostics;
using System.Security;
using System.Text.RegularExpressions;

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

            // map dimensions
            int mapWidth = 600;
            int mapHeight = 600;

            // convert GPS coordinates to decimal degrees
            double latitude = ConvertToDecimalDegrees(latitudeDegrees, latitudeMinutes, latitudeSeconds);
            double longitude = ConvertToDecimalDegrees(longitudeDegrees, longitudeMinutes, longitudeSeconds);

            // convert GPS coordinates to XY coordinates
            double x = ConvertToX(longitude, mapWidth);
            double y = ConvertToY(latitude, mapHeight);

            return $"{x},{y}";
        }

        private double ConvertToDecimalDegrees(double degrees, double minutes, double seconds)
        {
            // Convert degrees, minutes, and seconds to decimal degrees
            double decimalDegrees = degrees + (minutes / 60) + (seconds / 3600);
            return decimalDegrees;
        }

        private double ConvertToX(double longitude, int mapWidth)
        {
            // Convert longitude to X coordinate
            double x = (longitude + 180) * (mapWidth / 360.0);
            return x;
        }

        private double ConvertToY(double latitude, int mapHeight)
        {
            // Convert latitude to Y coordinate
            double y = (90 - latitude) * (mapHeight / 180.0);
            return y;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            /*if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    using (Stream str = openFileDialog1.OpenFile())
                    {
                        Process.Start("notepad.exe", filePath);
                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }*/

            Console.WriteLine(ConvertFromGPSToCoords("-128° 23' 59\" O , 65° 41' 59\" N"));
        }

        private void PlaceOnMap(string gpsCoords)
        {

        }
    }
}