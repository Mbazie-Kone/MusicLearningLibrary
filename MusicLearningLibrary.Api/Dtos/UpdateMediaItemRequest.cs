using System.ComponentModel.DataAnnotations;

namespace MusicLearningLibrary.Api.Dtos
{
    public record UpdateMediaItemRequest(
        [Required]
        [MaxLength(200)]
        string Title
    );
}
