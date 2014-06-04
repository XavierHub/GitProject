using System;

namespace MapLinkApi.Model.Repository
{
    public interface IEnderecoRepository
    {
        Coordinates GetCoordenates(Address addressFind);
    }
}
