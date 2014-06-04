using MapLinkApi.Model;
using MapLinkApi.Model.Repository;
using MapLinkApi.Repository.RouteServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MapLinkApi.Repository
{
    public class RotaWcfRepository : IRotaRepository
    {
        public RouteTotal getTotalRoute(Address[] addresses, RouteConfigOptions RouteOption)
        {
            RouteTotal routeTotal = null;
            using (RouteServices.RouteSoapClient proxy = new RouteServices.RouteSoapClient())
            {
                RouteInfo routeInfo;
                IList<RouteStop> routeStoplist = new List<RouteStop>();

                foreach (Address addr in addresses)
                {
                    routeStoplist.Add(new RouteStop()
                    {
                        point = new Point() {
                            x = addr.Coordinates.X,
                            y = addr.Coordinates.Y
                        }
                    });                    
                }

                routeInfo = proxy.getRoute(routeStoplist.ToArray(), this.ConfigRouteOptions(RouteOption), RepositoryUtils.getTokenMapLink());

                routeTotal = new RouteTotal();
                routeTotal.Time = routeInfo.routeTotals.totalTime;
                routeTotal.Distance = routeInfo.routeTotals.totalDistance;
                routeTotal.FuelCost = routeInfo.routeTotals.totalfuelCost;
                routeTotal.TotalCostWithToll = routeInfo.routeTotals.totalCost;
            };
            return routeTotal; 
        }

        private RouteServices.RouteOptions ConfigRouteOptions(RouteConfigOptions RouteOption)
        {
            RouteServices.RouteOptions routeOptions = new RouteOptions();

            routeOptions.vehicle = new RouteServices.Vehicle()
            {
                averageConsumption = RouteOption.Vehicle.AverageConsumption,
                averageSpeed = RouteOption.Vehicle.AverageSpeed,
                fuelPrice = RouteOption.Vehicle.FuelPrice,
                tankCapacity = RouteOption.Vehicle.TankCapacity,
                tollFeeCat = (int)RouteOption.Vehicle.TollFeeCat,
            };
            routeOptions.routeDetails = new RouteDetails()
            {
                routeType = (int)RouteOption.RouteType
            };

            return routeOptions;
        }
    }
}
