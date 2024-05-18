using System.ComponentModel.DataAnnotations;


namespace Unikktle.Models
{
    public class InputModel_MindEdit
    {
        [Required(ErrorMessage = "96")]     // ErrorMessage = "The Title field is required."
        public string Title { get; set; }

        [Required(ErrorMessage = "97")]     // ErrorMessage = "The Explanation is required."
        public string Explanation { get; set; } = "";

        [Required(ErrorMessage = "99")]     // ErrorMessage = "The RelationshipItem is required."
        public string ItemRelation { get; set; }

        [Required]
        public bool AllowOtherEdit { get; set; } = false;

        [Required]
        public bool PublishOnlyToMe { get; set; } = true;

    }

}
