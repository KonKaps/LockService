using System;
using System.Collections.Generic;
namespace TodoApi.Models
{
    public class Locks
    {
        public string Id { get; set; }
        public string BuildingId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string Floor { get; set; }
        public string RoomNumber { get; set; }
        public int Weight { get; set; }

    }
}
