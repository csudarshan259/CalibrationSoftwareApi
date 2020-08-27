//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CalibrationSoftware.Models
{
    using FluentValidation;
    using FluentValidation.Attributes;
    using System;
    using System.Collections.Generic;
    
    [Validator(typeof(CustomerValidator))]
    public partial class CustomerMaster
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContactPersonName { get; set; }
        public string CustomerContactNumber { get; set; }
    }
    public class CustomerValidator: AbstractValidator<CustomerMaster>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.ID).NotNull();
            RuleFor(x => x.CustomerName).Length(0, 10);
            RuleFor(x => x.CustomerEmail).EmailAddress();
            RuleFor(x => x.CustomerContactPersonName).Length(0, 15);
            RuleFor(x => x.CustomerContactNumber).MinimumLength(10).MaximumLength(10);
        }
    }
}
