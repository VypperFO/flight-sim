namespace Tp_02_02.model.Clients.SpecialClients
{
    public class FireClient : SpecialClient
    {
        public int Intensity { get; set; }

        public FireClient()
        {
            Intensity = new Random().Next(2, 4);
        }
    }
}
