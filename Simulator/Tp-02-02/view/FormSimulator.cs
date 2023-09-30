using System.Numerics;
using System.Security;
using System.Text.RegularExpressions;
using Tp_02_02.controller;

namespace Tp_02_02
{
    /// <summary>
    /// Classe du  formulaire du simulateur de scenario
    /// </summary>
    public partial class FormSimulator : Form
    {
        private COTAI cotai; // controlleur du form

        /// <summary>
        /// constructeur du form
        /// </summary>
        /// <param name="simulatorController">Controlleur du form</param>
        public FormSimulator(COTAI simulatorController)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            this.cotai = simulatorController;
        }

        /// <summary>
        /// Active le button demarer du scenario quand un fichier XML est choissis
        /// </summary>
        /// <param name="sender">objet qui a fait l'action</param>
        /// <param name="e">evenet listener</param>
        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            button1.Enabled = false;
        }

        /// <summary>
        /// Ouvre le dialog de choix de fichier pour choisir le scenario
        /// </summary>
        /// <param name="sender">>objet qui a fait l'action</param>
        /// <param name="e">>evenet listener</param>
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        /// <summary>
        /// Convertis les coordonnees de type GPS en coordonnes de type (X,Y)
        /// </summary>
        /// <param name="gpsCoords">les coordonnees sous forme GPS</param>
        /// <returns>les coordonnees sous forme (X,Y)</returns>
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

        /// <summary>
        /// Convertis les degres en decimal
        /// </summary>
        /// <param name="degrees">degre qui serons transformer </param>
        /// <param name="minutes">degre en minutes qui seront transformer </param>
        /// <param name="seconds">degre en seconmds qui seront transformer</param>
        /// <returns>les degres convertis en decimal</returns>
        private double ConvertToDecimalDegrees(double degrees, double minutes, double seconds)
        {
            double decimalDegrees = degrees + (minutes / 60) + (seconds / 3600);
            return decimalDegrees;
        }

        /// <summary>
        /// Convertis la longitude en coordone de map
        /// </summary>
        /// <param name="longitude">la longitude</param>
        /// <param name="mapWidth">la largeur de la map</param>
        /// <returns>la longitude convertis en coordonne de map</returns>
        private double ConvertToX(double longitude, int mapWidth)
        {
            double x = (longitude + 180) * (mapWidth / 360.0);
            return x;
        }

        /// <summary>
        /// Convertis la latitude en coordone de map
        /// </summary>
        /// <param name="latitude">la latitude</param>
        /// <param name="mapHeight">la hauteur de la map</param>
        /// <returns>la latitude convertis en coordonne de map</returns>
        private double ConvertToY(double latitude, int mapHeight)
        {
            double y = (90 - latitude) * (mapHeight / 180.0);
            return y;
        }

        /// <summary>
        /// Place les aeroport et leur nom sur la map
        /// </summary>
        /// <param name="gpsCoords">Coordonne de l'aeroport</param>
        /// <param name="name">nom de l'aeroport</param>
        public void PlaceAirportsOnMap(string gpsCoords, string name)
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

            GC.Collect();
        }

        /// <summary>
        /// Place un feu sur la map
        /// </summary>
        /// <param name="GPSx">coordonne X du feu</param>
        /// <param name="GPSy">coordonne Y du feu</param>
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
                panel1.Invoke(new MethodInvoker(delegate
                {
                    panel1.Controls.Add(pictureBox);
                }));
            }
        }

        /// <summary>
        /// Place un rescue sur la map
        /// </summary>
        /// <param name="GPSx">>coordonne X du rescue</param>
        /// <param name="GPSy">>coordonne Y du rescue</param>
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
                panel1.Invoke(new MethodInvoker(delegate
                {
                    panel1.Controls.Add(pictureBox);
                }));
            }

        }

        /// <summary>
        /// Deplace l'image d'un avion au coordonne donner en paramettre
        /// </summary>
        /// <param name="coords">coordonne de l'avion</param>
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

        /// <summary>
        /// Active ou desactive le bouton pour demarrer ou arreter la partis
        /// </summary>
        /// <param name="enabled">true pour l'activer, false pour le desactiver</param>
        public void SetPlayBtnEnable(bool enabled)
        {
            button2.Enabled = enabled;
        }

        /// <summary>
        ///  Load le fichier selectionner dans le dialogue
        /// </summary>
        /// <param name="sender">personne qui a fait l'action</param>
        /// <param name="e">event listener</param>
        private void SelectFileBtn_click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    using (Stream str = openFileDialog1.OpenFile())
                    {
                        cotai.Load(filePath);
                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        /// <summary>
        /// Desactive ou active les boutons en dependant de si le scenario roule ou non
        /// </summary>
        /// <param name="sender">personne qui a fait l'action</param>
        /// <param name="e">event listener</param>
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
                cotai.resetSpeed();

            }
            cotai.Play();
        }

        /// <summary>
        /// Met les noms des aeroport dans la listbox du form
        /// </summary>
        public void setAirportsName()
        {
            string[] airportsTotal = cotai.AirportsToStrings();
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

        /// <summary>
        /// Met les avions de l'aeroport selectioner dans la listbox du form
        /// </summary>
        /// <param name="airportName">le nom de l'aeroport selectionner</param>
        public void setAircrafts(string airportName)
        {
            string[] airportsTotal = cotai.AirportsToStrings();
            List<string> names = new List<string>();
            string[] temp;
            string[] temp2;
            string[] temp4;
            string[] temp3;


            foreach (string item in airportsTotal)
            {
                temp = item.Split(".");
                temp4 = temp[1].Split(';');
                temp2 = temp4[0].Split(",");
                temp3 = item.Split(",");
                if (temp3[0] == airportName)
                {
                    foreach (string item2 in temp2)
                    {
                        names.Add(item2);
                    }
                }
            }
            foreach (string item in names)
            {
                listBox3.Items.Add(item);
            }

        }

        /// <summary>
        /// Supprimer tout les controls du panel du form
        /// </summary>
        public void clearAll()
        {
            if (panel1.InvokeRequired)
            {
                panel1.Invoke(new MethodInvoker(delegate { panel1.Controls.Clear(); }));
            }

            GC.Collect();
        }

        /// <summary>
        /// Met les clients de l'aeroport selectioner dans la listbox du form
        /// </summary>
        /// <param name="airportName">le nom de l'aeroport selectionner</param>
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
            string[] airportsTotal = cotai.AirportsToStrings();
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

        /// <summary>
        /// Change les listbox clients et avions en dependant de l'aeroport choissis.
        /// </summary>
        /// <param name="sender">personne qui a fait l'aciton</param>
        /// <param name="e">event listener</param>
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

        /// <summary>
        /// Vide tout les listbox du form
        /// </summary>
        public void clearListboxes()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
        }

        /// <summary>
        /// Donne le texte selectionner dans le list box des aeroport 
        /// </summary>
        /// <returns>le texte selectionner dans le list box des aeroport </returns>
        public string getListbox1Selected()
        {
            string Text = String.Empty;
            Invoke((MethodInvoker)delegate
            {
                Text = listBox1.GetItemText(listBox1.Text);
            });
            return Text;
        }

        /// <summary>
        /// Actualise le temps dans le form pour le temps du scenario
        /// </summary>
        /// <param name="time"></param>
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

        /// <summary>
        /// Accelere la vitesse du scenario
        /// </summary>
        /// <param name="sender">Personne qui a fait l'action</param>
        /// <param name="e">Event listener</param>
        private void button3_Click(object sender, EventArgs e)
        {
            cotai.IncreaseSpeed();
        }

        /// <summary>
        /// decelere la vitesse du scenario
        /// </summary>
        /// <param name="sender">Personne qui a fait l'action</param>
        /// <param name="e">Event listener</param>
        private void button4_Click(object sender, EventArgs e)
        {
            cotai.DecreaseSpeed();
        }

        /// <summary>
        /// dessine la ligne du vol d'un avion
        /// </summary>
        /// <param name="startPoint">point de depart de la ligne</param>
        /// <param name="endPoint">point de fin de la ligne<</param>
        /// <param name="color">couleur de la ligne</param>
        public void DrawLine(Point startPoint, Point endPoint, Color color)
        {
            using (Graphics graphics = panel1.CreateGraphics())
            using (Pen pen = new Pen(color, 3))
            {
                graphics.DrawLine(pen, startPoint, endPoint);
                pen.Dispose();
            }
        }

        /// <summary>
        /// Ferme le programme quand le bouton x est fermer
        /// </summary>
        /// <param name="sender">Personne qui a fait l'action</param>
        /// <param name="e">event listener</param>
        private void FormSimulator_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }
    }
}