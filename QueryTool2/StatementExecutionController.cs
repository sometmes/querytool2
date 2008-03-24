using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.Generic;

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
            _commandTimeout = My.Settings.CommandTimeout;
            _dataset = new DataSet();

            execWorker.RunWorkerAsync();

            if (ExecuteAsyncCompleted != null)
                ExecuteAsyncCompleted.Invoke();
            if (End != null)
                End.Invoke();
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
                    col.DataType = (Type)Type.GetType("DataType");
                    col.AllowDBNull = (bool)schemarow["AllowDBNull"];
                    col.AutoIncrement = (bool)schemarow["IsAutoIncrement"];
                    col.ReadOnly = (bool)schemarow["IsReadOnly"];
                    col.Unique = (bool)schemarow["IsUnique"];
                    if ((bool)schemarow["IsKey"] == true)
                        primaryKeys.Add(col);
                }
                table.Constraints.Add(tablename + "_PK", primaryKeys.ToArray(), true);
            }
            return table;
        }

        void execWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = _stm;
            cmd.Connection = _connection;
            cmd.CommandTimeout = _commandTimeout;
            IDataReader r = cmd.ExecuteReader(CommandBehavior.Default);
            execWorker.ReportProgress(0, "ReaderExecuted");
            do
            {
                DataTable schematable = r.GetSchemaTable();
                execWorker.ReportProgress(1, schematable);
                DataTable table = BuildDataTable("Table" + _dataset.Tables.Count, schematable);
                
                while (r.Read())
                {
                    object[] values = new object[r.FieldCount];
                    r.GetValues(values);
                    lock (_dataset)
                    {
                        table.Rows.Add(values);
                    }
                }
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
                    ExecuteAsyncNewResult.Invoke(e.UserState as DataTable);
            }
        }

        void execWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (ExecuteAsyncRowFetchComplete != null)
                ExecuteAsyncRowFetchComplete.Invoke();
            End.Invoke();
        }

        public void RunAsistedAsync(string stm)
        {
        }

        public delegate void ConnectAsyncCompletedDelegate();
        public event ConnectAsyncCompletedDelegate ConnectAsyncCompleted;

        public delegate void ExecuteAsyncCompletedDelegate();
        public event ExecuteAsyncCompletedDelegate ExecuteAsyncCompleted;

        public delegate void ExecuteAsyncNewResultDelegate(DataTable schema);
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
