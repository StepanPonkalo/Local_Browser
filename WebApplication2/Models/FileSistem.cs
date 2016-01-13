using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;



namespace WebApplication2.Models
{
    public struct Folder
    {
        public string OriginalPath;
        public string FullPath;
    }
    public class FileSistem
    {

        public DriveInfo[] allDrives;
        public Folder[] FolderInfo;
        public string[] FilesInFolder;
        public int FilesLess10;
        public int FilesLess50More10;
        public int FilesMore100;
        public string logg;
        public string currentPath;
        public string PrevPath;
        public FileSistem()
        {
            allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                if (drive.IsReady)
                {
                   DirSearch(drive.Name);
                }
            }
            PrevPath = null;
            currentPath = null;
        }
        public FileSistem(string path)
        {
            if ((path != null)&&(path!="undefined"))
            {
                
                try
                {
                    DirSearch(path);
                    currentPath = path;
                    var dir = new DirectoryInfo(path);
                    string[] FoldersInDir = Directory.GetDirectories(path);
                    Folder[] newFolderInfo = new Folder[FoldersInDir.Length];
                    for (int i = 0; i < FoldersInDir.Length; i++)
                    {
                        newFolderInfo[i].FullPath = FoldersInDir[i];
                        newFolderInfo[i].OriginalPath = GetName(FoldersInDir[i], path);
                    }
                    FolderInfo = newFolderInfo;
                    string[] files = Directory.GetFiles(path);
                    for (int i = 0; i < Directory.GetFiles(path).Length; i++)
                    {
                        files[i] = GetName(files[i], path);
                    }
                    FilesInFolder = files;
                    PrevPath = GetPrev(path);
                }
                catch {
                    if (path.Length < 4) 
                    {
                        FileSistem Fs = new FileSistem();
                        this.allDrives = Fs.allDrives;
                        this.currentPath = Fs.currentPath;
                        this.FilesLess10 = Fs.FilesLess10;
                        this.FilesLess50More10 = Fs.FilesLess50More10;
                        this.FilesMore100 = Fs.FilesMore100;
                        this.PrevPath = Fs.PrevPath;
                    }
                
                }
            }
            else
            {
                FileSistem Fs = new FileSistem();
                this.allDrives = Fs.allDrives;
                this.currentPath = Fs.currentPath;
                this.FilesLess10 = Fs.FilesLess10;
                this.FilesLess50More10 = Fs.FilesLess50More10;
                this.FilesMore100 = Fs.FilesMore100;
                this.PrevPath = Fs.PrevPath;
            }
        }

        private void DirSearch(string sDir)
        {

            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    FileInfo currentFile = new FileInfo(f);
                    if (currentFile.Length <= 10000000)
                    {
                        FilesLess10++;
                    }
                    else if ((currentFile.Length > 10000000) && (currentFile.Length <= 50000000))
                    {
                        FilesLess50More10++;
                    }
                    else if (currentFile.Length > 100000000)
                    {
                        FilesMore100++;
                    }

                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    DirSearch(d);
                }
            }
            catch
            {
            }


        }
        private string GetPrev(string uri)
        {
            if ((uri == null) || (uri.Length < 4))
            {
                return "";
            }
            else
            {
                string l = uri;
                int i = uri.Length - 1;
                while ((uri[i] != '\\'))
                {
                    i--;
                    if (i < 4)
                    {
                        i = 3;
                        break;
                    }
                }

                return l.Substring(0, i);
            }
        }
        private string GetName(string name, string path)
        {
            if (path.Length > 4)
                return name.Remove(0, path.Length + 1);
            else
                return name.Remove(0, path.Length);
        }
    }

}
