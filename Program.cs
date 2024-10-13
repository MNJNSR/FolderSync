using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

class FolderSync
{
    static string sourcePath;
    static string replicaPath;
    static string logFilePath;
    static int syncInterval; // in seconds

    static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Usage: FolderSync <source> <replica> <syncIntervalInSeconds> <logFilePath>");
            return;
        }

        sourcePath = args[0];
        replicaPath = args[1];
        syncInterval = int.Parse(args[2]);
        logFilePath = args[3];

        Log($"Starting folder synchronization: {DateTime.Now}");

        // Periodic synchronization
        Timer timer = new Timer(SyncFolders, null, 0, syncInterval * 1000);
        
        // Keep the application running
        Console.ReadLine();
    }

    static void SyncFolders(object state)
    {
        try
        {
            Log($"Synchronization started at {DateTime.Now}");

            // Sync from source to replica
            SyncSourceToReplica();

            // Remove files from replica that are no longer in source
            RemoveStaleFilesFromReplica();

            Log($"Synchronization completed at {DateTime.Now}");
        }
        catch (Exception ex)
        {
            Log($"Error during synchronization: {ex.Message}");
        }
    }

    static void SyncSourceToReplica()
    {
        foreach (string sourceFilePath in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
        {
            string relativePath = Path.GetRelativePath(sourcePath, sourceFilePath);
            string replicaFilePath = Path.Combine(replicaPath, relativePath);

            // Create directory if not exists
            string replicaDir = Path.GetDirectoryName(replicaFilePath);
            if (!Directory.Exists(replicaDir))
            {
                Directory.CreateDirectory(replicaDir);
                Log($"Created directory: {replicaDir}");
            }

            // Copy file if it doesn't exist or if it's different
            if (!File.Exists(replicaFilePath) || !FilesAreEqual(sourceFilePath, replicaFilePath))
            {
                File.Copy(sourceFilePath, replicaFilePath, true);
                Log($"Copied file: {sourceFilePath} to {replicaFilePath}");
            }
        }
    }

    static void RemoveStaleFilesFromReplica()
    {
        foreach (string replicaFilePath in Directory.GetFiles(replicaPath, "*", SearchOption.AllDirectories))
        {
            string relativePath = Path.GetRelativePath(replicaPath, replicaFilePath);
            string sourceFilePath = Path.Combine(sourcePath, relativePath);

            // If file exists in replica but not in source, delete it
            if (!File.Exists(sourceFilePath))
            {
                File.Delete(replicaFilePath);
                Log($"Deleted file: {replicaFilePath}");
            }
        }
    }

    static bool FilesAreEqual(string file1, string file2)
    {
        using (var md5 = MD5.Create())
        {
            using (var stream1 = File.OpenRead(file1))
            using (var stream2 = File.OpenRead(file2))
            {
                return md5.ComputeHash(stream1).SequenceEqual(md5.ComputeHash(stream2));
            }
        }
    }

    static void Log(string message)
    {
        Console.WriteLine(message);
        File.AppendAllText(logFilePath, message + Environment.NewLine);
    }
}
