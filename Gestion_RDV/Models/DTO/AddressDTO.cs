namespace Gestion_RDV.Models.DTO
{
    public class AddressDTO
    {
        public int AdresseId { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public int CodePostal { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is AddressDTO dTO &&
                   this.AdresseId == dTO.AdresseId &&
                   this.Adresse == dTO.Adresse &&
                   this.Ville == dTO.Ville &&
                   this.CodePostal == dTO.CodePostal;
        }
    }
    public class AddressPostDTO
    {
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public int CodePostal { get; set; }
    }
}
