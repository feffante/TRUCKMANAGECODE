using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;

namespace UtilityBE
{
    public class AppLogger
    {
        public static void Tag(params string[] msg)
        {
            RemoteAppLogger oRemoteAppLogger = new RemoteAppLogger();
            oRemoteAppLogger.Tag(msg);
        }
        public static void Info(params string[] msg)
        {
            RemoteAppLogger oRemoteAppLogger = new RemoteAppLogger();
            oRemoteAppLogger.Info(msg);
        }
        public static void Debug(params string[] msg)
        {
            RemoteAppLogger oRemoteAppLogger = new RemoteAppLogger();
            oRemoteAppLogger.Debug(msg);
        }
        public static void Warn(params string[] msg)
        {
            RemoteAppLogger oRemoteAppLogger = new RemoteAppLogger();
            oRemoteAppLogger.Warn(msg);
        }
        public static void Error(params string[] msg)
        {
            RemoteAppLogger oRemoteAppLogger = new RemoteAppLogger();
            oRemoteAppLogger.Error(msg);
        }
        public static void Audit(params string[] msg)
        {
            RemoteAppLogger oRemoteAppLogger = new RemoteAppLogger();
            oRemoteAppLogger.Audit(msg);
        }
    }

    public class RemoteAppLogger : MarshalByRefObject
    {
        //	Variabili locali alla classe
        private static readonly string _separator = ", ";			//	Separatore di default

        public RemoteAppLogger()
        {
        }


        public void Tag(params string[] msg) { raiseError(msg, -1); }
        public void Info(params string[] msg) { raiseError(msg, 0); }
        public void Debug(params string[] msg) { raiseError(msg, 1); }
        public void Warn(params string[] msg) { raiseError(msg, 2); }
        public void Error(params string[] msg) { raiseError(msg, 3); }

        public void Audit(params string[] msg) { raiseError(msg, 4); }


        private void raiseError(string[] msg, int severity)
        {
            // StackTrace stack = new StackTrace(true);
            StringBuilder text = new StringBuilder("");

            try
            {
                // aggiungo anche il nome della macchina
                text.Append((String)System.Environment.MachineName);
                text.Append(_separator);

                // Accodo tutte le stinghe passate nel campo text, utilizzando il separatore...
                foreach (string s in msg)
                {
                    text.Append(s);
                    text.Append(_separator);
                }

                // Se la severity � maggiore o uguale del minimo da loggare, vado a scrivere 
                if (severity >= int.Parse(System.Configuration.ConfigurationManager.AppSettings["applicationLogLevel"]))
                {
                    //verifico la lunhezza della stringha di messaggio
                    if (text.Length >= 1900)
                    {
                        string sText = text.ToString();
                        //creo un ciclo che spezzetta la string in tante string + piccole
                        int iStatString = 0;

                        string sLog = "";
                        DateTime dtNow = DateTime.Now;
                        string sDtNow = dtNow.ToString();
                        for (int iEndString = 1900; iEndString < sText.Length; iEndString += 1900)
                        {
                            sLog = sText.Substring(iStatString, 1900);
                            //loggo il pezzo di string che ho estratto 
                            // InsertIntoPortal_Log(sDtNow + "->" + sLog.ToString(), severity);
                            APP_InsertIntoAPPLICATION_LOG("RSU", sDtNow + "->" + sLog.ToString(), severity);
                            iStatString += 1900;
                        }
                        sLog = sText.Substring(iStatString);
                        //loggo l'ultimo pezzo finale della string 
                        //InsertIntoPortal_Log(sDtNow + "->" + sLog.ToString(), severity);
                        APP_InsertIntoAPPLICATION_LOG("RSU", sDtNow + "->" + sLog.ToString(), severity);
                    }
                    else
                    {
                        // eseguo la insert normale del message	                        
                        //InsertIntoPortal_Log(text.ToString(), severity);
                        APP_InsertIntoAPPLICATION_LOG("RSU", text.ToString(), severity);
                    }

                }
            }
            catch { }
        }



        #region APP_InsertIntoAPPLICATION_LOG
        /// <summary>
        /// inserisce record nella tabella dei log della applicazione (SqlServer calling type)
        /// </summary>
        /// <param name="xDBNAME">(contained value would be passed into the field "@DBNAME" of called Stored)</param>
        /// <param name="xMESSAGE">(contained value would be passed into the field "@MESSAGE" of called Stored)</param>
        /// <param name="xSEVERITY">(contained value would be passed into the field "@SEVERITY" of called Stored)</param>
        /// <returns>
        /// int numero di righe inserite
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "APP_InsertIntoAPPLICATION_LOG" returning its output.
        /// </newpara>
        /// </remarks>
        public int APP_InsertIntoAPPLICATION_LOG(
            /* par. 001*/  String xDBNAME
            /* par. 002*/ , String xMESSAGE
            /* par. 003*/ , int xSEVERITY
                                                )
        {
            int iResult = 0;
            // Use a try-catch to get (and log) the exception
            try
            {
                // Getting DB connection string from WebPortal...
                MySqlConnection myConnection = new MySqlConnection(AccessoDB.StradaDBXStored);

                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("APPLICATION_Insert_Into_Log", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterMESSAGE = new MySqlParameter("@MESSAGE", MySqlDbType.VarChar, 2000);
                MySqlParameter parameterSEVERITY = new MySqlParameter("@SEVERITY", MySqlDbType.Int32, 4);

                parameterMESSAGE.Value = xMESSAGE;
                parameterSEVERITY.Value = xSEVERITY;

                myCommand.SelectCommand.Parameters.Add(parameterMESSAGE);
                myCommand.SelectCommand.Parameters.Add(parameterSEVERITY);

                // Open DB connection, execute SQL command and close the connection               
                myConnection.Open();
                iResult = myCommand.SelectCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception)
            {
               return -1;// System.Windows.Forms.MessageBox.Show(oEx.Message);
            }
            // Gets output data
            return iResult;

        }

        public int APP_InsertIntoAPPLICATION_LOG(
                            bool TxtFileCreate
            /* par. 001*/ , String xDBNAME
            /* par. 002*/ , String xMESSAGE
            /* par. 003*/ , int xSEVERITY
                                              )
        {

            int iResult = 0;
            // Use a try-catch to get (and log) the exception
            try
            {
                string sPathFileLog = ConfigurationManager.AppSettings["LOG_FILE"].ToString();

                xMESSAGE = (DateTime.Now.ToString()) + " - " + xSEVERITY + " - " + xMESSAGE;

                Utility oUtility = new UtilityBE.Utility();
                oUtility.Log(xMESSAGE, sPathFileLog);
            }
            catch { }
            // Gets output data
            return iResult;
        }

        #endregion APP_InsertIntoAPPLICATION_LOG

    }
}
