using System;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace UtilityBE
{
    // static sulla classe obbliga tutti i suoi metodi ad essere statici
    public static class Singleton
    {
        //id dell'utente che si logga alla partenza
        public static int iIdUser;
    }

    public class UsersSCR
    {
        public int IdUser { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Note { get; set; }
        public DateTime DtCreazione { get; set; }

        /// <summary>
        /// Preleva la password partendo dall'user
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public DataSet GetPasswordPerUser(string User)
        {
            DataSet dsData = new DataSet();
            try
            {
                dsData = this.GetPasswordPerUser(User);
            }
            catch (Exception oEx)
            {
                throw (oEx);
            }
            return dsData;
        }

        /// <summary>
        /// preleva l'oggetto user partendo dalla key della tabella
        /// </summary>
        /// <param name="Iduser"></param>
        /// <returns></returns>
        public UsersSCR GetUserSingolo(int Iduser)
        {
            UsersSCR oUsersSCR = new UsersSCR();
            try
            {
                DataSet dsData = new DataSet();
                dsData = this.Users_GetTutti_PerKey(Iduser);

                if (dsData.Tables[0].Rows.Count > 0)
                {
                    //assegno le proprieta all'oggeto 
                    oUsersSCR.IdUser = int.Parse(dsData.Tables[0].Rows[0]["ID_USER"].ToString());
                    oUsersSCR.User = dsData.Tables[0].Rows[0]["COMUNE"].ToString();
                    oUsersSCR.Password = dsData.Tables[0].Rows[0]["PASSWORD"].ToString();
                    oUsersSCR.DtCreazione = DateTime.Parse(dsData.Tables[0].Rows[0]["DT_CREAZIONE"].ToString());
                    oUsersSCR.Note = dsData.Tables[0].Rows[0]["NOTE"].ToString();
                }
                else
                    throw new Exception("nessun elemento trovata");
            }
            catch (Exception oEx)
            {
                throw (oEx);
            }
            return oUsersSCR;
        }

        /// <summary>
        /// prelevo tutti i record della tabella Users partendo dal filtro in like in ingresso
        /// </summary>
        /// <param name="iIdUserFrom"></param>
        /// <param name="iIdUserTo"></param>
        /// <param name="sUser"></param>
        /// <param name="sPassword"></param>
        /// <param name="dtCreazioneFrom"></param>
        /// <param name="dtCreazioneTo"></param>
        /// <param name="sNote"></param>
        /// <returns>dataSet chec ontien una table</returns>
        public DataSet GetAllUsers(int iIdUserFrom, int iIdUserTo, string sUser, string sPassword, DateTime dtCreazioneFrom, DateTime dtCreazioneTo, string sNote)
        {
            DataSet dsData = new DataSet();
            try
            {
                dsData = this.Users_GetTutti_InLike(iIdUserFrom, iIdUserTo, dtCreazioneFrom, dtCreazioneTo, sNote, sPassword, sUser);
            }
            catch (Exception oEx)
            {
                throw (oEx);
            }
            return dsData;
        }

        /// <summary>
        /// inserisce un user nuovo verificando se esiste gia
        /// </summary>
        /// <param name="iIdUser"></param>
        /// <param name="sUser"></param>
        /// <param name="sPassword"></param>
        /// <param name="dtCreazione"></param>
        /// <param name="sNote"></param>
        /// <returns></returns>
        public int InsertUser(int iIdUser, string sUser, string sPassword, DateTime dtCreazione, string sNote)
        {
            int iReturn = 0;
            try
            {
                DataSet dsUserEsiste = new DataSet();

                //verifico se esiste già l'item che sto cercando di inserire
                //dsMagazzinoEsiste = this.GetAllCassonetto(-2147483648, 2147483647, -2147483648, 2147483647, -2147483648, 2147483647, -2147483648, 2147483647, -2147483648, 2147483647, -2147483648, 2147483647, sNumero, "", "", "", false, true, false, true, false, true, false, true, false, true, false, true, false, true);
                dsUserEsiste = this.GetUsers_PerUser(sUser);
                if (dsUserEsiste.Tables[0].Rows.Count != 0)
                {
                    iReturn = -1; //("Attenzione item già esistente!");  
                }
                else
                {
                    iReturn += this.InsertUser(iIdUser, sUser, sPassword, dtCreazione, sNote);
                }
            }
            catch (Exception oEx)
            {
                throw (oEx);
            }
            return iReturn;
        }

        /// <summary>
        /// aggionameto della tabella users
        /// </summary>
        /// <param name="iIdUser"></param>
        /// <param name="sUser"></param>
        /// <param name="sPassword"></param>
        /// <param name="dtCreazione"></param>
        /// <param name="sNote"></param>
        /// <returns></returns>
        public int UpdateUser(int iIdUser, string sUser, string sPassword, DateTime dtCreazione, string sNote)
        {
            int iReturn = 0;
            try
            {
                DataSet dsUserEsiste = new DataSet();
                DataSet dsData = new DataSet();

                //verifico se esiste già l'item che sto cercando di aggiornare(verifico se per key esiste una row corrispondente nel db)
                dsData = this.Users_GetTutti_PerKey(IdUser);

                if (dsData.Tables[0].Rows.Count != 1)
                {
                    iReturn = -1; //("Attenzione items da aggironare non trovata!");  
                }
                else
                {
                    iReturn += this.Users_UpdateTutti_PerKey(iIdUser, dtCreazione, sNote, sPassword, sUser);
                }
            }
            catch (Exception oEx)
            {
                throw (oEx);
            }
            return iReturn;
        }

        /// <summary>
        /// verifica l'hash che c'è sul db con quello calcolato in base al vslore passato
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public int VerifyAutenticate(string User, string Password)
        {
            //Iret decide cosa succede
            //iRet = 0 utente inesistente
            //iRet = 1 OK utente esistente e autenticato
            //iRet = 2 Password errata

            Utility oUtility = new Utility();
            CryptoUtil oCryptoUtil = new CryptoUtil();
            DataSet dsUser = new DataSet();
            // RijndaelManaged myRijndael = new RijndaelManaged();
            int iRet = -1;
            try
            {
                //verifico se esiste l'utente
                dsUser = this.GetUsers_PerUser(User);
                if (dsUser.Tables[0].Rows.Count == 0)
                    return 0;

                //se esiste preleviamo la psw che è sul db
                string sPswCryptDB = dsUser.Tables[0].Rows[0]["PASSWORD"].ToString();

                //calcolo l'hash in base al chiaro che prelevo dal fe
                string sHashChiaro = oCryptoUtil.getMd5Hash(Password);

                //verifico che l'hash che ho calcolato sia = a quello che hoo sul db
                if (sHashChiaro == sPswCryptDB)
                    iRet = 1;
                else
                    iRet = 2;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method VerifyAutenticate", "", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method VerifyAutenticate", "", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
            return iRet;
        }

        /// <summary>
        /// preleva l'user id dal nome e dalla passww dopo averlo riautenticato
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public int GetIdUser(string User, string Password)
        {
            //Iret decide cosa succede
            //iRet = 0 utente inesistente
            //iRet = X OK utente esistente e autenticato numero id
            //iRet =-2 Password errata

            Utility oUtility = new Utility();
            CryptoUtil oCryptoUtil = new CryptoUtil();
            DataSet dsUser = new DataSet();

            int iRet = -1;
            try
            {
                //verifico se esiste l'utente
                dsUser = this.GetUsers_PerUser(User);
                if (dsUser.Tables[0].Rows.Count == 0)
                    return 0;

                //se esiste preleviamo la psw che è sul db
                string sPswCryptDB = dsUser.Tables[0].Rows[0]["PASSWORD"].ToString();

                //calcolo l'hash in base al chiaro che prelevo dal fe
                string sHashChiaro = oCryptoUtil.getMd5Hash(Password);

                //verifico che l'hash che ho calcolato sia = a quello che hoo sul db
                if (sHashChiaro == sPswCryptDB)
                {
                    this.GetUsers_PerUser(User);
                    iRet = int.Parse(dsUser.Tables[0].Rows[0]["ID_USER"].ToString());
                }
                else
                    iRet = -2;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method GetIdUser", "", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method GetIdUser", "", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
            return iRet;
        }


        #region sp per la table Users

        #region SCR_Users_Delete_PerKey (apr 13 2008 11:57AM)
        /// <summary>
        /// cancella dalla tabella Users (SqlServer calling type)
        /// </summary>
        /// <param name="xID_USER">(contained value would be passed into the field "@ID_USER" of called Stored)</param>
        /// <returns>
        /// int numero di righe cancellate
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_Delete_PerKey" returning its output.
        /// </newpara>
        /// </remarks>
        public int Users_Delete_PerKey(
            /* par. 001*/  int xID_USER
                                      )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Getting DB connection string from WebPortal...
                MySqlConnection myConnection = new MySqlConnection(AccessoDB.StradaDBXStored);

                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_Delete_PerKey", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterID_USER = new MySqlParameter("@ID_USER", MySqlDbType.Int32, 4);

                parameterID_USER.Value = xID_USER;

                myCommand.SelectCommand.Parameters.Add(parameterID_USER);

                // Open DB connection, execute SQL command and close the connection
                int iResult = 0;
                myConnection.Open();
                iResult = myCommand.SelectCommand.ExecuteNonQuery();
                myConnection.Close();

                // Gets output data
                return iResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_Delete_PerKey", "Executed stored: SCR_Users_Delete_PerKey", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_Delete_PerKey", "Executed stored: SCR_Users_Delete_PerKey", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        /// <summary>
        /// cancella dalla tabella Users
        /// Overload with external DB connection.
        /// </summary>
        /// <param name="myConnection">External DB connection</param>
        /// <param name="xID_USER">(contained value would be passed into the field "@ID_USER" of called Stored)</param>
        /// <returns>
        /// int numero di righe cancellate (SqlServer calling type)
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_Delete_PerKey" returning its output.
        /// </newpara>
        /// </remarks>
        public int Users_Delete_PerKey(
                                       MySqlConnection myConnection
            /* par. 001*/ , int xID_USER
                                      )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_Delete_PerKey", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterID_USER = new MySqlParameter("@ID_USER", MySqlDbType.Int32, 4);

                parameterID_USER.Value = xID_USER;

                myCommand.SelectCommand.Parameters.Add(parameterID_USER);

                // Execute SQL command
                int iResult = 0;
                iResult = myCommand.SelectCommand.ExecuteNonQuery();

                // Gets output data
                return iResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_Delete_PerKey", "Executed stored: SCR_Users_Delete_PerKey", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_Delete_PerKey", "Executed stored: SCR_Users_Delete_PerKey", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        /// <summary>
        /// cancella dalla tabella Users (SqlServer calling type)
        /// Overload with external DB connection and external DB transaction.
        /// </summary>
        /// <param name="myConnection">External DB connection</param>
        /// <param name="myTransaction">External DB transaction</param>
        /// <param name="xID_USER">(contained value would be passed into the field "@ID_USER" of called Stored)</param>
        /// <returns>
        /// int numero di righe cancellate
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_Delete_PerKey" returning its output.
        /// </newpara>
        /// </remarks>
        public int Users_Delete_PerKey(
                                       MySqlConnection myConnection
                                      , MySqlTransaction myTransaction
            /* par. 001*/ , int xID_USER
                                      )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_Delete_PerKey", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                myCommand.SelectCommand.Transaction = myTransaction;

                // Setting stored parameters
                MySqlParameter parameterID_USER = new MySqlParameter("@ID_USER", MySqlDbType.Int32, 4);

                parameterID_USER.Value = xID_USER;

                myCommand.SelectCommand.Parameters.Add(parameterID_USER);

                // Execute SQL command
                int iResult = 0;
                iResult = myCommand.SelectCommand.ExecuteNonQuery();

                // Gets output data
                return iResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_Delete_PerKey", "Executed stored: SCR_Users_Delete_PerKey", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_Delete_PerKey", "Executed stored: SCR_Users_Delete_PerKey", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        #endregion SCR_Users_Delete_PerKey (apr 13 2008 11:57AM)

        #region SCR_Users_GetTutti_InLike (apr 13 2008 12:00PM)
        /// <summary>
        /// preleva tutti i record che corrispondono con il filtro in like in input dalla tabella Users (SqlServer calling type)
        /// </summary>
        /// <param name="xDT_CREAZIONE_FROM">(contained value would be passed into the field "@DT_CREAZIONE_FROM" of called Stored)</param>
        /// <param name="xDT_CREAZIONE_TO">(contained value would be passed into the field "@DT_CREAZIONE_TO" of called Stored)</param>
        /// <param name="xID_USER_FROM">(contained value would be passed into the field "@ID_USER_FROM" of called Stored)</param>
        /// <param name="xID_USER_TO">(contained value would be passed into the field "@ID_USER_TO" of called Stored)</param>
        /// <param name="xNOTE">(contained value would be passed into the field "@NOTE" of called Stored)</param>
        /// <param name="xPASSWORD">(contained value would be passed into the field "@PASSWORD" of called Stored)</param>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// DataSet che contiene una table con tutte le row che corrispondono
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_GetTutti_InLike" returning its output.
        /// </newpara>
        /// </remarks>
        public DataSet Users_GetTutti_InLike(
            /* par. 001*/  int xID_USER_FROM
            /* par. 002*/ , int xID_USER_TO
            /* par. 003*/ , DateTime xDT_CREAZIONE_FROM
            /* par. 004*/ , DateTime xDT_CREAZIONE_TO
            /* par. 005*/ , String xNOTE
            /* par. 006*/ , String xPASSWORD
            /* par. 007*/ , String xUSER
                                            )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Getting DB connection string from WebPortal...
                MySqlConnection myConnection = new MySqlConnection(AccessoDB.StradaDBXStored);

                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_GetTutti_InLike", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterDT_CREAZIONE_FROM = new MySqlParameter("@DT_CREAZIONE_FROM", MySqlDbType.DateTime, 8);
                MySqlParameter parameterDT_CREAZIONE_TO = new MySqlParameter("@DT_CREAZIONE_TO", MySqlDbType.DateTime, 8);
                MySqlParameter parameterID_USER_FROM = new MySqlParameter("@ID_USER_FROM", MySqlDbType.Int32, 4);
                MySqlParameter parameterID_USER_TO = new MySqlParameter("@ID_USER_TO", MySqlDbType.Int32, 4);
                MySqlParameter parameterNOTE = new MySqlParameter("@NOTE", MySqlDbType.VarChar, 150);
                MySqlParameter parameterPASSWORD = new MySqlParameter("@PASSWORD", MySqlDbType.VarChar, 200);
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 200);

                parameterDT_CREAZIONE_FROM.Value = xDT_CREAZIONE_FROM;
                parameterDT_CREAZIONE_TO.Value = xDT_CREAZIONE_TO;
                parameterID_USER_FROM.Value = xID_USER_FROM;
                parameterID_USER_TO.Value = xID_USER_TO;
                parameterNOTE.Value = xNOTE;
                parameterPASSWORD.Value = xPASSWORD;
                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterDT_CREAZIONE_FROM);
                myCommand.SelectCommand.Parameters.Add(parameterDT_CREAZIONE_TO);
                myCommand.SelectCommand.Parameters.Add(parameterID_USER_FROM);
                myCommand.SelectCommand.Parameters.Add(parameterID_USER_TO);
                myCommand.SelectCommand.Parameters.Add(parameterNOTE);
                myCommand.SelectCommand.Parameters.Add(parameterPASSWORD);
                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Open DB connection, execute SQL command and close the connection
                DataSet dsResult = new DataSet();
                myConnection.Open();
                myCommand.Fill(dsResult);
                myConnection.Close();

                // Gets output data
                return dsResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetTutti_InLike", "Executed stored: SCR_Users_GetTutti_InLike", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetTutti_InLike", "Executed stored: SCR_Users_GetTutti_InLike", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        /// <summary>
        /// preleva tutti i record che corrispondono con il filtro in like in input dalla tabella Users
        /// Overload with external DB connection.
        /// </summary>
        /// <param name="myConnection">External DB connection</param>
        /// <param name="xDT_CREAZIONE_FROM">(contained value would be passed into the field "@DT_CREAZIONE_FROM" of called Stored)</param>
        /// <param name="xDT_CREAZIONE_TO">(contained value would be passed into the field "@DT_CREAZIONE_TO" of called Stored)</param>
        /// <param name="xID_USER_FROM">(contained value would be passed into the field "@ID_USER_FROM" of called Stored)</param>
        /// <param name="xID_USER_TO">(contained value would be passed into the field "@ID_USER_TO" of called Stored)</param>
        /// <param name="xNOTE">(contained value would be passed into the field "@NOTE" of called Stored)</param>
        /// <param name="xPASSWORD">(contained value would be passed into the field "@PASSWORD" of called Stored)</param>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// DataSet che contiene una table con tutte le row che corrispondono (SqlServer calling type)
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_GetTutti_InLike" returning its output.
        /// </newpara>
        /// </remarks>
        public DataSet Users_GetTutti_InLike(
                                             MySqlConnection myConnection
            /* par. 001*/ , int xID_USER_FROM
            /* par. 002*/ , int xID_USER_TO
            /* par. 003*/ , DateTime xDT_CREAZIONE_FROM
            /* par. 004*/ , DateTime xDT_CREAZIONE_TO
            /* par. 005*/ , String xNOTE
            /* par. 006*/ , String xPASSWORD
            /* par. 007*/ , String xUSER
                                            )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_GetTutti_InLike", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterDT_CREAZIONE_FROM = new MySqlParameter("@DT_CREAZIONE_FROM", MySqlDbType.DateTime, 8);
                MySqlParameter parameterDT_CREAZIONE_TO = new MySqlParameter("@DT_CREAZIONE_TO", MySqlDbType.DateTime, 8);
                MySqlParameter parameterID_USER_FROM = new MySqlParameter("@ID_USER_FROM", MySqlDbType.Int32, 4);
                MySqlParameter parameterID_USER_TO = new MySqlParameter("@ID_USER_TO", MySqlDbType.Int32, 4);
                MySqlParameter parameterNOTE = new MySqlParameter("@NOTE", MySqlDbType.VarChar, 150);
                MySqlParameter parameterPASSWORD = new MySqlParameter("@PASSWORD", MySqlDbType.VarChar, 200);
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 200);

                parameterDT_CREAZIONE_FROM.Value = xDT_CREAZIONE_FROM;
                parameterDT_CREAZIONE_TO.Value = xDT_CREAZIONE_TO;
                parameterID_USER_FROM.Value = xID_USER_FROM;
                parameterID_USER_TO.Value = xID_USER_TO;
                parameterNOTE.Value = xNOTE;
                parameterPASSWORD.Value = xPASSWORD;
                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterDT_CREAZIONE_FROM);
                myCommand.SelectCommand.Parameters.Add(parameterDT_CREAZIONE_TO);
                myCommand.SelectCommand.Parameters.Add(parameterID_USER_FROM);
                myCommand.SelectCommand.Parameters.Add(parameterID_USER_TO);
                myCommand.SelectCommand.Parameters.Add(parameterNOTE);
                myCommand.SelectCommand.Parameters.Add(parameterPASSWORD);
                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Execute SQL command
                DataSet dsResult = new DataSet();
                myCommand.Fill(dsResult);

                // Gets output data
                return dsResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetTutti_InLike", "Executed stored: SCR_Users_GetTutti_InLike", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetTutti_InLike", "Executed stored: SCR_Users_GetTutti_InLike", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        /// <summary>
        /// preleva tutti i record che corrispondono con il filtro in like in input dalla tabella Users (SqlServer calling type)
        /// Overload with external DB connection and external DB transaction.
        /// </summary>
        /// <param name="myConnection">External DB connection</param>
        /// <param name="myTransaction">External DB transaction</param>
        /// <param name="xDT_CREAZIONE_FROM">(contained value would be passed into the field "@DT_CREAZIONE_FROM" of called Stored)</param>
        /// <param name="xDT_CREAZIONE_TO">(contained value would be passed into the field "@DT_CREAZIONE_TO" of called Stored)</param>
        /// <param name="xID_USER_FROM">(contained value would be passed into the field "@ID_USER_FROM" of called Stored)</param>
        /// <param name="xID_USER_TO">(contained value would be passed into the field "@ID_USER_TO" of called Stored)</param>
        /// <param name="xNOTE">(contained value would be passed into the field "@NOTE" of called Stored)</param>
        /// <param name="xPASSWORD">(contained value would be passed into the field "@PASSWORD" of called Stored)</param>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// DataSet che contiene una table con tutte le row che corrispondono
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_GetTutti_InLike" returning its output.
        /// </newpara>
        /// </remarks>
        public DataSet Users_GetTutti_InLike(
                                             MySqlConnection myConnection
                                            , MySqlTransaction myTransaction
            /* par. 001*/ , int xID_USER_FROM
            /* par. 002*/ , int xID_USER_TO
            /* par. 003*/ , DateTime xDT_CREAZIONE_FROM
            /* par. 004*/ , DateTime xDT_CREAZIONE_TO
            /* par. 005*/ , String xNOTE
            /* par. 006*/ , String xPASSWORD
            /* par. 007*/ , String xUSER
                                            )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_GetTutti_InLike", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                myCommand.SelectCommand.Transaction = myTransaction;

                // Setting stored parameters
                MySqlParameter parameterDT_CREAZIONE_FROM = new MySqlParameter("@DT_CREAZIONE_FROM", MySqlDbType.DateTime, 8);
                MySqlParameter parameterDT_CREAZIONE_TO = new MySqlParameter("@DT_CREAZIONE_TO", MySqlDbType.DateTime, 8);
                MySqlParameter parameterID_USER_FROM = new MySqlParameter("@ID_USER_FROM", MySqlDbType.Int32, 4);
                MySqlParameter parameterID_USER_TO = new MySqlParameter("@ID_USER_TO", MySqlDbType.Int32, 4);
                MySqlParameter parameterNOTE = new MySqlParameter("@NOTE", MySqlDbType.VarChar, 150);
                MySqlParameter parameterPASSWORD = new MySqlParameter("@PASSWORD", MySqlDbType.VarChar, 200);
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 200);

                parameterDT_CREAZIONE_FROM.Value = xDT_CREAZIONE_FROM;
                parameterDT_CREAZIONE_TO.Value = xDT_CREAZIONE_TO;
                parameterID_USER_FROM.Value = xID_USER_FROM;
                parameterID_USER_TO.Value = xID_USER_TO;
                parameterNOTE.Value = xNOTE;
                parameterPASSWORD.Value = xPASSWORD;
                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterDT_CREAZIONE_FROM);
                myCommand.SelectCommand.Parameters.Add(parameterDT_CREAZIONE_TO);
                myCommand.SelectCommand.Parameters.Add(parameterID_USER_FROM);
                myCommand.SelectCommand.Parameters.Add(parameterID_USER_TO);
                myCommand.SelectCommand.Parameters.Add(parameterNOTE);
                myCommand.SelectCommand.Parameters.Add(parameterPASSWORD);
                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Execute SQL command
                DataSet dsResult = new DataSet();
                myCommand.Fill(dsResult);

                // Gets output data
                return dsResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetTutti_InLike", "Executed stored: SCR_Users_GetTutti_InLike", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetTutti_InLike", "Executed stored: SCR_Users_GetTutti_InLike", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        #endregion SCR_Users_GetTutti_InLike (apr 13 2008 12:00PM)

        #region SCR_Users_GetTutti_PerKey (apr 13 2008 12:00PM)
        /// <summary>
        /// preleva elemento corrispondente alla key in input della tabella Users (SqlServer calling type)
        /// </summary>
        /// <param name="xID_USER">(contained value would be passed into the field "@ID_USER" of called Stored)</param>
        /// <returns>
        /// DataSet che contiene una table con una row
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_GetTutti_PerKey" returning its output.
        /// </newpara>
        /// </remarks>
        public DataSet Users_GetTutti_PerKey(
            /* par. 001*/  int xID_USER
                                            )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Getting DB connection string from WebPortal...
                MySqlConnection myConnection = new MySqlConnection(AccessoDB.StradaDBXStored);

                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_GetTutti_PerKey", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterID_USER = new MySqlParameter("@ID_USER", MySqlDbType.Int32, 4);

                parameterID_USER.Value = xID_USER;

                myCommand.SelectCommand.Parameters.Add(parameterID_USER);

                // Open DB connection, execute SQL command and close the connection
                DataSet dsResult = new DataSet();
                myConnection.Open();
                myCommand.Fill(dsResult);
                myConnection.Close();

                // Gets output data
                return dsResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetTutti_PerKey", "Executed stored: SCR_Users_GetTutti_PerKey", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetTutti_PerKey", "Executed stored: SCR_Users_GetTutti_PerKey", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        /// <summary>
        /// preleva elemento corrispondente alla key in input della tabella Users
        /// Overload with external DB connection.
        /// </summary>
        /// <param name="myConnection">External DB connection</param>
        /// <param name="xID_USER">(contained value would be passed into the field "@ID_USER" of called Stored)</param>
        /// <returns>
        /// DataSet che contiene una table con una row (SqlServer calling type)
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_GetTutti_PerKey" returning its output.
        /// </newpara>
        /// </remarks>
        public DataSet Users_GetTutti_PerKey(
                                             MySqlConnection myConnection
            /* par. 001*/ , int xID_USER
                                            )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_GetTutti_PerKey", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterID_USER = new MySqlParameter("@ID_USER", MySqlDbType.Int32, 4);

                parameterID_USER.Value = xID_USER;

                myCommand.SelectCommand.Parameters.Add(parameterID_USER);

                // Execute SQL command
                DataSet dsResult = new DataSet();
                myCommand.Fill(dsResult);

                // Gets output data
                return dsResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetTutti_PerKey", "Executed stored: SCR_Users_GetTutti_PerKey", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetTutti_PerKey", "Executed stored: SCR_Users_GetTutti_PerKey", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        /// <summary>
        /// preleva elemento corrispondente alla key in input della tabella Users (SqlServer calling type)
        /// Overload with external DB connection and external DB transaction.
        /// </summary>
        /// <param name="myConnection">External DB connection</param>
        /// <param name="myTransaction">External DB transaction</param>
        /// <param name="xID_USER">(contained value would be passed into the field "@ID_USER" of called Stored)</param>
        /// <returns>
        /// DataSet che contiene una table con una row
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_GetTutti_PerKey" returning its output.
        /// </newpara>
        /// </remarks>
        public DataSet Users_GetTutti_PerKey(
                                             MySqlConnection myConnection
                                            , MySqlTransaction myTransaction
            /* par. 001*/ , int xID_USER
                                            )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_GetTutti_PerKey", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                myCommand.SelectCommand.Transaction = myTransaction;

                // Setting stored parameters
                MySqlParameter parameterID_USER = new MySqlParameter("@ID_USER", MySqlDbType.Int32, 4);

                parameterID_USER.Value = xID_USER;

                myCommand.SelectCommand.Parameters.Add(parameterID_USER);

                // Execute SQL command
                DataSet dsResult = new DataSet();
                myCommand.Fill(dsResult);

                // Gets output data
                return dsResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetTutti_PerKey", "Executed stored: SCR_Users_GetTutti_PerKey", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetTutti_PerKey", "Executed stored: SCR_Users_GetTutti_PerKey", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        #endregion SCR_Users_GetTutti_PerKey (apr 13 2008 12:00PM)

        #region SCR_Users_InsertInto (apr 13 2008 12:01PM)
        /// <summary>
        /// inserisce nella tabella Users (SqlServer calling type)
        /// </summary>
        /// <param name="xDT_CREAZIONE">(contained value would be passed into the field "@DT_CREAZIONE" of called Stored)</param>
        /// <param name="xNOTE">(contained value would be passed into the field "@NOTE" of called Stored)</param>
        /// <param name="xPASSWORD">(contained value would be passed into the field "@PASSWORD" of called Stored)</param>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// int numero di righe inserite
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_InsertInto" returning its output.
        /// </newpara>
        /// </remarks>
        public int Users_InsertInto(
            /* par. 001*/  DateTime xDT_CREAZIONE
            /* par. 002*/ , String xNOTE
            /* par. 003*/ , String xPASSWORD
            /* par. 004*/ , String xUSER
                                   )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Getting DB connection string from WebPortal...
                MySqlConnection myConnection = new MySqlConnection(AccessoDB.StradaDBXStored);

                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_InsertInto", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterDT_CREAZIONE = new MySqlParameter("@DT_CREAZIONE", MySqlDbType.DateTime, 8);
                MySqlParameter parameterNOTE = new MySqlParameter("@NOTE", MySqlDbType.VarChar, 150);
                MySqlParameter parameterPASSWORD = new MySqlParameter("@PASSWORD", MySqlDbType.VarChar, 200);
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 200);

                parameterDT_CREAZIONE.Value = xDT_CREAZIONE;
                parameterNOTE.Value = xNOTE;
                parameterPASSWORD.Value = xPASSWORD;
                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterDT_CREAZIONE);
                myCommand.SelectCommand.Parameters.Add(parameterNOTE);
                myCommand.SelectCommand.Parameters.Add(parameterPASSWORD);
                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Open DB connection, execute SQL command and close the connection
                int iResult = 0;
                myConnection.Open();
                iResult = myCommand.SelectCommand.ExecuteNonQuery();
                myConnection.Close();

                // Gets output data
                return iResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_InsertInto", "Executed stored: SCR_Users_InsertInto", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_InsertInto", "Executed stored: SCR_Users_InsertInto", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        /// <summary>
        /// inserisce nella tabella Users
        /// Overload with external DB connection.
        /// </summary>
        /// <param name="myConnection">External DB connection</param>
        /// <param name="xDT_CREAZIONE">(contained value would be passed into the field "@DT_CREAZIONE" of called Stored)</param>
        /// <param name="xNOTE">(contained value would be passed into the field "@NOTE" of called Stored)</param>
        /// <param name="xPASSWORD">(contained value would be passed into the field "@PASSWORD" of called Stored)</param>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// int numero di righe inserite (SqlServer calling type)
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_InsertInto" returning its output.
        /// </newpara>
        /// </remarks>
        public int Users_InsertInto(
                                    MySqlConnection myConnection
            /* par. 001*/ , DateTime xDT_CREAZIONE
            /* par. 002*/ , String xNOTE
            /* par. 003*/ , String xPASSWORD
            /* par. 004*/ , String xUSER
                                   )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_InsertInto", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterDT_CREAZIONE = new MySqlParameter("@DT_CREAZIONE", MySqlDbType.DateTime, 8);
                MySqlParameter parameterNOTE = new MySqlParameter("@NOTE", MySqlDbType.VarChar, 150);
                MySqlParameter parameterPASSWORD = new MySqlParameter("@PASSWORD", MySqlDbType.VarChar, 200);
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 200);

                parameterDT_CREAZIONE.Value = xDT_CREAZIONE;
                parameterNOTE.Value = xNOTE;
                parameterPASSWORD.Value = xPASSWORD;
                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterDT_CREAZIONE);
                myCommand.SelectCommand.Parameters.Add(parameterNOTE);
                myCommand.SelectCommand.Parameters.Add(parameterPASSWORD);
                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Execute SQL command
                int iResult = 0;
                iResult = myCommand.SelectCommand.ExecuteNonQuery();

                // Gets output data
                return iResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_InsertInto", "Executed stored: SCR_Users_InsertInto", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_InsertInto", "Executed stored: SCR_Users_InsertInto", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        /// <summary>
        /// inserisce nella tabella Users (SqlServer calling type)
        /// Overload with external DB connection and external DB transaction.
        /// </summary>
        /// <param name="myConnection">External DB connection</param>
        /// <param name="myTransaction">External DB transaction</param>
        /// <param name="xDT_CREAZIONE">(contained value would be passed into the field "@DT_CREAZIONE" of called Stored)</param>
        /// <param name="xNOTE">(contained value would be passed into the field "@NOTE" of called Stored)</param>
        /// <param name="xPASSWORD">(contained value would be passed into the field "@PASSWORD" of called Stored)</param>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// int numero di righe inserite
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_InsertInto" returning its output.
        /// </newpara>
        /// </remarks>
        public int Users_InsertInto(
                                    MySqlConnection myConnection
                                   , MySqlTransaction myTransaction
            /* par. 001*/ , DateTime xDT_CREAZIONE
            /* par. 002*/ , String xNOTE
            /* par. 003*/ , String xPASSWORD
            /* par. 004*/ , String xUSER
                                   )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_InsertInto", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                myCommand.SelectCommand.Transaction = myTransaction;

                // Setting stored parameters
                MySqlParameter parameterDT_CREAZIONE = new MySqlParameter("@DT_CREAZIONE", MySqlDbType.DateTime, 8);
                MySqlParameter parameterNOTE = new MySqlParameter("@NOTE", MySqlDbType.VarChar, 150);
                MySqlParameter parameterPASSWORD = new MySqlParameter("@PASSWORD", MySqlDbType.VarChar, 200);
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 200);

                parameterDT_CREAZIONE.Value = xDT_CREAZIONE;
                parameterNOTE.Value = xNOTE;
                parameterPASSWORD.Value = xPASSWORD;
                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterDT_CREAZIONE);
                myCommand.SelectCommand.Parameters.Add(parameterNOTE);
                myCommand.SelectCommand.Parameters.Add(parameterPASSWORD);
                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Execute SQL command
                int iResult = 0;
                iResult = myCommand.SelectCommand.ExecuteNonQuery();

                // Gets output data
                return iResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_InsertInto", "Executed stored: SCR_Users_InsertInto", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_InsertInto", "Executed stored: SCR_Users_InsertInto", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        #endregion SCR_Users_InsertInto (apr 13 2008 12:01PM)

        #region SCR_Users_UpdateTutti_PerKey (apr 13 2008 12:01PM)
        /// <summary>
        /// aggiorna la tabella Users (SqlServer calling type)
        /// </summary>
        /// <param name="xDT_CREAZIONE">(contained value would be passed into the field "@DT_CREAZIONE" of called Stored)</param>
        /// <param name="xID_USER">(contained value would be passed into the field "@ID_USER" of called Stored)</param>
        /// <param name="xNOTE">(contained value would be passed into the field "@NOTE" of called Stored)</param>
        /// <param name="xPASSWORD">(contained value would be passed into the field "@PASSWORD" of called Stored)</param>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// int numero di righe aggiornate
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_UpdateTutti_PerKey" returning its output.
        /// </newpara>
        /// </remarks>
        public int Users_UpdateTutti_PerKey(
            /* par. 001*/  int xID_USER
            /* par. 002*/ , DateTime xDT_CREAZIONE
            /* par. 003*/ , String xNOTE
            /* par. 004*/ , String xPASSWORD
            /* par. 005*/ , String xUSER
                                           )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Getting DB connection string from WebPortal...
                MySqlConnection myConnection = new MySqlConnection(AccessoDB.StradaDBXStored);

                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_UpdateTutti_PerKey", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterDT_CREAZIONE = new MySqlParameter("@DT_CREAZIONE", MySqlDbType.DateTime, 8);
                MySqlParameter parameterID_USER = new MySqlParameter("@ID_USER", MySqlDbType.Int32, 4);
                MySqlParameter parameterNOTE = new MySqlParameter("@NOTE", MySqlDbType.VarChar, 150);
                MySqlParameter parameterPASSWORD = new MySqlParameter("@PASSWORD", MySqlDbType.VarChar, 200);
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 200);

                parameterDT_CREAZIONE.Value = xDT_CREAZIONE;
                parameterID_USER.Value = xID_USER;
                parameterNOTE.Value = xNOTE;
                parameterPASSWORD.Value = xPASSWORD;
                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterDT_CREAZIONE);
                myCommand.SelectCommand.Parameters.Add(parameterID_USER);
                myCommand.SelectCommand.Parameters.Add(parameterNOTE);
                myCommand.SelectCommand.Parameters.Add(parameterPASSWORD);
                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Open DB connection, execute SQL command and close the connection
                int iResult = 0;
                myConnection.Open();
                iResult = myCommand.SelectCommand.ExecuteNonQuery();
                myConnection.Close();

                // Gets output data
                return iResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_UpdateTutti_PerKey", "Executed stored: SCR_Users_UpdateTutti_PerKey", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_UpdateTutti_PerKey", "Executed stored: SCR_Users_UpdateTutti_PerKey", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        /// <summary>
        /// aggiorna la tabella Users
        /// Overload with external DB connection.
        /// </summary>
        /// <param name="myConnection">External DB connection</param>
        /// <param name="xDT_CREAZIONE">(contained value would be passed into the field "@DT_CREAZIONE" of called Stored)</param>
        /// <param name="xID_USER">(contained value would be passed into the field "@ID_USER" of called Stored)</param>
        /// <param name="xNOTE">(contained value would be passed into the field "@NOTE" of called Stored)</param>
        /// <param name="xPASSWORD">(contained value would be passed into the field "@PASSWORD" of called Stored)</param>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// int numero di righe aggiornate (SqlServer calling type)
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_UpdateTutti_PerKey" returning its output.
        /// </newpara>
        /// </remarks>
        public int Users_UpdateTutti_PerKey(
                                            MySqlConnection myConnection
            /* par. 001*/ , int xID_USER
            /* par. 002*/ , DateTime xDT_CREAZIONE
            /* par. 003*/ , String xNOTE
            /* par. 004*/ , String xPASSWORD
            /* par. 005*/ , String xUSER
                                           )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_UpdateTutti_PerKey", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterDT_CREAZIONE = new MySqlParameter("@DT_CREAZIONE", MySqlDbType.DateTime, 8);
                MySqlParameter parameterID_USER = new MySqlParameter("@ID_USER", MySqlDbType.Int32, 4);
                MySqlParameter parameterNOTE = new MySqlParameter("@NOTE", MySqlDbType.VarChar, 150);
                MySqlParameter parameterPASSWORD = new MySqlParameter("@PASSWORD", MySqlDbType.VarChar, 200);
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 200);

                parameterDT_CREAZIONE.Value = xDT_CREAZIONE;
                parameterID_USER.Value = xID_USER;
                parameterNOTE.Value = xNOTE;
                parameterPASSWORD.Value = xPASSWORD;
                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterDT_CREAZIONE);
                myCommand.SelectCommand.Parameters.Add(parameterID_USER);
                myCommand.SelectCommand.Parameters.Add(parameterNOTE);
                myCommand.SelectCommand.Parameters.Add(parameterPASSWORD);
                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Execute SQL command
                int iResult = 0;
                iResult = myCommand.SelectCommand.ExecuteNonQuery();

                // Gets output data
                return iResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_UpdateTutti_PerKey", "Executed stored: SCR_Users_UpdateTutti_PerKey", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_UpdateTutti_PerKey", "Executed stored: SCR_Users_UpdateTutti_PerKey", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        /// <summary>
        /// aggiorna la tabella Users (SqlServer calling type)
        /// Overload with external DB connection and external DB transaction.
        /// </summary>
        /// <param name="myConnection">External DB connection</param>
        /// <param name="myTransaction">External DB transaction</param>
        /// <param name="xDT_CREAZIONE">(contained value would be passed into the field "@DT_CREAZIONE" of called Stored)</param>
        /// <param name="xID_USER">(contained value would be passed into the field "@ID_USER" of called Stored)</param>
        /// <param name="xNOTE">(contained value would be passed into the field "@NOTE" of called Stored)</param>
        /// <param name="xPASSWORD">(contained value would be passed into the field "@PASSWORD" of called Stored)</param>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// int numero di righe aggiornate
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_UpdateTutti_PerKey" returning its output.
        /// </newpara>
        /// </remarks>
        public int Users_UpdateTutti_PerKey(
                                            MySqlConnection myConnection
                                           , MySqlTransaction myTransaction
            /* par. 001*/ , int xID_USER
            /* par. 002*/ , DateTime xDT_CREAZIONE
            /* par. 003*/ , String xNOTE
            /* par. 004*/ , String xPASSWORD
            /* par. 005*/ , String xUSER
                                           )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_UpdateTutti_PerKey", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                myCommand.SelectCommand.Transaction = myTransaction;

                // Setting stored parameters
                MySqlParameter parameterDT_CREAZIONE = new MySqlParameter("@DT_CREAZIONE", MySqlDbType.DateTime, 8);
                MySqlParameter parameterID_USER = new MySqlParameter("@ID_USER", MySqlDbType.Int32, 4);
                MySqlParameter parameterNOTE = new MySqlParameter("@NOTE", MySqlDbType.VarChar, 150);
                MySqlParameter parameterPASSWORD = new MySqlParameter("@PASSWORD", MySqlDbType.VarChar, 200);
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 200);

                parameterDT_CREAZIONE.Value = xDT_CREAZIONE;
                parameterID_USER.Value = xID_USER;
                parameterNOTE.Value = xNOTE;
                parameterPASSWORD.Value = xPASSWORD;
                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterDT_CREAZIONE);
                myCommand.SelectCommand.Parameters.Add(parameterID_USER);
                myCommand.SelectCommand.Parameters.Add(parameterNOTE);
                myCommand.SelectCommand.Parameters.Add(parameterPASSWORD);
                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Execute SQL command
                int iResult = 0;
                iResult = myCommand.SelectCommand.ExecuteNonQuery();

                // Gets output data
                return iResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_UpdateTutti_PerKey", "Executed stored: SCR_Users_UpdateTutti_PerKey", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_UpdateTutti_PerKey", "Executed stored: SCR_Users_UpdateTutti_PerKey", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        #endregion SCR_Users_UpdateTutti_PerKey (apr 13 2008 12:01PM)

        #region SCR_Users_GetPsw_PerUser (apr 13 2008 12:22PM)
        /// <summary>
        /// preleva la password che corrispondono all utente inserito nella tabella Users (SqlServer calling type)
        /// </summary>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// DataSet che contiene una table con 1 row
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_GetPsw_PerUser" returning its output.
        /// </newpara>
        /// </remarks>
        public DataSet Users_GetPsw_PerUser(
            /* par. 001*/  String xUSER
                                           )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Getting DB connection string from WebPortal...
                MySqlConnection myConnection = new MySqlConnection(AccessoDB.StradaDBXStored);

                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_GetPsw_PerUser", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 100);

                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Open DB connection, execute SQL command and close the connection
                DataSet dsResult = new DataSet();
                myConnection.Open();
                myCommand.Fill(dsResult);
                myConnection.Close();

                // Gets output data
                return dsResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetPsw_PerUser", "Executed stored: SCR_Users_GetPsw_PerUser", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetPsw_PerUser", "Executed stored: SCR_Users_GetPsw_PerUser", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        /// <summary>
        /// preleva la password che corrispondono all utente inserito nella tabella Users
        /// Overload with external DB connection.
        /// </summary>
        /// <param name="myConnection">External DB connection</param>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// DataSet che contiene una table con 1 row (SqlServer calling type)
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_GetPsw_PerUser" returning its output.
        /// </newpara>
        /// </remarks>
        public DataSet Users_GetPsw_PerUser(
                                            MySqlConnection myConnection
            /* par. 001*/ , String xUSER
                                           )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_GetPsw_PerUser", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 100);

                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Execute SQL command
                DataSet dsResult = new DataSet();
                myCommand.Fill(dsResult);

                // Gets output data
                return dsResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetPsw_PerUser", "Executed stored: SCR_Users_GetPsw_PerUser", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetPsw_PerUser", "Executed stored: SCR_Users_GetPsw_PerUser", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        /// <summary>
        /// preleva la password che corrispondono all utente inserito nella tabella Users (SqlServer calling type)
        /// Overload with external DB connection and external DB transaction.
        /// </summary>
        /// <param name="myConnection">External DB connection</param>
        /// <param name="myTransaction">External DB transaction</param>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// DataSet che contiene una table con 1 row
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_Users_GetPsw_PerUser" returning its output.
        /// </newpara>
        /// </remarks>
        public DataSet Users_GetPsw_PerUser(
                                            MySqlConnection myConnection
                                           , MySqlTransaction myTransaction
            /* par. 001*/ , String xUSER
                                           )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_Users_GetPsw_PerUser", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
                myCommand.SelectCommand.Transaction = myTransaction;

                // Setting stored parameters
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 100);

                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Execute SQL command
                DataSet dsResult = new DataSet();
                myCommand.Fill(dsResult);

                // Gets output data
                return dsResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetPsw_PerUser", "Executed stored: SCR_Users_GetPsw_PerUser", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method Users_GetPsw_PerUser", "Executed stored: SCR_Users_GetPsw_PerUser", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }

        #endregion SCR_Users_GetPsw_PerUser (apr 13 2008 12:22PM)

        #region SCR_GetUsers_PerUser (apr 13 2008  1:46PM)
        /// <summary>
        /// preleva tutti i record che corrispondono secondo i parametri di input dalla tabella Users (SqlServer calling type)
        /// </summary>
        /// <param name="xPASSWORD">(contained value would be passed into the field "@PASSWORD" of called Stored)</param>
        /// <param name="xUSER">(contained value would be passed into the field "@USER" of called Stored)</param>
        /// <returns>
        /// DataSet che contiene una table con tutte le row che corrispondono
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Calls Stored procedure "SCR_GetUsers_PerUser_Psw" returning its output.
        /// </newpara>
        /// </remarks>
        public DataSet GetUsers_PerUser(
            /* par. 001*/  String xUSER
                                           )
        {
            // Use a try-catch to get (and log) the exception
            try
            {
                // Getting DB connection string from WebPortal...
                MySqlConnection myConnection = new MySqlConnection(AccessoDB.StradaDBXStored);

                // Mark SQL command as Stored Procedure
                MySqlDataAdapter myCommand = new MySqlDataAdapter("SCR_GetUsers_PerUser", myConnection);
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Setting stored parameters
                MySqlParameter parameterUSER = new MySqlParameter("@USER", MySqlDbType.VarChar, 100);

                parameterUSER.Value = xUSER;

                myCommand.SelectCommand.Parameters.Add(parameterUSER);

                // Open DB connection, execute SQL command and close the connection
                DataSet dsResult = new DataSet();
                myConnection.Open();
                myCommand.Fill(dsResult);
                myConnection.Close();

                // Gets output data
                return dsResult;
            }
            catch (Exception oEx)
            {
                // Logging application exception
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method GetUsers_PerUser_Psw", "Executed stored: SCR_GetUsers_PerUser_Psw", "Message: " + oEx.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Application error in SqlServer method GetUsers_PerUser_Psw", "Executed stored: SCR_GetUsers_PerUser_Psw", "StackTrace: " + oEx.StackTrace);
                // Exception throwing
                throw (oEx);
            }
        }


        #endregion SCR_GetUsers_PerUser (apr 13 2008  1:46PM)

        #endregion sp per la table Users
    }
    public class CryptoUtil
    {
        public string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    //TODO verificare la classe degli users
    //CLASSE MAI PROVATA!!!!
    //IN TEORIA DOVREBBE SEERVIRE PER RESETTARE LA PASSWORD MA DEVO ANCORA PENSARE COME FARE
    static class Generator
    {
        private static Random _random = new Random();

        /// <summary>
        /// crea una stringa random di n caratteri
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                //26 letters in the alfabet, ascii + 65 for the capital letters
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _random.NextDouble() + 65))));
            }
            return builder.ToString();
        }
    }
}
