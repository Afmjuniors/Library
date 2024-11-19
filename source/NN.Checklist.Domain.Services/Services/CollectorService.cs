using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TDCore.DependencyInjection;

namespace NN.Checklist.Domain.Services
{
    public class CollectorService : ObjectBase, ICollectorService
    {
        /// <summary>
        /// Name: "CollectAlarms" 
        /// Description: method is of type asynchronous and has no return. It connects to OleDb collecting the alarm data.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async void CollectAlarms()
        {
            //try
            //{
            //    var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
            //    var processes = Process.Repository.ListAll();

            //    foreach (var process in processes)
            //    {

            //        if (!string.IsNullOrEmpty(process.ConnectionStringAlarms) && process.ConnectionStringAlarms.Trim().Length > 0)
            //        {
            //            DateTime now = DateTime.Now;
            //            using (var conn = new SqlConnection(process.ConnectionStringAlarms))
            //            {
            //                try
            //                {
            //                    conn.Open();

            //                    if (conn.State == ConnectionState.Open)
            //                    {
            //                        var sql = process.QueryAlarms;

            //                        try
            //                        {
            //                            now = DateTime.Now;
            //                            //Collect alarms
            //                            using (SqlCommand comm = new SqlCommand(process.QueryAlarms, (SqlConnection)conn))
            //                            {
            //                                string msg = "";

            //                                now = DateTime.Now;
            //                                if (process.QueryAlarms.Contains("@alarmId"))
            //                                {
            //                                    var alarmId = Process.Repository.Get(process.ProcessId).FirstId;

            //                                    var par = new SqlParameter("alarmId", System.Data.SqlDbType.BigInt);
            //                                    par.Value = alarmId;
            //                                    comm.Parameters.Add(par);

            //                                    msg = alarmId.ToString();
            //                                }

            //                                var list = comm.ExecuteReader();
                                            
            //                                now = DateTime.Now;

            //                                if (list != null)
            //                                {
            //                                    int cont = 0;
            //                                    long id = 0;                                                
            //                                    while (list.Read())
            //                                    {
            //                                        var now2 = DateTime.Now;
            //                                        id = ParseAlarm(process, list);
            //                                        if (id < 0)
            //                                        {
            //                                            Logger.Log(LogType.Error, new Exception($"AlarmId number invalid: {id}"));
            //                                        }

            //                                        if (id > 0)
            //                                        {
            //                                            process.UpdateFirstId(id);
            //                                        }

            //                                        cont++;
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                }                                            

            //                                list.Close();
            //                            }
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            Logger.Log(LogType.Error, ex);
            //                        }
            //                    }
            //                }
            //                catch (Exception ex)
            //                {
            //                    Logger.Log(LogType.Information, "INFO", $"Erro na conexão: '{process.ConnectionStringAlarms}'");
            //                    Logger.Log(TDCore.Core.Logging.LogType.Error, ex);
            //                }
            //                finally
            //                {
            //                    if (conn.State == ConnectionState.Open)
            //                    {
            //                        conn.Close();
            //                    }
            //                }
            //            }
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.Log(TDCore.Core.Logging.LogType.Error, ex);
            //}
        }

