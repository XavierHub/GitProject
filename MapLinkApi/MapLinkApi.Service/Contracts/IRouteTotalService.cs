using MapLinkApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MapLinkApi.Service.Constants;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using EntLibContrib.Validation.Validators;

namespace Services.Maplink.Contracts
{
    [ValidationBehavior]
    [ServiceContract(Namespace = ServiceConstants.NAMESPACE_SERVICE)]        
    public interface IRouteTotalService
    {
        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        RouteTotal GetRouteTotalByAddress(
                [CollectionCountValidator(2, RangeBoundaryType.Inclusive, 100, RangeBoundaryType.Ignore)]
                [ObjectCollectionValidator(typeof(Address))]
            Address[] addresses,
                [NotNullValidator(MessageTemplate = "The routeType field is required")]
            RouteType routeType            
        );
    }
}
