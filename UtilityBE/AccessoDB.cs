using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace UtilityBE
{
    public class AccessoDB
    {
        //connessione al db madre
        private static string StaticStradaDB;
        private static string StaticStradaDbXStored;

        static AccessoDB()
        {
            //if (System.Configuration.ConfigurationManager.ConnectionStrings["TRUCKMANAGEEntitiesStored"] != null)
                StaticStradaDbXStored ="";// System.Configuration.ConfigurationManager.ConnectionStrings["TRUCKMANAGEEntitiesStored"].ToString();

            //if (System.Configuration.ConfigurationManager.ConnectionStrings["TRUCKMANAGEEntities"] != null)
                StaticStradaDB = "";//System.Configuration.ConfigurationManager.ConnectionStrings["TRUCKMANAGEEntities"].ToString();
        }

        public static string StradaDB
        {
            get
            {
                return StaticStradaDB;
            }
        }

        public static string StradaDBXStored
        {
            get
            {
                return StaticStradaDbXStored;
            }
        }



        public void CloseDbConnection(MySqlConnection myConnection)
        {
            if ((myConnection.State & System.Data.ConnectionState.Open) == System.Data.ConnectionState.Open)
            {
                myConnection.Close();
            }
        }
    }

    public class TransactionalDbAccess : MarshalByRefObject
    {
        public TransactionalDbAccess()
        {
            //not implemented to be used with remoting
        }
        // Properties
        public MySqlTransaction Transaction
        {
            get
            {
                return pTransaction;
            }
        }
        public MySqlConnection Connection
        {
            get
            {
                return pConnection;
            }
        }
        public MySqlCommand Command
        {
            get
            {
                return pCommand;
            }
        }

        public void TransConnectionCommit()
        {
            pTransaction.Commit();
            pConnection.Close();
            return;
        }
        public void TransConnectionRollBack()
        {
            if (pTransaction.Connection != null)
            {
                pTransaction.Rollback();
                pConnection.Close();
            }
            return;
        }

        private MySqlTransaction pTransaction;
        private MySqlConnection pConnection;
        private MySqlCommand pCommand;
    }
}
