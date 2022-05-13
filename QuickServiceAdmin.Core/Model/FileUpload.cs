using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace QuickServiceAdmin.Core.Model
{
    public class FileUpload
    {
        [Required] public IFormFile File { get; set; }
    }
}