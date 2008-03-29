///Created by Roy Osherove, Team Agile
/// Blog: www.ISerializable.com
/// Roy@TeamAgile.com

using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace App
{
    /// <summary>
    /// Extends the standard BackgroundWorker Component in .NET 2.0 Winforms
    /// To support the ability of aborting the thread the worker it is using
    /// </summary>
    /// <remarks></remarks> using System;
    public partial class BackgroundWorker2 : BackgroundWorker
    {
        private Thread mThread;

        public BackgroundWorker2()
        {
            WorkerSupportsCancellation = true;
        }

        protected override void OnProgressChanged(ProgressChangedEventArgs e)
        {
            if (threadAborted)
                return;

            base.OnProgressChanged(e);
        }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            threadAborted = false;
            mThread = Thread.CurrentThread;
            try
            {
                base.OnDoWork(e);
            }
            catch (ThreadAbortException)
            {
                // sometmes 13/01/2008
                // This is not needed if we override OnRunWorkerCompleted
                //Thread.ResetAbort();
            }
        }

        private bool threadAborted = false;
        public void Abort()
        {
            if (!IsBusy || mThread == null)
            {
                return;
            }
            try
            {
                CancelAsync();
                Thread.SpinWait(10);
                mThread.Abort();
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception)
            {
                throw;
            }
            try
            {
                threadAborted = true;
                //setPrivateFieldValue<bool>("isRunning", false);
                AsyncOperation op = getPrivateFieldValue<AsyncOperation>("asyncOperation");
                SendOrPostCallback completionCallback = getPrivateFieldValue<SendOrPostCallback>("operationCompleted");
                RunWorkerCompletedEventArgs completedArgs = new RunWorkerCompletedEventArgs(null, null, true);
                op.PostOperationCompleted(completionCallback, completedArgs);
                // Ends up calling AsyncOperationCompleted, that resets isRunning field.
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected override void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            // sometmes 13/01/2008
            // When aborting we do not call completed event. 
            // This prevents ThreadAbortException to propagate to calling thread.
            if (!threadAborted)
                base.OnRunWorkerCompleted(e);
        }

        //type safe reflection
        private FieldType getPrivateFieldValue<FieldType>(string fieldName)
        {
            Type type = typeof(BackgroundWorker);
            FieldInfo field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            object fieldVal = field.GetValue(this);

            return safeCastTo<FieldType>(fieldVal);
        }

        private void setPrivateFieldValue<FieldType>(string fieldName, FieldType value)
        {
            Type type = typeof(BackgroundWorker);
            FieldInfo field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            field.SetValue(this, value);
            Debug.Assert(field.GetValue(this).Equals(value));
        }

        /// <summary>
        /// Works like a strongly typed "as" operator
        /// If the object is not of the requested type, 
        /// the default value for that type is returns instead of throwing an exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private T safeCastTo<T>(object obj)
        {
            if (obj == null)
            {
                return default(T);
            }
            if (!(obj is T))
            {
                return default(T);
            }
            return (T)obj;
        }
    }
}
