namespace CMShorty.URLShortModul.Mappers
{
    using AutoMapper;
    using CMShorty.URLShortModul.Models;
    using ShortyIODataRequest.CreateShortURL;

    public class ShortURLMapper
    {
        public ShortURLModel Map(ShortRequstSuccessResult source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ShortRequstSuccessResult, ShortURLModel>()
                    .ForMember(dest => dest.OriginalURL, opt => opt.MapFrom(src => src.originalURL))
                    .ForMember(dest => dest.ShortURL, opt => opt.MapFrom(src => src.shortURL))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdAt));
            });

            IMapper mapper = config.CreateMapper();

            return mapper.Map<ShortURLModel>(source);
        }
    }
}
