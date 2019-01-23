using System.ComponentModel;

namespace MSTest
{
    public interface IProcessDirty : INotifyPropertyChanged
    {
        bool IsNew { get; }
        bool IsDirty { get; }
        bool IsDeleted { get; }


    }
}