using System.ComponentModel.DataAnnotations;

namespace NilDevStudio.WebAPI.DTO
{
    public class SocialNetworkDTO
    {
        public int Id { get; set; }

		[Required]
        public string Name { get; set; }

		[Required]
        public string URL { get; set; }
    }
}
