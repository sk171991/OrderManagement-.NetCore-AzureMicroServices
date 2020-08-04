using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities.ServiceModels
{
    public class RestaurantInformation
    {
        public int restaurant_ID { get; set; }
        public string restaurant_Name { get; set; }
        public string restaurant_ContactNo { get; set; }
        public string restaurant_Address { get; set; }
        public double xaxis { get; set; }
        public double yaxis { get; set; }
        public string website { get; set; }
        public string opening_Time { get; set; }
        public string closing_Time { get; set; }
    }
}
