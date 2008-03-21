using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

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

        public void ExecuteAsync(string stm)
        {
            if (Start != null)
                Start.Invoke();

            _stm = stm;
            execWorker.RunWorkerAsync();

            if (ExecuteAsyncCompleted != null)
                ExecuteAsyncCompleted.Invoke();
            if (End != null)
                End.Invoke();
        }

        void execWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = _stm;
            cmd.Connection = _connection;
            //cmd.CommandTimeout = My.Settings.CommandTimeout;
            IDataReader r = cmd.ExecuteReader();
            execWorker.ReportProgress(0, "ReaderExecuted");
            do
            {
                execWorker.ReportProgress(1, r.GetSchemaTable());
                while (r.Read())
                {
                    object[] values = new object[r.FieldCount];
                    r.GetValues(values);
                    execWorker.ReportProgress(2, values);
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
            else if (e.ProgressPercentage == 2)
            {
                if (ExecuteAsyncNewRow != null)
                    ExecuteAsyncNewRow.Invoke(e.UserState as object[]);
            }
        }

        void execWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (ExecuteAsyncCompleted != null)
                ExecuteAsyncCompleted.Invoke();
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

        public delegate void ExecuteAsyncNewRowDelegate(object[] values);
        public event ExecuteAsyncNewRowDelegate ExecuteAsyncNewRow;

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
