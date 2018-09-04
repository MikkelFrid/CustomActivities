using System;
using System.Activities;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DeloitteDatabase
{
    public class DatabaseConnectionLog : IDisposable
    {
        private DbConnection Connection;
        private DbCommand Command;
        private DbTransaction Transaction;
        private string ProviderName;
        public string ProcessName;
        public string CaseIdentifier;

        public DatabaseConnectionLog(string connectionString, string providerName, string processName, string caseIdentifier)
        {
            this.CaseIdentifier = caseIdentifier;
            this.ProcessName = processName;
            this.ProviderName = providerName;
            this.Connection = DbProviderFactories.GetFactory(providerName).CreateConnection();
            this.Connection.ConnectionString = connectionString;
            this.OpenConnection();
        }

        public void OpenConnection()
        {
            if (this.Connection.State == ConnectionState.Open)
                return;
            this.Connection.Open();
        }

        public void BeginTransaction()
        {
            this.Transaction = this.Connection.BeginTransaction();
        }

        public DataTable InsertLog(string sql, int commandTimeout, CommandType commandType = CommandType.Text)
        {
            this.OpenConnection();
            this.SetupCommand(sql, commandTimeout, commandType);
            this.Command.Transaction = this.Transaction;
            DataTable dataTable = new DataTable();
            dataTable.Load((IDataReader)this.Command.ExecuteReader());
            return dataTable;
        }

        public int Execute(string sql, int commandTimeout, CommandType commandType = CommandType.Text)
        {
            this.OpenConnection();
            this.SetupCommand(sql, commandTimeout, commandType);
            this.Command.Transaction = this.Transaction;
            int num = this.Command.ExecuteNonQuery();
            return num;
        }

        public int InsertDataTable(string tableName, DataTable dataTable)
        {
            DbDataAdapter dataAdapter = DbProviderFactories.GetFactory(this.ProviderName).CreateDataAdapter();
            DbCommandBuilder commandBuilder = DbProviderFactories.GetFactory(this.ProviderName).CreateCommandBuilder();
            commandBuilder.DataAdapter = dataAdapter;
            dataAdapter.ContinueUpdateOnError = false;
            dataAdapter.SelectCommand = this.Connection.CreateCommand();
            dataAdapter.SelectCommand.Transaction = this.Transaction;
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            dataAdapter.SelectCommand.CommandText = string.Format("select {0} from {1}", (object)this.GetColumnNames(dataTable), (object)tableName);
            dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
            dataAdapter.InsertCommand.Connection = this.Connection;
            dataAdapter.InsertCommand.Transaction = this.Transaction;
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (row.RowState == DataRowState.Unchanged)
                    row.SetAdded();
            }
            return dataAdapter.Update(dataTable);
        }

        public void Commit()
        {
            if (this.Transaction == null)
                return;
            this.Transaction.Commit();
        }

        public void Rollback()
        {
            if (this.Transaction == null)
                return;
            this.Transaction.Rollback();
        }

        public void Dispose()
        {
            if (this.Command != null)
                this.Command.Dispose();
            if (this.Transaction != null)
                this.Transaction.Dispose();
            if (this.Connection == null)
                return;
            this.Connection.Dispose();
        }

        private void SetupCommand(string sql, int commandTimeout, CommandType commandType = CommandType.Text)
        {
            if (this.Connection == null)
                throw new InvalidOperationException("Connection is not initialized");
            if (this.Command == null)
                this.Command = this.Connection.CreateCommand();
            int num = (int)Math.Ceiling((double)commandTimeout / 1000.0);
            if (num != 0)
                this.Command.CommandTimeout = num;
            this.Command.CommandType = commandType;
            this.Command.CommandText = sql.Replace("[-[ProcessName]-]", ProcessName).Replace("[-[CaseIdentifier]-]", CaseIdentifier);
            this.Command.Parameters.Clear();
            return;
        }

        private string GetColumnNames(DataTable table)
        {
            if (table.Columns.Count < 1 || table.Rows.Count < 1)
                return string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (DataColumn column in (InternalDataCollectionBase)table.Columns)
                stringBuilder.Append(column.ColumnName + ",");
            return stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString();
        }

        private static ParameterDirection WokflowDbParameterToParameterDirection(ArgumentDirection argumentDirection)
        {
            if (argumentDirection == ArgumentDirection.In)
                return ParameterDirection.Input;
            return argumentDirection == ArgumentDirection.Out ? ParameterDirection.Output : ParameterDirection.InputOutput;
        }

        private static ArgumentDirection WokflowParameterDirectionToDbParameter(ParameterDirection parameterDirection)
        {
            switch (parameterDirection)
            {
                case ParameterDirection.Input:
                    return ArgumentDirection.In;
                case ParameterDirection.Output:
                    return ArgumentDirection.Out;
                case ParameterDirection.InputOutput:
                    return ArgumentDirection.InOut;
                default:
                    throw new ArgumentException("The provided ParameterDirection argument does not have a coresponding ArgumentDirection value");
            }
        }
    }
}
