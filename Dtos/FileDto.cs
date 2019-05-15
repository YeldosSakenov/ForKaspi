using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class FileDto
    {
        public int Id { get; set; }
        [Display(Name = nameof(Resources.Resources.FileName), ResourceType = typeof(Resources.Resources))]
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public int OfferId { get; set; }
        public string ContentType { get; set; }

        public virtual OfferDto Offer { get; set; }
    }
}
