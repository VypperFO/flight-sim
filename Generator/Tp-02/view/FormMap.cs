namespace Tp_02.view
{
    /// <summary>
    /// formulaire contenant la map du monde 
    /// </summary>
    public partial class FormMap : Form
    {
        public FormGenerator formGenerator;
        private Point start; // coordonnes choisis par l'utilisateur sur la map


        /// <summary>
        /// initialise tout les components du formulaire
        /// </summary>
        public FormMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// affiche les cordonnes du click de l'utilisateur
        /// </summary>
        /// <param name="e">event</param>
        private void ClickSelection(EventArgs e)
        {

            MouseEventArgs meStart = (MouseEventArgs)e;
            Point coordinates = meStart.Location;
            start = coordinates;
            label1.Text = start.ToString();
        }

        /// <summary>
        /// ferme le form et set les coordonne dans le form generateur
        /// </summary>
        /// <param name="sender">qui a fait l'action</param>
        /// <param name="e">event</param>
        private void button1_Click(object sender, EventArgs e)
        {
            formGenerator.setCoords();
            Close();
        }

        /// <summary>
        /// event listener quand la map est cliquer
        /// </summary>
        /// <param name="sender">qui a fait l'action</param>
        /// <param name="e">event</param>
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ClickSelection(e);
        }


        /// <summary>
        /// converti les coordonne x,y en coordonne gps
        /// </summary>
        /// <param name="coords">les coordonne sous form x,y</param>
        /// <returns>>les coordonne sous form GPS</returns>
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

        /// <summary>
        /// transforme les coordonnes sous form gps
        /// </summary>
        /// <returns> les coordonnes sous form gps</returns>
        public string getCoords()
        {
            return convertCoordsToGPS(label1.Text);
        }
    }
}
