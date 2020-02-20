using dbModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace ViewModels
{
    public class PostLaunchReviewViewModel : PostLaunchReview
    {
        public List<PostLaunchReviewViewModel> Versions { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string DoneWell { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string DonePoorly { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Bottlenecks { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string LessonsLearned { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string ActualVSExpected { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Commercial { get; set; }
        
        public new virtual Stage Stage { get; set; }
    }
}
