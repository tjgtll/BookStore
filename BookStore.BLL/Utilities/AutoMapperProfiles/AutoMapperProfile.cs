using AutoMapper;
using BookStore.DAL.Entities;
using BookStore.DTO.DTOs;

namespace BookStore.BLL.Utilities.AutoMapperProfile;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //Book
        CreateMap<Book, BookDto>().ReverseMap();
        //Order
        CreateMap<Order, OrderDto>().ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.OrderBooks))
                                    .ReverseMap();
        //OrderBook
        CreateMap<OrderBook, OrderBookDto>()
           .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title))
           .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Book.Author))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Book.Price))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
    }
}