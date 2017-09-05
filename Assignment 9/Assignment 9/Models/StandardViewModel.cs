using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_9.Models
{
    public class StandardViewModel
    {
        public int StandardId { get; set; }
        public string Name { get; set; }

        public ICollection<StudentViewModel> Students { get; set; }
    }
}