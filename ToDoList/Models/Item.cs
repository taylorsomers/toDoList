using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ToDoList.Models
{
    public class Item
    {
        public Item()
        {
            this.Categories = new HashSet<CategoryItem>();
            this.Completed = false;
        }

        public int ItemId { get; set; }
        public string Description { get; set; }
        [DisplayName("Task completed?")]
        public bool Completed { get; set; }
        
        public ICollection<CategoryItem> Categories { get;}
    }
}