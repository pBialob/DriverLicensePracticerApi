using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriverLicensePracticerApi.Models
{
    public class RandomSpecifiedDto
    {
        public string Points { get; set; }
        public string Level { get; set; }
        public string Category { get; set; }
    }
}