        /// <summary>
        /// Name: "ParseAlarm" 
        /// Description: method parses the alarms from the sql that is passed by the parameters.
        /// Created by: wazc Programa Novo 2022-09-08
        /// Updated by: wazc CR0838177 2023-10-26
        /// Description: Add release and impact areas to new types occurrences records
        /// </summary>
        private long ParseAlarm(Process process, SqlDataReader item)
        {
            return 0;
            //string error = "";
            //var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

            //try
            //{
            //    var service = ObjectFactory.GetSingleton<IMessageService>();
            //    long id = Convert<long>(item["alarm_history_id"]);
                
            //    System.DateTime dateTimeStart = Convert<DateTime>(item["date_time_start"]);
       
                
            //    System.DateTime? dateTimeAck = item["date_time_ack"] == DBNull.Value ? null : Convert<DateTime>(item["date_time_ack"]);
            //    System.String ackUser = item["ack_user"] == DBNull.Value ? null : Convert<string>(item["ack_user"]);
            //    System.String[] ackUserArray = { };
            //    if (!string.IsNullOrWhiteSpace(ackUser))
            //    {
            //        ackUserArray = ackUser.Split("::");
            //        ackUser = ackUserArray[1];
            //    }
            //    System.DateTime? dateTimeEnd = item["date_time_end"] == DBNull.Value ? null : Convert<DateTime>(item["date_time_end"]);


            //    System.String systemNodeName = Convert<string>(item["system_node_name"]);
            //    System.Int32? typeQualityId = item["historian_quality_id"] == DBNull.Value ? null : Convert<int>(item["historian_quality_id"]);
                
            //    int? typeSeverityId = item["severity"] == DBNull.Value ? null : Convert<int>(item["severity"]);
            //    System.Int32 typeEventCategoryId = Convert<int>(item["type_event_category_id"]);
            //    System.String message = item["message"] == DBNull.Value ? null : Convert<string>(item["Message"]);
            //    System.String tag = Convert<string>(item["tag"]);
            //    System.Int32? typeOccurrenceLevelId = item["type_occurrence_level_id"] == DBNull.Value ? null : Convert<int>(item["type_occurrence_level_id"]);

            //    if (item["alarm_id"] == DBNull.Value)
            //    {
            //        return id;
            //    }

            //    System.Int64 alarmId = Convert<long>(item["alarm_id"]);
            //    System.Int32 stateId = 4;


            //    var systemNode = SystemNode.Repository.GetByName(systemNodeName);

            //    if (systemNode == null && systemNodeName.Trim().Length > 0)
            //    {
            //        systemNode = new SystemNode(systemNodeName);
            //    }

            //    var typeQuality = new TypeQuality();

            //    if (typeQualityId != null)
            //    {
            //        typeQuality = TypeQuality.Repository.Get(typeQualityId.Value);
            //    }

            //    string record = $"AlarmId:{alarmId};Start:{dateTimeStart};End:{dateTimeEnd};Mesage:{message};SYstem Node:{systemNode.SystemNodeId};Process:{process.Acronym};Quality:{typeQuality.Description};Severity:{typeSeverityId.Value}";

            //    if (tag != null)
            //    {
            //        //Search for the occurrence
            //        var occurrence = OccurrenceRecord.Repository.GetByNodeIdAlarmId(systemNode.SystemNodeId, alarmId);
                  
            //        bool ended = false;
                                
            //        if (occurrence == null)
            //        {
            //            error = $"AlarmId: {alarmId} Tag: {tag}  TypeOccurrenceLevelId: {typeOccurrenceLevelId}  ProcessId: {process.ProcessId}";
            //            var typeOccurrence = TypeOccurrenceRecord.Repository.GetByTypeLevelProcess(EnumOccurrenceType.Alarm, tag, typeOccurrenceLevelId, process.ProcessId, message);
                        
            //            bool newType = false;

            //            if (typeOccurrence == null)
            //            {
            //                var strArea = globalization.GetString("UndefinedArea");
            //                var areaObj = Area.Repository.GetByName(process.ProcessId, strArea);
            //                if (areaObj == null)
            //                {
            //                    areaObj = new Area(null, strArea, strArea, process.ProcessId, null);
            //                }

            //                typeOccurrence = new AlarmRecord(null, null, areaObj.AreaId, message, tag, tag, true, false, null, false, null);

            //                var releaseArea = new ReleaseArea(typeOccurrence.TypeOccurrenceRecordId, areaObj.AreaId);

            //                var impactedArea = new ImpactedArea(typeOccurrence.TypeOccurrenceRecordId, areaObj.AreaId);

            //                newType = true;
            //            }

            //            if (typeOccurrence != null)            {
            //                try
            //                {
            //                    occurrence = new OccurrenceRecord(null, ackUser, alarmId, dateTimeAck.HasValue ? dateTimeAck.Value.ToLocalTime() : null, dateTimeEnd.HasValue ? dateTimeEnd.Value.ToLocalTime() : null, dateTimeStart.ToLocalTime(), message,
            //                                                stateId, systemNode.SystemNodeId, typeEventCategoryId, typeOccurrence.TypeOccurrenceRecordId, typeQualityId.HasValue ? typeQualityId.Value : 0, typeSeverityId.Value);
            //                }
            //                catch (Exception ex)
            //                {
            //                    Logger.Log(LogType.Error, ex);
            //                }
            //            }

            //            if (dateTimeEnd.HasValue)
            //            {
            //                ended = true;
            //            }

            //            if (typeOccurrence.Notify || typeOccurrence.AssessmentNeeded)
            //            {
            //                service.SendMessages(process.Acronym, process.Description, typeOccurrence.TypeOccurrenceRecordId, typeOccurrence.Description, typeOccurrence.AreaId, typeOccurrence.Area.Name,
            //                                            typeOccurrence.Area.Description, typeOccurrence.Tag, typeOccurrence.Notify, typeOccurrence.AssessmentNeeded, newType,
            //                                            occurrence.OccurrenceRecordId, occurrence.DateTimeStart, occurrence.DateTimeEnd, occurrence.Message);
            //            }

            //            if (typeOccurrence.AssessmentNeeded && ended)
            //            {
            //                service.SendMessagesToIAs(process.Acronym, occurrence.TypeOccurrenceRecord.TypeOccurrenceRecordId, occurrence.TypeOccurrenceRecord.AreaId, occurrence.TypeOccurrenceRecord.Tag,
            //                                                 occurrence.TypeOccurrenceRecord.Notify, occurrence.TypeOccurrenceRecord.AssessmentNeeded, occurrence.OccurrenceRecordId,
            //                                                 occurrence.Message, occurrence.DateTimeEnd);
            //            }
            //        }
            //        else
            //        {
            //            if (!occurrence.DateTimeEnd.HasValue && dateTimeEnd.HasValue)
            //            {
            //                ended = true;
            //            }

            //            occurrence.UpdateAlarm(null, ackUser, dateTimeAck.HasValue ? dateTimeAck.Value.ToLocalTime() : null, dateTimeEnd.HasValue ? dateTimeEnd.Value.ToLocalTime() : null, message, stateId, typeQuality != null ? typeQuality.TypeQualityId : 0, typeSeverityId.Value);

            //            if (occurrence.TypeOccurrenceRecord.AssessmentNeeded && ended)
            //            {
            //                service.SendMessagesToIAs(process.Acronym, occurrence.TypeOccurrenceRecord.TypeOccurrenceRecordId, occurrence.TypeOccurrenceRecord.AreaId, occurrence.TypeOccurrenceRecord.Tag,
            //                                                 occurrence.TypeOccurrenceRecord.Notify, occurrence.TypeOccurrenceRecord.AssessmentNeeded, occurrence.OccurrenceRecordId,
            //                                                 occurrence.Message, occurrence.DateTimeEnd);
            //            }
            //        }


            //        if (dateTimeEnd.HasValue)
            //        {
            //            if (!string.IsNullOrEmpty(process.ConnectionStringExtreme))
            //            {
            //                var occurrenceExtremeValue = OccurrenceRecord.Repository.Get(occurrence.OccurrenceRecordId);

            //                var xtremeValue = GetExtremeValues(process.ConnectionStringExtreme, process.QueryExtreme, tag, systemNodeName, dateTimeStart, dateTimeEnd.Value, occurrenceExtremeValue.TypeOccurrenceRecord.CompensationValue);

            //                if (xtremeValue != null && xtremeValue.Value.Value != null)
            //                {

            //                    var extreme = xtremeValue.Value.Value;
            //                    occurrenceExtremeValue.UpdateExtremeValue(null, xtremeValue.Value.Value.ToString(), xtremeValue.Value.Key);
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        SendMessageAdmins(process, record);
            //    }

            //    return id;
            //}
            //catch (Exception ex)
            //{
            //    Logger.Log(LogType.Information, "Information", ex.StackTrace);
            //    Logger.Log(TDCore.Core.Logging.LogType.Error, ex);
            //    return -1;
            //}
        }

