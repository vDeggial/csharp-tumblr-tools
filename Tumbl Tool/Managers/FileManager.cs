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

        public bool downloadFile(string url, string fullPath, string prefix = "", int method = 1)
        {
            url = FileHelper.fixURL(url);

            fullPath = FileHelper.getFullFilePath(url, fullPath, prefix);
            fullPath = FileHelper.fixFileName(fullPath);
            this.percentDownloaded = 0;
            this.statusCode = downloadStatusCodes.OK;

            if (WebHelper.urlExists(@url))
            {
                switch (method)
                {
                    case 1:
                        using (WebClient webClient = new WebClient())
                        {
                            try
                            {
                                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadCompleted);
                                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                                webClient.DownloadFileAsync(new Uri(@url), fullPath);

                                while (this.statusCode != downloadStatusCodes.Done && this.statusCode != downloadStatusCodes.UnableDownload)
                                {
                                }

                                if (this.percentDownloaded < 100 && this.statusCode == downloadStatusCodes.UnableDownload)
                                {
                                    webClient.CancelAsync();
                                    (new FileInfo(fullPath)).Delete(); //  delete partial file
                                    this.statusCode = downloadStatusCodes.UnableDownload;
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
                                (new FileInfo(fullPath)).Delete(); //  delete partial file
                                return false;
                            }
                        }
                    case 2:

                        int timeoutInSeconds = 10;
                        HttpWebRequest MyRequest = (HttpWebRequest)WebRequest.Create(url);
                        MyRequest.Proxy = null;
                        MyRequest.Timeout = timeoutInSeconds * 1000;
                        try
                        {
                            // Get the web response
                            using (HttpWebResponse MyResponse = (HttpWebResponse)MyRequest.GetResponse())
                            {
                                // Make sure the response is valid
                                if (HttpStatusCode.OK == MyResponse.StatusCode)
                                {
                                    // Open the response stream
                                    using (Stream MyResponseStream = MyResponse.GetResponseStream())
                                    {
                                        // Open the destination file
                                        using (FileStream MyFileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
                                        {
                                            // Create a 4K buffer to chunk the file
                                            byte[] MyBuffer = new byte[4096];
                                            int BytesRead;
                                            // Read the chunk of the web response into the buffer
                                            while (0 < (BytesRead = MyResponseStream.Read(MyBuffer, 0, MyBuffer.Length)))
                                            {
                                                // Write the chunk from the buffer to the file
                                                MyFileStream.Write(MyBuffer, 0, BytesRead);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception err)
                        {
                            string e = err.Message;
                            statusCode = downloadStatusCodes.UnableDownload;
                            // throw new Exception("Error saving file from URL:" + err.Message, err);
                            return false;
                        }
                        downloadedList.Add(fullPath);
                        return true;
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