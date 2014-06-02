using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAnnotationsExtensions;
using WellaMates.Models.Validation;

namespace WellaMates.ViewModel
{
    public class ContactVM
    {
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
        [Display(Name = "Email")]
        [Email(ErrorMessage = "Endereço de Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo 'Título' é obrigatório.")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo 'Mensagem' é obrigatório.")]
        [Display(Name = "Mens.")]
        public string Message { get; set; }
    }
}