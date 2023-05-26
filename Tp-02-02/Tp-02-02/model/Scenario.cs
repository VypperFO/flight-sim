using System.Numerics;
using Tp_02_02.model.Clients;
using Tp_02_02.model.Clients.SpecialClients;
using Tp_02_02.model.States;

namespace Tp_02_02.model
{
    /// <summary>
    /// Scenario contenant les aeroport,les avions et les clients
    /// </summary>
    public class Scenario
    {
        public List<Airport> AirportList { get; set; } // liste de tout les aeroports dans le scenario
        private State state; // etat du scenario 
        public int speed { get; set; } // vitesse du scenario
        public double time { get; set; } //temps du scenario en secondes

        /// <summary>
        /// initialise les donnes membre du scenario
        /// </summary>
        public Scenario()
        {
            speed = 1000;
            time = 0;
            state = new ReadyState(this);
            AirportList = new List<Airport>();
        }

        #region Main operations

        /// <summary>
        /// Effectue un tick du scenario
        /// </summary>
        /// <returns>le scenario apres le tick a ete fait</returns>
        public Scenario PerformOperations()
        {
            for (int i = 0; i < AirportList.Count; i++)
            {
                AirportList = AirportList[i].RunAirport(AirportList);
            }

            InjectSpecialClients();
            InjectTransportClients();

            return this;
        }

        /// <summary>
        /// Creer des client special dans le scenario a des moments random
        /// </summary>
        private void InjectSpecialClients()
        {
            ClientFactory clientFactory;
            if (time % 1800 == 0)
            {
                clientFactory = new ClientFactory();
                SpecialClient fireClient = clientFactory.CreateSpecialClientWithRandomPos("Fire");
                AssignEmergencyClients(fireClient);
            }

            if (time % 3600 == 0)
            {
                clientFactory = new ClientFactory();
                SpecialClient rescueClient = clientFactory.CreateSpecialClientWithRandomPos("Rescue");
                AssignEmergencyClients(rescueClient);
            }

            if (time % 1200 == 0)
            {
                clientFactory = new ClientFactory();
                SpecialClient observerClient = clientFactory.CreateSpecialClientWithRandomPos("Observer");
                AssignEmergencyClients(observerClient);
            }
        }

        /// <summary>
        /// Creer des client dans le scenario a des moments random
        /// </summary>
        private void InjectTransportClients()
        {
            if (time % 3600 == 0)
            {
                for (int i = 0; i < AirportList.Count; i++)
                {
                    AirportList[i].InjectClients(AirportList);
                }
            }
        }

        #endregion

        #region Utils

        /// <summary>
        /// assigne un client special a un aeroport
        /// </summary>
        /// <param name="emergencyClient">le client special</param>
        private void AssignEmergencyClients(SpecialClient emergencyClient)
        {
            Airport closest = getNearestAirport(emergencyClient.Position, emergencyClient.GetType().Name);
            if (closest != null)
            {
                closest.ClientList.Add(emergencyClient);
            }
        }

        /// <summary>
        /// Donne l'aeroport le plus proche du client donner
        /// </summary>
        /// <param name="position">la position du client que l'areoport est comparer a</param>
        /// <param name="type">le type de client</param>
        /// <returns>l'aeroport la plus proche du client</returns>
        private Airport getNearestAirport(Vector2 position, string type)
        {
            int index = 0;
            float closest = 0;
            float tempDistance;
            bool isOk = false;
            for (int i = 0; i < AirportList.Count; i++)
            {
                if (type.Equals("FireClient"))
                {
                    isOk = AirportList[i].contains("fire");
                }
                else if (type.Equals("RescueClient"))
                {
                    isOk = AirportList[i].contains("helicopter");
                }
                else if (type.Equals("ObserverClient"))
                {
                    isOk = AirportList[i].contains("observer");
                }
                else
                {
                    isOk = false;
                }

                if (isOk)
                {
                    Vector2 pos = AirportList[i].ConvertFromGPSToCoords(AirportList[i].Coords);
                    tempDistance = Vector2.Distance(pos, position);
                    if (closest == 0)
                    {
                        closest = tempDistance;
                        index = i;
                    }
                    else if (tempDistance < closest)
                    {
                        closest = tempDistance;
                        index = i;
                    }


                }
            }
            return AirportList[index];
        }

        /// <summary>
        /// Affiche le temps du scenario
        /// </summary>
        /// <returns>le temps du scenario</returns>
        public string giveMeTheTime()
        {
            double f = time;
            TimeSpan t = TimeSpan.FromSeconds(f);
            return string.Format("{0}:{1}:{2}:{3}", ((int)t.TotalHours), t.Minutes, t.Seconds, t.Milliseconds);
        }

        /// <summary>
        /// Donne le state du scenario en se moment
        /// </summary>
        /// <returns>le state du scenario</returns>
        public State GetState() { return state; }

        /// <summary>
        /// Change le state du scenario pour lui donner en parametre
        /// </summary>
        /// <param name="state">state qui sera affecter au scenario</param>
        public void changeState(State state)
        {
            this.state = state;
        }

        // Facade
        public void Play() { state.PlayStop(); }

        // Facade
        public void Forward() { state.Forward(); }

        #endregion
    }
}
