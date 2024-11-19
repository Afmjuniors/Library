using NN.Checklist.Domain.Common;
using NN.Checklist.Domain.DTO.Response;
using NN.Checklist.Domain.Entities.Bases;
using NN.Checklist.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TDCore.DependencyInjection;
using TDCore.Domain;
using TDCore.Domain.Exceptions;
using TDCore.Globalization;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 2024 12:16:44

#endregion

namespace NN.Checklist.Domain.Entities
{
    public class AdGroup : DomainBaseSRM<AdGroup, IAdGroupRepository<AdGroup, System.Int64>, System.Int64>
    {

        #region Constructors

        /// <summary>
        /// Name: AdGroup
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public AdGroup()
        {

        }

        /// <summary>
        /// Name: AdGroup
        /// Description: Constructor method that receives as parameter user, name, administrator, maintenance, impactAnalyst, qaAnalyst, permissions, comments and checks if the permissions are null or zero, 
        /// otherwise, it uses the validate method to validate and insert the permission in the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public AdGroup(AuthenticatedUserDTO user, System.String name, bool administrator, bool maintenance, bool impactAnalyst, bool qaAnalyst, List<PermissionDTO> permissions, string comments)
        {
            var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

            string lang = null;

            if (user != null)
            {
                lang = user.CultureInfo;

            }

            Name = name;
            Administrator = administrator;
            Maintenance = maintenance;
            ImpactAnalyst = impactAnalyst;
            QAAnalyst = qaAnalyst;

            if (permissions == null || permissions.Count == 0)
            {
                throw new DomainException(globalization.GetString(lang, "AdGroup003").Result);
            }

            if (Validate(user, true).Result)
            {
                string permissionsCreated = "";
                string and = "";

                foreach (var item in permissions)
                {
                    permissionsCreated += and + item.Description;
                    and = ";";
                }

                using (var trans = new TransactionScope())
                {
                    Insert().Wait();

                    foreach (var novo in permissions)
                    { 
                        AddPermission(novo.PermissionId).Wait();
                    }
                    
                    trans.Complete();
                }

                var msg = globalization.GetString(globalization.DefaultLanguage, "NewAdGroup",
                            new string[] { Name, permissionsCreated }).Result;
                new SystemRecord(msg, AdGroupId, EnumSystemFunctionality.SystemsAccesses, user != null ? user.UserId : null, comments);
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("Id", true, EnumValueRanges.Positive)]
        public System.Int64 AdGroupId { get; internal set; }

        [AttributeDescriptor("Name", true)] 
        public System.String Name { get; set; }

        [AttributeDescriptor("Administrator", true)]
        public bool Administrator { get; set; }

        [AttributeDescriptor("Maintenance", true)]
        public bool Maintenance { get; set; }

        [AttributeDescriptor("Impact Analyst", true)]
        public bool ImpactAnalyst { get; set; }

        [AttributeDescriptor("QA Analyst", true)]
        public bool QAAnalyst { get; set; }

        public IList<AdGroupPermission> Permissions { get => GetOneToManyData<AdGroupPermission>().Result; }

        #endregion

        #region User Code

        /// <summary>
        /// Name: Validate
        /// Description: Method that receives as parameter user, newRecord and validates if user is different from null, if yes, lang receives user.CultureInfo.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<bool> Validate(AuthenticatedUserDTO user, bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
                string lang = null;

                if (user != null)
                {
                    lang = user.CultureInfo;
                }

                List<DomainError> errors = new List<DomainError>();

                if (!newRecord && AdGroupId <= 0)
                {
                    errors.Add(new DomainError("AdGroupId", await globalization.GetString(lang, "AdGroup001")));
                }

                if (errors.Count > 0)
                {
                    throw new DomainException(await globalization.GetString(lang, "DataDomainError"), errors);
                }

                return true;
            }
            catch (DomainException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Name: Update
        /// Description: Method that receives as a parameter user, name, administrator, maintenance, impact_analyst, qa_analyst, permissions, comments and update the user group.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task Update(AuthenticatedUserDTO user, string name, bool administrator, bool maintenance, bool impact_analyst, bool qa_analyst, List<PermissionDTO> permissions, string comments)
        {
            try
            {
                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

                string lang = null;
                string permissionsBefore = "";
                string and = "";

                if (user != null)
                {
                    lang = user.CultureInfo;

                    foreach (var item in Permissions)
                    {
                        permissionsBefore += and + item.Permission.Name;
                        and = " | ";
                    }
                }

                var before = (AdGroup)this.MemberwiseClone();

                Name = name;
                Administrator = administrator;
                Maintenance = maintenance;
                ImpactAnalyst = impact_analyst;
                QAAnalyst = qa_analyst;

                if (permissions == null || permissions.Count == 0)
                {
                    throw new DomainException(await globalization.GetString(lang, "AdGroup003"));
                }

                if (await Validate(user, false))
                {
                    using (var trans = new TransactionScope())
                    {
                        await RemovePermissions();
                        if (permissions != null)
                        {
                            foreach (var novo in permissions)
                                await AddPermission(novo.PermissionId);
                        }

                        await Update();
                        trans.Complete();
                    }

                    if (user != null)
                    {
                        string permissionsAfter = "";

                        foreach (var item in Permissions)
                        {
                            permissionsAfter += and + item.Permission.Name;
                            and = " | ";
                        }

                        var msg = await globalization.GetString(globalization.DefaultLanguage, "UpdatedAdGroup",
                            new string[] { GetDifferences(this, before), permissionsBefore, permissionsAfter });
                        new SystemRecord(msg, AdGroupId, EnumSystemFunctionality.SystemsAccesses, user != null ? user.UserId : null, comments);
                    }
                }
            }            
            catch (DomainException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Name: AddPermission
        /// Description: Method that takes idPermission as a parameter and adds the permission.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task AddPermission(long idPermission)
        {
            try
            {
                var permissions = await AdGroupPermission.Repository.ListAdGroupPermissionsByIdAdGroup(AdGroupId);

                if (permissions == null || !permissions.Any(x => x.PermissionId == idPermission))
                {
                    var obj = new AdGroupPermission(null, AdGroupId, idPermission);
                }
            }
            catch (DomainException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DomainException($"Erro na atualização das permissões: {ex.Message}");
            }
        }

        /// <summary>
        /// Name: RemovePermissions
        /// Description: Method that removes the permission.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task RemovePermissions()
        {
            try
            {
                var permissions = await AdGroupPermission.Repository.ListAdGroupPermissionsByIdAdGroup(AdGroupId);

                if (permissions != null)
                {
                    foreach (var permission in permissions)
                    {
                        var obj = permissions.Any(x => x.PermissionId == permission.PermissionId);
                        if (obj != null)
                        {
                            await permission.Delete();
                        }
                    }
                }
            }
            catch (DomainException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Name: Delete
        /// Description: Method that receives as a parameter user, comments and deletes the permission group.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task Delete(AuthenticatedUserDTO user, string comments)
        {
            try
            {
                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

                string lang = null;

                if (user != null)
                {
                    lang = user.CultureInfo;
                }
                var before = GetStringData(this);
                var beforeName = Name;

                using (var tran = new TransactionScope())
                {
                    var permissions = await AdGroupPermission.Repository.ListAdGroupPermissionsByIdAdGroup(AdGroupId);

                    if (permissions != null)
                    {
                        foreach (var per in permissions)
                        {
                            await per.Delete();
                        }
                    }

                    await Delete();

                    tran.Complete();
                }

                var msg = await globalization.GetString(globalization.DefaultLanguage, "RemovedAdGroup",
                            new string[] { beforeName });
                new SystemRecord(msg, AdGroupId, EnumSystemFunctionality.SystemsAccesses, user != null ? user.UserId : null, comments);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}