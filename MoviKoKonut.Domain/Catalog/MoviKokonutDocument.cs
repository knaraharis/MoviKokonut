using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace MoviKokonut.Domain.Catalog
    {
    public class MoviKokonutDocument : Resource
        {
        public string Type { get; set; }
        public MoviKokonutDocument()
            {
            this.Type = this.GetType().Name;
            }
        }
    }
