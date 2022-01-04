using System.Collections.Generic;

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
        public virtual ICollection<QuestionCategory> QuestionCategories { get; set; }
    }
}