        /// <summary>
        /// Name: "CollectAlarmsHistorian" 
        /// Description: method is of type asynchronous and has no return. It connects to OleDb collecting the alarm data.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async void CollectAlarmsHistorian()
        {
            //try
            //{
            //    var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
            //    var processes = Process.Repository.ListAll();

            //    foreach (var process in processes)
            //    {

            //        if (!string.IsNullOrEmpty(process.ConnectionStringAlarms) && process.ConnectionStringAlarms.Trim().Length > 0)
            //        {
            //            using (var conn = new OleDbConnection(process.ConnectionStringAlarms))
            //            {
            //                try
            //                {
            //                    conn.Open();

            //                    if (conn.State == ConnectionState.Open)
            //                    {
            //                        var sql = process.QueryAlarms;
            //                        if (sql.Contains("@lastTimestamp"))
            //                        {
            //                            var timestamp = process.LastTimestamp;
            //                            if (!timestamp.HasValue)
            //                            {
            //                                timestamp = DateTime.Now.AddHours(-2);
            //                            }
                                        
            //                            sql = sql.Replace("@lastTimestamp", timestamp?.ToString("yyyy-MM-dd HH:mm:ss"));
            //                        }

            //                        DataTable list = null;

            //                        try
            //                        {
            //                            //Collect alarms
            //                            using (OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn))
            //                            {
            //                                using (DataSet ds = new DataSet())
            //                                {
            //                                    adapter.Fill(ds);
            //                                    list = ds.Tables[0];
            //                                }
            //                            }
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            Logger.Log(LogType.Error, ex);
            //                        }

                                    
            //                        if (list != null && list.Rows != null && list.Rows.Count > 0)
            //                        {
            //                            string msg = "";

            //                            if (list != null)
            //                            {
            //                                int cont = 0;
            //                                long id = 0;
            //                                DateTime last = new DateTime();

            //                                foreach (DataRow row in list.Rows)
            //                                {
            //                                    id = ParseAlarmHistorian(process, row);
            //                                    if (id < 0)
            //                                    {
            //                                        Logger.Log(LogType.Error, new Exception($"AlarmId number invalid: {id}"));
            //                                    }                                                
            //                                    last = Convert<DateTime>(row["timestamp"]);

            //                                    if (id > 0)
            //                                    {
            //                                        process.UpdateLastTimestamp(last);
            //                                    }

            //                                    cont++;
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //                catch (Exception ex)
            //                {
            //                    Logger.Log(LogType.Information, "INFO", $"Erro na conexão: '{process.ConnectionStringAlarms}'");
            //                    Logger.Log(TDCore.Core.Logging.LogType.Error, ex);
            //                }
            //                finally
            //                {
            //                    if (conn.State == ConnectionState.Open)
            //                    {
            //                        conn.Close();
            //                    }
            //                }
            //            }
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.Log(TDCore.Core.Logging.LogType.Error, ex);
            //}
        }


