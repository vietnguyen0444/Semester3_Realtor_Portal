﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETAPI_SEM3.Entities
{
	public class PropertyNews
	{
        public int PropertyId { get; set; }
        public string Title { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public string GoogleMap { get; set; }
        public double Price { get; set; }
        public int BedNumber { get; set; }
        public int RoomNumber { get; set; }
        public double Area { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime BuildYear { get; set; }
        public int StatusId { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set; }
        public int MemberId { get; set; }
        public string Description { get; set; }
        public string ThumbailName { get; set; }
    }
}
