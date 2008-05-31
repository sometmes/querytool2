using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

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

        public PagedGridView()
        {
            VirtualMode = true;
            PageSize = 100;
            RowHeadersWidth = 15;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
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

        protected override void OnCellValueNeeded(DataGridViewCellValueEventArgs e)
        {
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
                        string dataPropertyName = col.DataPropertyName;
                        if (dataPropertyName != "")
                            e.Value = _bindingSourceCols[dataPropertyName].GetValue(_pagingBindingSource[e.RowIndex]);
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