        /// <summary>
        /// Name: "ParseAlarmHistorian" 
        /// Description: method parses the alarms from the historian that is passed by the parameters.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>

        private long ParseAlarmHistorian(Process process, DataRow item)
        {
            return 0;
            //string error = "";
            //var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
            //var messageService = ObjectFactory.GetSingleton<IMessageService>();

            //try
            //{
            //    var service = ObjectFactory.GetSingleton<IMessageService>();
            //    long id = Convert<long>(item["alarm_id"]);

            //    System.DateTime dateTimeStart = Convert<DateTime>(item["date_time_start"]);

            //    System.DateTime? dateTimeAck = item["date_time_ack"] == DBNull.Value ? null : Convert<DateTime>(item["date_time_ack"]);
                
            //    System.String ackUser = item["ack_user"] == DBNull.Value ? null : Convert<string>(item["ack_user"]);
                

            //    System.String[] ackUserArray = { };
            //    if (!string.IsNullOrWhiteSpace(ackUser))
            //    {
            //        ackUserArray = ackUser.Split("::");
            //        ackUser = ackUserArray[1];
            //    }

            //    System.DateTime? dateTimeEnd = item["date_time_end"] == DBNull.Value ? null : Convert<DateTime>(item["date_time_end"]);

            //    System.String systemNodeName = Convert<string>(item["system_node_name"]);
                
            //    string typeQualityName = item["historian_quality"] == DBNull.Value ? null : Convert<string>(item["historian_quality"]);
                
            //    int? typeSeverityId = item["severity"] == DBNull.Value ? null : Convert<int>(item["severity"]);
                
            //    string typeEventCategoryName = Convert<string>(item["type_event_category"]);

            //    System.String message = item["message"] == DBNull.Value ? null : Convert<string>(item["Message"]);

            //    System.String tag = Convert<string>(item["tag"]);

            //    System.Int32? typeOccurrenceLevelId = null;

            //    if (item.Table.Columns.Contains("type_occurrence_level_id"))
            //    {
            //        typeOccurrenceLevelId = item["type_occurrence_level_id"] == DBNull.Value ? null : Convert<int>(item["type_occurrence_level_id"]);
            //    }                

            //    if (item["alarm_id"] == DBNull.Value)
            //    {
            //        return id;
            //    }

            //    if (tag.Contains(systemNodeName))
            //    {
            //        tag = tag.Replace(systemNodeName + ".", "");
            //    }

            //    System.Int64 alarmId = Convert<long>(item["alarm_id"]);
            //    System.Int32 stateId = 4;

            //    var systemNode = SystemNode.Repository.GetByName(systemNodeName);
            //    if (systemNode == null && systemNodeName.Trim().Length > 0)
            //    {
            //        systemNode = new SystemNode(systemNodeName);
            //    }

            //    var typeQuality = TypeQuality.Repository.GetByDescription(typeQualityName);
            //    if (typeQuality == null && typeQualityName.Trim().Length > 0)
            //    {
            //        typeQuality = new TypeQuality(typeQualityName);
            //    }

            //    var typeEventCategory = TypeEventCategory.Repository.GetByDescription(typeEventCategoryName);
            //    if (typeEventCategory == null && typeEventCategoryName.Trim().Length > 0)
            //    {
            //        typeEventCategory = new TypeEventCategory(typeEventCategoryName);
            //    } 

            //    if (dateTimeAck.HasValue && dateTimeAck.Value.CompareTo(new DateTime(1970, 1, 1, 0, 0, 0)) <= 0)
            //    {
            //        dateTimeAck = null;
            //    }

            //    if (dateTimeEnd.HasValue && dateTimeEnd.Value.CompareTo(new DateTime(1970, 1, 1, 0, 0, 0)) <= 0)
            //    {
            //        dateTimeEnd = null;
            //    }

            //    string record = $"AlarmId:{alarmId};Start:{dateTimeStart};End:{dateTimeEnd};Mesage:{message};SYstem Node:{systemNode.SystemNodeId};Process:{process.Acronym};Quality:{typeQuality.Description};Severity:{typeSeverityId.Value}";

            //    string compensationValue = null;

            //    if (tag != null)
            //    {
            //        //Search for the occurrence
            //        var occurrence = OccurrenceRecord.Repository.GetByNodeIdAlarmId(systemNode.SystemNodeId, alarmId);

            //        bool ended = false;

            //        if (occurrence == null)
            //        {                        
            //            var typeOccurrence = TypeOccurrenceRecord.Repository.GetByTypeLevelProcess(EnumOccurrenceType.Alarm, tag, typeOccurrenceLevelId, process.ProcessId, null);
            //            bool newType = false;

            //            if (typeOccurrence == null)
            //            {
            //                var strArea = globalization.GetString("UndefinedArea");
            //                var areaObj = Area.Repository.GetByName(process.ProcessId, strArea);
            //                if (areaObj == null)
            //                {
            //                    areaObj = new Area(null, strArea, strArea, process.ProcessId, null, null);
            //                }

            //                typeOccurrence = new AlarmRecord(null, null, areaObj.AreaId, message, tag, tag, true, false, null, false, null);

            //                var releaseArea = new ReleaseArea(typeOccurrence.TypeOccurrenceRecordId, areaObj.AreaId);

            //                var impactedArea = new ImpactedArea(typeOccurrence.TypeOccurrenceRecordId, areaObj.AreaId);

            //                newType = true;
            //            }

            //            try
            //            {
            //                occurrence = new OccurrenceRecord(null, ackUser, alarmId, dateTimeAck, dateTimeEnd, dateTimeStart, message,
            //                                                stateId, systemNode.SystemNodeId, typeEventCategory.TypeEventCategoryId, typeOccurrence.TypeOccurrenceRecordId, typeQuality.TypeQualityId, typeSeverityId.Value);
            //            }
            //            catch (Exception ex)
            //            {
            //                Logger.Log(LogType.Error, ex);
            //            }

            //            if (dateTimeEnd.HasValue)
            //            {
            //                ended = true;
            //            }

            //            if (typeOccurrence.Notify)
            //            {
            //                messageService.SendMessages(process.Acronym, process.Description, typeOccurrence.TypeOccurrenceRecordId, typeOccurrence.Description, typeOccurrence.AreaId, typeOccurrence.Area.Name,
            //                                            typeOccurrence.Area.Description, typeOccurrence.Tag, typeOccurrence.Notify, typeOccurrence.AssessmentNeeded, newType,
            //                                            occurrence.OccurrenceRecordId, occurrence.DateTimeStart, occurrence.DateTimeEnd, occurrence.Message);
            //            }

            //            if (typeOccurrence.AssessmentNeeded && ended)
            //            {
            //                messageService.SendMessagesToIAs(process.Acronym, occurrence.TypeOccurrenceRecord.TypeOccurrenceRecordId, occurrence.TypeOccurrenceRecord.AreaId, occurrence.TypeOccurrenceRecord.Tag,
            //                                                 occurrence.TypeOccurrenceRecord.Notify, occurrence.TypeOccurrenceRecord.AssessmentNeeded, occurrence.OccurrenceRecordId,
            //                                                 occurrence.Message, occurrence.DateTimeEnd);
            //            }

            //        }
            //        else
            //        {
            //            if (!occurrence.DateTimeEnd.HasValue && dateTimeEnd.HasValue)
            //            {
            //                ended = true;
            //            }
            //            occurrence.UpdateAlarm(null, ackUser, dateTimeAck, dateTimeEnd, message, stateId, typeQuality.TypeQualityId, typeSeverityId.Value);
                        
            //            if (occurrence.TypeOccurrenceRecord.AssessmentNeeded && ended)
            //            {
            //                messageService.SendMessagesToIAs(process.Acronym, occurrence.TypeOccurrenceRecord.TypeOccurrenceRecordId, occurrence.TypeOccurrenceRecord.AreaId, occurrence.TypeOccurrenceRecord.Tag,
            //                                                 occurrence.TypeOccurrenceRecord.Notify, occurrence.TypeOccurrenceRecord.AssessmentNeeded, occurrence.OccurrenceRecordId,
            //                                                 occurrence.Message, occurrence.DateTimeEnd);
            //            }
            //        }

            //        if (dateTimeEnd.HasValue)
            //        {
            //            if (!string.IsNullOrEmpty(process.ConnectionStringExtreme) && !string.IsNullOrEmpty(occurrence.TypeOccurrenceRecord.TagDevice))
            //            {
            //                var xtremeValue = GetExtremeValues(process.ConnectionStringExtreme, process.QueryExtreme, tag, occurrence.TypeOccurrenceRecord.TagDevice, dateTimeStart, dateTimeEnd.Value, occurrence.TypeOccurrenceRecord.CompensationValue);

            //                if (xtremeValue != null && xtremeValue.Value.Value != null)
            //                {
            //                    var extreme = xtremeValue.Value.Value;
            //                    occurrence.UpdateExtremeValue(null, xtremeValue.Value.Value.ToString(), xtremeValue.Value.Key);
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        SendMessageAdmins(process, record);
            //    }

            //    return id;
            //}
            //catch (Exception ex)
            //{
            //    Logger.Log(LogType.Information, "Information", error);
            //    Logger.Log(LogType.Information, "Information", ex.StackTrace);
            //    Logger.Log(TDCore.Core.Logging.LogType.Error, ex);
            //    return -1;
            //}
        }


