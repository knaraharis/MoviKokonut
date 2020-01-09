using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviKokonut.Domain.Catalog
    {
       public class Video : MoviKokonutDocument
        {
        public string VideoId { get; set; }
        public Uri VideoURL { get; set; }
        public enum VideoType { Movies, Songs, Albums };
        public enum Language { Tamil, English, Telegu, Hindi, Malayalam, Kannada }
        public IEnumerable<ProductAdvertisement> products { get; set; }
        public IEnumerable<SellerProductShortInventory> inventory { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        }
   
    }
