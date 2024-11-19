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

            var records = GetMenuRecords(user);
                
            if (records != null)
            {
                lst.AddRange(records);
            }

            var impact = GetMenuAnalysisImpact(user);
            if (impact != null)
            {
                lst.AddRange(impact);
            }

            var qa = GetMenuQaAnalysis(user);
            if (qa != null)
            {
                lst.AddRange(qa);
            }

            var approval = GetMenuApprovalControl(user);
            if (approval != null)
            {
                lst.AddRange(approval);
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
        public static List<ItemMenuDTO> GetMenuRecords(AuthenticatedUserDTO user)
        {
            var listSub = new List<ItemMenuDTO>();

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "ALARMS").Count() > 0)
            {
                var alarms = new ItemMenuDTO()
                {
                    Title = "Alarmes",
                    Alignment = "left",
                    Page = "/alarms",
                    Translate = "MENU.ALARMS"
                };
                listSub.Add(alarms);
            }


            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "EVENTS").Count() > 0)
            {
                var events = new ItemMenuDTO()
                {
                    Title = "Eventos",
                    Alignment = "left",
                    Page = "/events",
                    Translate = "MENU.EVENTS"
                };

                listSub.Add(events);
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

                listSub.Add(audit);
            }
                        

            if (listSub.Count > 0)
            {
                var ret = new List<ItemMenuDTO>();

                var menuItem = new ItemMenuDTO()
                {
                    Root = true,
                    Title = "Registros",
                    Alignment = "Left",
                    Toggle = "click",
                    Translate = "MENU.RECORDS",
                    Submenu = listSub
                };

                ret.Add(menuItem);

                return ret;
            }
            return null;
        }

        /// <summary>
        /// Name: GetMenuAnalysisImpact
        /// Description: Method that takes as a parameter user and checks the permissions and returns the information from the AnalysisImpact submenu.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static List<ItemMenuDTO> GetMenuAnalysisImpact(AuthenticatedUserDTO user)
        {
            var listSub = new List<ItemMenuDTO>();

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "IMPACT_ANALYSIS").Count() > 0)
            {
                var overview = new ItemMenuDTO()
                {
                    Title = "Overview",
                    Alignment = "left",
                    Page = "/impact-analysis-overview",
                    Translate = "MENU.OVERVIEW"
                };


                listSub.Add(overview);

                var performAnalysis = new ItemMenuDTO()
                {
                    Title = "Realizar Análise",
                    Alignment = "left",
                    Page = "/perform-impact-analysis",
                    Translate = "MENU.PERFORM_ANALYSIS"
                };

                listSub.Add(performAnalysis);

                var ret = new List<ItemMenuDTO>();

                var menuItem = new ItemMenuDTO()
                {
                    Title = "Análise Impacto",
                    Root = true,
                    Alignment = "Left",
                    Toggle = "click",
                    Translate = "MENU.ANALYSIS_IMPACT",
                    Submenu = listSub
                };

                ret.Add(menuItem);

                return ret;
            }

            return null;
        }

        /// <summary>
        /// Name: GetMenuQaAnalysis
        /// Description: Method that takes as a parameter user and checks the permissions and returns the information from the QaAnalysis submenu.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static List<ItemMenuDTO> GetMenuQaAnalysis(AuthenticatedUserDTO user)
        {
            var listSub = new List<ItemMenuDTO>();

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "QA_OVERVIEW").Count() > 0)
            {
                var overview = new ItemMenuDTO()
                {
                    Title = "Overview",
                    Alignment = "left",
                    Page = "/qa-overview",
                    Translate = "MENU.OVERVIEW"
                };
                listSub.Add(overview);
            }

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "QA_ANALYSIS").Count() > 0)
            {
                var performApproval = new ItemMenuDTO()
                {
                    Title = "Realizar Aprovação",
                    Alignment = "left",
                    Page = "/qa-analysis",
                    Translate = "MENU.PERFORM_APPROVAL"
                };
                listSub.Add(performApproval);
            }

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "QA_REPORT").Count() > 0)
            {
                var reportApprovals = new ItemMenuDTO()
                {
                    Title = "Relatório de Aprovações",
                    Alignment = "left",
                    Page = "/qa-report",
                    Translate = "MENU.REPORT_APPROVALS"
                };

                listSub.Add(reportApprovals);
            }

            if (listSub.Count > 0)
            {
                var ret = new List<ItemMenuDTO>();

                var menuItem = new ItemMenuDTO()
                {
                    Root = true,
                    Title = "Análise QA",
                    Alignment = "Left",
                    Toggle = "click",
                    Translate = "MENU.QA_ANALYSIS",
                    Submenu = listSub
                };

                ret.Add(menuItem);

                return ret;
            }

            return null;
        }

        /// <summary>
        /// Name: GetMenuApprovalControl
        /// Description: Method that takes as a parameter user and checks the permissions and returns the information from the ApprovalControl submenu.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static List<ItemMenuDTO> GetMenuApprovalControl(AuthenticatedUserDTO user)
        {
            var listSub = new List<ItemMenuDTO>();

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "BATCH_REVIEW").Count() > 0)
            {
                var batch = new ItemMenuDTO()
                {
                    Title = "Batch Release",
                    Alignment = "left",
                    Page = "/batch",
                    Translate = "MENU.BATCH_RELEASE"
                };
                listSub.Add(batch);


                var ret = new List<ItemMenuDTO>();

                var menuItem = new ItemMenuDTO()
                {
                    Root = true,
                    Title = "Controle de Aprovação",
                    Alignment = "Left",
                    Toggle = "click",
                    Translate = "MENU.APPROVAL_CONTROL",
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
                
                var setup = new ItemMenuDTO()
                {
                    Title = "Grupos de Registros",
                    Alignment = "left",
                    Page = "/config",
                    Translate = "MENU.CONFIGURE_RECORDS"
                };
                listSub.Add(setup);
                
                var adGroups = new ItemMenuDTO()
                {
                    Title = "Grupos de Ad",
                    Alignment = "left",
                    Page = "/ad-groups",
                    Translate = "ACCESS_PROFILES"
                };
                listSub.Add(adGroups);


            }

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "DIAGNOSTIC").Count() > 0)
            {
                var users = new ItemMenuDTO()
                {
                    Title = "Diagnóstico",
                    Alignment = "left",
                    Page = "/diagnostic",
                    Translate = "MENU.DIAGNOSTIC"
                };
                listSub.Add(users);
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

            if (user != null && user.Permissions != null && user.Permissions.Count > 0 && user.Permissions.Where(x => x.Tag == "MANAGE_AREAS").Count() > 0)
            {
                var users = new ItemMenuDTO()
                {
                    Title = "Lista de Áreas",
                    Alignment = "left",
                    Page = "/areas",
                    Translate = "MENU.AREAS"
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
