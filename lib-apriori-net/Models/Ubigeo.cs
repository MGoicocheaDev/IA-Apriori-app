using System.ComponentModel.DataAnnotations;

namespace lib_apriori_net.Models
{
    public class Ubigeo
    {
        [Display(Name = "UBIGEO_PACIENTE")]
        public string codigo { get; set; }
        [Display(Name = "DEPARTAMENTO_PACIENTE")]
        public string Region { get; set; }
        [Display(Name = "PROVINCIA_PACIENTE")]
        public string Provincia { get; set; }
        [Display(Name = "DISTRITO_PACIENTE")]
        public string Distrito { get; set; }

    }
}
