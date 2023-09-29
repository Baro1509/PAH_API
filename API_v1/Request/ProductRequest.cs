﻿namespace API.Request
{
    public class ProductRequest
    {
        public int? CategoryId { get; set; }
        public int? MaterialId { get; set; }
        public int? SellerId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Dimension { get; set; }
        public decimal? Weight { get; set; }
        public string? Origin { get; set; }
        public string? PackageMethod { get; set; }
        public string? PackageContent { get; set; }
        public int Condition { get; set; }
        public int Type { get; set; }
    }
}
