using System;
using CodingCampusCSharpHomework;
using System.Collections.Generic;
using System.Linq;

namespace HomeworkTemplate
{
    public struct DefibliratorInfo : IComparable<DefibliratorInfo>
    {
        public DefibliratorInfo(string name, string address, float dist)
        {
            Name = name;
            Address = address;
            Distance = dist;
        }

        public string Name { get; }
        public string Address { get; }
        public float Distance { get; }
        
        public override string ToString() => $"Name: {Name}; Address: {Address}";

        public int CompareTo(DefibliratorInfo other)
        {
            return Distance.CompareTo(other.Distance);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Func<Task3, string> TaskSolver = task =>
            {
                // Your solution goes here
                // You can get all needed inputs from task.[Property]
                // Good luck!
                string UserLongitude = task.UserLongitude;
                string UserLatitude = task.UserLatitude;
                int placesAmount = task.DefibliratorStorages.Length;
                
                float userLon, userLat;
                float.TryParse(UserLongitude, out userLon);
                float.TryParse(UserLatitude, out userLat);

                var defbInfoList = new List<DefibliratorInfo>();
                for (int i = 0; i < placesAmount; i++)
                {
                    string defibliratorStorage = task.DefibliratorStorages[i];
                    string[] words = defibliratorStorage.Split(';');

                    float lon, lat;
                    if (!float.TryParse(words[2], out lon)
                     || !float.TryParse(words[3], out lat))
                    {
                        continue;
                    }
                    float dist = Distance(userLon, userLat, lon, lat);
                    defbInfoList.Add(new DefibliratorInfo(words[0],words[1], dist));
                }   

                return defbInfoList.Min().ToString();
            };

            Task3.CheckSolver(TaskSolver);
        }

        static float Distance(float lonA, float latA, float lonB, float latB)
        {
            const float EARTH_RADIUS = 6371.0f;
            float lonARad = ConvertDegreesToRadians(lonA);
            float latARad = ConvertDegreesToRadians(latA);
            float lonBRad = ConvertDegreesToRadians(lonB);
            float latBRad = ConvertDegreesToRadians(latB);

            float x = (lonBRad - lonARad) * MathF.Cos((latARad + latBRad) / 2.0f);
            float y = (latBRad - latARad);
            return MathF.Sqrt(x*x + y*y) * EARTH_RADIUS;
        }
        public static float ConvertDegreesToRadians (float degrees)
        {
            return (MathF.PI / 180.0f) * degrees;
        }
    }
}
