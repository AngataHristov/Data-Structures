namespace TraverseAndSaveDirectoryContentsTree
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class EntryPoint
    {
        private const string StartDirectory = @"D:\Работата";
        private static IDictionary<string, Folder> folders;

        public static void Main()
        {
            folders = new Dictionary<string, Folder>();
            TraverseFolders();
            var sampleFolder = GetFolderByPath(@"D:\DOWNLOADS\GENERAL");
            Console.WriteLine($"Total size of {sampleFolder.Name} folder is: {{{sampleFolder.Size}}} bytes.");
        }

        private static void TraverseFolders(string startDirectory = StartDirectory)
        {
            var folderProcessor = new Queue<string>();
            folderProcessor.Enqueue(startDirectory);

            while (folderProcessor.Count > 0)
            {
                var currentFolderPath = folderProcessor.Dequeue();
                var currentFolder = GetFolderByPath(currentFolderPath);

                var dirInfo = new DirectoryInfo(currentFolderPath);

                var currentFiles = dirInfo.GetFiles();

                foreach (FileInfo currentFile in currentFiles)
                {
                    var file = new File(currentFile.Name, currentFile.Length);
                }

                var currentDirs = dirInfo.GetDirectories();

                foreach (DirectoryInfo directoryInfo in currentDirs)
                {
                    var folder = new Folder(directoryInfo.FullName);
                    folders[directoryInfo.FullName] = folder;
                    currentFolder.Folders.Add(folder);

                    folderProcessor.Enqueue(directoryInfo.FullName);
                }
            }
        }

        private static Folder GetFolderByPath(string folderPath)
        {
            if (!folders.ContainsKey(folderPath))
            {
                folders[folderPath] = new Folder(folderPath);
            }

            return folders[folderPath];
        }
    }
}
