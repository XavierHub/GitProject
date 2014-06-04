using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using MapLinkApi.Dependency;
using MapLinkApi.Service;
using Services.Maplink.Contracts;
using MapLinkApi.Model;
using System.ServiceModel;
using Ninject.Extensions.Wcf;


namespace UnitTestMapLinkApi
{
    [TestClass]
    public class UnitTestIntegrationMapLinkApi
    {
        private Address _addressOrigem;
        private Address _addressDestino;
        private IKernel _kernel;
        private RouteService.RouteTotalServiceClient _proxy;

        [TestInitialize()]
        public void inicializeTest()
        {
            _addressOrigem = new Address();
            _addressDestino = new Address();
            _proxy = new RouteService.RouteTotalServiceClient();
            _proxy.Open();
        }

        [TestCleanup]
        public void TearDown()
        {
            if (_proxy != null)
                _proxy.Close();
        }


        [TestMethod]
        public void obterValorTotalRotaPadraoMaisRapida()
        {
            _addressOrigem.Street = "Jurubatuba";
            _addressOrigem.City = new City() { Name = "São Bernardo do campo", State = "São Paulo" };
            _addressOrigem.Number = 1447;

            _addressDestino.Street = "Av. Paulista";
            _addressDestino.City = new City() { Name = "São Paulo", State = "São Paulo" };
            _addressDestino.Number = 1200;

            var routeTotal = _proxy.GetRouteTotalByAddress(new Address[] { _addressOrigem, _addressDestino }, RouteType.DefaultAndFaster);

            Assert.AreEqual(21.56, routeTotal.Distance, 5);
            Assert.AreEqual(routeTotal.TotalCostWithToll, routeTotal.TotalCostWithToll, 1);
            Assert.IsNotNull(routeTotal.Time);
        }

        [TestMethod]
        public void obterValorTotalRotaEvitandoTransito()
        {
            _addressOrigem.Street = "Jurubatuba";
            _addressOrigem.City = new City() { Name = "São Bernardo do campo", State = "São Paulo" };
            _addressOrigem.Number = 1447;

            _addressDestino.Street = "Av. Paulista";
            _addressDestino.City = new City() { Name = "São Paulo", State = "São Paulo" };
            _addressDestino.Number = 1200;

            var routeTotal = _proxy.GetRouteTotalByAddress(new Address[] { _addressOrigem, _addressDestino }, RouteType.AvoidingTraffic);

            Assert.AreEqual(29.92, routeTotal.Distance, 5);
            Assert.AreEqual(routeTotal.TotalCostWithToll, routeTotal.TotalCostWithToll, 1);
            Assert.IsNotNull(routeTotal.Time);
        }

        [TestMethod]
        public void obterValorTotalRotaEnderecosIguais()
        {
            _addressOrigem.Street = "Jurubatuba";
            _addressOrigem.City = new City() { Name = "São Bernardo do campo", State = "São Paulo" };
            _addressOrigem.Number = 1447;

            _addressDestino.Street = "Jurubatuba";
            _addressDestino.City = new City() { Name = "São Bernardo do campo", State = "São Paulo" };
            _addressDestino.Number = 1447;

            var routeTotal = _proxy.GetRouteTotalByAddress(new Address[] { _addressOrigem, _addressDestino }, RouteType.DefaultAndFaster);

            Assert.AreEqual(0, routeTotal.Distance, 1);
            Assert.AreEqual(routeTotal.TotalCostWithToll, routeTotal.TotalCostWithToll, 1);
            Assert.IsNotNull(routeTotal.Time);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<RouteService.ValidationFault>))]
        public void obterValorTotalRotaUnicoEndereco()
        {
            _addressOrigem.Street = "Jurubatuba";
            _addressOrigem.City = new City() { Name = "São Bernardo do campo", State = "São Paulo" };
            _addressOrigem.Number = 1447;

            var routeTotal = _proxy.GetRouteTotalByAddress(new Address[] { _addressOrigem }, RouteType.DefaultAndFaster);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<RouteService.ValidationFault>))]
        public void obterValorTotalRotaSemNomeDaRua()
        {
            _addressOrigem.City = new City() { Name = "São Bernardo do campo", State = "São Paulo" };
            _addressOrigem.Number = 1447;

            _addressDestino.City = new City() { Name = "São Paulo", State = "São Paulo" };
            _addressDestino.Number = 1200;

            var routeTotal = _proxy.GetRouteTotalByAddress(new Address[] { _addressOrigem, _addressDestino }, RouteType.DefaultAndFaster);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<RouteService.ValidationFault>))]
        public void obterValorTotalRotaSemNomeDaRuaECidade()
        {
            _addressOrigem.Number = 1447;
            _addressDestino.Number = 1200;

            var routeTotal = _proxy.GetRouteTotalByAddress(new Address[] { _addressOrigem, _addressDestino }, RouteType.DefaultAndFaster);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<RouteService.ValidationFault>))]
        public void obterValorTotalRotaSemEnderecos()
        {
            var routeTotal = _proxy.GetRouteTotalByAddress(new Address[] { }, RouteType.DefaultAndFaster);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<RouteService.ValidationFault>))]
        public void obterValorTotalRotaSomenteNomeDaRua()
        {
            _addressOrigem.Street = "Jurubatuba";            
            _addressDestino.Street = "Av. Paulista";
            
            var routeTotal = _proxy.GetRouteTotalByAddress(new Address[] { _addressOrigem, _addressDestino }, RouteType.DefaultAndFaster);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }


        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void obterValorTotalRotaSemNomeCidade()
        {
            _addressOrigem.Street = "Jurubatuba";
            _addressOrigem.City = new City() { State = "São Paulo" };
            _addressOrigem.Number = 1447;

            _addressDestino.Street = "Av. Paulista";
            _addressDestino.City = new City() { State = "São Paulo" };
            _addressDestino.Number = 1200;

            var routeTotal = _proxy.GetRouteTotalByAddress(new Address[] { _addressOrigem, _addressDestino }, RouteType.DefaultAndFaster);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }
    }
}
