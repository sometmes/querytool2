using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace App
{
    class PagedGridView:DataGridView
    {
        BindingSource _pagingBindingSource;
        PropertyDescriptorCollection _bindingSourceCols;

        int _pageSize = 30;
        int _rowRequested = 0;
        bool _lastPageReached = false;

        public delegate void PageNeededDelegate(int rowIndex, int pageSize);
        public event PageNeededDelegate PageNeeded;

        public delegate void DisableConstraintsDelegate(object dataSource);
        public event DisableConstraintsDelegate DisableConstraints;

        public PagedGridView()
        {
            VirtualMode = true;
            PageSize = 100;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            NewRowNeeded += new DataGridViewRowEventHandler(PagedGridView_NewRowNeeded);
            CellValuePushed += new DataGridViewCellValueEventHandler(PagedGridView_CellValuePushed);
            CancelRowEdit += new QuestionEventHandler(PagedGridView_CancelRowEdit);
            RowLeave += new DataGridViewCellEventHandler(PagedGridView_RowLeave);
            RowValidating += new DataGridViewCellCancelEventHandler(PagedGridView_RowValidating);
            CellBeginEdit+=new DataGridViewCellCancelEventHandler(PagedGridView_CellBeginEdit);
        }

        void  PagedGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
 	        if (!_uncommitedRows.ContainsKey(e.RowIndex))
                _uncommitedRows.Add(e.RowIndex,object[] of values for the row);
        }

        void PagedGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (this.IsCurrentRowDirty)
            {
                try
                {
                    bool nocellerrors = true;
                    foreach (DataGridViewCell cell in CurrentRow.Cells)
                    {
                        if (!string.IsNullOrEmpty(cell.ErrorText))
                        {
                            nocellerrors = false;
                            break;
                        }
                    }
                    _pagingBindingSource.EndEdit();
                    if (nocellerrors)
                    {
                        _uncommitedRows.Remove(e.RowIndex);
                        CurrentRow.ErrorText = "";
                    }
                    else
                        throw new ApplicationException("Some cells still have errors. Please correct them and try again.");
                }
                catch (Exception ex)
                {
                    CurrentRow.ErrorText = ex.GetBaseException().Message;
                    DialogResult res = MessageBox.Show("Changes for the row cannot be commit because some errors have been found.\r\nPress Retry button to return to the row and correct those errors, Abort to discard changes and Ignore to disable constraints and continue editing rows (erros needs no be resolved before save)", "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    if (res == DialogResult.Retry)
                        e.Cancel = true;
                    else if (res == DialogResult.Abort)
                    {
                        object row = _pagingBindingSource[e.RowIndex];
                        this.CancelEdit();
                        DataGridViewRow insert = this.Rows[this.NewRowIndex];
                        insert.ErrorText = "";
                        foreach (DataGridViewCell cell in insert.Cells)
                            cell.ErrorText = "";
                    }
                    else if (res == DialogResult.Ignore)
                    {
                        if (DisableConstraints != null)
                            DisableConstraints.Invoke(_pagingBindingSource.DataSource);
                        _pagingBindingSource.EndEdit();
                    }
                }
            }
            else
                _pagingBindingSource.CancelEdit();
        }

        void PagedGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        void PagedGridView_CancelRowEdit(object sender, QuestionEventArgs e)
        {
            _pagingBindingSource.CancelEdit();
            _uncommitedRows.Remove(CurrentRow.Index);
        }

        void PagedGridView_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                _uncommitedRows[e.RowIndex][e.ColumnIndex] = e.Value;
                _bindingSourceCols[e.ColumnIndex].SetValue(_pagingBindingSource[e.RowIndex], e.Value);
                CurrentCell.ErrorText = "";
            }
            catch (Exception ex)
            {
                CurrentCell.ErrorText = ex.GetBaseException().Message;
            }
        }

        void PagedGridView_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {
            Trace.TraceInformation("NewRowNeeded");
            _pagingBindingSource.AddNew();
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        internal void BindPagedDataSource()
        {
            _bindingSourceCols = _pagingBindingSource.GetItemProperties(null);
            this.RowCount = _pagingBindingSource.Count + PageSize + (this.AllowUserToAddRows ? 1 : 0);
            _rowRequested = _pagingBindingSource.Count;
            _lastPageReached = false;
        }

        protected override void OnDataMemberChanged(EventArgs e)
        {
            if (PagedDataSource != null)
                BindPagedDataSource();
            base.OnDataMemberChanged(e);
        }

        [AttributeProvider(typeof(IListSource)), RefreshProperties(RefreshProperties.Repaint)]
        public object PagedDataSource
        {
            get
            {
                if (_pagingBindingSource == null)
                    return null;
                return _pagingBindingSource.DataSource;
            }
            set
            {
                if (value != null)
                {
                    lock (value)
                    {
                        DataSource = null;
                        VerticalScrollBar.MouseCaptureChanged -= new EventHandler(VerticalScrollBar_MouseCaptureChanged);
                        VerticalScrollBar.MouseCaptureChanged += new EventHandler(VerticalScrollBar_MouseCaptureChanged);
                        _pagingBindingSource = new BindingSource(value, this.DataMember);
                        BindPagedDataSource();
                    }
                }
                else //value == null
                {
                    _pagingBindingSource = null;
                    _bindingSourceCols = null;
                    this.RowCount = (this.AllowUserToAddRows ? 1 : 0);
                    _rowRequested = 0;
                    VerticalScrollBar.MouseCaptureChanged -= new EventHandler(VerticalScrollBar_MouseCaptureChanged);
                }
            }
        }

        public void NoMorePagesAvailable()
        {
            if (_pagingBindingSource == null) return;

            _lastPageReached = true;
            NewRowCount = _pagingBindingSource.Count + (this.AllowUserToAddRows ? 1 : 0);
        }

        System.Collections.Generic.Dictionary<int, object[]> _uncommitedRows = new Dictionary<int,object[]>();

        protected override void OnCellValueNeeded(DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex == 6)
                Trace.TraceInformation("{0} {1} Row, {2} CellValueNeeded",DateTime.Now.ToLongTimeString(),e.RowIndex,e.ColumnIndex);

            if (_pagingBindingSource != null)
            {
                lock (_pagingBindingSource.DataSource)
                {
                    DataGridViewColumn col = this.Columns[e.ColumnIndex];

                    if (e.RowIndex > (_rowRequested - this.DisplayedRowCount(true))
                        && !_lastPageReached)
                    {
                        _rowRequested = _pagingBindingSource.Count + _pageSize;
                        this.NewRowCount = _rowRequested + _pageSize + (this.AllowUserToAddRows ? 1 : 0);
                    }

                    if (e.RowIndex < _pagingBindingSource.Count)
                    {
                        object[] uncommitedrow;
                        if (_uncommitedRows.TryGetValue(e.RowIndex,out uncommitedrow))
                        {
                            e.Value = uncommitedrow[e.ColumnIndex];
                        }
                        else
                        {
                            string dataPropertyName = col.DataPropertyName;
                            if (dataPropertyName != "")
                                e.Value = _bindingSourceCols[dataPropertyName].GetValue(_pagingBindingSource[e.RowIndex]);
                        }
                    }


                    if (e.RowIndex > (_pagingBindingSource.Count - this.DisplayedRowCount(true))
                        && (_rowRequested == _pagingBindingSource.Count)
                        && !_lastPageReached)
                    {
                        _rowRequested = _pagingBindingSource.Count + _pageSize;
                        if (PageNeeded != null)
                            PageNeeded.Invoke(_pagingBindingSource.Count, _pageSize);
                        this.NewRowCount = _rowRequested + _pageSize + (this.AllowUserToAddRows ? 1 : 0);
                    }
                }
            }

            base.OnCellValueNeeded(e);
        }

        void VerticalScrollBar_MouseCaptureChanged(object sender, EventArgs e)
        {
            lock (sync)
            {
                if (_newRowCount != -1)
                {
                    RowCount = _newRowCount;
                    _newRowCount = -1;
                }
            }
        }

        private int _newRowCount = -1;
        object sync = 1;
        protected int NewRowCount
        {
            set
            {
                lock (sync)
                {
                    if (!VerticalScrollBar.Capture)
                        RowCount = value;
                    else
                        _newRowCount = value;
                }
            }
        }
    }
}
