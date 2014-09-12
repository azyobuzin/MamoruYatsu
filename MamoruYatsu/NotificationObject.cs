using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace MamoruYatsu
{
    class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged<T>(Expression<Func<T>> prop)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs((prop.Body as MemberExpression).Member.Name));
        }
    }
}
