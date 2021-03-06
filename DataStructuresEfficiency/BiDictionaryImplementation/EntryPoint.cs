﻿namespace BiDictionaryImplementation
{
    public class EntryPoint
    {
        public static void Main()
        {
            BiDictionary<string, string, int> distances = new BiDictionary<string, string, int>();

            distances.Add("Sofia", "Varna", 443);
            distances.Add("Sofia", "Varna", 468);
            distances.Add("Sofia", "Varna", 490);
            distances.Add("Sofia", "Plovdiv", 145);
            distances.Add("Sofia", "Bourgas", 383);
            distances.Add("Plovdiv", "Bourgas", 253);
            distances.Add("Plovdiv", "Bourgas", 292);

            var distancesFromSofia = distances.FindByKey1("Sofia"); // [443, 468, 490, 145, 383]
            var distancesToBourgas = distances.FindByKey2("Bourgas"); // [383, 253, 292]
            var distancesPlovdivBourgas = distances.Find("Plovdiv", "Bourgas"); // [253, 292]
            var distancesRousseVarna = distances.Find("Rousse", "Varna"); // []
            var distancesSofiaVarna = distances.Find("Sofia", "Varna"); // [443, 468, 490]
            bool isRemoved = distances.Remove("Sofia", "Varna"); // true
            var distancesFromSofiaAgain = distances.FindByKey1("Sofia"); // [145, 383]
            var distancesToVarna = distances.FindByKey2("Varna"); // []
            var distancesSofiaVarnaAgain = distances.Find("Sofia", "Varna"); // []
        }
    }
}
