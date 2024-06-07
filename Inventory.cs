using Microsoft.AspNetCore.Components;


public class Medication
{
    public string? Name { get; set; }
    public int? Quantity {get; set;}
    public long? NDC {get; set;}
    public int? Strength {get; set;}
    public string? Form {get; set;}
    public string? VolumeStrength {set; get;}
    public string? StrengthMass {get;set;}

    public static readonly string[] MedicationForms = new string[]
    {
        "Oral Tablet",
        "Oral Liquid",
        "Otic Drop",
        "Opthalmic",
        "Topical",
        "Suppository",
        "Reconstition Vial",
        "Injectable Liquid Vial",
        "Vaccine"
    };
    public static readonly string[] StrenghtVolume = new string[] 
    {
        "mg / mL",
        "mcg / mL",
        "g / mL",
        "mEq /mL"
    };
    public static readonly string[] MassStrength = new string []
    {
        "g",
        "mg",
        "mcg"
    };
}