        /// <summary>
        /// Name: "CollectExtremeValues" 
        /// Description: method collects the extreme value.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async void CollectExtremeValues()
        {
            //try
            //{
            //    var lst = OccurrenceRecord.Repository.ListToGetExtremeValues();

            //    if (lst != null && lst.Count > 0)
            //    {
            //        foreach (var item in lst.ToList())
            //        {
            //            try
            //            {
            //                var process = item.TypeOccurrenceRecord.Area.Process;
            //                if (!string.IsNullOrEmpty(process.ConnectionStringExtreme))
            //                {
            //                    var xtremeValue = GetExtremeValues(process.ConnectionStringExtreme, process.QueryExtreme, item.TypeOccurrenceRecord.Tag, item.TypeOccurrenceRecord.TagDevice, item.DateTimeStart, item.DateTimeEnd.Value, item.TypeOccurrenceRecord.CompensationValue);

            //                    if (xtremeValue == null)
            //                    {
            //                        xtremeValue = GetExtremeValues(process.ConnectionStringExtreme, process.QueryExtreme, item.TypeOccurrenceRecord.Tag, item.TypeOccurrenceRecord.TagDevice, item.DateTimeStart.AddMinutes(-15), item.DateTimeEnd.Value, item.TypeOccurrenceRecord.CompensationValue);
            //                    }

            //                    if (xtremeValue == null)
            //                    {
            //                        xtremeValue = GetExtremeValues(process.ConnectionStringExtreme, process.QueryExtreme, item.TypeOccurrenceRecord.Tag, item.TypeOccurrenceRecord.TagDevice, item.DateTimeStart.AddMinutes(-30), item.DateTimeEnd.Value, item.TypeOccurrenceRecord.CompensationValue);
            //                    }

            //                    if (xtremeValue != null && xtremeValue.Value.Value != null)
            //                    {
            //                        var extreme = xtremeValue.Value.Value;
            //                        item.UpdateExtremeValue(null, xtremeValue.Value.Value.ToString(), xtremeValue.Value.Key);
            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                Logger.Log(LogType.Error, new Exception($"Erro no id {item.OccurrenceRecordId}", ex));
            //            }                        
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.Log(LogType.Error, ex);
            //}
        }

