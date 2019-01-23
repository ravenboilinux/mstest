using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSTest
{
    public abstract class DomainBase : IProcessDirty
    {
        private bool isObjectNew = true;
        private bool isObjectDirty = true;
        private bool isObjectDelete;

        protected int id;


        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    PropertyHasChanged(nameof(Id));
                }
            }
        }
        #region IProcessDirty Members
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsNew { get => isObjectNew; set => isObjectNew = value; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsDirty { get => isObjectDirty; set => isObjectDirty = value; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsDeleted { get => isObjectDelete; set => isObjectDelete = value; }

        #endregion
        private PropertyChangedEventHandler _nonSerializableHandler;
        private PropertyChangedEventHandler _serializableHandler;



        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool IsSavable => isObjectDirty;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {

                //if (value.Method.IsPublic && (value.Method.DeclaringType.IsSerializable || value.Method.IsStatic))
                //    _serializableHandler = (PropertyChangedEventHandler)Delegate.Combine(_serializableHandler, value);
                //else
                _nonSerializableHandler = (PropertyChangedEventHandler)Delegate.Combine(_nonSerializableHandler, value);
            }
            remove
            {
                //if (value.Method.IsPublic && (value.Method.DeclaringType.IsSerializable || value.Method.IsStatic))
                //    _serializableHandler = (PropertyChangedEventHandler)Delegate.Remove(_serializableHandler, value);
                //else
                _nonSerializableHandler = (PropertyChangedEventHandler)Delegate.Remove(_nonSerializableHandler, value);
            }
        }


        protected virtual void OnUnknownPropertyChanged()
        {
            OnPropertyChanged(string.Empty);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (_nonSerializableHandler != null)
            {
                _nonSerializableHandler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            if (_serializableHandler != null)
            {
                _serializableHandler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void MarkNew()
        {
            isObjectNew = true;
            isObjectDelete = false;
            MarkDirty();
        }

        protected virtual void MarkOld()
        {
            isObjectNew = false;
            MarkClean();
        }

        protected void MarkDeleted()
        {
            isObjectDelete = true;
            MarkDirty();
        }

        protected void MarkDirty()
        {
            MarkDirty(false);
        }

        protected void MarkDirty(bool suppressEvent)
        {
            isObjectDirty = true;
            if (!suppressEvent)
                OnUnknownPropertyChanged();
        }

        protected void PropertyHasChange()
        {
            PropertyHasChanged(new StackTrace(Thread.CurrentThread, false).GetFrames()[1].GetMethod().Name.Substring(4));
        }


        protected virtual void PropertyHasChanged(string propertyName)
        {
            MarkDirty(true);
            OnPropertyChanged(propertyName);
        }

        protected void MarkClean()
        {
            isObjectDirty = false;
        }

        public virtual void Delete()
        {
            MarkDeleted();
        }
    }
}
