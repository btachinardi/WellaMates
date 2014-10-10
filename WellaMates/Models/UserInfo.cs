using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using DataAnnotationsExtensions;
using WellaMates.Models.Validation;

namespace WellaMates.Models
{
    [ComplexType]
    public class PersonalInfo
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }


        [Display(Name = "Nome Artístico")]
        public string ArtisticName { get; set; }

        [Display(Name = "CPF")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:000.000.000-00}")]
        [CPF(ErrorMessage = "Número de CPF Inválido")]
        public string CPF { get; set; }
        
        [Display(Name = "Data de Nascimento")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Sexo")]
        public Gender? Gender { get; set; }
    }

    [ComplexType]
    public class ContactInfo
    {
        [Display(Name = "Email")]
        [Email(ErrorMessage = "Endereço de Email inválido")]
        public string Email { get; set; }

        //Phones
        [Display(Name = "Telefone Residencial")]
        public string HomePhone { get; set; }

        [Display(Name = "Telefone Celular")]
        public string CellPhone { get; set; }
    }

    [ComplexType]
    public class AddressInfo
    {
        //Location
        [Display(Name = "CEP")]
        [RegularExpression(@"^\d{8}$|^\d{5}-\d{3}$", ErrorMessage = "O código postal deverá estar no formato 00000000 ou 00000-000")]
        public string CEP { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}