        /// <summary>
        /// Name: "CollectEvents" 
        /// Description: method gets a list of processes and event collection.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        public async void CollectEvents()
        {
            //try
            //{
            //    var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
            //    var processes = Process.Repository.ListAll();

            //    foreach (var process in processes)
            //    {
                    
            //        if (!string.IsNullOrEmpty(process.ConnectionStringEvents) && process.ConnectionStringEvents.Trim().Length > 0)
            //        {
            //            DateTime now = DateTime.Now;
            //            using (IDbConnection conn = new SqlConnection(process.ConnectionStringEvents))
            //            {
            //                try
            //                {
            //                    conn.Open();


            //                    if (conn.State == ConnectionState.Open)
            //                    {
            //                        now = DateTime.Now;
            //                        //Collect events                                        
            //                        using (SqlCommand comm = new SqlCommand(process.QueryEvents, (SqlConnection)conn))
            //                        {

            //                            string msg = "";
            //                            if (process.QueryEvents.Contains("@pLastDateTime"))
            //                            {
            //                                var lastDateTime = process.LastTimestamp; 
                                                    
            //                                if (!lastDateTime.HasValue)
            //                                {

            //                                    lastDateTime = DateTime.Now.AddHours(-24);
            //                                }
            //                                var par = new SqlParameter("pLastDateTime", System.Data.SqlDbType.DateTime);
            //                                par.Value = lastDateTime.Value;

            //                                comm.Parameters.Add(par);

            //                                msg = lastDateTime.ToString();
            //                            }

            //                            var list = comm.ExecuteReader();

            //                            now = DateTime.Now;

            //                            if (list != null)
            //                            {
            //                                int cont = 0;
            //                                while (list.Read())
            //                                {
            //                                    var now2 = DateTime.Now;
            //                                    ParseEvent(process, list);
                                                
            //                                    now = Convert<DateTime>(list["date_time_start"]);
            //                                    process.UpdateLastTimestamp(now);

            //                                    cont++;                                                        
            //                                }


            //                            }
            //                            else
            //                            {
            //                            }
            //                            list.Close();
            //                        }
                                         
            //                    }
            //                }
            //                catch (Exception ex)
            //                {
            //                    Logger.Log(TDCore.Core.Logging.LogType.Error, ex);
            //                }
            //                finally
            //                {
            //                    if (conn.State == ConnectionState.Open)
            //                    {
            //                        conn.Close();
            //                    }
            //                }
            //            }
            //        }                        
                    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.Log(TDCore.Core.Logging.LogType.Error, ex);
            //}
        }


