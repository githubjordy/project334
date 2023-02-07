

using System.ComponentModel.DataAnnotations;

namespace FormsTagHelper.ViewModels
{
    public class YgoDataViewModel
    {
        [Required]
        //[EmailAddress]
        //[Display(Name = "Email Address")]
        public string DeckUrl { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        public string DeckName { get; set; }
    }
}