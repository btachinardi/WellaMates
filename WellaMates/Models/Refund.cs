using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

namespace WellaMates.Models
{
    [Table("RefundProfile")]
    public class RefundProfile
    {
        [Key, ForeignKey("User")]
        public int UserID { get; set; }

        public virtual UserProfile User { get; set; }

        public virtual ICollection<RefundItemUpdate> RefundItemUpdates { get; set; }

        public virtual Freelancer Freelancer { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual RefundAdministrator RefundAdministrator { get; set; }
        public virtual RefundVisualizator RefundVisualizator { get; set; }
    }

    [Table("Freelancer")]
    public class Freelancer
    {
        [Key, ForeignKey("RefundProfile")]
        public int UserID { get; set; }
        public virtual RefundProfile RefundProfile { get; set; }

        [Display(Name = "Cargo")]
        public FreelancerType Type { get; set; }

        [Display(Name = "Dias Trabalhados por Mês")]
        public int WorkDays { get; set; }

        [Display(Name = "Remuneração")]
        public decimal Remuneration { get; set; }

        [Display(Name = "Aux. Refeição")]
        public decimal MealAssistance { get; set; }

        [Display(Name = "Telefonia")]
        public decimal TelephoneAssistance { get; set; }

        [Display(Name = "Supervisores")]
        public virtual ICollection<Manager> Managers { get; set; }

        [Display(Name = "Eventos")]
        public virtual ICollection<Event> Events { get; set; }

        [Display(Name = "Visitas")]
        public virtual ICollection<Visit> Visits { get; set; }

        [Display(Name = "Mensais")]
        public virtual ICollection<Monthly> Monthlies { get; set; }

        internal static FreelancerType GetType(string p)
        {
            switch (p.ToLower())
            {
                case "educador":
                    return FreelancerType.EDUCATOR;
                case "assist. adm":
                case "assistente administrativo":
                    return FreelancerType.ADMIN_ASSISTANT;
                default:
                    throw new InvalidDataException("Not a valid Freelancer Type string.");
            }
        }
    }

    [Table("Manager")]
    public class Manager
    {
        [Key, ForeignKey("RefundProfile")]
        public int UserID { get; set; }
        public virtual RefundProfile RefundProfile { get; set; }

        public string Identification { get; set; }

        [Display(Name = "Equipe")]
        public virtual ICollection<Freelancer> Freelancers { get; set; }
    }

    [Table("RefundAdministrator")]
    public class RefundAdministrator
    {
        [Key, ForeignKey("RefundProfile")]
        public int UserID { get; set; }
        public virtual RefundProfile RefundProfile { get; set; }
    }

    [Table("RefundVisualizator")]
    public class RefundVisualizator
    {
        [Key, ForeignKey("RefundProfile")]
        public int UserID { get; set; }
        public virtual RefundProfile RefundProfile { get; set; }
    }

    [Table("Refund")]
    public class Refund
    {
        public Refund()
        {
            PaymentDate = SqlDateTime.MinValue.Value;
        }

        [Display(Name = "#")]
        public int RefundID { get; set; }
        
        [Display(Name = "Itens de Reembolso")]
        public virtual ICollection<RefundItem> RefundItems { get; set; }

        [Display(Name = "Status")]
        public RefundStatus Status { get; set; }

