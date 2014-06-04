using System;

namespace MapLinkApi.Model
{
    public class Vehicle
    {
        public float AverageConsumption { get; set; }
        public int AverageSpeed { get; set; }
        public double FuelPrice { get; set; }
        public int TankCapacity { get; set; }
        public TollFeeCat TollFeeCat { get; set; }
    }
}
