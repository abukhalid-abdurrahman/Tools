using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Tools
{
    public class SQLManager
    {
        private string ConnectionString = string.Empty;
        private string DataBaseName = string.Empty;
        public SQLManager(string _ServerName, string _DataBaseName, string _SecurityType)
        {
            this.DataBaseName = _DataBaseName;
            this.ConnectionString = $"Data Source = {_ServerName};Initial catalog = {_DataBaseName};{_SecurityType}";
        }
        public void InsertRecord(string _TableName, string _ColumnsName, string _Values)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    if (this.isConnected(sqlConnection))
                    {
                        sqlCommand.CommandText = $"insert into [{DataBaseName}].[dbo].[{_TableName}] {_ColumnsName} values({_Values})";
                        sqlCommand.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        sqlConnection.Close();
                    }
                }
                catch (SqlException ex)
                {
                    UserInterface.Output($"SQL EXCEPTION: {ex.Message}", ConsoleColor.Red);
                    sqlTransaction.Rollback();
                }
            }
        }
        public void UpdateRecord(string _TableName, string _Query, string _Condition)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    if (this.isConnected(sqlConnection))
                    {
                        sqlCommand.CommandText = $"update [{DataBaseName}].[dbo].[{_TableName}] set {_Query} where {_Condition}";
                        sqlCommand.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        sqlConnection.Close();
                    }
                }
                catch (SqlException ex)
                {
                    UserInterface.Output($"SQL EXCEPTION: {ex.Message}", ConsoleColor.Red);
                    sqlTransaction.Rollback();
                }
            }
        }
        public void DeleteRecord(string _TableName, string _Condition)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    if (this.isConnected(sqlConnection))
                    {
                        sqlCommand.CommandText = $"delete from [{DataBaseName}].[dbo].[{_TableName}] where {_Condition}";
                        sqlCommand.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        sqlConnection.Close();
                    }
                }
                catch (SqlException ex)
                {
                    UserInterface.Output($"SQL EXCEPTION: {ex.Message}", ConsoleColor.Red);
                    sqlTransaction.Rollback();
                }
            }
        }
        public List<string> Select(string _TableName, string _Columns, string _Condition, int _ColumnsCount)
        {
            List<string> queryResults = new List<string>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    if (this.isConnected(sqlConnection))
                    {
                        sqlCommand.CommandText = $"select {_Columns} from [{DataBaseName}].[dbo].[{_TableName}] where {_Condition}";
                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                        object[] _columns = new object[]{};
                        while(sqlDataReader.Read())
                        {
                            sqlDataReader.GetValues(_columns);
                            queryResults.Add(string.Join('|', _columns));
                            _columns = new object[]{};
                        }
                        sqlTransaction.Commit();
                        sqlConnection.Close();
                    }
                }
                catch (SqlException ex)
                {
                    UserInterface.Output($"SQL EXCEPTION: {ex.Message}", ConsoleColor.Red);
                    sqlTransaction.Rollback();
                }
            }
            return queryResults;
        }

        private bool isConnected(SqlConnection sqlConnection)
        {
            if (sqlConnection.State == ConnectionState.Open)
                return true;
            else
                return false;
        }

    }
}