        public void Update()
        {
            List<RefundItem> NotDeletedItems = RefundItems == null ? new List<RefundItem>() : RefundItems.Where(ri => 
                ri.Status != RefundItemStatus.DELETED).ToList();
            //Status
            if (!NotDeletedItems.Any() ||
                NotDeletedItems.Any(ri => ri.Status == RefundItemStatus.UPDATED || ri.Status == RefundItemStatus.CREATED))
            {
                Status = RefundStatus.NON_EXISTENT;
            }
            else if (NotDeletedItems.All(r => r.Status == RefundItemStatus.REJECTED_NO_APPEAL))
            {
                Status = RefundStatus.REJECTED;
            }
            else if (NotDeletedItems.All(r => r.Status == RefundItemStatus.PAID || r.Status == RefundItemStatus.REJECTED_NO_APPEAL))
            {
                Status = RefundStatus.PAID;
            }
            else if (NotDeletedItems.All(r => r.Status == RefundItemStatus.ACCEPTED || r.Status == RefundItemStatus.REJECTED_NO_APPEAL || r.Status == RefundItemStatus.PAID))
            {
                Status = PaymentDate == SqlDateTime.MinValue.Value ? RefundStatus.ACCEPTED : RefundStatus.TO_BE_PAID;
            }
            else if (NotDeletedItems.Any(r => r.Status == RefundItemStatus.REJECTED))
            {
                Status = RefundStatus.WAITING_FOR_FREELANCER;
            }
            else
            {
                Status = RefundStatus.WAITING_FOR_MANAGER;
            }

            //Value
            Value = NotDeletedItems.Count > 0 ? NotDeletedItems.Sum(item => item.Value) : 0;

            //Approved Value
            AcceptedValue = RefundItems == null ? 0 : RefundItems.Sum(item => (item.Status == RefundItemStatus.ACCEPTED ||
                    item.Status == RefundItemStatus.PAID) && item.Status != RefundItemStatus.DELETED ? item.Value : 0);
        }

        [Display(Name = "Data do Pagamento")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Valor")]
        public decimal Value { get; set; }

        [Display(Name = "Valor Aprovado")]
        public decimal AcceptedValue { get; set; }
    }

    [Table("RefundItem")]
    public class RefundItem
    {

        public RefundItem()
        {
            Date = SqlDateTime.MinValue.Value;
        }
        [Display(Name = "#")]
        public int RefundItemID { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "O campo 'Categoria' é obrigatório")]
        public RefundItemCategory Category { get; set; }

        [Display(Name = "Sub Categoria")]
        public RefundItemSubCategory SubCategory { get; set; }

        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Display(Name = "Especificação")]
        [StringLength(40, ErrorMessage = "A {0} não deve conter mais de {1} caracteres.")]
        public string OtherSpecification { get; set; }

        [Display(Name = "Atividade")]
        [Required(ErrorMessage = "O campo 'Atividade' é obrigatório")]
        public string Activity { get; set; }

