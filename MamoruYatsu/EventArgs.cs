using System;

namespace MamoruYatsu
{
    class EventArgs<T> : EventArgs
    {
        public EventArgs(T data)
        {
            this.Data = data;
        }

        public T Data { get; private set; }
    }

    static class GenericEventArgs
    {
        public static EventArgs<T> Create<T>(T data)
        {
            return new EventArgs<T>(data);
        }
    }
}
