using AutoMapper;
using NilDevStudio.Domain;
using NilDevStudio.WebAPI.DTO;
using System.Linq;

namespace NilDevStudio.WebAPI.Helper
{
    public class AutoMapperProfile : Profile
    {
		public AutoMapperProfile()
		{
			CreateMap<MyEvent, MyEventDTO>()
				.ForMember(dest => dest.Speakers, opt => {
					opt.MapFrom(src => src.EventSpeakers.Select(x => x.Speaker).ToList());
				}).ReverseMap();

			CreateMap<Speaker, SpeakerDTO>()
				.ForMember(dest => dest.MyEvents, opt => {
					opt.MapFrom(src => src.EventSpeakers.Select(x => x.MyEvent).ToList());
				}).ReverseMap();

			CreateMap<Lot, LotDTO>().ReverseMap();
			CreateMap<SocialNetwork, SocialNetworkDTO>().ReverseMap();
		}
    }
}
