using Library.Domain.DTO.Request;
using Library.Domain.DTO.Response;
using Library.Domain.Entities;
using Library.Domain.Services.Specifications;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDCore.Core;
using TDCore.Core.Logging;
using TDCore.Data.Paging;
using TDCore.DependencyInjection;
using TDCore.Domain.Exceptions;
using TDCore.Web.Security;
using TDCore.Windows.ActiveDirectory;

namespace Library.Domain.Services
{
    public class AccessControlService : ObjectBase, IAccessControlService
    {
        protected readonly ILog _logger = ObjectFactory.GetSingleton<ILog>();

        /// <summary>
        /// Name: "Authenticate" 
        /// Description: method receives "user" and "privateKey" as parameters to authenticate the user.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>        
        public async Task<AuthenticatedUserDTO> Authenticate(UserAuthDTO user, string privateKey)
        {
            try
            {

                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

                try
                {
                    AuthenticatedUserDTO AutdUserDTO = new AuthenticatedUserDTO();
                    User userDb = null;
                    string language = null;





#if DEBUG


#else
                    

#endif

                    return default;


                }
                catch (Exception ex)
                {
                    Logger.Log(LogType.Error, ex);
                    throw ex;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Name: "AuthenticateAdmin" 
        /// Description: method authenticates the admin user.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<AuthenticatedUserDTO> AuthenticateAdmin(UserAuthDTO user, string privateKey)
        {
            AuthenticatedUserDTO AutdUserDTO = new AuthenticatedUserDTO();

            var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

            string username = "admin";
            string password = "Novo@123";




            return null;
        }




        /// <summary>
        /// Name: "Token" 
        /// Description: method authenticates the user's token.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>        
        public AuthenticatedUserDTO Token(string token)
        {
            try
            {
                AuthenticatedUserDTO resp = new AuthenticatedUserDTO();
                var usuario = new AuthenticatedUserDTO();

                var client = new RestClient($"?token={token}");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + token);
                IRestResponse response = client.Execute(request);

                var obj = JsonConvert.DeserializeObject<AuthenticatedUserDTO>(response.Content);
                resp = obj;
                return resp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        /// <summary>
        /// Name: Configuration
        /// Description: Getting values from appsettags.json
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public static IConfigurationRoot Configuration
        {
            get
            {
                IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                return builder.Build();
            }
        }


        /// <summary>
        /// Name: "GetUser" 
        /// Description: method gets a user authenticated by "IdUser"
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<AuthenticatedUserDTO> GetUser(long idUser)
        {
            AuthenticatedUserDTO autdUserDTO = new AuthenticatedUserDTO();
            if (idUser == 1)
            {
                autdUserDTO.FirstName = "Administrador";
                autdUserDTO.LastName = "do Sistema";
                autdUserDTO.CultureInfo = "en-US";
                autdUserDTO.UserId = 1;

            }
            else
            {
                autdUserDTO = (await User.Repository.Get(idUser)).Transform<AuthenticatedUserDTO>();

            }

            return autdUserDTO;
        }


        ///// <summary>
        ///// Name: "SearchUsers" 
        ///// Description: method searches users by the data.
        ///// Created by: wazc Programa Novo 2022-09-08 .
        ///// </summary>
        //public async Task<PageMessage<UserDTO>> SearchUsers(UsersPageMessage data)
        //{
        //    try
        //    {
        //        return await User.Repository.Search(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Name: "GetAdGroupById" 
        /// Description: method gets the ad groups by the id "adGroupId".
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task UpdateUserLanguage(long actionUserId, long userId, string language)
        {
            try
            {
                var obj = await User.Repository.Get(userId);

                if (obj == null)
                {
                    throw new DomainException("UserNotFound");
                }
                var languageId = await Language.Repository.GetIdByCode(language);

                if (languageId == 0)
                {
                    throw new DomainException("LangguageNotFound");
                }
                if (obj.LanguageId == languageId)
                {
                    return;
                }

                //await obj.UpdateLanguage(actionUserId, languageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
