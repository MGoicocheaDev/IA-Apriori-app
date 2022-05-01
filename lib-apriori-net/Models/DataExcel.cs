using System.ComponentModel.DataAnnotations;

namespace lib_apriori_net.Models
{
    public class DataExcel
    {
        [Display(Name ="Sexo")]
        public string Sexo { get; set; }
        [Display(Name = "Edad")]
        public string Edad { get; set; }
        [Display(Name = "UBIGEO_PACIENTE")]
        public string Ubigeo { get; set; }
        [Display(Name = "RESULTADO")]
        public string Resultado { get; set; }
    }
}
