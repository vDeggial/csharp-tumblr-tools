/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: December, 2014
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
    public class FileManager
    {
        public FileManager()
        {
            downloadedList = new HashSet<string>();
            totalSize = 0;
            fileSizeRecieved = 0;
            saveFileFormat = "JSON";
        }

        public HashSet<string> downloadedList { get; set; }

        public double fileSizeRecieved { get; set; }

        public double percentDownloaded { get; set; }

        public downloadStatusCodes statusCode { get; set; }

        public double totalSize { get; set; }

        public int totalToDownload { get; set; }

        public string saveFileFormat { get; set; }

        public bool downloadFile(string url, string fullPath)
        {
            FileInfo file;

            url = FileHelper.fixURL(url);

            fullPath = FileHelper.getFullFilePath(url, fullPath);
            fullPath = FileHelper.fixFileName(fullPath);
            this.percentDownloaded = 0;
            this.statusCode = downloadStatusCodes.OK;

            if (WebHelper.urlExists(@url))
            {
                using (MyWebClient webClient = new MyWebClient())
                {
                    try
                    {
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadCompleted);
                        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                        webClient.DownloadFileAsync(new Uri(@url), fullPath);

                        while (this.statusCode != downloadStatusCodes.Done && this.statusCode != downloadStatusCodes.UnableDownload)
                        {
                        }

                        if (this.statusCode == downloadStatusCodes.UnableDownload)
                        {
                            webClient.CancelAsync();

                            if (FileHelper.fileExists(fullPath))
                            {
                                file = new FileInfo(fullPath);

                                if (!FileHelper.IsFileLocked(file)) file.Delete();
                            }

                            return false;
                        }

                        if (this.statusCode == downloadStatusCodes.Done)
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
                        this.statusCode = downloadStatusCodes.UnableDownload;
                        if (FileHelper.fileExists(fullPath))
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

        public SaveFile readTumblrFile(string location)
        {
            SaveFile saveFile = FileHelper.readTumblrFile(location, "BIN");

            if (saveFile == null)
                saveFile = FileHelper.readTumblrFile(location, "XML");

            if (saveFile == null)
                saveFile = FileHelper.readTumblrFile(location, "JSON");

            return saveFile;
        }

        public bool saveTumblrFile(string location, SaveFile saveFile)
        {
            return FileHelper.saveTumblrFile(location, saveFile, saveFileFormat);
        }

        public void wc_DownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            this.percentDownloaded = e.ProgressPercentage;

            if (e.ProgressPercentage >= 100)
                this.fileSizeRecieved = Convert.ToDouble(e.TotalBytesToReceive);
            else
                this.fileSizeRecieved = Convert.ToDouble(e.BytesReceived);
        }

        private void wc_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                this.statusCode = downloadStatusCodes.UnableDownload;
                return;
            }

            if (e.Error != null)
            {
                this.statusCode = downloadStatusCodes.UnableDownload;
                return;
            }

            if (e.Cancelled == false && e.Error == null)
            {
                this.statusCode = downloadStatusCodes.Done;
                this.totalSize += fileSizeRecieved;
                return;
            }
        }
    }
}