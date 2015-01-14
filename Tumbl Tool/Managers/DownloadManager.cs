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
 *  Last Updated: January, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using Tumblr_Tool.Common_Helpers;
using Tumblr_Tool.Enums;

namespace Tumblr_Tool.Managers
{
    public class DownloadManager
    {
        public DownloadManager()
        {
            downloadedList = new HashSet<string>();
            totalSize = 0;
            fileSizeRecieved = 0;
            saveFileFormat = "JSON";
        }

        public HashSet<string> downloadedList { get; set; }

        public double fileSizeRecieved { get; set; }

        public double percentDownloaded { get; set; }

        public string saveFileFormat { get; set; }

        public DownloadStatusCodes statusCode { get; set; }

        public double totalSize { get; set; }

        public int totalToDownload { get; set; }

        public bool DownloadFile(string url, string fullPath)
        {
            FileInfo file;

            url = WebHelper.FixURL(url);

            fullPath = FileHelper.GetFullFilePath(url, fullPath);
            fullPath = FileHelper.FixFileName(fullPath);
            this.percentDownloaded = 0;
            this.statusCode = DownloadStatusCodes.OK;

            if (WebHelper.UrlExists(@url))
            {
                using (MyWebClient webClient = new MyWebClient())
                {
                    try
                    {
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Wc_DownloadCompleted);
                        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Wc_DownloadProgressChanged);
                        webClient.DownloadFileAsync(new Uri(@url), fullPath);

                        while (this.statusCode != DownloadStatusCodes.Done && this.statusCode != DownloadStatusCodes.UnableDownload)
                        {
                        }

                        if (this.statusCode == DownloadStatusCodes.UnableDownload)
                        {
                            webClient.CancelAsync();

                            if (FileHelper.FileExists(fullPath))
                            {
                                file = new FileInfo(fullPath);

                                if (!FileHelper.IsFileLocked(file)) file.Delete();
                            }

                            return false;
                        }

                        if (this.statusCode == DownloadStatusCodes.Done)
                        {
                            this.downloadedList.Add(fullPath);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        this.statusCode = DownloadStatusCodes.UnableDownload;
                        if (FileHelper.FileExists(fullPath))
                        {
                            file = new FileInfo(fullPath);

                            if (!FileHelper.IsFileLocked(file)) file.Delete();
                        }
                        return false;
                    }
                }
            }

            return false;
        }

        public void Wc_DownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            this.percentDownloaded = e.ProgressPercentage;

            if (e.ProgressPercentage >= 100)
                this.fileSizeRecieved = Convert.ToDouble(e.TotalBytesToReceive);
            else
                this.fileSizeRecieved = Convert.ToDouble(e.BytesReceived);
        }

        private void Wc_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                this.statusCode = DownloadStatusCodes.UnableDownload;
                return;
            }

            if (e.Error != null)
            {
                this.statusCode = DownloadStatusCodes.UnableDownload;
                return;
            }

            if (e.Cancelled == false && e.Error == null)
            {
                this.statusCode = DownloadStatusCodes.Done;
                this.totalSize += fileSizeRecieved;
                return;
            }
        }
    }
}