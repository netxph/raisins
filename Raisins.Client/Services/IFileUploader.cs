using Raisins.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Services
{
    public interface IFileUploader
    {
        IEnumerable<Payment> ExcelUpload(HttpPostedFileBase upload);
    }
}