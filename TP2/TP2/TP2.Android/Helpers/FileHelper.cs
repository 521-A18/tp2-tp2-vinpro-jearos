using System;
using TP2.Droid.Helpers;
using Xamarin.Forms;
using TP2.Helpers;
using System.IO;


[assembly: Dependency(typeof(FileHelper))]
namespace TP2.Droid.Helpers
{
    class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}