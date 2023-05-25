using System.Numerics;
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
            this.DoubleBuffered = true;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
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
        public Vector2 ConvertFromGPSToCoords(string gpsCoords)
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

            Vector2 vector = new();
            vector.X = (int)Math.Round(ConvertToX(longitude, mapWidth));
            vector.Y = (int)Math.Round(ConvertToY(latitude, mapHeight));


            return vector;
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
            Vector2 coords = ConvertFromGPSToCoords(gpsCoords);
            PictureBox pictureBox = new PictureBox();
            Image image = Properties.Resources.pngwing_com__1_;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Image = image;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Size = new Size(35, 35);
            pictureBox.Location = new Point((int)coords.X, (int)coords.Y);
            if (panel1.InvokeRequired)
            {
                panel1.Invoke(new MethodInvoker(delegate { panel1.Controls.Add(pictureBox); }));

            }
            else
            {
                panel1.Controls.Add(pictureBox);
            }
            Label label = new Label();
            // a mettre une size au label pour pas qui interfere avec les image davion
            label.AutoSize = true;
            label.Text = name;
            label.Font = new Font("Arial", 12);
            label.BackColor = Color.Transparent;

            int labelX = (int)coords.X; // y relative to the picturebox
            int labelY = (int)coords.Y - 10; // y relative to the picturebox
            label.Location = new Point(labelX, labelY);

            if (panel1.InvokeRequired)
            {
                panel1.Invoke(new MethodInvoker(delegate { panel1.Controls.Add(label); }));
                panel1.Invoke(new MethodInvoker(delegate { label.BringToFront(); }));
            }
            else
            {
                panel1.Controls.Add(label);
                label.BringToFront();
            }
        }

        public void PlaceFire(float GPSx, float GPSy)
        {

            PictureBox pictureBox = new PictureBox();
            Image image = Properties.Resources.pngegg;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Image = image;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Size = new Size(35, 35);
            pictureBox.Location = new Point((int)GPSx, (int)GPSy);
            if (panel1.InvokeRequired)
            {
                panel1.Invoke(new MethodInvoker(delegate { panel1.Controls.Add(pictureBox); }));
            }
        }


        public void PlaceRescue(float GPSx, float GPSy)
        {

            PictureBox pictureBox = new PictureBox();
            Image image = Properties.Resources.pngwing_com__2_;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Image = image;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Size = new Size(35, 35);
            pictureBox.Location = new Point((int)GPSx, (int)GPSy);
            if (panel1.InvokeRequired)
            {
                panel1.Invoke(new MethodInvoker(delegate { panel1.Controls.Add(pictureBox); }));
            }
        }

        public void MovePlane(Vector2 coords)
        {
            PictureBox pictureBox = new();
            Image image = Properties.Resources.Battle_bus_icon;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Image = image;
            pictureBox.Size = new Size(35, 35);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox.Location = new Point((int)coords.X, (int)coords.Y);
            if (panel1.InvokeRequired)
            {
                panel1.Invoke(new MethodInvoker(delegate { panel1.Controls.Add(pictureBox); }));
                panel1.Invoke(new MethodInvoker(delegate { pictureBox.BringToFront(); }));
            }

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
            bool changed = false;
            if (button2.Text == "Démarrer")
            {
                button2.Text = "Pause";
                button3.Enabled = true;
                button4.Enabled = true;
                button1.Enabled = false;
                changed = true;
            }
            if (button2.Text == "Pause" && changed == false)
            {
                button2.Text = "Démarrer";
                button3.Enabled = false;
                button4.Enabled = false;
                button1.Enabled = true;

            }
            simulatorController.Play();
        }

        public void setAirportsName()
        {
            string[] airportsTotal = simulatorController.AirportsToStrings();
            List<string> names = new List<string>();
            string[] temp;
            foreach (string airport in airportsTotal)
            {
                temp = airport.Split(",");
                names.Add(temp[0]);
            }
            foreach (string airport in names)
            {
                listBox1.Items.Add(airport);
            }

        }

        public void setAircrafts(string airportName)
        {
            string[] airportsTotal = simulatorController.AirportsToStrings();
            List<string> names = new List<string>();
            string[] temp;
            string[] temp2;
            string[] temp3;
            foreach (string item in airportsTotal)
            {
                temp = item.Split(".");
                temp2 = temp[1].Split(",");
                temp3 = item.Split(",");
                if (temp3[0] == airportName)
                {
                    names.Add(temp2[0]);
                }
            }
            foreach (string item in names)
            {
                listBox3.Items.Add(item);
            }

        }

        public void clearAll()
        {
            if (panel1.InvokeRequired)
            {
                panel1.Invoke(new MethodInvoker(delegate { panel1.Controls.Clear(); }));
            }
        }

        public void setClients(string airportName)
        {
            if (listBox2.InvokeRequired)
            {
                listBox2.Invoke(new MethodInvoker(delegate { listBox2.Items.Clear(); }));
            }
            else
            {
                listBox2.Items.Clear();
            }
            string[] airportsTotal = simulatorController.AirportsToStrings();
            string[] temp;
            string[] temp2;
            string[] temp3;
            string[] temp4;

            foreach (string item in airportsTotal)
            {
                temp = item.Split(",");
                if (temp[0] == airportName)
                {
                    temp2 = item.Split(";");
                    temp3 = temp2[1].Split(".");
                    for (int i = 0; i < temp3.Count() - 1; i++)
                    {
                        if (listBox2.InvokeRequired)
                        {
                            temp4 = temp3[i].Split(",");
                            listBox2.Invoke(new MethodInvoker(delegate { listBox2.Items.Add(temp4[1] + " " + temp4[2] + " en direction de " + temp4[0]); }));
                        }
                        else
                        {
                            temp4 = temp3[i].Split(",");
                            listBox2.Items.Add(temp4[1] + " " + temp4[2] + " en direction de " + temp4[0]);
                        }
                    }

                }
            }

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Text.Length > 0)
            {

                listBox3.Items.Clear();
                listBox2.Items.Clear();
                setAircrafts(listBox1.Text);
                setClients(listBox1.Text);
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void clearListboxes()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
        }

        public string getListbox1Selected()
        {
            string Text = String.Empty;
            Invoke((MethodInvoker)delegate
            {
                Text = listBox1.GetItemText(listBox1.Text);
            });
            return Text;
        }

        public void setTime(string time)
        {
            if (label6.InvokeRequired)
            {
                label6.Invoke(new MethodInvoker(delegate { label6.Text = time; }));
            }
            else
            {
                label6.Text = time;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            simulatorController.IncreaseSpeed();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            simulatorController.DecreaseSpeed();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormSimulator_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}