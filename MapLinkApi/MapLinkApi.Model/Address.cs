using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MapLinkApi.Model
{
    [DataContract]
    public class Address
    {
        [DataMember(IsRequired = true)]   
        [Required(AllowEmptyStrings=false)]  
        [StringLength(5000,MinimumLength=1 , ErrorMessage="The street field can not less than 1 and greater than 5000")]
        public string Street { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        public int? Number { get; set; } 
       
        public Coordinates Coordinates { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        public City City { get; set; }     
    }
}
