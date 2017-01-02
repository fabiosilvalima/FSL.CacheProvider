using System;
using System.Collections.Generic;

namespace FSL.CacheProvider.Models
{
    public class HomePageViewModel
    {
        public Customer Customer { get; set; }
        public List<Gender> Genders { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
    }
}