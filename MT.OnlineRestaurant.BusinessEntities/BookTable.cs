using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class BookTable
    {
        public int Id { get; set; }
        public int capacityCount { get; set; }

        public int CustomerId { get; set; }

        public DateTime BookingDate { get; set; }

        public int RestaurantId { get; set; }
    }
}
