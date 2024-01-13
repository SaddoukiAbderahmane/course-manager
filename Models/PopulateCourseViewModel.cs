using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace coursemanagement.Models
{
    public class PopulateCourseViewModel
    {
        [Display(Name = "Nom")]
        public string Name { get; set; }
        public string professeur { get; set; }
        public int Id { get; set; }
        public string filiere { get; set; }
       

    }
}