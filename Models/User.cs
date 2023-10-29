using System.ComponentModel.DataAnnotations;

namespace MyCsvProj.Models
{
    public class User
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string FavoriteSport { get; set; }
    }

}
