using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCMS.ModelSector;


namespace FleetOps.Models
{
    public class FileManager
    {
        public string StorageRoot
        {
            private get;
            set;
        }
        DirectoryInfo userDirs;
        private List<FileInfo> files;
        private List<DirectoryInfo> directories;


        public FileManager(string path)
        {
            this.StorageRoot = path;
            userDirs = new DirectoryInfo(this.StorageRoot);
            files = new List<FileInfo>();
            directories = new List<DirectoryInfo>();
        }

        public List<DirHierarchy> GetAllFiles(string pattern)
        {
            return new List<DirHierarchy>(){
            new DirHierarchy{  isFolder=true, key="fddf", title="rgr"}
            };


        }
        public List<fileManagerFiles> GetFiles(string path) {
            var location = this.StorageRoot + path;
            var list = new List<fileManagerFiles>();
            DirectoryInfo info = new DirectoryInfo(location);
           foreach (var file in info.GetFiles())
            {
                list.Add(new fileManagerFiles { FileName = file.Name, Extension = file.Extension, CreatedDate = file.CreationTime.ToShortDateString(), LastModified=file.LastWriteTime.ToShortDateString(), Size=(file.Length/1024).ToString()+" kb" });
            }

            return list;
        }
        public bool hasDirectories(string path) {

            var info = new DirectoryInfo(path);
            if (info.GetDirectories().Any())
                return true;
            else
                return false;
        }
        public List<DirHierarchy> GetDirectories()
        {

            var dirs = new List<DirHierarchy>();
            try
            {
                //foreach (var dir in userDirs.GetDirectories())
                //{
                //        dirs.Add(new DirHierarchy { title = dir.Name, key = dir.Name, isFolder = true, children = this.GetDirectories(dir.FullName) });
                       
                //}
              dirs.Insert(0,new DirHierarchy{ title=userDirs.Name,key=userDirs.Name,isFolder=true , children = this.GetDirectories(userDirs.FullName) });
                return dirs;
            }
            catch (Exception)
            {
                return dirs;
            }
        }
        public List<DirHierarchy> GetDirectories(string path)
        {
            var info = new DirectoryInfo(path);
            var dirs = new List<DirHierarchy>();
            try
            {
                foreach (var dir in info.GetDirectories())
                {
                 
                        dirs.Add(new DirHierarchy { title = dir.Name, key = dir.Name, isFolder = true, children = this.GetDirectories(dir.FullName) });
                    
                }
                return dirs;
            }
            catch (Exception)
            {

                throw;

            }
        
        }









    }
}