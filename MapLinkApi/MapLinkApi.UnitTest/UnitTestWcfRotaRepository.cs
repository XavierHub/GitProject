using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapLinkApi.Repository;
using MapLinkApi.Model;
using System.ServiceModel;

namespace UnitTestMapLinkApi
{
    [TestClass]
    public class UnitTestWcfRotaRepository
    {
        private Address _addressOrigem;
        private Address _addressDestino;
        private RotaWcfRepository _rotaWcfRepository;
        private RouteTotal _routeTotal;
        private RouteConfigOptions _routeOption;
               
        [TestInitialize]
        public void inicializeTest()
        {
            _routeTotal = new RouteTotal();
            _addressOrigem = new Address();
            _addressDestino = new Address();
            _rotaWcfRepository = new RotaWcfRepository();
            _routeOption = new RouteConfigOptions();

            _routeOption.RouteType = RouteType.DefaultAndFaster; 
            _routeOption.Vehicle = new Vehicle()
            {
                AverageConsumption = 12,
                AverageSpeed = 80,
                FuelPrice = 2.57,
                TankCapacity = 50,
                TollFeeCat = TollFeeCat.Automovel
            };
        }

        [TestMethod]
        public void obterValorTotalRota()
        {
            _addressOrigem.Coordinates = new Coordinates() { X = -46.6333094, Y = -23.5505199 };  //Sao Paulo
            _addressDestino.Coordinates = new Coordinates() { X = -43.1970773, Y = -22.9082998 }; //Rio de Janeiro  
            
            _routeTotal = _rotaWcfRepository.getTotalRoute(
                        new Address[] { _addressOrigem, _addressDestino },
                        _routeOption
                        );

            Assert.AreEqual(436.25, _routeTotal.Distance, 5);
            Assert.IsNotNull(_routeTotal.TotalCostWithToll);
            Assert.IsNotNull(_routeTotal.Time);            
        }

        [TestMethod]        
        public void obterValorTotalRotaEvitandoTransito()
        {
            _routeOption.RouteType = RouteType.AvoidingTraffic;

            _addressOrigem.Coordinates = new Coordinates() { X = -46.6333094, Y = -23.5505199 };  //Sao Paulo
            _addressDestino.Coordinates = new Coordinates() { X = -43.1970773, Y = -22.9082998 }; //Rio de Janeiro  

            _routeTotal = _rotaWcfRepository.getTotalRoute(
                        new Address[] { _addressOrigem, _addressDestino },
                        _routeOption
                        );

            Assert.AreEqual(433.28, _routeTotal.Distance, 5);
            Assert.IsNotNull(_routeTotal.TotalCostWithToll);
            Assert.IsNotNull(_routeTotal.Time);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void obterValorTotalRotaSemCoordenadas()
        {
            _routeTotal = _rotaWcfRepository.getTotalRoute(
                        new Address[] { },
                        _routeOption
                        );
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void obterValorTotalRotaSemConfigOpcoes()
        {
            _addressOrigem.Coordinates = new Coordinates() { X = -46.6333094, Y = -23.5505199 };  //Sao Paulo
            _addressDestino.Coordinates = new Coordinates() { X = -43.1970773, Y = -22.9082998 }; //Rio de Janeiro  

            _routeTotal = _rotaWcfRepository.getTotalRoute(
                        new Address[] { _addressOrigem, _addressDestino },
                        null
                        );
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        public void obterValorTotalRotaComCoordenadasIguais()
        {
            _addressOrigem.Coordinates = new Coordinates() { X = -46.6333094, Y = -23.5505199 };  //Sao Paulo
            _addressDestino.Coordinates = new Coordinates() { X = -46.6333094, Y = -23.5505199 }; //Rio de Janeiro  

            _routeTotal = _rotaWcfRepository.getTotalRoute(
                        new Address[] { _addressOrigem, _addressDestino },
                        _routeOption
                        );

            Assert.AreEqual(0, _routeTotal.Distance, 0.1);
            Assert.AreEqual(0, _routeTotal.TotalCostWithToll, 0.1);
            Assert.IsNotNull(_routeTotal.Time);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void obterValorTotalRotaComUnicaCoordenada()
        {
            _addressOrigem.Coordinates = new Coordinates() { X = -46.6333094, Y = -23.5505199 };  //Sao Paulo            

            _routeTotal = _rotaWcfRepository.getTotalRoute(
                        new Address[] { _addressOrigem},
                        _routeOption
                        );
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void obterValorTotalRotaNullParametros()
        {
            _routeTotal = _rotaWcfRepository.getTotalRoute(null, null);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void obterValorTotalRotaComCoordenadasZeradas()
        {
            _addressOrigem.Coordinates = new Coordinates() { X = 0, Y = 0 }; 
            _addressDestino.Coordinates = new Coordinates() { X = 0, Y = 0 };

            _routeTotal = _rotaWcfRepository.getTotalRoute(
                        new Address[] { _addressOrigem, _addressDestino },
                        _routeOption
                        );

            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void obterValorTotalRotaComUmaCoordenadaZeradas()
        {
            _addressOrigem.Coordinates = new Coordinates() { X = -46.6333094, Y = -23.5505199 };  //Sao Paulo    
            _addressDestino.Coordinates = new Coordinates() { X = 0, Y = 0 };

            _routeTotal = _rotaWcfRepository.getTotalRoute(
                        new Address[] { _addressOrigem, _addressDestino },
                        _routeOption
                        );
         
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void obterValorTotalRotaComCoordenadaValorMaximo()
        {
            _addressOrigem.Coordinates = new Coordinates() { X = -46.6333094, Y = -23.5505199 };  //Sao Paulo    
            _addressDestino.Coordinates = new Coordinates() { X = double.MaxValue, Y = double.MaxValue };

            _routeTotal = _rotaWcfRepository.getTotalRoute(
                        new Address[] { _addressOrigem, _addressDestino },
                        _routeOption
                        );

            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void obterValorTotalRotaComCoordenadaValorMinimo()
        {
            _addressOrigem.Coordinates = new Coordinates() { X = -46.6333094, Y = -23.5505199 };  //Sao Paulo    
            _addressDestino.Coordinates = new Coordinates() { X = double.MinValue, Y = double.MinValue };

            _routeTotal = _rotaWcfRepository.getTotalRoute(
                        new Address[] { _addressOrigem, _addressDestino },
                        _routeOption
                        );

            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }
    }
}
