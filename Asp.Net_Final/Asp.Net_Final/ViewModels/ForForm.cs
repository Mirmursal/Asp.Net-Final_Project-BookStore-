using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp.Net_Final.ViewModels
{
    public class ForForm
    {
        public string ConfirmPassword { get; set; }
        public HttpPostedFileBase image { get; set; }
    }
}