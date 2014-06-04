using System;
using System.Runtime.Serialization;

namespace MapLinkApi.Model
{
    [DataContract]
    public class RouteTotal
    {
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public double Distance { get; set; }
        [DataMember]
        public double FuelCost { get; set; }
        [DataMember]
        public double TotalCostWithToll { get; set; }
    }
}
