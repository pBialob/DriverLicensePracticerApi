using DriverLicensePracticerApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriverLicensePracticerApi.Models
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
