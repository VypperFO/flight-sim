namespace Tp_02_02.model.Clients.SpecialClients
{
    /// <summary>
    /// Classe des clients de type feux
    /// </summary>
    public class FireClient : SpecialClient
    {
        public int Intensity { get; set; } // intensite du feux

        /// <summary>
        /// initialise le feux avec une intensite random
        /// </summary>
        public FireClient()
        {
            Intensity = new Random().Next(2, 4);
        }
    }
}
