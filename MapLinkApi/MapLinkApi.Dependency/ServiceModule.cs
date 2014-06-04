using MapLinkApi.Model.Repository;
using MapLinkApi.Repository;
using Ninject.Modules;

namespace MapLinkApi.Dependency
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IEnderecoRepository>().To<EnderecoWcfRepository>();
            Kernel.Bind<IRotaRepository>().To<RotaWcfRepository>();
        }
    }    
}
