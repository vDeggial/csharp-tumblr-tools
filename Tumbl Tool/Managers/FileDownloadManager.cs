/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Class: DownloadManager
 *
 *  Description: Class provides functionality for downloading file from Internet
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: January, 2018
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Helpers;

namespace Tumblr_Tool.Managers
{
    public class FileDownloadManager : INotifyPropertyChanged
    {
        private readonly AutoResetEvent _readyToStop = new AutoResetEvent(false);

        private double downloadedFilesSize;

        private HashSet<string> downloadedList;

        private DownloadStatusCode downloadStatusCode;

        private double fileSizeRecieved;

        private int numberOfFilesDownloaded;

        private double percentDownloaded;

        /// <summary>
        /// 
        /// </summary>
        public FileDownloadManager()
        {
            downloadedList = new HashSet<string>();
            downloadedFilesSize = 0;
            fileSizeRecieved = 0;
            numberOfFilesDownloaded = 0;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public double DownloadedFilesSize
        {
            get
            {
                return downloadedFilesSize;
            }
            set
            {
                if (value != downloadedFilesSize)
                {
                    downloadedFilesSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public HashSet<string> DownloadedList
        { get
            {
                return downloadedList;
            }
            set
            {
                downloadedList = value;
            }
        }
        public DownloadStatusCode DownloadStatusCode
        { get
            {
                return downloadStatusCode;
            }
            set
            {
                if (value != downloadStatusCode)
                {
                    downloadStatusCode = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int NumberOfFilesDownloaded
        {
            get
            {
                return numberOfFilesDownloaded;
            }
            set
            {
                if (value != numberOfFilesDownloaded)
                {
                    numberOfFilesDownloaded = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int NumberOfFilesToDownload { get; set; }

        public double PercentDownloaded
        { get
            {
                return percentDownloaded;
            }
            set
            {
                if (value != percentDownloaded)
                {
                    percentDownloaded = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double FileSizeRecieved
        { get
            {
                return fileSizeRecieved;
            }
            set
            {
                if (value != fileSizeRecieved)
                {
                    fileSizeRecieved = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="remoteFilePath"></param>
        /// <param name="localPath"></param>
        /// <returns></returns>
        public bool DownloadFile(DownloadMethod method, string remoteFilePath, string localPath)
        {
            try
            {
                remoteFilePath = WebHelper.RemoveTrailingBackslash(remoteFilePath);

                var localFileFullPath = FileHelper.AddFileExtension(FileHelper.GenerateLocalPathToFile(remoteFilePath, localPath), "jpg");
                switch (method)
                {
                    case DownloadMethod.WebClientAsync:
                        return DownloadFileWebClientAsync(remoteFilePath, localFileFullPath);

                    case DownloadMethod.RestSharp:
                        return DownloadFileRestSharp(remoteFilePath, localFileFullPath);

                    case DownloadMethod.WebClient:
                        return DownloadFileWebClient(remoteFilePath, localFileFullPath);

                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteFilePath"></param>
        /// <param name="localFilePath"></param>
        /// <returns></returns>
        private bool DownloadFileRestSharp(string remoteFilePath, string localFilePath)
        {
            try
            {
                PercentDownloaded = 0;
                DownloadStatusCode = DownloadStatusCode.Ok;

                Uri uri = new Uri(remoteFilePath);
                string domain = uri.Host;
                string protocol = uri.Scheme + Uri.SchemeDelimiter;
                string path = uri.PathAndQuery;

                var client = new RestClient(new StringBuilder(protocol).Append(domain).ToString());
                var request = new RestRequest(path, Method.GET);

                client.DownloadData(request).SaveAs(localFilePath);
                PercentDownloaded = 100;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteFilePath"></param>
        /// <param name="localFilePath"></param>
        /// <returns></returns>
        private bool DownloadFileWebClient(string remoteFilePath, string localFilePath)
        {
            try
            {
                PercentDownloaded = 0;
                DownloadStatusCode = DownloadStatusCode.Ok;

                if (WebHelper.UrlExists(remoteFilePath))
                {
                    string outputFolder = Path.GetDirectoryName(localFilePath);
                    if (outputFolder != null && !Directory.Exists(outputFolder))
                        Directory.CreateDirectory(outputFolder);

                    MyWebClient webClient = new MyWebClient { Proxy = null };

                    using (Stream webStream = webClient.OpenRead(remoteFilePath))
                    using (FileStream fileStream = new FileStream(localFilePath, FileMode.Create))
                    {
                        var buffer = new byte[32768];
                        int bytesRead;
                        Int64 bytesReadComplete = 0;  // Use Int64 for files larger than 2 gb

                        // Get the size of the file to download
                        Int64 bytesTotal = Convert.ToInt64(webClient.ResponseHeaders["Content-Length"]);

                        // Start a new StartWatch for measuring download time
                        Stopwatch sw = Stopwatch.StartNew();

                        // Download file in chunks
                        while (webStream != null && (bytesRead = webStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bytesReadComplete += bytesRead;
                            fileStream.Write(buffer, 0, bytesRead);

                            // Output current progress to the "Output" editor
                            //StringBuilder sb = new StringBuilder();
                            // ReSharper disable once PossibleLossOfFraction
                            PercentDownloaded = bytesReadComplete * 100 / bytesTotal;
                            FileSizeRecieved = bytesReadComplete;
                            //sb.AppendLine($"Time Elapsed: {sw.ElapsedMilliseconds:0,.00}s");
                            //sb.AppendLine(
                            //    $"Average Speed: {(sw.ElapsedMilliseconds > 0 ? bytesReadComplete/sw.ElapsedMilliseconds/1.024 : 0):0,0} KB/s");
                        }

                        sw.Stop();
                        DownloadedFilesSize += FileSizeRecieved;
                        DownloadStatusCode = DownloadStatusCode.Done;
                        return true;
                    }
                }
                DownloadStatusCode = DownloadStatusCode.UnableDownload;
                return false;
            }
            catch
            {
                DownloadStatusCode = DownloadStatusCode.UnableDownload;
                if (FileHelper.FileExists(localFilePath))
                {
                    FileInfo file = new FileInfo(localFilePath);

                    if (!FileHelper.IsFileLocked(file)) file.Delete();
                }
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="remoteFilePath"></param>
        /// <param name="localFilePath"></param>
        /// <returns></returns>
        private bool DownloadFileWebClientAsync(string remoteFilePath, string localFilePath)
        {
            try
            {
                PercentDownloaded = 0;
                DownloadStatusCode = DownloadStatusCode.Ok;

                if (WebHelper.UrlExists(remoteFilePath))
                {
                    using (MyWebClient webClient = new MyWebClient())
                    {
                        try
                        {
                            webClient.Proxy = null;
                            webClient.DownloadFileCompleted += Wc_DownloadCompleted;
                            webClient.DownloadProgressChanged += Wc_DownloadProgressChanged;
                            webClient.DownloadFileAsync(new Uri(remoteFilePath), localFilePath, localFilePath);

                            _readyToStop.WaitOne();

                            return DownloadStatusCode == DownloadStatusCode.Done;
                        }
                        catch (Exception)
                        {
                            DownloadStatusCode = DownloadStatusCode.UnableDownload;
                            if (FileHelper.FileExists(localFilePath))
                            {
                                var file = new FileInfo(localFilePath);

                                if (!FileHelper.IsFileLocked(file)) file.Delete();
                            }
                            return false;
                        }
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Wc_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    DownloadStatusCode = DownloadStatusCode.UnableDownload;
                    if (FileHelper.FileExists((string)e.UserState))
                    {
                        FileInfo file = new FileInfo((string)e.UserState);

                        if (!FileHelper.IsFileLocked(file)) file.Delete();
                    }
                    _readyToStop.Set();
                    return;
                }

                if (e.Error != null)
                {
                    DownloadStatusCode = DownloadStatusCode.UnableDownload;
                    if (FileHelper.FileExists((string)e.UserState))
                    {
                        FileInfo file = new FileInfo((string)e.UserState);

                        if (!FileHelper.IsFileLocked(file)) file.Delete();
                    }
                    _readyToStop.Set();
                    return;
                }

                if (e.Cancelled == false && e.Error == null)
                {
                    DownloadStatusCode = DownloadStatusCode.Done;
                    this.DownloadedList.Add((string)e.UserState);
                    NumberOfFilesDownloaded++;
                    DownloadedFilesSize += FileSizeRecieved;
                    _readyToStop.Set();
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Wc_DownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                PercentDownloaded = e.ProgressPercentage;

                FileSizeRecieved = Convert.ToDouble(e.ProgressPercentage >= 100 ? e.TotalBytesToReceive : e.BytesReceived);
            }
            catch
            {
                return;
            }
        }
    }
}
