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
using WhistleblowerSystem.Business.DTOs;
using WhistleblowerSystem.Business.DTOs.Config;

namespace WhistleblowerSystem.Initialization
{
    public class Initializer
    {
        private const string UsersFileName = "users.json";
        private const string CompaniesFileName = "companies.json";

        CompanyService _companyService;
        UserService _userService;
        FormTemplateService _formTemplateService;
        IDbContext _dbContext;
        IMapper _mapper;

        public Initializer(IDbContext unitOfWork,
            CompanyService companyService,
            UserService userService,
            FormTemplateService formTemplateService,
            IMapper mapper)
        {
            _companyService = companyService;
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
            string companiesFile = Path.Combine(executingPath, CompaniesFileName);

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

            //init the database with initial companies and users
            List<CompanyDto> companies = new();
            if (File.Exists(companiesFile))
            {
                if (!await _dbContext.CheckCollectionExistsAsync<Company>())
                {
                    companies = JsonConvert.DeserializeObject<List<CompanyDto>>(File.ReadAllText(companiesFile))!;
                    List<Task<CompanyDto>> tasks = companies.Select(c => _companyService.CreateCompanyAsync(c)).ToList();
                    await Task.WhenAll(tasks);
                    companies = tasks.Select(t => t.Result).ToList(); // get the result
                }
            }

            if (File.Exists(usersFile))
            {
                if (!await _dbContext.CheckCollectionExistsAsync<User>())
                {
                    List<UserDto> userDtos = JsonConvert.DeserializeObject<List<UserDto>>(File.ReadAllText(usersFile))!;
                    userDtos.ForEach(u => u.CompanyId = companies[0].Id!);

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
                new("", Shared.Enums.Language.German, "Beschreibung", "Beschreibung"),
                new("", Shared.Enums.Language.English, "Description", "Beschreibung")
            };
            FormFieldTemplateDto formField1 = new FormFieldTemplateDto("", languageEntries1, Shared.Enums.ControlType.Textbox, null);


            List<LanguageEntryDto> languageEntries2 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Möchten Sie Ihren Namen eingeben?", "Namen"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Would you like to enter your name?", "Namen")
            };

