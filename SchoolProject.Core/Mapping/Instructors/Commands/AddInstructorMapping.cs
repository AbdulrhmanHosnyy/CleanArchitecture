﻿using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Instructors
{
    public partial class InstructorProfile
    {
        public void AddInstructorMapping()
        {
            CreateMap<AddInstructorCommand, Instructor>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.INameAr, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.INameEn, opt => opt.MapFrom(src => src.NameEn));
        }
    }
}
