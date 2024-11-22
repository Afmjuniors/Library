using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDCore.DependencyInjection;
using TDCore.Domain.Exceptions;
using TDCore.Web.Security;

namespace NN.Checklist.Api.Controllers
{
    /// <summary>
    /// Name: "BaseController" 
    /// Description: is an abstract class inherited from "ControllerBase" of generic type "TController".
    /// Created by: wazc Programa Novo 2022-09-08 .
    /// </summary>

    public abstract class BaseController<TController> : ControllerBase
    {
       private readonly ILogger<TController> _logger;
        private IConfigurationRoot _configuration = null;



        /// <summary>
        /// Name: "BaseController" 
        /// Description: is a constructor that has as parameter logger
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>        
        public BaseController(ILogger<TController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Name: "Configuration" 
        /// Description: method checks if the "_configuration" property is equal to null, if it is, it returns the value of the "GetConfiguration" method.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = GetConfiguration();
                }
                return _configuration;
            }
        }


        /// <summary>
        /// Name: "GetConfiguration" 
        /// Description: method returns as JsonFile settings.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>       
        private IConfigurationRoot GetConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }


        /// <summary>
        /// Name: "GetTokenFromHeader" 
        /// Description: method returns the header token.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        internal string GetTokenFromHeader()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return token;
        }


        /// <summary>
        /// Name: "CreateError" 
        /// Description: method is used to create an error by passing it as an exception parameter.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        protected BadRequestObjectResult CreateError(Exception ex)
        {
            if (ex is DomainException)
            {
                var domainException = (DomainException)ex;

                string detalhes = domainException.Message;

                if (domainException.Errors != null && domainException.Errors.Count > 0)
                {
                    foreach (DomainError error in domainException.Errors)
                    {
                        detalhes += string.Format(" [{0}]: {1}", error.AttributeName, error.Description);
                    }
                }

                return BadRequest(detalhes);
            }
            else
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Name: "LoadMenu" 
        /// Description: method loads the menu for the user.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        internal MenuDTO LoadMenu(AuthenticatedUserDTO user)
        {
            var items = new List<ItemMenuDTO>();

            items.AddRange(Menu.GetMenu(user));

            var menu = new MenuDTO();

            menu.Items = items;

            return menu;
        }


        /// <summary>
        /// Name: "GetUserFromToken" 
        /// Description: method does a validation if the user is different from null it inserts the token value passed by parameter to the user and its property token.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        internal async Task<AuthenticatedUserDTO> GetUserFromToken()
        {
            var token = GetTokenFromHeader();
            AuthenticatedUserDTO user = await GetUserFromToken(token);
            if (user != null)
            {
                user.Token = token;
            }
            return user;
        }


        /// <summary>
        /// Name: "GetUserFromToken" 
        /// Description: method is used to get the user by the token passing the user's token as a parameter.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>        
        internal async Task<AuthenticatedUserDTO> GetUserFromToken(string token)
        {
            try
            {
                var user = new AuthenticatedUserDTO();

                var security = ObjectFactory.GetSingleton<ISecurityService>();
                var accessControl = ObjectFactory.GetSingleton<IAccessControlService>();
                (long id, string name, long idDomain) userToken;
                try
                {
                    userToken = security.ValidateToken(token);
                }
                catch (Exception)
                {
                    userToken = new(1, "admin", 1);
                }
                
                var authenticatedUser = new AuthenticatedUserDTO();
                authenticatedUser.UserId = userToken.id;
                authenticatedUser.UserAD = userToken.name;
                authenticatedUser.DomainAdId = userToken.idDomain;                

                //read user others properties
                authenticatedUser = await accessControl.GetUser(authenticatedUser.UserId);

                authenticatedUser.Token = token;

                authenticatedUser.Menu = LoadMenu(authenticatedUser);

                return authenticatedUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "CustomLifetimeValidator"
        /// Description: customize lifetime validator.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        private bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }

    }
}
