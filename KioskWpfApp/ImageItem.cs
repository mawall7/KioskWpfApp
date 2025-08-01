using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskWpfApp
{
    public class ImageItem
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public FoodItems OrderItem { get; set; }

        private bool _isReady = false;  

        public bool IsReady
        {
            get { return _isReady; }
            set { _isReady = value; }
        }

    }

}
