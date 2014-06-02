using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using DataAnnotationsExtensions;
using WellaMates.Models.Validation;
using WellaMates.ViewModel;

namespace WellaMates.Models
{
    public enum Gender
    {
        [Display(Name = "Masculino")]
        Masculino,

        [Display(Name = "Feminino")]
        Feminino
    }

    public class UserAgreement
    {
        public const string CurrentVersion = "version1";
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string UserName { get; set; }
        
        public string RecoverPasswordToken { get; set; }
        public DateTime? RecoverPasswordExpiration { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string CurrentAcceptedAgreement { get; set; }

        [Display(Name = "Desejo receber informações via SMS sobre a Campanha")]
        public string AcceptSMS { get; set; }

        [Display(Name = "Desejo receber informações via E-mail sobre a Campanha")]
        public string AcceptEmail { get; set; }

        public PersonalInfo PersonalInfo { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public AddressInfo AddressInfo { get; set; }

        public virtual RefundProfile RefundProfile { get; set; }
    }
}