            List<LanguageEntryDto> languageEntries2_1 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Ja", "Ja"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Yes", "Ja")
            };
            SelectionValueDto selectionValue1 = new SelectionValueDto("", languageEntries2_1, "1");

            List<LanguageEntryDto> languageEntries2_2 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Nein", "Nein"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "No", "Nein")
            
            };
            SelectionValueDto selectionValue2 = new SelectionValueDto("", languageEntries2_2, "2");

            List<SelectionValueDto> selectionValues1 = new List<SelectionValueDto>();
            selectionValues1.Add(selectionValue1);
            selectionValues1.Add(selectionValue2);

            FormFieldTemplateDto formField2 = new FormFieldTemplateDto("", languageEntries2, Shared.Enums.ControlType.Radiobutton, selectionValues1);


            List<LanguageEntryDto> languageEntries3 = new()
            {
                new("", Shared.Enums.Language.German, "Bitte beschreiben Sie den Vorfall so detailliert wie möglich", "Vorfall"),
                new("", Shared.Enums.Language.English, "Please describe the incident as detailed as possible", "Vorfall")
            };
            FormFieldTemplateDto formField3 = new FormFieldTemplateDto("", languageEntries3, Shared.Enums.ControlType.Textarea, null);


            List<LanguageEntryDto> languageEntries4 = new()
            {
                new("", Shared.Enums.Language.German, "Um die Bearbeitung der Nachricht zu optimieren, beantworten Sie bitte die folgenden zusätzlichen Fragen, auch wenn Sie die Antworten bereits im Textfeld angegeben haben", "ZusatzText"),
                new("", Shared.Enums.Language.English, "To optimize the processing of the message, please answer the following additional questions, even if you have already mentioned the answers in the text field", "ZusatzText")
            };
            FormFieldTemplateDto formField4 = new FormFieldTemplateDto("", languageEntries4, Shared.Enums.ControlType.Text, null);


            List<LanguageEntryDto> languageEntries5 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Wie ist Ihre Beziehung zu unserem Unternehmen?", "Beziehung"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "What is your relation with our company?", "Beziehung")
            };

            List<LanguageEntryDto> languageEntries5_1 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Beziehung 1", "Beziehung1"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Relationship 1", "Beziehung1")
            };
            SelectionValueDto selectionValue5_1 = new SelectionValueDto("", languageEntries5_1, "1");

            List<LanguageEntryDto> languageEntries5_2 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Beziehung 2", "Beziehung2"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Relationship 2", "Beziehung2")

            };
            SelectionValueDto selectionValue5_2 = new SelectionValueDto("", languageEntries5_2, "2");
            
            List<LanguageEntryDto> languageEntries5_3 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Beziehung 3", "Beziehung3"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Relationship 3", "Beziehung3")

            };
            SelectionValueDto selectionValue5_3 = new SelectionValueDto("", languageEntries5_3, "3");

            List<SelectionValueDto> selectionValues5 = new List<SelectionValueDto>();
            selectionValues5.Add(selectionValue5_1);
            selectionValues5.Add(selectionValue5_2);
            selectionValues5.Add(selectionValue5_3);

            FormFieldTemplateDto formField5 = new FormFieldTemplateDto("", languageEntries5, Shared.Enums.ControlType.Dropdown, selectionValues5);


            List<LanguageEntryDto> languageEntries6 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Ist der Vorfall noch aktuell?", "VorfallAktuell"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Is the incident still going on?", "VorfallAktuell")
            };

            List<LanguageEntryDto> languageEntries6_1 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Ja", "Ja"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Yes", "Ja")
            };
            SelectionValueDto selectionValue6_1 = new SelectionValueDto("", languageEntries6_1, "1");

            List<LanguageEntryDto> languageEntries6_2 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Nein", "Nein"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "No", "Nein")

            };
            SelectionValueDto selectionValue6_2 = new SelectionValueDto("", languageEntries6_2, "2");

            List<SelectionValueDto> selectionValues6 = new List<SelectionValueDto>();
            selectionValues6.Add(selectionValue6_1);
            selectionValues6.Add(selectionValue6_2);

            FormFieldTemplateDto formField6 = new FormFieldTemplateDto("", languageEntries6, Shared.Enums.ControlType.Radiobutton, selectionValues6);


            List<LanguageEntryDto> languageEntries7 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Ist bereits ein Schaden eingetreten?", "SchadenEingetreten"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Has damage already occured?", "SchadenEingetreten")
            };

            List<LanguageEntryDto> languageEntries7_1 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Ja", "Ja"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Yes", "Ja")
            };
            SelectionValueDto selectionValue7_1 = new SelectionValueDto("", languageEntries7_1, "1");

            List<LanguageEntryDto> languageEntries7_2 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Nein", "Nein"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "No", "Nein")

            };
            SelectionValueDto selectionValue7_2 = new SelectionValueDto("", languageEntries7_2, "2");

            List<SelectionValueDto> selectionValues7 = new List<SelectionValueDto>();
            selectionValues7.Add(selectionValue7_1);
            selectionValues7.Add(selectionValue7_2);

            FormFieldTemplateDto formField7 = new FormFieldTemplateDto("", languageEntries7, Shared.Enums.ControlType.Radiobutton, selectionValues7);


            List<LanguageEntryDto> languageEntries8 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Sind Manager oder andere interne Abteilungen über den Vorfall informiert?", "InternInformiert"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Are managers or other internal departements aware of the incident?", "InternInformiert")
            };

            List<LanguageEntryDto> languageEntries8_1 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Ja", "Ja"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Yes", "Ja")
            };
            SelectionValueDto selectionValue8_1 = new SelectionValueDto("", languageEntries8_1, "1");

            List<LanguageEntryDto> languageEntries8_2 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Nein", "Nein"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "No", "Nein")

            };
            SelectionValueDto selectionValue8_2 = new SelectionValueDto("", languageEntries8_2, "2");

            List<SelectionValueDto> selectionValues8 = new List<SelectionValueDto>();
            selectionValues7.Add(selectionValue8_1);
            selectionValues7.Add(selectionValue8_2);

            FormFieldTemplateDto formField8 = new FormFieldTemplateDto("", languageEntries8, Shared.Enums.ControlType.Radiobutton, selectionValues8);


            List<LanguageEntryDto> languageEntries9 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Sind Dritte über den Vorfall informiert?", "DritteInformiert"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Are third parties aware of the incident?", "DritteInformiert")
            };

            List<LanguageEntryDto> languageEntries9_1 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Ja", "Ja"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "Yes", "Ja")
            };
            SelectionValueDto selectionValue9_1 = new SelectionValueDto("", languageEntries9_1, "1");

            List<LanguageEntryDto> languageEntries9_2 = new()
            {
                new LanguageEntryDto("", Shared.Enums.Language.German, "Nein", "Nein"),
                new LanguageEntryDto("", Shared.Enums.Language.English, "No", "Nein")

            };
            SelectionValueDto selectionValue9_2 = new SelectionValueDto("", languageEntries9_2, "2");

            List<SelectionValueDto> selectionValues9 = new List<SelectionValueDto>();
            selectionValues9.Add(selectionValue9_1);
            selectionValues9.Add(selectionValue9_2);

            FormFieldTemplateDto formField9 = new FormFieldTemplateDto("", languageEntries9, Shared.Enums.ControlType.Radiobutton, selectionValues9);

            List<FormFieldTemplateDto> formFields = new()
            {
                formField1,
                formField2,
                formField3,
                formField4,
                formField5,
                formField6,
                formField7,
                formField8,
                formField9
            };

            FormTemplateDto formTemplate = new FormTemplateDto("",  formFields);
            return formTemplate;
        }
    }
}