        [Display(Name = "Status")]
        public RefundItemStatus Status { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "O campo 'Valor' é obrigatório")]
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }

        [Display(Name = "KM")]
        public decimal KM { get; set; }

        [Display(Name = "Nota Fiscal Recebida?")]
        public bool ReceivedInvoice { get; set; }

        [Display(Name = "# do Reembolso")]
        public int RefundID { get; set; }

        [ForeignKey("RefundID")]
        [Display(Name = "Reembolso")]
        public virtual Refund Refund { get; set; }

        [Display(Name = "Histórico")]
        public virtual ICollection<RefundItemUpdate> History { get; set; }

        [Display(Name = "Anexos")]
        public virtual ICollection<File> Files { get; set; }

    }

    [Table("RefundItemUpdate")]
    public class RefundItemUpdate
    {

        public RefundItemUpdate()
        {
            Date = SqlDateTime.MinValue.Value;
        }

        [Display(Name = "#")]
        public int RefundItemUpdateID { get; set; }

        [Display(Name = "Comentários")]
        public string Comment { get; set; }

        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Display(Name = "Status")]
        public RefundItemStatus Status { get; set; }

        [Display(Name = "Mudanças")]
        public string Changelog { get; set; }

        [Display(Name = "# do Autor")]
        public int RefundProfileID { get; set; }

        [ForeignKey("RefundProfileID")]
        [Display(Name = "Autor")]
        public virtual RefundProfile RefundProfile { get; set; }

        [Display(Name = "# do Item de Reembolso")]
        public int RefundItemID { get; set; }

        [Display(Name = "Item de Reembolso")]
        public virtual RefundItem RefundItem { get; set; }

        [Display(Name = "Anexos")]
        public virtual ICollection<File> Files { get; set; }

        [Display(Name = "Nota Fiscal Recebida?")]
        public bool ReceivedInvoice { get; set; }
    }
    
    public enum FreelancerType
    {
        [Display(Name = "Educador")]
        EDUCATOR = 1,
        [Display(Name = "Assistente Administrativo")]
        ADMIN_ASSISTANT = 2
    }

    public enum RefundStatus
    {
        [Display(Name = "Esperando pelo Supervisor")]
        WAITING_FOR_MANAGER = 1,
        [Display(Name = "Esperando pelo Freelancer")]
        WAITING_FOR_FREELANCER = 2,
        [Display(Name = "Recusado")]
        REJECTED = 3,
        [Display(Name = "Aceito")]
        ACCEPTED = 4,
        [Display(Name = "Pago")]
        PAID = 5,
        [Display(Name = "Editando")]
        EDITING = 6,
        [Display(Name = "Sem Reembolso")]
        NON_EXISTENT = 7,
        [Display(Name = "Pago")]
        TO_BE_PAID = 8
    }

    public enum RefundItemStatus
    {
        [Display(Name = "Criado")]
        CREATED = 1,
        [Display(Name = "Atualizado")]
        UPDATED = 2,
        [Display(Name = "Aceito")]
        ACCEPTED = 3,
        [Display(Name = "Rejeitado")]
        REJECTED = 4,
        [Display(Name = "Apelado")]
        APPEALED = 5,
        [Display(Name = "Rejeitado sem direito a apelo")]
        REJECTED_NO_APPEAL = 6,
        [Display(Name = "Pago")]
        PAID = 7,
        [Display(Name = "Deletado")]
        DELETED = 8,
        [Display(Name = "Enviado")]
        SENT = 9
    }

    public enum RefundItemCategory
    {
        [Display(Name = "Transporte")]
        TRANSPORTATION = 1,
        [Display(Name = "Alimentação")]
        MEAL = 2,
        [Display(Name = "Telefone/Comunicações")]
        TELEPHONE = 3,
        [Display(Name = "Outros")]
        OTHER = 4,
        [Display(Name = "Salario")]
        SALARY = 5,
        [Display(Name = "Xerox/Cópia")]
        XEROX_COPY = 6,
        [Display(Name = "Correio/Sedex")]
        MAIL_SEDEX = 7,
        [Display(Name = "Excesso de Bagagem")]
        LUGGAGE = 8
    }

    public enum RefundItemSubCategory
    {
        [Display(Name = "Nenhuma")]
        NONE = 0,
        [Display(Name = "Reembolso de KM")]
        TRANSPORTATION_KM = 1,
        [Display(Name = "Reembolso de Passagem Rodoviária")]
        TRANSPORTATION_BUS_TICKET = 2,
        [Display(Name = "Pedágio")]
        TRANSPORTATION_TOOL = 3,
        [Display(Name = "Taxi")]
        TRANSPORTATION_TAXI = 4,

        [Display(Name = "Almoço")]
        MEAL_LUNCH = 11,
        [Display(Name = "Jantar")]
        MEAL_DINNER = 12
    }

    public enum Month
    {
        [Display(Name = "Inválido")]
        INVALID = 0,
        [Display(Name = "Janeiro")]
        JANUARY = 1,
        [Display(Name = "Fevereiro")]
        FEBRUARY = 2,
        [Display(Name = "Março")]
        MARCH = 3,
        [Display(Name = "Abril")]
        APRIL = 4,
        [Display(Name = "Maio")]
        MAY = 5,
        [Display(Name = "Junho")]
        JUNE = 6,
        [Display(Name = "Julho")]
        JULY = 7,
        [Display(Name = "Agosto")]
        AUGUST = 8,
        [Display(Name = "Setembro")]
        SEPTEMBER = 9,
        [Display(Name = "Outubro")]
        OCTOBER = 10,
        [Display(Name = "Novembro")]
        NOVEMBER = 11,
        [Display(Name = "Dezembro")]
        DECEMBER = 12
    }
    
    public interface IRefundOwner
    {
        Refund Refund { get; set; }
        int RefundID { get; set; }
        Freelancer Freelancer { get; set; }
        int FreelancerID { get; set; }
    }

    [Table("Monthly")]
    public class Monthly : IRefundOwner
    {

        [Display(Name = "#")]
        public int MonthlyID { get; set; }

        [Display(Name = "# do Requerente")]
        public int FreelancerID { get; set; }

        [ForeignKey("FreelancerID")]
        [Display(Name = "Requerente")]
        public virtual Freelancer Freelancer { get; set; }

        [Display(Name = "# do Reembolso")]
        public int RefundID { get; set; }

        [ForeignKey("RefundID")]
        [Display(Name = "Reembolso")]
        public virtual Refund Refund { get; set; }

        [Display(Name = "Mês")]
        [Required(ErrorMessage = "O campo 'Mês' é obrigatório")]
        public Month Month { get; set; }

        [Display(Name = "Ano")]
        [Required(ErrorMessage = "O campo 'Ano' é obrigatório")]
        public int Year { get; set; }

        [NotMapped]
        public DateTime MonthStart {
            get
            {
                try
                {
                    return new DateTime(Year, (int)Month, 1, 0, 0, 0, 0);
                }
                catch (Exception)
                {

                    return DateTime.MinValue;
                }
            } 
        }

        [NotMapped]
        public DateTime MonthEnd {
            get
            {
                try
                {
                    return new DateTime(Year, (int)Month, DateTime.DaysInMonth(Year, (int)Month), 23, 59, 59, 999);
                }
                catch (Exception)
                {

                    return DateTime.MinValue;
                }
            }
            
        }
    }

    [Table("Event")]
    public class Event : IRefundOwner
    {
        public Event()
        {
            StartDate = SqlDateTime.MinValue.Value;
            EndDate = SqlDateTime.MinValue.Value;
        }

        [Display(Name = "#")]
        public int EventID { get; set; }

        [Display(Name = "Evento")]
        [Required(ErrorMessage = "O campo 'Evento' é obrigatório")]
        [StringLength(40, ErrorMessage = "A {0} não deve conter mais de {1} caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Comentários")]
        [Required(ErrorMessage = "O campo 'Comentários' é obrigatório")]
        public string Comments { get; set; }

        [Display(Name = "# do Requerente")]
        public int FreelancerID { get; set; }

        [ForeignKey("FreelancerID")]
        [Display(Name = "Requerente")]
        public virtual Freelancer Freelancer { get; set; }

        [Display(Name = "# do Reembolso")]
        public int RefundID { get; set; }

        [ForeignKey("RefundID")]
        [Display(Name = "Reembolso")]
        public virtual Refund Refund { get; set; }

        [Display(Name = "Início")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fim")]
        public DateTime EndDate { get; set; }
    }

    [Table("Visit")]
    public class Visit : IRefundOwner
    {
        public Visit()
        {
            Date = SqlDateTime.MinValue.Value;
        }

        [Display(Name = "#")]
        public int VisitID { get; set; }

        [Display(Name = "# do Requerente")]
        public int FreelancerID { get; set; }

        [ForeignKey("FreelancerID")]
        [Display(Name = "Requerente")]
        public virtual Freelancer Freelancer { get; set; }

        [Display(Name = "# do Reembolso")]
        public int RefundID { get; set; }

        [ForeignKey("RefundID")]
        [Display(Name = "Reembolso")]
        public virtual Refund Refund { get; set; }

        [Display(Name = "Data")]
        public DateTime Date { get; set; }
    }
}