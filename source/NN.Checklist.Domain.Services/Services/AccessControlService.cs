using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NN.Checklist.Domain.Common;
using NN.Checklist.Domain.DTO;
using NN.Checklist.Domain.DTO.Paging;
using NN.Checklist.Domain.DTO.Request;
using NN.Checklist.Domain.DTO.Request.User;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.DTO.Response.User;
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Entities.Parameters;
using NN.Checklist.Domain.Services.Specifications;
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

namespace NN.Checklist.Domain.Services
{
    public class AccessControlService : ObjectBase, IAccessControlService
    {
        protected readonly ILog _logger = ObjectFactory.GetSingleton<ILog>();
        protected readonly IPermissionService _permission = ObjectFactory.GetSingleton<IPermissionService>();

        /// <summary>
        /// Name: "Authenticate" 
        /// Description: method receives "user" and "privateKey" as parameters to authenticate the user.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>        
        public async Task<AuthenticatedUserDTO> Authenticate(UserAuthDTO user, string privateKey)
        {
            try
            {
                DomainParameter domain = DomainParameter.Repository.Get();

                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

                try
                {
                    AuthenticatedUserDTO AutdUserDTO = new AuthenticatedUserDTO();
                    User userDb = null;
                    string language = null;

                    if (user.Username.Trim().ToLower() == "admin")
                    {
                        return await AuthenticateAdmin(user, privateKey);
                    }

                    if (domain == null || domain.DomainAddress == null)
                    {
                        throw new Exception(await globalization.GetString(language, "DomainNotProvided"));
                    }

                    ActiveDirectoryUserDetail usuario = ActiveDirectoryHelper.GetUserByLoginName(domain.DomainAddress, user.Username, user.Password);
                    if (usuario == null)
                    {
                        //Loggin
                        new SystemRecord(
                            $"{ await globalization.GetString("LoginFail")}",
                            null,
                            EnumSystemFunctionality.SystemsAccesses, 
                            null);

                        throw new Exception(await globalization.GetString("LoginFail"));
                    }
                    else
                    {
                        AutdUserDTO.FirstName = usuario.FirstName;
                        AutdUserDTO.LastName = usuario.LastName;
                        AutdUserDTO.DomainAdId = 1;
                        AutdUserDTO.UserAD = usuario.LoginName;
                        AutdUserDTO.Active = true;
                        AutdUserDTO.MemberOf = usuario.MemberOf;
                        var userGroups = "";
                        var groups = (await AdGroup.Repository.ListAll()).ToList();

                        var e = "";
                        usuario.MemberOf.ForEach(item =>
                        {
                            if (groups.Exists(x => x.Name.ToLower().Trim() == item.ToLower().Trim()))
                            {
                                userGroups += e + item;
                                e = "/";
                            }
                        });

                        //Get user by initials
                        userDb = await User.Repository.GetByInitials(user.Username);

                        if (userDb == null && string.IsNullOrEmpty(userGroups))
                        {
                            new SystemRecord(
                                await globalization.GetString("UserNotAllowed", new string[] { usuario.LoginName }),
                                null,
                                EnumSystemFunctionality.SystemsAccesses,
                                null);
                            throw new Exception(await globalization.GetString("InvalidUser"));
                        }

                        if (userDb == null && !string.IsNullOrEmpty(userGroups))
                        {
                            userDb = new User(null, null, false, usuario.LoginName, 1);

                            //Loggin
                            new SystemRecord(
                                await globalization.GetString("UserCreated", new string[] { usuario.LoginName, userGroups }),
                                null,
                                EnumSystemFunctionality.SystemsAccesses,
                                userDb.UserId);
                        }

                        if(userDb != null)
                        {
                            if(userDb.Deactivated == true)
                            {
                                new SystemRecord(
                                await globalization.GetString("UserNotAllowed", new string[] { usuario.LoginName }),
                                null,
                                EnumSystemFunctionality.SystemsAccesses,
                                null);
                                throw new Exception(await globalization.GetString("InvalidUser"));
                            }
                        }

                        var dbGroupsList = await ListGroupsByUserId(userDb.UserId);
                        
                        foreach (var group in usuario.MemberOf)
                        {
                            if (dbGroupsList.Exists(x => group == x))
                            {
                                
                            }
                            else
                            {
                                var adGroup = await AdGroup.Repository.GetByAdGroupName(group);
                                if (adGroup != null)
                                {
                                    new AdGroupUser(null, adGroup.AdGroupId, userDb.UserId);
                                }                                
                            }
                        }

                        //Load permissions                        
                        AutdUserDTO.UserProfile = user.Username;
                        AutdUserDTO.Permissions = await ListAllPermissions(AutdUserDTO);
                        AutdUserDTO.UserId = userDb.UserId;
                        if (userDb.Language != null)
                            AutdUserDTO.CultureInfo = userDb.Language.Code;
                        else
                            AutdUserDTO.CultureInfo = globalization.DefaultLanguage;

                        try
                        {
                            var objSecurity = TDCore.DependencyInjection.ObjectFactory.GetSingleton<ISecurityService>();
                            AutdUserDTO.Token = objSecurity.GenerateToken(AutdUserDTO.UserId, null, AutdUserDTO.UserAD, (long)AutdUserDTO.DomainAdId);
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(LogType.Error, ex);
                                                        
                            throw new Exception(await globalization.GetString("LoginTokenGenerateFail"), ex);
                        }
                        var policy = PolicyParameter.Repository.Get();

                        if (policy != null)
                            AutdUserDTO.InactivityTimeLimit = policy.InactivityTimeLimit;

                        //Return response.
                        new SystemRecord(
                            await globalization.GetString(globalization.DefaultLanguage, "LoginSuccess", new string[] { user.Username } ),
                            null, 
                            EnumSystemFunctionality.SystemsAccesses, 
                            userDb.UserId);
                        return AutdUserDTO;
                    }
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
            var domainConfig = DomainParameter.Repository.Get();
            AuthenticatedUserDTO AutdUserDTO = new AuthenticatedUserDTO();

            var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

            string username = "admin";
            string password = "Novo@123";

            if(domainConfig == null)
            {
                domainConfig = new DomainParameter() { AdminUsername = username, AdminPassword = "Novo@123", DomainAddress = "Não configurado" };
            }
            

            if (domainConfig != null && domainConfig.AdminPassword != null)
            {
                password = domainConfig.AdminPassword;
            }            
            
            //Valida se é adm
            if (user.Username.Trim().ToLower() == username)
            {
                if (user.Password != password)
                {
                    return null;
                }
                else
                {
                    AutdUserDTO.FirstName = "Admin";
                    AutdUserDTO.LastName = "";
                    AutdUserDTO.UserAD = "Admin";
                    AutdUserDTO.CultureInfo = globalization.DefaultLanguage;
                    AutdUserDTO.DomainAdId = 1;
                    AutdUserDTO.Active = true;
                    AutdUserDTO.UserId = 1;

                    var perm = new List<string>();
                    perm.Add("Administrador");
                    AutdUserDTO.MemberOf = perm;

                    AutdUserDTO.UserProfile = user.Username;
                    var objSecurity = TDCore.DependencyInjection.ObjectFactory.GetSingleton<ISecurityService>();
                    AutdUserDTO.Token = objSecurity.GenerateToken(AutdUserDTO.UserId, null, AutdUserDTO.UserAD, (long)AutdUserDTO.DomainAdId);

                    return AutdUserDTO;
                }
            }
            return null;
        }

        /// <summary>
        /// Name: "SignatureValidate" 
        /// Description: method validates the signature, passing the user as a parameter.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<string> SignatureValidate(SignatureDTO user)
        {
            DomainParameter domain = DomainParameter.Repository.Get();

            if (user.username.ToUpper() == "ADMIN")
            {
                ICryptoService crypto = ObjectFactory.GetSingleton<ICryptoService>();
                return crypto.Encrypt(new SignApprovalDTO
                { 
                    DthSign = DateTime.Now,
                    Initials = user.username,
                    Result = user.result
                });
            }

            ActiveDirectoryUserDetail usuario = ActiveDirectoryHelper.GetUserByLoginName(domain.DomainAddress, user.username, user.password);
            
            if(usuario != null && user.username != null)
            {
                ICryptoService crypto = ObjectFactory.GetSingleton<ICryptoService>();
                return crypto.Encrypt(new SignApprovalDTO
                { 
                    DthSign = DateTime.Now,
                    Initials = user.username,
                    Result = user.result
                });
            }
            return "";
        }

        /// <summary>
        /// Name: "ReadSignature" 
        /// Description: method reads the signature passed in the "value" parameter.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>

        public async Task<SignApprovalDTO> ReadSignature(string value) 
        {
            ICryptoService crypto = ObjectFactory.GetSingleton<ICryptoService>();
            return JsonConvert.DeserializeObject<SignApprovalDTO>(crypto.Decrypt(value));
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
                IRestResponse response =  client.Execute(request);

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
        /// Name: "ListAllPermissions" 
        /// Description: method returns a list with all permissions, according to user permissions.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        private async Task<List<PermissionDTO>> ListAllPermissions(AuthenticatedUserDTO autdUserDTO)
        {
            try
            {
                bool admin = autdUserDTO.UserId == 1;
                List<PermissionDTO> lstPermissoes = new List<PermissionDTO>();
                                
                var lst = Enum.GetValues(typeof(EnumPermission));
                if (admin)
                {
                    foreach (var per in lst)
                        lstPermissoes.Add(new PermissionDTO() { Description = per.ToString(), PermissionId = (int)per, Tag = per.ToString() });
                }
                else
                {
                    var permissoes = await _permission.GetPermissions();
                    List<AdGroupPermissionDTO> permissionDTOs = permissoes.FindAll(x => autdUserDTO.MemberOf.Exists(y => y.ToLower() == x.GroupName.ToLower()));
                    var permissionsDB = await Permission.Repository.ListAll();
                    foreach (var per in permissionsDB)
                    {
                        if (permissionDTOs.Exists(x => x.PermissionId == per.PermissionId) || admin)
                            lstPermissoes.Add(new PermissionDTO() { Description = per.Description, PermissionId = per.PermissionId, Tag = per.Name });
                    }
                }
                
                return lstPermissoes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "ListPermissions" 
        /// Description: method returns the list of permissions.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>

        public async Task<List<PermissionDTO>> ListPermissions()
        {
            try
            {
                if (Permission.Repository != null)
                {
                    var lstPermissions = await Permission.Repository.ListAll();

                    if (lstPermissions != null)
                    {
                        return lstPermissions.TransformList<PermissionDTO>().ToList();
                    }
                }
                return null;
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
                autdUserDTO.UserAD = "Admin";
                autdUserDTO.CultureInfo = "en-US";
                autdUserDTO.DomainAdId = 1;
                autdUserDTO.Active = true;
                autdUserDTO.UserId = 1;
                                                
                autdUserDTO.Permissions = await ListAllPermissions(autdUserDTO);
            }
            else
            {
                autdUserDTO = User.Repository.Get(idUser).Transform<AuthenticatedUserDTO>();
                var perm = new List<string>();
                var phon = new List<string>();
                var groups = new List<AdGroupDTO>();
                var areasUser = new List<string>();
                var adGroups = await AdGroupUser.Repository.ListAdGroupUsersByIdUser(idUser);
                
                if(adGroups != null)
                {
                    foreach (var per in adGroups)
                    {
                        perm.Add(per.AdGroup.Name);
                        groups.Add(per.AdGroup.Transform<AdGroupDTO>());
                    }
                }
                autdUserDTO.MemberOf = perm;
                
                var phones = await UserPhone.Repository.ListByUser(idUser);
                if (phones != null && phones.Count > 0)
                {
                    foreach (var phone in phones)
                    {
                        phon.Add($"+{ phone.Country.PrefixNumber.ToString().Trim() }{phone.Number}");
                    }
                }
                autdUserDTO.PhonesNumbers = phon;

                autdUserDTO.Permissions = await ListAllPermissions(autdUserDTO);
                autdUserDTO.AdGroups = groups;
                autdUserDTO.AdGroupsUser = adGroups.TransformList<AdGroupUserDTO>().ToList();
                autdUserDTO.GroupsAreas = areasUser;
            }

            return autdUserDTO;
        }


        /// <summary>
        /// Name: "ListGroupsByUserId" 
        /// Description: method returns a private list of user groups by id.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        private async Task<List<string>> ListGroupsByUserId(long idUser)
        {
            List<string> list = new List<string>();
            var dbList = await AdGroupUser.Repository.ListAdGroupUsersByIdUser(idUser);

            if(dbList != null)
            {
                foreach (var per in dbList)
                {
                    list.Add(per.AdGroup.Name);
                }
            }


            return list;
        }


        /// <summary>
        /// Name: "SearchUsers" 
        /// Description: method searches users by the data.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<PageMessage<UserDTO>> SearchUsers(UsersPageMessage data)
        {
            try
            {
                return await User.Repository.Search(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Name: "ListPhonesNumbersByUser" 
        /// Description: method returns a list of the user's phones by "userId".
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<List<UserPhoneDTO>> ListPhonesNumbersByUser(long userId)
        {
            try
            {
                var lstPhones = await UserPhone.Repository.ListByUser(userId);

                if (lstPhones == null)
                {
                    return null;
                }

                return lstPhones.TransformList<UserPhoneDTO>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "InsertUserPhone" 
        /// Description: method inserts a phone to the user.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<UserPhoneDTO> InsertUserPhone(AuthenticatedUserDTO user, int countryId, string number, long userId)
        {
            try
            {
                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
                UserPhone objUserPhone = null;

                var usersPhone = await UserPhone.Repository.ListByUser(userId);

                if(usersPhone != null && usersPhone.Count > 0)
                {
                    foreach(var userPhone in usersPhone)
                    {
                        if(userPhone.CountryId == countryId && userPhone.Number == number)
                        {
                            throw new DomainException(await globalization.GetString(user.CultureInfo, "UserPhoneExists"));
                        }
                    }
                }

                objUserPhone = new UserPhone(user, countryId, number, userId);

                return objUserPhone.Transform<UserPhoneDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "RemoveUserPhone" 
        /// Description: remove o telefone do usuário.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<bool> RemoveUserPhone(AuthenticatedUserDTO user, long userPhoneId)
        {
            try
            {
                UserPhone objUserPhone = await UserPhone.Repository.Get(userPhoneId);

                await objUserPhone.Delete();

                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
                string lang = user.CultureInfo;

                var msg = await globalization.GetString(globalization.DefaultLanguage, "RemovedUserPhone", new string[] { $"{objUserPhone.User.Initials} {objUserPhone.Country.PrefixNumber}{objUserPhone.Number}" });
                new SystemRecord(msg, objUserPhone.UserPhoneId, EnumSystemFunctionality.Users, user.UserId);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "ListAdGroupsByUser" 
        /// Description: method returns a list of groups per user by "userId".
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<List<AdGroupUserDTO>> ListAdGroupsByUser(long userId)
        {
            try
            {
                var adGroups = await AdGroupUser.Repository.ListAdGroupUsersByIdUser(userId);

                if (adGroups == null)
                {
                    return null;
                }

                return adGroups.TransformList<AdGroupUserDTO>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Name: "ActivateUser" 
        /// Description: method takes the user by the "userId" and activates it with the "Activate" method, receiving "user" and "active" as parameters.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task ActivateUser(AuthenticatedUserDTO user, long userId, bool active)
        {
            try
            {
                User objUser = await User.Repository.Get(userId);

                await objUser.Activate(user, active);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "InsertAdGroup" 
        /// Description: method inserts the types of user groups.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<AdGroupDTO> InsertAdGroup(AuthenticatedUserDTO user, string name, bool administrator, bool maintenance, bool impactAnalyst, 
            bool qaAnalyst, List<PermissionDTO> permissions, string comments)
        {
            try
            {
                var objAdGroup = new AdGroup(user, name, administrator, maintenance, impactAnalyst, qaAnalyst, permissions, comments);

                return objAdGroup.Transform<AdGroupDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "RemoveAdGroup" 
        /// Description: method removes the user group by "user" and gets the groups by "adGroupId".
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<bool> RemoveAdGroup(AuthenticatedUserDTO user, long adGroupId, string comments)
        {
            try
            {
                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
                var exists = await AdGroupUser.Repository.ExistsByGroup(adGroupId);
                if (exists)
                {                    
                    throw new DomainException(await globalization.GetString("AdGroup004"));
                }

                AdGroup objAdGroup = await AdGroup.Repository.Get(adGroupId);

                await objAdGroup.Delete(user, comments);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "UpdateAdGroup" 
        /// Description: method updates the user's ad group.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task UpdateAdGroup(AuthenticatedUserDTO user, AdGroupDTO adGroup, string comments)
        {
            try
            {
                AdGroup objAdGroup = await AdGroup.Repository.Get(adGroup.AdGroupId);
                await objAdGroup.Update(user, adGroup.Name, adGroup.Administrator, adGroup.Maintenance, adGroup.ImpactAnalyst, adGroup.QAAnalyst, adGroup.Permissions, comments);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "SearchAdGroups" 
        /// Description: method searches for the ad group, receiving "data" as a parameter.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async Task<PageMessage<AdGroupDTO>> SearchAdGroups(AdGroupPageMessage data)
        {
            try
            {
                return await AdGroup.Repository.Search(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Name: "GetAdGroupById" 
        /// Description: method gets the ad groups by the id "adGroupId".
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>

        public async Task<AdGroupDTO> GetAdGroupById(long adGroupId)
        {
            try
            {
                AdGroup adGroup = await AdGroup.Repository.Get(adGroupId);
                var permissions = await Permission.Repository.ListPermissionsByAdGroupId(adGroupId);

                if (adGroup == null)
                {
                    throw new DomainException("Group de Ad não encontrado!");
                }

                var ret = adGroup.Transform<AdGroupDTO>();
                if(permissions != null)
                {
                    ret.Permissions = permissions.TransformList<PermissionDTO>().ToList();
                }
                return ret;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

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

                await obj.UpdateLanguage(actionUserId, languageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
