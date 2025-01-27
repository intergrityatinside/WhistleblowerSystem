﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Shared.DTOs.Config;
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
            CreateFormMap();
            CreateFormFieldMap();
            CreateFormFieldTemplateMap();
            CreateFormMessageMap();
            CreateFormTemplateMap();
            CreateSelectionValueMap();
            CreateUserMap();
            CreateWhistleblowerMap();
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

        private void CreateFormMap()
        {
            CreateMap<Form, FormDto>()
                .ConstructUsing((x, ctx) => new FormDto(x.Id.ToString(), x.TopicId.ToString(), x.FormTemplateId.ToString(),
                 ctx.Mapper.Map<List<FormFieldDto>>(x.FormFields),
                 ctx.Mapper.Map<List<AttachementMetaDataDto>>(x.Attachements),
                 ctx.Mapper.Map<List<FormMessageDto>>(x.Messages),
                 x.State, x.Datetime))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<FormDto, Form>()
                .ConstructUsing((x, ctx) => new Form(x.Id, x.TopicId!, x.FormTemplateId,
                 ctx.Mapper.Map<List<FormField>>(x.FormFields),
                 ctx.Mapper.Map<List<AttachementMetaData>>(x.Attachements), x.Datetime))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateFormFieldMap()
        {
            CreateMap<FormField, FormFieldDto>()
                .ConstructUsing((x, ctx) => new FormFieldDto(x.Id.ToString(), ctx.Mapper.Map<List<LanguageEntryDto>>(x.Texts), x.Type, x.SelectedValues, ctx.Mapper.Map<List<SelectionValueDto>>(x.SelectionValues), x.Mandatory))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<FormFieldDto, FormField>()
                .ConstructUsing((x, ctx) => new FormField(x.Id, ctx.Mapper.Map<List<LanguageEntry>>(x.Texts), x.Type, x.SelectedValues!, ctx.Mapper.Map<List<SelectionValue>>(x.SelectionValues), x.Mandatory))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateFormFieldTemplateMap()
        {
            CreateMap<FormFieldTemplate, FormFieldTemplateDto>()
                .ConstructUsing((x, ctx) => new FormFieldTemplateDto(x.Id.ToString(), ctx.Mapper.Map<List<LanguageEntryDto>>(x.Text), x.Type, ctx.Mapper.Map<List<SelectionValueDto>>(x.SelectionValues), x.Mandatory))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<FormFieldTemplateDto, FormFieldTemplate>()
                .ConstructUsing((x, ctx) => new FormFieldTemplate(x.Id, ctx.Mapper.Map<List<LanguageEntry>>(x.Text), x.Type, ctx.Mapper.Map<List<SelectionValue>>(x.SelectionValues), x.Mandatory))
                .ForAllMembers(opt => opt.Ignore());
        }

        private void CreateFormMessageMap()
        {
            CreateMap<FormMessage, FormMessageDto>()
                .ConstructUsing((x, ctx) => new FormMessageDto(x.Id.ToString(), x.Text, ctx.Mapper.Map<UserDto>(x.User), x.Timestamp))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<FormMessageDto, FormMessage>()
                .ConstructUsing((x, ctx) => new FormMessage(x.Id, x.Text, ctx.Mapper.Map<User>(x.User), x.Timestamp))
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

        private void CreateUserMap()
        {
            CreateMap<User, UserDto>()
                .ConstructUsing((x, _) => new UserDto(x.Id.ToString(),
                string.Empty, //do not map the (hashed) pw
                x.Name,
                x.FirstName,
                x.Email))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<UserDto, User>()
                .ConstructUsing((x, ctx) => new User(x.Id,
                x.Password ?? throw new Exception($"{nameof(x.Password)} can not be null"),
                 x.Name,
                x.FirstName,
                x.Email))
                .ForAllMembers(opt => opt.Ignore());
        }
        private void CreateWhistleblowerMap()
        {
            CreateMap<Whistleblower, WhistleblowerDto>()
                .ConstructUsing((x, _) => new WhistleblowerDto(x.Id.ToString(),
                x.FormId.ToString(),
                string.Empty))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<WhistleblowerDto, Whistleblower>()
                .ConstructUsing((x, ctx) => new Whistleblower(x.Id,
                x.FormId,
                x.Password ?? throw new Exception($"{nameof(x.Password)} can not be null")))
                .ForAllMembers(opt => opt.Ignore());
        }
    }
}
