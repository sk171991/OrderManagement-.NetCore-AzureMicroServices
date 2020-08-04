using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities.ServiceModels
{
    public class RestaurantMenu
    {
        public int menu_ID { get; set; }
        public string dish_Name { get; set; }
        public decimal price { get; set; }
        public int running_Offer { get; set; }
        public string cuisine { get; set; }
    }
}
