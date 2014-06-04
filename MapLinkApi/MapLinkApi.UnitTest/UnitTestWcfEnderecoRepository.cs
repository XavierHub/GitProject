using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapLinkApi.Repository;
using MapLinkApi.Model;
using System.ServiceModel;

namespace UnitTestMapLinkApi
{
    [TestClass]
    public class UnitTestWcfEnderecoRepository
    {
        private Coordinates _coordinates;
        private Address _address;
        private EnderecoWcfRepository _enderecoWcfRepository;

        [TestInitialize]
        public void inicializeTest() {
            _coordinates = null;
            _address = new Address();
            _enderecoWcfRepository = new EnderecoWcfRepository();
        }

        [TestMethod]
        public void obterCoordenadasEndereco()
        {
            _address.Street =  "Jurubatuba";
            _address.City = new City() { Name = "São Bernardo do campo", State = "São Paulo" };            
            _address.Number = 1447;

            _coordinates = _enderecoWcfRepository.GetCoordenates(_address);

            Assert.AreEqual(-46.5532181, _coordinates.X, 0.01);
            Assert.AreEqual(-23.7100941, _coordinates.Y, 0.01);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void obterCoordenadasEnderecoInexistente()
        {
            _address.Street = "Pindaíba";
            _address.City = new City() { Name = "São Bernardo do campo", State = "São Paulo" };            
            _address.Number = 123;

            _coordinates = _enderecoWcfRepository.GetCoordenates(_address);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void obterCoordenadasEnderecoSemNomeRua()
        {
            _address.City = new City() { Name = "São Bernardo do campo", State = "São Paulo" };            
            _address.Number = 1447;
            
            _coordinates = _enderecoWcfRepository.GetCoordenates(_address);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]   
        public void obterCoordenadasEnderecoSemNumero()
        {
            _address.Street = "Jurubatuba";
            _address.City = new City() { Name = "São Bernardo do campo", State = "São Paulo" };                       

            _coordinates = _enderecoWcfRepository.GetCoordenates(_address);

            Assert.AreEqual(-46.5529478, _coordinates.X, 0.01);
            Assert.AreEqual(-23.6966355, _coordinates.Y, 0.01);
        }

        [TestMethod]        
        public void obterCoordenadasEnderecoSemEstado()
        {
            _address.Street = "Jurubatuba";
            _address.City = new City() { Name = "São Bernardo do campo" };            
            _address.Number = 1447;

            _coordinates = _enderecoWcfRepository.GetCoordenates(_address);

            Assert.AreEqual(-46.5532181, _coordinates.X, 0.01);
            Assert.AreEqual(-23.7100941, _coordinates.Y, 0.01);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void obterCoordenadasEnderecoSemNomeCidade()
        {
            _address.Street = "Jurubatuba";
            _address.City = new City() {State = "São Paulo" };            
            _address.Number = 1447;
            
            _coordinates = _enderecoWcfRepository.GetCoordenates(_address);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void obterCoordenadasEnderecoSemNomeCidadeEEstado()
        {
            _address.Street = "Jurubatuba";           
            _address.Number = 1447;

            _coordinates = _enderecoWcfRepository.GetCoordenates(_address);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void obterCoordenadasEnderecoSemNomeCidadeEstadoENumero()
        {
            _address.Street = "Jurubatuba";            
            _coordinates = _enderecoWcfRepository.GetCoordenates(_address);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void obterCoordenadasEnderecoSemNomeCidadeENumero()
        {
            _address.Street = "Jurubatuba";
            _address.City = new City() { State = "São Paulo" };
            
            _coordinates = _enderecoWcfRepository.GetCoordenates(_address);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void obterCoordenadasNullParametros()
        {
            _coordinates = _enderecoWcfRepository.GetCoordenates(null);
            Assert.Fail("Se você chegou aqui, a exceção de validação não aconteceu");
        }
    }
}
