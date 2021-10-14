using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Business.DTOs.Config;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Entities.Config;

namespace WhistleblowerSystem.Business.Mapping.AutoMapper
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateLanguageEntryMap();
            CreateAttachementMap();
            CreateAttachementMetaDataMap();
            CreateCompanyMap();
            CreateFormMap();
            CreateFormFieldMap();
            CreateFormTemplateMap();
            CreateSelectionValueMap();
            CreateTopicMap();
            CreateUserMap();
        }

        private void CreateLanguageEntryMap()
        {
            CreateMap<LanguageEntry, LanguageEntryDto>()
                .ConstructUsing((x, _) => new LanguageEntryDto(x.Id.ToString(), x.Language, x.Text, x.Value))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<LanguageEntryDto, LanguageEntry>()
                .ConstructUsing((x, _) => new LanguageEntry(x.Id, x.Language, x.Text, x.Value))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateAttachementMap()
        {
            CreateMap<Attachement, AttachementDto>()
                .ConstructUsing((x, _) => new AttachementDto(x.Id.ToString(), x.Name, x.Bytes))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<AttachementDto, Attachement>()
                .ConstructUsing((x, _) => new Attachement(x.Id, x.Name, x.Bytes))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateAttachementMetaDataMap()
        {
            CreateMap<AttachementMetaData, AttachementMetaDataDto>()
                .ConstructUsing((x, _) => new AttachementMetaDataDto(x.Filename, x.Filetype, x.AttachementId.ToString()))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<AttachementMetaDataDto, AttachementMetaData>()
                .ConstructUsing((x, _) => new AttachementMetaData(x.Filename, x.Filetype, x.AttachementId))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateCompanyMap()
        {
            CreateMap<Company, CompanyDto>()
                .ConstructUsing((x, _) => new CompanyDto(x.Id.ToString(), x.Name, x.Address, x.Zipcode))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<CompanyDto, Company>()
                .ConstructUsing((x, _) => new Company(x.Id, x.Name, x.Address, x.Zipcode))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateFormMap()
        {
            CreateMap<Form, FormDto>()
                .ConstructUsing((x, ctx) => new FormDto(x.Id.ToString(), x.UserId.ToString(), x.CompanyId.ToString(), x.TopicId.ToString(), x.FormTemplateId.ToString(),
                 ctx.Mapper.Map<List<FormFieldDto>>(x.FormFields),
                 ctx.Mapper.Map<List<AttachementMetaDataDto>>(x.Attachements),
                 ctx.Mapper.Map<List<FormMessageDto>>(x.Messages),
                 x.State))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<FormDto, Form>()
                .ConstructUsing((x, ctx) => new Form(x.Id, x.UserId, x.CompanyId, x.TopicId, x.FormTemplateId,
                 ctx.Mapper.Map<List<FormField>>(x.FormFields),
                 ctx.Mapper.Map<List<AttachementMetaData>>(x.Attachements)))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateFormFieldMap()
        {
            CreateMap<FormField, FormFieldDto>()
                .ConstructUsing((x, ctx) => new FormFieldDto(x.Id.ToString(), ctx.Mapper.Map<List<LanguageEntryDto>>(x.Texts), x.Type, ctx.Mapper.Map<List<SelectionValueDto>>(x.SelectedValues), ctx.Mapper.Map<List<SelectionValueDto>>(x.SelectionValues)))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<FormFieldDto, FormField>()
                .ConstructUsing((x, ctx) => new FormField(x.Id, ctx.Mapper.Map<List<LanguageEntry>>(x.Texts), x.Type, ctx.Mapper.Map<List<SelectionValue>>(x.SelectedValues), ctx.Mapper.Map<List<SelectionValue>>(x.SelectionValues)))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateFormFieldTemplateMap()
        {
            CreateMap<FormFieldTemplate, FormFieldTemplateDto>()
                .ConstructUsing((x, ctx) => new FormFieldTemplateDto(x.Id.ToString(), ctx.Mapper.Map<List<LanguageEntryDto>>(x.Text), x.Type, ctx.Mapper.Map<List<SelectionValueDto>>(x.SelectionValues)))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<FormFieldTemplateDto, FormFieldTemplate>()
                .ConstructUsing((x, ctx) => new FormFieldTemplate(x.Id, ctx.Mapper.Map<List<LanguageEntry>>(x.Text), x.Type, ctx.Mapper.Map<List<SelectionValue>>(x.SelectionValues)))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateFormTemplateMap()
        {
            CreateMap<FormTemplate, FormTemplateDto>()
                .ConstructUsing((x, ctx) => new FormTemplateDto(x.Id.ToString(), ctx.Mapper.Map<List<FormFieldTemplateDto>>(x.Fields)))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<FormTemplateDto, FormTemplate>()
                .ConstructUsing((x, ctx) => new FormTemplate(x.Id, ctx.Mapper.Map<List<FormFieldTemplate>>(x.Fields)))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateSelectionValueMap()
        {
            CreateMap<SelectionValue, SelectionValueDto>()
                .ConstructUsing((x, ctx) => new SelectionValueDto(x.Id.ToString(), ctx.Mapper.Map<List<LanguageEntryDto>>(x.Text), x.Value))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<SelectionValueDto, SelectionValue>()
                .ConstructUsing((x, ctx) => new SelectionValue(x.Id, ctx.Mapper.Map<List<LanguageEntry>>(x.Text), x.Value))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateTopicMap()
        {
            CreateMap<Topic, TopicDto>()
                .ConstructUsing((x, _) => new TopicDto(x.Id.ToString(), x.CompanyId.ToString(), x.Name))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<TopicDto, Topic>()
                .ConstructUsing((x, ctx) => new Topic(x.Id, x.CompanyId, x.Name))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateUserMap()
        {
            CreateMap<User, UserDto>()
                .ConstructUsing((x, _) => new UserDto(x.Id.ToString(),
                x.CompanyId.ToString(),
                string.Empty, //do not map the (hashed) pw
                x.Name,
                x.FirstName,
                x.Email))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<UserDto, User>()
                .ConstructUsing((x, ctx) => new User(x.Id,
                x.CompanyId,
                x.Password ?? throw new Exception($"{nameof(x.Password)} can not be null"),
                 x.Name,
                x.FirstName,
                x.Email))
                .ForAllMembers(opt => opt.Ignore());
        }
    }
}
