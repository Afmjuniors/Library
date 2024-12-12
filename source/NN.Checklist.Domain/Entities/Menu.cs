using NN.Checklist.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NN.Checklist.Domain.Entities
{
    public class Menu
    {
        /// <summary>
        /// Name: GetMenu
        /// Description: Method that takes the user as a parameter and checks the user's permissions and returns the menu to the same.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static List<ItemMenuDTO> GetMenu(AuthenticatedUserDTO user)
        {
     
            var lst = new List<ItemMenuDTO>();

            var checklists = GetMenuChecklists(user);

            if (checklists != null)
            {
                lst.AddRange(checklists);
            }

            var operations = GetMenuValidations(user);
                
            if (operations != null)
            {
                lst.AddRange(operations);
            }

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "AUDIT_TRAIL").Count() > 0)
            {
                var audit = new ItemMenuDTO()
                {
                    Title = "Audit Trail",
                    Alignment = "left",
                    Page = "/audit-trail",
                    Translate = "MENU.AUDIT_TRAIL"
                };

                lst.Add(audit);
            }

            var setup = GetMenuSetup(user);
            if (setup != null)
            {
                lst.AddRange(setup);
            }


            return lst;
        
        }

        /// <summary>
        /// Name: GetMenuRecords
        /// Description: Method that takes as a parameter user and checks the permissions and returns the submenu information of the records.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static List<ItemMenuDTO> GetMenuChecklists(AuthenticatedUserDTO user)
        {
            var listSub = new List<ItemMenuDTO>();

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "CHECKLISTS").Count() > 0)
            {             
                var ret = new List<ItemMenuDTO>();

                var menuItem = new ItemMenuDTO()
                {
                    Root = true,
                    Title = "Operações",
                    Alignment = "left",
                    Toggle = "click",
                    Translate = "MENU.CHECKLISTS",
                    Page = "/checklists",
                };

                ret.Add(menuItem);

                return ret;
            }

            return null;
        }

        /// <summary>
        /// Name: GetMenuRecords
        /// Description: Method that takes as a parameter user and checks the permissions and returns the submenu information of the records.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static List<ItemMenuDTO> GetMenuValidations(AuthenticatedUserDTO user)
        {
            var listSub = new List<ItemMenuDTO>();

            var a11Filling = GetMenuA11Filling(user);

            if (a11Filling != null)
            {
                listSub.AddRange(a11Filling);
            }

            var a11Formulation = GetMenuA11Formulation(user);

            if (a11Formulation != null)
            {
                listSub.AddRange(a11Formulation);
            }

            var a12Release = GetMenuA12ReleaseANP(user);

            if (a12Release != null)
            {
                listSub.AddRange(a12Release);
            }

            if (listSub.Count > 0)
            {
                var ret = new List<ItemMenuDTO>();

                var menuItem = new ItemMenuDTO()
                {
                    Root = true,
                    Title = "Validações",
                    Alignment = "left",
                    Toggle = "click",
                    Translate = "MENU.VALIDATIONS",
                    Submenu = listSub
                };

                ret.Add(menuItem);

                return ret;
            }
            return null;
        }

        
        /// <summary>
        /// Name: GetMenuRecords
        /// Description: Method that takes as a parameter user and checks the permissions and returns the submenu information of the records.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static List<ItemMenuDTO> GetMenuA11Filling(AuthenticatedUserDTO user)
        {
            var listSub = new List<ItemMenuDTO>();

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "VALIDATIONS").Count() > 0)
            {
                var materialConsumption = new ItemMenuDTO()
                {
                    Title = "Consumo de Material",
                    Alignment = "left",
                    Page = "/material-consumption",
                    Translate = "MENU.MATERIAL_CONSUMPTION"
                };
                listSub.Add(materialConsumption);
            }

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "VALIDATIONS").Count() > 0)
            {
                var servicesOrders = new ItemMenuDTO()
                {
                    Title = "Busca de Ordens de Serviços",
                    Alignment = "left",
                    Page = "/services-orders-search",
                    Translate = "MENU.SERVICES_ORDERS_SEARCH"
                };
                listSub.Add(servicesOrders);
            }
            
            if (listSub.Count > 0)
            {
                var ret = new List<ItemMenuDTO>();

                var menuItem = new ItemMenuDTO()
                {
                    Title = "A11 Enchimento",
                    Alignment = "right",
                    Toggle = "click",
                    Translate = "MENU.A11FILLING",
                    Submenu = listSub
                };

                ret.Add(menuItem);

                return ret;
            }
            return null;
        }

        /// <summary>
        /// Name: GetMenuRecords
        /// Description: Method that takes as a parameter user and checks the permissions and returns the submenu information of the records.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static List<ItemMenuDTO> GetMenuA11Formulation(AuthenticatedUserDTO user)
        {
            var listSub = new List<ItemMenuDTO>();

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "VALIDATIONS").Count() > 0)
            {
                var materialsStatus = new ItemMenuDTO()
                {
                    Title = "Status de Materiais",
                    Alignment = "left",
                    Page = "/materials-status",
                    Translate = "MENU.MATERIALS_STATUS"
                };
                listSub.Add(materialsStatus);
            }

            if (listSub.Count > 0)
            {
                var ret = new List<ItemMenuDTO>();

                var menuItem = new ItemMenuDTO()
                {
                    Title = "A11 Formulação",
                    Alignment = "right",
                    Toggle = "click",
                    Translate = "MENU.A11FORMULATION",
                    Submenu = listSub
                };

                ret.Add(menuItem);

                return ret;
            }
            return null;
        }

        /// <summary>
        /// Name: GetMenuRecords
        /// Description: Method that takes as a parameter user and checks the permissions and returns the submenu information of the records.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static List<ItemMenuDTO> GetMenuA12ReleaseANP(AuthenticatedUserDTO user)
        {
            var listSub = new List<ItemMenuDTO>();

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "VALIDATIONS").Count() > 0)
            {
                var analisysQC = new ItemMenuDTO()
                {
                    Title = "Resultados de Análise de QC",
                    Alignment = "left",
                    Page = "/analisys-qc",
                    Translate = "MENU.ANALISYS_QC"
                };
                listSub.Add(analisysQC);
            }

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "VALIDATIONS").Count() > 0)
            {
                var wfi = new ItemMenuDTO()
                {
                    Title = "WFI",
                    Alignment = "left",
                    Page = "/wfi",
                    Translate = "MENU.WFI"
                };
                listSub.Add(wfi);
            }

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "VALIDATIONS").Count() > 0)
            {
                var cpeReports = new ItemMenuDTO()
                {
                    Title = "Relatórios do CPE",
                    Alignment = "left",
                    Page = "/cpe-reports",
                    Translate = "MENU.CPE_REPORTS"
                };
                listSub.Add(cpeReports);
            }

            if (listSub.Count > 0)
            {
                var ret = new List<ItemMenuDTO>();

                var menuItem = new ItemMenuDTO()
                {
                    Title = "A12 Liberação - ANP",
                    Alignment = "right",
                    Toggle = "click",
                    Translate = "MENU.A12RELEASE_ANP",
                    Submenu = listSub
                };

                ret.Add(menuItem);

                return ret;
            }
            return null;
        }

        /// <summary>
        /// Name: GetMenuSetup
        /// Description: Method that takes as a parameter user and checks the permissions and returns the information from the Setup submenu.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static List<ItemMenuDTO> GetMenuSetup(AuthenticatedUserDTO user)
        {
            var listSub = new List<ItemMenuDTO>();

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "MANAGE_PARAMETER").Count() > 0)
            {

                var parameters = new ItemMenuDTO()
                {
                    Title = "Parâmetros",
                    Alignment = "left",
                    Page = "/parameters",
                    Translate = "MENU.PARAMETERS"
                };
                listSub.Add(parameters);
               
                var adGroups = new ItemMenuDTO()
                {
                    Title = "Grupos de Ad",
                    Alignment = "left",
                    Page = "/ad-groups",
                    Translate = "MENU.ACCESS_PROFILES"
                };
                listSub.Add(adGroups);


            }

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "MANAGE_USERS").Count() > 0)
            {
                var users = new ItemMenuDTO()
                {
                    Title = "Lista de Usuários",
                    Alignment = "left",
                    Page = "/users",
                    Translate = "MENU.USERS"
                };
                listSub.Add(users);
            }
      

            if (listSub.Count > 0)
            {
                var ret = new List<ItemMenuDTO>();

                var menuItem = new ItemMenuDTO()
                {
                    Root = true,
                    Title = "Configurações",
                    Alignment = "Left",
                    Toggle = "click",
                    Translate = "MENU.SETUP",
                    Submenu = listSub
                };

                ret.Add(menuItem);

                return ret;
            }
            return null;
        }
    }
}
