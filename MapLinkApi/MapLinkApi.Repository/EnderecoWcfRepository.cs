using MapLinkApi.Model;
using MapLinkApi.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapLinkApi.Repository
{
    public class EnderecoWcfRepository : IEnderecoRepository
    {
        public Coordinates GetCoordenates(Address addressFind)
        {
            Coordinates coordinates = null;

            using(AddressServices.AddressFinderSoapClient proxy = new AddressServices.AddressFinderSoapClient()){

                AddressServices.Point point = null;

                coordinates = new Coordinates();
                AddressServices.Address address = new AddressServices.Address();
                
                address.street = addressFind.Street;
                address.houseNumber = addressFind.Number.ToString();
                address.city = new AddressServices.City() {
                    name = addressFind.City.Name,
                    state = addressFind.City.State
                };
                
                point = proxy.getXY(address, RepositoryUtils.getTokenMapLink());

                coordinates.X = point.x;
                coordinates.Y = point.y;
            }   
         
            return coordinates;
        }
    }
}
