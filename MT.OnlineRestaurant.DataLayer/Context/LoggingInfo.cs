using System;
using System.Collections.Generic;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public partial class LoggingInfo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public DateTime? RecordTimeStamp { get; set; }
    }
}
