using AutoMapper;
using ShapingAPI.Entities;
using ShapingAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapingAPI.Infrastructure.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Track, TrackViewModel>();

            CreateMap<Album, AlbumViewModel>().PreserveReferences();
                 //.ForMember(vm => vm.ArtistName, map => map.MapFrom(a => a.Artist.Name));

            CreateMap<Artist, ArtistViewModel>();

            CreateMap<Customer, CustomerViewModel>()
                .ForMember(vm => vm.Address, map => map.MapFrom(c => new AddressViewModel()
                {
                    Address = c.Address,
                    City = c.City,
                    Country = c.Country,
                    PostalCode = c.PostalCode,
                    State = c.State
                }))
                .ForMember(vm => vm.Contact, map => map.MapFrom(c => new ContactViewModel()
                {
                    Email = c.Email,
                    Fax = c.Fax,
                    Phone = c.Phone
                }))
                .ForMember(vm => vm.TotalInvoices, map => map.MapFrom(c => c.Invoice.Count()));

            //Mapper.CreateMap<Employee, EmployeeViewModel>();

            //Mapper.CreateMap<Genre, GenreViewModel>()
            //    .ForMember(vm => vm.Tracks, map => map.MapFrom(g => g.Track.Select(t => t.TrackId).ToList()));

            CreateMap<InvoiceLine, InvoiceLineViewModel>();

            CreateMap<Invoice, InvoiceViewModel>();

            //Mapper.CreateMap<MediaType, MediaTypeViewModel>()
            //    .ForMember(vm => vm.Tracks, map => map.MapFrom(m => m.Track.Select(t => t.TrackId).ToList()));

            //Mapper.CreateMap<PlaylistTrack, PlaylistTrackViewModel>();

            //Mapper.CreateMap<Playlist, PlaylistViewModel>();
        }
    }
}
