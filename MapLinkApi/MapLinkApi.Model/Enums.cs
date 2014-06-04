using System.Runtime.Serialization;

namespace MapLinkApi.Model
{
    [DataContract]
    public enum RouteType
    {
        [EnumMember]
        DefaultAndFaster = 1,
        [EnumMember]
        
        AvoidingTraffic = 23
    }

    public enum TollFeeCat
    { 
        SemCalculoPedagio = 0,
        Automovel = 2
    }
}