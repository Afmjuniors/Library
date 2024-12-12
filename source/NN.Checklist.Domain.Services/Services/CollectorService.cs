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
        /// Name: "SendMessageAdmins" 
        /// Description: method sends a message to the administrators if the tag is null.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        private void SendMessageAdmins(object process, string record)
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