        /// <summary>
        /// Name: "ParseEvent" 
        /// Description: method parses the events that have been passed.
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>
        private void ParseEvent(Process process, SqlDataReader item)
        {
            //string error = "";
            //try
            //{
            //    var service = ObjectFactory.GetSingleton<IMessageService>();

            //    System.DateTime dateTimeStart = Convert<DateTime>(item["date_time_start"]);

            //    System.String tag = Convert<string>(item["tag"]);
            //    string beforeValue = item["before_value"] == DBNull.Value ? null : Convert<string>(item["before_value"]);
            //    string newValue = item["new_value"] == DBNull.Value ? null : Convert<string>(item["new_value"]);
            //    System.String systemNodeName = item["system_node_name"] == DBNull.Value ? null : Convert<string>(item["system_node_name"]);
            //    System.Int32 typeEventCategoryId = Convert<int>(item["type_event_category_id"]);
            //    System.String comment = item["comment"] == DBNull.Value ? null : Convert<string>(item["comment"]);
            //    System.String description = item["description"] == DBNull.Value ? null : Convert<string>(item["description"]);
            //    System.String area = item["area"] == DBNull.Value ? null : Convert<string>(item["area"]);
            //    System.String responsible = item["responsible"] == DBNull.Value ? null : Convert<string>(item["responsible"]).Trim();
            //    int stateId = 3;
             
            //    var systemNode = SystemNode.Repository.GetByName(systemNodeName);
            //    if (systemNode == null && systemNodeName != null && systemNodeName.Trim().Length > 0)
            //    {
            //        systemNode = new SystemNode(systemNodeName);
            //    }

            //    var typeEventCategory = TypeEventCategory.Repository.GetByDescription(systemNodeName);
            //    if (systemNode == null && systemNodeName != null && systemNodeName.Trim().Length > 0)
            //    {
            //        systemNode = new SystemNode(systemNodeName);
            //    }

            //    var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

            //    var areaObj = Area.Repository.GetByName(process.ProcessId, area);
            //    if (areaObj == null)
            //    {
            //        var strArea = globalization.GetString("UndefinedArea", new string[] { tag });
            //        areaObj = Area.Repository.GetByName(process.ProcessId, strArea);
            //        if (areaObj == null)
            //        {
            //            areaObj = new Area(null, strArea, strArea, process.ProcessId, null, null);
            //        }
            //    }
                                
            //    if (tag != null)
            //    {
            //        var newType = false;
            //        //Search for the occurrence type
            //         var typeOccurrenceRecord = TypeOccurrenceRecord.Repository.GetByTypeLevelProcess(EnumOccurrenceType.Event, tag, null, process.ProcessId, description);
            //        if (typeOccurrenceRecord == null )
            //        {
            //            typeOccurrenceRecord = new EventRecord(null, null, areaObj.AreaId, description, tag, true, false, null);

            //            var releaseArea = new ReleaseArea(typeOccurrenceRecord.TypeOccurrenceRecordId, areaObj.AreaId);

            //            var impactedArea = new ImpactedArea(typeOccurrenceRecord.TypeOccurrenceRecordId, areaObj.AreaId);

            //            newType = true;
            //        }

            //        var occurrence = OccurrenceRecord.Repository.GetEvent(dateTimeStart, tag, typeEventCategoryId, description);

            //        if (occurrence == null)
            //        {
            //            occurrence = new OccurrenceRecord(null, dateTimeStart, comment, stateId, systemNode.SystemNodeId, typeEventCategoryId, typeOccurrenceRecord.TypeOccurrenceRecordId, beforeValue, newValue, responsible);

            //        }
            //        if (typeOccurrenceRecord.Notify || typeOccurrenceRecord.AssessmentNeeded)
            //        {
            //            service.SendMessages(process.Acronym, process.Description, typeOccurrenceRecord.TypeOccurrenceRecordId, typeOccurrenceRecord.Description, typeOccurrenceRecord.AreaId, typeOccurrenceRecord.Area.Name,
            //                                        typeOccurrenceRecord.Area.Description, typeOccurrenceRecord.Tag, typeOccurrenceRecord.Notify, typeOccurrenceRecord.AssessmentNeeded, newType,
            //                                        occurrence.OccurrenceRecordId, occurrence.DateTimeStart, occurrence.DateTimeEnd, occurrence.Message);
            //        }

            //        if (typeOccurrenceRecord.AssessmentNeeded)
            //        {
            //            service.SendMessagesToIAs(process.Acronym, occurrence.TypeOccurrenceRecord.TypeOccurrenceRecordId, occurrence.TypeOccurrenceRecord.AreaId, occurrence.TypeOccurrenceRecord.Tag,
            //                            occurrence.TypeOccurrenceRecord.Notify, occurrence.TypeOccurrenceRecord.AssessmentNeeded, occurrence.OccurrenceRecordId,
            //                            occurrence.Message, occurrence.DateTimeEnd);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.Log(TDCore.Core.Logging.LogType.Information, "Information", ex.StackTrace);
            //    Logger.Log(TDCore.Core.Logging.LogType.Error, ex);
            //}
        }


