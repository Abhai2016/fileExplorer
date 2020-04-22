using System;
using System.Collections.Generic;

namespace FileSystem
{
    public abstract class BaseData
    {
        protected const int Copied = 0;
        protected const int Created = 1;
        protected const int Deleted = 2;
        protected const int Moved = 3;
        protected const int Opened = 4;
        protected const int Renamed = 5;


        
        public delegate void FileManagerStateHandler(string message);
        List<FileManagerStateHandler> events;
        public string Path { get; protected set; }





        public abstract void Copy(string newPath);

        public abstract void Create(string name);

        public abstract void Delete(string name);

        public abstract void Move(string newPath);

        public abstract void Rename(string newName);



        internal static string GetNameFromPath(string path)
        {
            try
            {
                int separatorIndex = path.LastIndexOf(@"\") + 1; // doesn't count the last backslash
                string name = path.Substring(separatorIndex);
                return name;
            }
            catch (Exception exception)
            {
                return "Couldn't get the name from path" + exception.Message;
            }
        }


        public void SetEventHandlers(List<FileManagerStateHandler> events)
        {
            this.events = events;
        }


        protected void SetEvent(int idEvent, string message)
        {
            events[idEvent]?.Invoke(message);
        }
    }
}
