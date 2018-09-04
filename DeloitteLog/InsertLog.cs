using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

// Added to all custom activities
using System.Activities;
using System.ComponentModel;
using System.Drawing;

using System.Data;
using System.Windows.Markup;
using DeloitteDatabase;

namespace DeloitteLog
{

    public class InsertLog : AsyncCodeActivity
    {
        private DatabaseConnectionLog DbConnection;

        [RequiredArgument]
        [Category("Connection Configuration")]
        [OverloadGroup("Existing Database Connection")]
        public InArgument<DatabaseConnectionLog> ExistingDbConnection { get; set; }

        [RequiredArgument]
        [Category("Input")]
        public InArgument<string> Message { get; set; }

        [Category("Common")]
        public InArgument<int> TimeoutMS { get; set; }
        // Get meta data
        DateTime timeStamp = DateTime.Now;
        string WindowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        string MachineName = System.Environment.MachineName;

        string JobId = UiPath.Executor.ExecutorManager.Instance.Id.ToString();

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            string connString = (string)null;
            string provName = (string)null;
            string sql = string.Empty;
            int commandTimeout = this.TimeoutMS.Get((ActivityContext)context);
            if (commandTimeout < 0)
                throw new ArgumentException("TimeoutMS");
            try
            {
                //string sads = DatabaseConnectionLog.TransactionId;
                this.DbConnection = this.ExistingDbConnection.Get((ActivityContext)context);
                sql = "INSERT INTO UiPathStats.dbo.Logs ([TimeStamp], [Level], [WindowsIdentity] ,[ProcessName] ,[JobKey],[RobotName],[CaseIdentifier],[Message]) VALUES ('" + timeStamp+ "', 2, '"+WindowsIdentity+ "', '[-[ProcessName]-]', '"+JobId+"','"+MachineName+ "','[-[CaseIdentifier]-]','" + this.Message.Get((ActivityContext)context)+"')";
            }
            catch (Exception ex)
            {
                this.HandleException(ex, false);
            }
            Func<System.Data.DataTable> func = (Func<System.Data.DataTable>)(() =>
            {
                if (this.DbConnection == null)
                    this.DbConnection = new DatabaseConnectionLog(connString, provName, "","");
                if (this.DbConnection == null)
                    return (System.Data.DataTable)null;
                return this.DbConnection.InsertLog(sql, commandTimeout, CommandType.Text);
            });
            context.UserState = (object)func;
            return func.BeginInvoke(callback, state);
        }

        private void HandleException(Exception ex, bool continueOnError)
        {
            if (!continueOnError)
                throw ex;
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            DatabaseConnectionLog DatabaseConnectionLog = this.ExistingDbConnection.Get((ActivityContext)context);

           if (DatabaseConnectionLog == null)
            this.DbConnection.Dispose();

        }
    }

}
