using Newtonsoft.Json;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using WhistleblowerSystem.Business.Services;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Interfaces;
using WhistleblowerSystem.Database.Repositories;
using WhistleblowerSystem.Shared.Exceptions;
using System.Linq;
using WhistleblowerSystem.Shared.DTOs;
using WhistleblowerSystem.Shared.DTOs.Config;

namespace WhistleblowerSystem.Initialization
{
    public class Initializer
    {
        private const string UsersFileName = "users.json";

        UserService _userService;
        FormTemplateService _formTemplateService;
        IDbContext _dbContext;
        IMapper _mapper;

        public Initializer(IDbContext unitOfWork,
            UserService userService,
            FormTemplateService formTemplateService,
            IMapper mapper)
        {
            _userService = userService;
            _formTemplateService = formTemplateService;
            _dbContext = unitOfWork;
            _mapper = mapper;
        }

        public async Task Init(InitializingMode initializingMode)
        {
            string? executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (executingPath == null) throw new NullException("execuingPath null");

            string usersFile = Path.Combine(executingPath, UsersFileName);

            if (initializingMode == InitializingMode.DeleteAndCreate)
            {
                //delete all collections
                List<Task> deletionTasks = _dbContext.GetCollectionTypes().Select(t =>
                {
                    MethodInfo method = _dbContext.GetType().GetMethod(nameof(IDbContext.DeleteCollectionAsync))!;
                    MethodInfo deleteCollection = method.MakeGenericMethod(t);
                    return (Task)deleteCollection.Invoke(_dbContext, null)!;
                }).ToList();
                await Task.WhenAll(deletionTasks);
            }

            //init the database with initial users
            if (File.Exists(usersFile))
            {
                if (!await _dbContext.CheckCollectionExistsAsync<User>())
                {
                    List<UserDto> userDtos = JsonConvert.DeserializeObject<List<UserDto>>(File.ReadAllText(usersFile))!;

                    List<Task<UserDto>> tasks = userDtos.Select(u => _userService.CreateUserAsync(u)).ToList();
                    await Task.WhenAll(tasks);
                }
            }
            else
            {
                throw new FileNotFoundException($"File {UsersFileName} not found");
            }

            await _formTemplateService.CreateFormTemplateAsync(InitalizeFormFields());

            await _dbContext.CreateCollectionsAsync();
        }

        private FormTemplateDto InitalizeFormFields() {
            List<LanguageEntryDto> languageEntries1 = new() 
            {
                new("", Shared.Enums.Language.German, "Betreff", "Beschreibung"),
                new("", Shared.Enums.Language.English, "Subject", "Beschreibung")
            };
            FormFieldTemplateDto formField1 = new FormFieldTemplateDto("", languageEntries1, Shared.Enums.ControlType.Textbox, null);
            
            List<LanguageEntryDto> languageEntries2 = new() 
            {
                new("", Shared.Enums.Language.German, "Wenn Sie möchten, können Sie hier Ihren Name angeben", "name"),
                new("", Shared.Enums.Language.English, "If you wish, you can enter your name here", "name")
            };
            FormFieldTemplateDto formField2 = new FormFieldTemplateDto("", languageEntries2, Shared.Enums.ControlType.Textbox, null);
           
            List<LanguageEntryDto> languageEntries3 = new()
            {
                new("", Shared.Enums.Language.German, "Bitte beschreiben Sie den Vorfall so detailliert wie möglich", "Vorfall"),
                new("", Shared.Enums.Language.English, "Please describe the incident as detailed as possible", "Vorfall")
            };
            FormFieldTemplateDto formField3 = new FormFieldTemplateDto("", languageEntries3, Shared.Enums.ControlType.Textarea, null);
            
            List<FormFieldTemplateDto> formFields = new()
            {
                formField1,
                formField2,
                formField3,
            };

            FormTemplateDto formTemplate = new FormTemplateDto("",  formFields);
            return formTemplate;
        }
    }
}
