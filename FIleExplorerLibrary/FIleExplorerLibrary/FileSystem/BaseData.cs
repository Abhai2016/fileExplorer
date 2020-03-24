using System;

namespace FileSystem
{
    public abstract class BaseData
    {
        protected internal event FileManagerStateHandler Copied;
        protected internal event FileManagerStateHandler Created;
        protected internal event FileManagerStateHandler Deleted;
        protected internal event FileManagerStateHandler Moved;
        protected internal event FileManagerStateHandler Renamed;

        public delegate void FileManagerStateHandler(string message);
        public string Path { get; protected set; }





        public abstract void Copy(string oldPath, string newPath);

        public abstract void Create(string name);

        public abstract void Delete(string name);

        public abstract void Move(string oldPath, string newPath);

        public abstract void Rename(string oldName, string newName);



        protected string getNameFromPath(string path)
        {
            try
            {
                int separatorIndex = path.LastIndexOf(@"\");
                string name = path.Substring(separatorIndex);
                return name;
            }
            catch (Exception exception)
            {
                return "Couldn't get the name from path" + exception.Message;
            }
        }


        public void setEventHandlers(FileManagerStateHandler copied, FileManagerStateHandler created,
            FileManagerStateHandler deleted, FileManagerStateHandler moved, FileManagerStateHandler renamed)
        {
            Copied += copied;
            Created += created;
            Deleted += deleted;
            Moved += moved;
            Renamed += renamed;
        }


        protected void SetEvent(string eventType, string message)
        {
            switch (eventType)
            {
                case "Copied":
                    Copied?.Invoke(message);
                    break;
                case "Created":
                    Created?.Invoke(message);
                    break;
                case "Deleted":
                    Deleted?.Invoke(message);
                    break;
                case "Moved":
                    Moved?.Invoke(message);
                    break;
                case "Renamed":
                    Renamed?.Invoke(message);
                    break;
                default:
                    throw new Exception("Didn't find this event");
            }
        }
    }
}