        /// <summary>
        /// Name: "SendMessageAdmins" 
        /// Description: method sends a message to the administrators if the tag is null.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        private void SendMessageAdmins(Process process, string record)
        {
            //try
            //{
            //    var service = ObjectFactory.GetSingleton<IMessageService>();
            //    var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();

            //    var allUsersAdmin = User.Repository.ListAdministrators();

            //    if (allUsersAdmin != null && allUsersAdmin.Count > 0)
            //    {
            //        foreach (var user in allUsersAdmin)
            //        {
            //            service.SendEmail(user.UserId, globalization.GetString(user.Language.Code, "EmptyTagSubject"), globalization.GetString(user.Language.Code, "EmptyTagText", new string[] { record }), null);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        /// <summary>
        /// Name: "GetExtremeValues" 
        /// Description: connects to the database and performs the search for the extreme value of a list of occurrences.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public KeyValuePair<DateTime, object>? GetExtremeValues(string connectionString, string sqlStatement, string tag, string tagDevice, DateTime start, DateTime end, string compensationValue)
        {
            return null;
            //try
            //{
                
            //    KeyValuePair<DateTime, object>? obj = null;

            //    using (OleDbConnection connection = new OleDbConnection(connectionString))
            //    {
            //        connection.Open();

            //        bool? max = null;

            //        if (sqlStatement.Contains("@tag"))
            //        {
            //            if (tag.ToUpper().Contains("ALM_H"))
            //            {
            //                max = true;
            //            }
            //            else if (tag.ToUpper().Contains("ALM_L"))
            //            {
            //                max = false;
            //            }

            //            var tagName = $"{tagDevice}";
                        
            //            sqlStatement = sqlStatement.Replace("@tag", $"'{tagName}'");

                        
            //        }

            //        if (sqlStatement.Contains("@start"))
            //        {
            //            sqlStatement = sqlStatement.Replace("@start", $"'{start.ToString("yyyy-MM-dd HH:mm:ss")}'");

            //        }

            //        if (sqlStatement.Contains("@end"))
            //        {
            //            sqlStatement = sqlStatement.Replace("@end", $"'{end.ToString("yyyy-MM-dd HH:mm:ss")}'");

            //        }

            //        if (max.HasValue)
            //        {
            //            if (max.Value == true)
            //            {
            //                sqlStatement = sqlStatement.Replace("@limit", "desc");
            //            }
            //            else
            //            {
            //                sqlStatement = sqlStatement.Replace("@limit", "");
            //            }

            //        }
            //        else
            //        {
            //            sqlStatement = sqlStatement.Replace("@limit", "");
            //        }

            //        try
            //        {                        
            //            using (OleDbDataAdapter adapter = new OleDbDataAdapter(sqlStatement, connection))
            //            {
            //                using (DataSet ds = new DataSet())
            //                {
            //                    adapter.Fill(ds);

            //                    if (ds.Tables[0].Rows.Count == 1)
            //                    {
            //                        var row = ds.Tables[0].Rows[0];

            //                        var datatype = row["datatype"].ToString().ToLower();
            //                        DateTime date = DateTime.Parse(row["timestamp"].ToString());


            //                        if (datatype.ToLower() == "singlefloat" || datatype.ToLower() == "doublefloat")
            //                        {
            //                            double value = System.Convert.ToDouble(row["value"].ToString());
            //                            double compensation = 0;

            //                            if (!string.IsNullOrEmpty(compensationValue))
            //                            {
            //                                compensation = System.Convert.ToDouble(compensationValue);

            //                                if (max.HasValue)
            //                                {
            //                                    if (max.Value)
            //                                    {
            //                                        value += compensation;
            //                                    }
            //                                    else
            //                                    {
            //                                        value -= compensation;
            //                                    }
            //                                }

            //                            }
            //                            obj = new KeyValuePair<DateTime, object>(date, value);
            //                        }
            //                        else if (datatype.ToLower() == "singleinteger" ||
            //                                datatype.ToLower() == "doubleinteger" ||
            //                                datatype.ToLower() == "quadinteger" ||
            //                                datatype.ToLower() == "udoubleinteger")
            //                        {
            //                            long value = System.Convert.ToInt64(row["value"].ToString());

            //                            long compensation = 0;
            //                            if (!string.IsNullOrEmpty(compensationValue))
            //                            {
            //                                compensation = System.Convert.ToInt64(compensationValue);
                                         
            //                                if (max.HasValue)
            //                                {
            //                                    if (max.Value)
            //                                    {
            //                                        value += compensation;
            //                                    }
            //                                    else
            //                                    {
            //                                        value -= compensation;
            //                                    }
            //                                }
            //                            }

            //                            obj = new KeyValuePair<DateTime, object>(date, value);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        Logger.Log(LogType.Error, "INFO", $"Registros não encontrados para : {sqlStatement}");
            //                    }

            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Logger.Log(LogType.Error, ex);
            //        }
                    
            //        connection.Close();
            //    }

            //    return obj;
            //}
            //catch (Exception ex)
            //{
            //    Logger.Log(LogType.Error, ex);
            //    return null;
            //}
        }


        /// <summary>
        /// Name: "Convert" 
        /// Description: method is a generic type converter
        /// Created by: wazc Programa Novo 2022-09-08 .
        /// </summary>

        private T Convert<T>(object value)
        {
            try
            {
                var destType = typeof(T);

                if(destType != value.GetType())
                {
                    return (T)System.Convert.ChangeType(value, destType);
                }

                return (T)value;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
