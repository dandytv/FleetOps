using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.ControlList;
using CardTrend.Domain.WebDto;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class UserAccessMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<UserAccessDTO, UserAccess>()
                .ForMember(d => d.Title, m => m.Ignore())
                .ForMember(d => d.DeptId, m => m.Ignore())
                .ForMember(d => d.Sts, m => m.Ignore())
                .ForMember(d => d.AccessInd, m => m.Ignore())
                .ForMember(d => d.SelectedMapUserId, m => m.MapFrom(src => src.AccessTmpl ))
                .ForMember(d => d.selectedSts, m => m.MapFrom(src => src.Sts))
                .ForMember(d => d.EmailAddr, m => m.MapFrom(src => src.EmailAddr))
                .ForMember(d => d.SeletedTitle, m => m.MapFrom(src => src.Title))
                .ForMember(d => d.SelectedDeptId, m => m.MapFrom(src =>src.DeptId ))
                .ForMember(d => d.PrivilegeCd, m => m.MapFrom(src => src.PrivilegeCd))
                .ForMember(d => d.ChangePasswordInd, m => m.MapFrom(src => NumberExtensions.BoolConverter(src.ChangePassInd)))               
                .ForMember(d => d.LastLogin, m => m.MapFrom(src => NumberExtensions.DateTimeConverter(src.LastLogin)))
                .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.CreationDate)))
                .ForMember(d => d.SelectedAccessInd, m => m.MapFrom(src => src.AccessInd))
                ;
            this.CreateMap<UserAccess, UserAccessDTO>()
                .ForMember(d => d.Title, m => m.Ignore())
                .ForMember(d => d.DeptId, m => m.Ignore())
                .ForMember(d => d.Sts, m => m.Ignore())
                .ForMember(d => d.AccessInd, m => m.Ignore())
                .ForMember(d => d.UserId, m => m.MapFrom(src => src.UserId))
                .ForMember(d => d.AccessTmpl, m => m.MapFrom(src => src.SelectedMapUserId))
                .ForMember(d => d.AccessInd, m => m.MapFrom(src => src.SelectedAccessInd))
                .ForMember(d => d.Sts, m => m.MapFrom(src => src.selectedSts))               
                .ForMember(d => d.Title, m => m.MapFrom(src => src.SeletedTitle))
                .ForMember(d => d.DeptId, m => m.MapFrom(src => (src.SelectedDeptId)))
                .ForMember(d => d.CreateBy, m => m.MapFrom(src => src.CreatedBy))
                .ForMember(d => d.ChangePassInd, m => m.MapFrom(src => NumberExtensions.ConvertBoolDB(src.ChangePasswordInd)))         
                .ForMember(d => d.MapUserId, m => m.MapFrom(src => src.SelectedMapUserId))
                .ForMember(d => d.LastLogin, m => m.MapFrom(src => NumberExtensions.ConvertDatetimeDB(src.LastLogin)))
                .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.ConvertDatetimeDB(src.CreationDate)))
                ;

            this.CreateMap<UserAccessListDTO, UserAccess>()
                .ForMember(d => d.Title, m => m.Ignore())
                .ForMember(d => d.DeptId, m => m.Ignore())
                .ForMember(d => d.Sts, m => m.Ignore())
                .ForMember(d => d.AccessInd, m => m.Ignore())
                .ForMember(d => d.UserId, m => m.MapFrom(src => src.UserId))
                .ForMember(d => d.Name, m => m.MapFrom(src => src.UserName))
                .ForMember(d => d.SeletedTitle, m => m.MapFrom(src => src.Title))
                .ForMember(d => d.selectedSts, m => m.MapFrom(src => src.Status))
                .ForMember(d => d.EmailAddr, m => m.MapFrom(src =>!string.IsNullOrEmpty(src.EmailAddress) ?src.EmailAddress : string.Empty ))
                .ForMember(d => d.SelectedMapUserId, m => m.MapFrom(src =>!string.IsNullOrEmpty(src.AccessTmpl) ? src.AccessTmpl : string.Empty))
                .ForMember(d => d.SelectedDeptId, m => m.MapFrom(src => !String.IsNullOrEmpty(src.DeptId) ? src.DeptId : string.Empty ))
                .ForMember(d => d.SelectedAccessInd, m => m.MapFrom(src => src.AccessInd))
                ;
            this.CreateMap<UserAccessLevelDTO, WebModule>()
                .ForMember(d => d.Level, m => m.MapFrom(src => Convert.ToInt32(src.Lvl)))
                .ForMember(d => d.ModuleId, m => m.MapFrom(src => src.ModuleId))
                .ForMember(d => d.ShortDescp, m => m.MapFrom(src => src.ShortDescp))
                .ForMember(d => d.Descp, m => m.MapFrom(src => src.Descp))
                .ForMember(d => d.Sts, m => m.MapFrom(src => src.Sts))
                ;
            this.CreateMap<UserAccessLevelDetailDTO, WebPage>()
                .ForMember(d => d.Descp, m => m.Ignore())
                .ForMember(d => d.PageId, m => m.MapFrom(src => Convert.ToString(src.PageId)))
                .ForMember(d => d.Descp, m => m.MapFrom(src => src.PageDescription))
                .ForMember(d => d.ModuleId, m => m.MapFrom(src => Convert.ToString(src.ModuleId)))
                .ForMember(d => d.Sts, m => m.MapFrom(src => Convert.ToInt32(src.Sts)))
                .ForMember(d => d.URL, m => m.MapFrom(src => src.Url))
                .ForMember(d => d.Level, m => m.MapFrom(src => Convert.ToInt32(src.Lvl)))
                ;
            this.CreateMap<UserAccessLevelDetailDTO, WebPageSection>()
               .ForMember(d => d.Descp, m => m.Ignore())
               .ForMember(d => d.PageId, m => m.MapFrom(src => Convert.ToString(src.PageId)))
               .ForMember(d => d.CtrlId, m => m.MapFrom(src => Convert.ToString(src.ControlId)))
               .ForMember(d => d.ModuleId, m => m.MapFrom(src => Convert.ToString(src.ModuleId)))
               .ForMember(d => d.Descp, m => m.MapFrom(src => src.ControlDescription))
               .ForMember(d => d.Sts, m => m.MapFrom(src => Convert.ToInt32(src.CtrlSts)))
               .ForMember(d => d.Level, m => m.MapFrom(src => Convert.ToInt32(src.Lvl)))
               .ForMember(d => d.Section, m => m.MapFrom(src => src.SectionName))
               .ForMember(d => d.SectionId, m => m.MapFrom(src => Convert.ToString(src.SectionId)))
               .ForMember(d => d.SectionStatus, m => m.MapFrom(src => Convert.ToInt32(src.SectionStatus)))
               .ForMember(d => d.URL, m => m.MapFrom(src => src.Url))
               ;
            this.CreateMap<WebModule, WebModuleDTO>();
            this.CreateMap<WebPage, WebPageDTO>();
            this.CreateMap<WebControl, WebControlDTO>();
            this.CreateMap<WebPageSection, WebPageSectionDTO>();
        }
    }
}