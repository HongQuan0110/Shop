using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Models
{
    interface IAuditable
    {
        DateTime? CreateDate { get; set; }

        string CreateBy { set; get; }

        DateTime? UpdateDate { set; get; }

        string UpdateBy { set; get; }

        string MetaKeyword { set; get; }

        string MetaDescription { set; get; }

        bool Status { set; get; }
    }
}
