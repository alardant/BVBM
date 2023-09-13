using System.ComponentModel.DataAnnotations;

public enum Package
{
    [Display(Name = "Consultation Individuelle")]
    ConsultationIndividuelle,
    [Display(Name = "Pack 3 mois")]
    Pack3mois,
    [Display(Name = "frigo futé")]
    Frigofuté
}