using AutoMapper;
using DichVuThuYService.Interfaces;
using DichVuThuYService.Datas;
using DichVuThuYRepository.ViewModel;
using System;

namespace DichVuThuYService.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVm, KhachHang>()
                .ForMember(dest => dest.TenKhachHang, opt => opt.MapFrom(src => src.HoTen))
                .ForMember(dest => dest.DiaChi, opt => opt.MapFrom(src => src.DiaChi))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.SoDienThoai, opt => opt.MapFrom(src => src.SoDienThoai))
                .ForMember(dest => dest.NgaySinh, opt => opt.MapFrom(src => src.NgaySinh.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.GioiTinh, opt => opt.MapFrom(src => src.GioiTinh == RegisterVm.GioiTinhEnum.Nam));
        }
    }
}
