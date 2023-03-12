using AutoMapper;
using Entities.Models;
using Interfaces;
using LoggerService;
using Microsoft.Extensions.Configuration;
using Service.Interfaces;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _categoryService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IConfiguration configuration) 
        {
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, logger, mapper)); 
        }

        public ICategoryService CategoryService => _categoryService.Value;
    }
}
