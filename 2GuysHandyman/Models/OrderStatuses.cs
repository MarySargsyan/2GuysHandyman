﻿namespace _2GuysHandyman.models
{
    public class OrderStatuses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
