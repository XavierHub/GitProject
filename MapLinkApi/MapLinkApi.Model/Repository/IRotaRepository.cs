using System;

namespace MapLinkApi.Model.Repository
{
    public interface IRotaRepository
    {
        RouteTotal getTotalRoute(Address[] addresses, RouteConfigOptions RouteOption);
    }
}
