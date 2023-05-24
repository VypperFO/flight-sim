using Tp_02.controller;
using Tp_02.view;

namespace Tp_02
{
    /// <summary>
    /// classe permettant de serialiser un scenario contenant des aeroports et plusieurs type d'avion.
    /// </summary>
    public partial class FormGenerator : Form
    {

        private FormMap formMap; // formulaire contenant la map du monde
        public GeneratorController GenController; // controlleur du logiciel

        /// <summary>
        /// initalise le formulaire
        /// </summary>
        public FormGenerator()
        {
            InitializeComponent();
            formMap = new FormMap();
            formMap.formGenerator = this;
        }

        /// <summary>
        /// instancie un nouveau formulaire de map quand un est fermer.
        /// </summary>
        /// <param name="sender">qui a fait l'action</param>
        /// <param name="e">event listener</param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (formMap.IsDisposed)
            {
                formMap = new FormMap();
                formMap.formGenerator = this;
            }
            formMap.Show();

        }

        /// <summary>
        /// Ouvre une message box qui confirme que le scenario a bien ete serialiser
        /// </summary>
        /// <param name="sender">qui a fait l'action</param>
        /// <param name="e">event listener</param>
        private void button3_Click(object sender, EventArgs e)
        {
            GenController.GenerateScenario();
            MessageBox.Show("Le scénario a bien été générer.", "Scénario Générer", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        /// <summary>
        /// set les coordonnes de form map dans le textbox de ce form
        /// </summary>
        public void setCoords()
        {
            textBox2­.Text = formMap.getCoords();
        }

        /// <summary>
        ///  ajoute un aeroport dans le scenario
        /// </summary>
        /// <param name="sender">>qui a fait l'action</param>
        /// <param name="e">>event listener</param>
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string position = textBox2.Text;
            string minPass = textBox3.Text;
            string maxPass = textBox4.Text;
            string minMerch = textBox5.Text;
            string maxMerch = textBox6.Text;

            if (name.Length > 0 && position.Length > 0 && minPass.Length > 0 && maxPass.Length > 0 && minMerch.Length > 0 && maxMerch.Length > 0 && minPass.All(char.IsDigit) && maxPass.All(char.IsDigit) && minMerch.All(char.IsDigit) && maxMerch.All(char.IsDigit) && Int32.Parse(minPass) < Int32.Parse(maxPass) && Int32.Parse(minMerch) < Int32.Parse(maxMerch))
            {
                if (!GenController.IsAirportNameExistent(name))
                {
                    listBox1.Items.Add(name + ", (" + position + ")," + minPass + ", " + maxPass + ", " + minMerch + ", " + maxMerch);
                    listBox1.SetSelected(listBox1.Items.Count - 1, true);
                    string[] airport = { name, position, minPass, maxPass, minMerch, maxMerch };
                    GenController.AddAirport(airport);
                }
                else
                {
                    MessageBox.Show("Ce nom est d'aéroport est déja utiliser veuiller en choisire un autre.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Veilleur entre les bonne données.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ajoute un avion dans un aeroport dans le scenario
        /// </summary>
        /// <param name="sender">qui a fait l'action</param>
        /// <param name="e">event listener</param>
        private void button2_Click(object sender, EventArgs e)
        {
            string Name = textBox12.Text;
            string Type = comboBox1.Text;
            string Capacity = textBox7.Text;
            string[] aircraft = { Name, Type, Capacity };
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Veilleur choisir un aéroport.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Name.Length > 0 && Type.Length > 0 && Capacity.Length > 0 && Capacity.All(char.IsDigit))
                {
                    if (!GenController.IsAircraftNameExistent(Name))
                    {
                        listBox2.Items.Add(Name + "," + Type + ", " + Capacity);
                        GenController.AddAirplane(listBox1.Text.Split(',')[0], aircraft);
                    }
                    else
                    {
                        MessageBox.Show("Ce nom est d'avion est déja utiliser veuiller en choisire un autre.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veilleur entre les bonne données.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// affiche les avions dans une aeroport
        /// </summary>
        /// <param name="sender">>qui a fait l'action</param>
        /// <param name="e">event listener</param>
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            List<string[]> aircraftList = GenController.GetAirplanList(listBox1.Text.Split(',')[0]);
            foreach (string[] aircraft in aircraftList)
            {
                string aircraftName = aircraft[0];
                string aircrafttype = aircraft[1];
                string aircraftcapacity = aircraft[2];
                string total = aircraftName + ", " + aircrafttype + ", " + aircraftcapacity;
                listBox2.Items.Add(total);
            }
        }
    }
}