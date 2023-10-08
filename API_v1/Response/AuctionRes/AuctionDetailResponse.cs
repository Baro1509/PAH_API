﻿namespace API.Response.AuctionRes
{
    public class AuctionDetailResponse
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string StaffName { get; set; }
        public string Title { get; set; } = null!;
        public decimal EntryFee { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal? CurrentPrice { get; set; }
        public decimal Step { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public List<string> ImageUrls { get; set; }

        public ProductRes.ProductResponse Product { get; set; }
        public SellerRes.SellerResponse Seller { get; set; }
    }
}
