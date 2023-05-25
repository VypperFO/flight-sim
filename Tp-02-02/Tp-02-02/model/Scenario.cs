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
        public List<SpecialClient> SpecialClientList { get; set; } // liste de tout les clients special dans le scenario
        private State state; // etat du scenario 
        public int speed { get; set; } // vitesse du scenario
        public double time { get; set; } //temps du scenario en secondes

        public Scenario()
        {
            speed = 1000;
            time = 0;
            state = new ReadyState(this);
            AirportList = new List<Airport>();
        }

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

        public Airport getNearestAirport(Vector2 position, string type)
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



        private void AssignEmergencyClients(SpecialClient emergencyClient)
        {
            Airport closest = getNearestAirport(emergencyClient.Position, emergencyClient.GetType().Name);
            if (closest != null)
            {
                closest.ClientList.Add(emergencyClient);
            }
        }

        private void InjectSpecialClients()
        {
            ClientFactory clientFactory = new ClientFactory();
            if (time % 1800 == 0)
            {
                Console.WriteLine("Fire request");
                SpecialClient fireClient = clientFactory.CreateSpecialClientWithRandomPos("Fire");
                AssignEmergencyClients(fireClient);
            }

            // CHANGE BACK TO 3600 THIS MESSSAGE IS APPROVED BY BARACK OBAMA. "I AM BARACK OBAMA AND I APPROVE THIS MESSAGE -_-"
            if (time % 3600 == 0)
            {
                Console.WriteLine("Rescue request");
                SpecialClient rescueClient = clientFactory.CreateSpecialClientWithRandomPos("Rescue");
                AssignEmergencyClients(rescueClient);
            }

            if (time % 1200 == 0)
            {
                Console.WriteLine("Observer request");
                SpecialClient observerClient = clientFactory.CreateSpecialClientWithRandomPos("Observer");
                AssignEmergencyClients(observerClient);
            }

        }

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

        public string giveMeTheTime()
        {
            double f = time;
            TimeSpan t = TimeSpan.FromSeconds(f);
            return string.Format("{0}:{1}:{2}:{3}", ((int)t.TotalHours), t.Minutes, t.Seconds, t.Milliseconds);
        }

        public State GetState() { return state; }

        public void changeState(State state)
        {
            this.state = state;
        }

        // Facade
        public void Play() { state.PlayStop(); }

        // Facade
        public void Forward() { state.Forward(); }
    }
}
