using MapLinkApi.Model;
using MapLinkApi.Model.Repository;
using MapLinkApi.Repository;
using MapLinkApi.Service.Constants;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Services.Maplink.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MapLinkApi.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Namespace = ServiceConstants.NAMESPACE_SERVICE)]    
    public class RouteService : IRouteTotalService
    {
        private IEnderecoRepository _enderecoRepository;
        private IRotaRepository _rotaRepository;

        public RouteService(IEnderecoRepository enderecoRepository, IRotaRepository rotaRepository)
        {
            this._enderecoRepository = enderecoRepository;
            this._rotaRepository = rotaRepository;
        }

        /// <summary>
        /// Serviço que retorna o total da rota
        /// </summary>
        /// <param name="addresses"></param>
        /// <param name="routeType"></param>
        /// <returns></returns>
        public RouteTotal GetRouteTotalByAddress(Address[] addresses, RouteType routeType)
        {

            Validator collValidator
          = new ObjectCollectionValidator(typeof(Address));

            // Validate all of the objects in the collection.
            ValidationResults results = collValidator.Validate(addresses);


            RouteConfigOptions routeOptions = this.GetDefaultOptions();
            routeOptions.RouteType = routeType;

            foreach (Address addr in addresses)
            {
                addr.Coordinates = this._enderecoRepository.GetCoordenates(addr);
            }

            return this._rotaRepository.getTotalRoute(addresses, routeOptions);
        }
       

        #region DefaultOptions
        private RouteConfigOptions GetDefaultOptions()
        {
            return new RouteConfigOptions()
            {
                Vehicle = new Vehicle()
                {
                    AverageConsumption = 12,
                    AverageSpeed = 80,
                    FuelPrice = 2.67,
                    TankCapacity = 55,
                    TollFeeCat = TollFeeCat.Automovel
                }
            };
        }
        #endregion
    }
}
