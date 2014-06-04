using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MapLinkApi.Model
{
    [DataContract]
    public class City
    {
        [DataMember(IsRequired = true)]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "The city field can not less than 1 and greater than 5000")]
        [Required(AllowEmptyStrings=false, ErrorMessage = "The city field can not be blank")]
        [NotNullValidator(MessageTemplate = "The cyt name field can not be null")]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "The state field can not less than 1 and greater than 5000")]
        [Required(AllowEmptyStrings = false)]
        [NotNullValidator(MessageTemplate="The state field can not be null")]
        public string State { get; set; }
    }
}
