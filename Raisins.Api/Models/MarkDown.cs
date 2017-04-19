using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Api.Models
{
    public class MarkDown
    {
        public MarkDown(string fileName, byte[] content)
        {
            FileName = fileName;
            Content = content;
        }
        public byte[] Content { get; set; }
        public string FileName { get; set; }
    }
}