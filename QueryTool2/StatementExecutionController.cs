using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Reflection;

namespace App
{
    class StatementExecutionController
    {
        private BackgroundWorker2 execWorker = new App.BackgroundWorker2();

        public StatementExecutionController()
        {
            execWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(execWorker_DoWork);
            execWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(execWorker_RunWorkerCompleted);
            execWorker.WorkerReportsProgress = true;
            execWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(execWorker_ProgressChanged);
        }

        IDbConnection _connection;

        public IDbConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }


        public void ConnectAsync(IDbConnection connection)
        {
            
        }

        string _stm;
        int _commandTimeout;
        DataSet _dataset;

        public DataSet DataSet
        {
            get { return _dataset; }
        }

        public void ExecuteAsync(string stm)
        {
            if (Start != null)
                Start.Invoke();

            _stm = stm;
            //_stm = "select * from AdventureWorks.Person.AddressType";
            _stm = "select * from AdventureWorks.Production.WorkOrder";
            _commandTimeout = My.Settings.CommandTimeout;
            _dataset = new DataSet();

            execWorker.RunWorkerAsync();

        }

        protected DataTable BuildDataTable(string tablename, DataTable schema)
        {
            DataTable table = null;
            lock (_dataset)
            {
                table = _dataset.Tables.Add(tablename);
                List<DataColumn> primaryKeys = new List<DataColumn>();

                foreach (DataRow schemarow in schema.Rows)
                {
                    DataColumn col = table.Columns.Add();
                    col.ColumnName = (string)schemarow["ColumnName"];
                    col.DataType = (Type)schemarow["DataType"];
                    col.AllowDBNull = (bool)schemarow["AllowDBNull"];
                    col.AutoIncrement = (bool)schemarow["IsAutoIncrement"];
                    col.ReadOnly = (bool)schemarow["IsReadOnly"];
                    col.Unique = (bool)schemarow["IsUnique"];
                    if (schemarow["IsKey"] != DBNull.Value && (bool)schemarow["IsKey"] == true)
                        primaryKeys.Add(col);
                }
                if (primaryKeys.Count > 0)
                    table.Constraints.Add(tablename + "_PK", primaryKeys.ToArray(), true);
            }
            return table;
        }

        void execWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            IDbCommand cmd = _connection.CreateCommand();
            if (_commandTimeout != -1)
                cmd.CommandTimeout = _commandTimeout;
            cmd.CommandText = _stm;
            cmd.Connection = _connection;
            IDataReader r = cmd.ExecuteReader(CommandBehavior.Default);
            execWorker.ReportProgress(0, "ReaderExecuted");
            do
            {
                DataTable schematable = r.GetSchemaTable();
                DataTable table = BuildDataTable("Table" + _dataset.Tables.Count, schematable);
                ExecuteNewResultEventArgs ee = new ExecuteNewResultEventArgs();
                ee.Schema = schematable;
                ee.Data = table;
                execWorker.ReportProgress(1, ee);
                
                while (r.Read())
                {
                    object[] values = new object[r.FieldCount];
                    r.GetValues(values);
                    lock (table)
                    {
                        table.Rows.Add(values);
                    }
                }

                execWorker.ReportProgress(2, "ReaderExecuted");
            }
            while (r.NextResult());
            r.Close();
        }

        void execWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            string step = e.UserState as string;
            if (e.ProgressPercentage == 0)
            {
                if (ExecuteAsyncCompleted != null)
                    ExecuteAsyncCompleted.Invoke();
            }
            else if (e.ProgressPercentage == 1)
            {
                if (ExecuteAsyncNewResult != null)
                    ExecuteAsyncNewResult.Invoke(e.UserState as ExecuteNewResultEventArgs);
            }
            else if (e.ProgressPercentage == 2)
            {
                if (ExecuteAsyncRowFetchComplete != null)
                    ExecuteAsyncRowFetchComplete.Invoke();
            }
        }

        void execWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw e.Error;

            End.Invoke();
        }

        public void RunAsistedAsync(string stm)
        {
        }

        public delegate void ConnectAsyncCompletedDelegate();
        public event ConnectAsyncCompletedDelegate ConnectAsyncCompleted;

        public delegate void ExecuteAsyncCompletedDelegate();
        public event ExecuteAsyncCompletedDelegate ExecuteAsyncCompleted;

        public class ExecuteNewResultEventArgs
        {
            public DataTable Schema;
            public DataTable Data;
        }
        public delegate void ExecuteAsyncNewResultDelegate(ExecuteNewResultEventArgs e);
        public event ExecuteAsyncNewResultDelegate ExecuteAsyncNewResult;

        public delegate void ExecuteAsyncRowFetchCompleteDelegate();
        public event ExecuteAsyncRowFetchCompleteDelegate ExecuteAsyncRowFetchComplete;

        public delegate void ExecuteAsyncMessageDelegate(object[] values);
        public event ExecuteAsyncMessageDelegate ExecuteAsyncMessage;

        public delegate void RunAsistedAsyncCompletedDelegate();
        public event RunAsistedAsyncCompletedDelegate RunAsistedAsyncCompleted;

        public delegate void StartDelegate();
        public event StartDelegate Start;

        public delegate void EndDelegate();
        public event EndDelegate End;
    }
}
