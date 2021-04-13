using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBITTask.ViewModels
{
    public class ReportVM
    {
        public List<ProductVM> products { get; set; }
        public string quality { get; set; }
    }
}