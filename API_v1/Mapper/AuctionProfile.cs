using API.Response.AuctionRes;
using AutoMapper;
using DataAccess.Models;

namespace API.Mapper
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile() 
        { 
            CreateMap<Auction, AuctionResponse>();
        }
    }
}
