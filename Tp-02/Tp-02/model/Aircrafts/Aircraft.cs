namespace Tp_02.model.Aircrafts
{
    /// <summary>
    /// Class Aircraft conteant les donnees membre minimum de tout Aircraft
    /// </summary>
    [Serializable]
    public class Aircraft
    {
        public string Name { get; set; }
        public int Capacity { get; set; }

        public Aircraft() { }
    }
}
