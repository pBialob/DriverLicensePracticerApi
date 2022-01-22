using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DriverLicensePracticerApi.Entities
{
    public class Category
    {
        public Category()
        {
            this.QuestionCategories = new List<QuestionCategory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<QuestionCategory> QuestionCategories { get; set; }
    }
}
