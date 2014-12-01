using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
            downloadedList = new List<string>();
            totalSize = 0;
            fileSizeRecieved = 0;

        }

        public List<string> downloadedList { get; set; }

        public double fileSizeRecieved { get; set; }

        public double percentDownloaded { get; set; }

        public downloadStatusCodes statusCode { get; set; }

        public double totalSize { get; set; }

        public int totalToDownload { get; set; }


        public bool downloadFile(string url, string fullPath, string prefix = "", int method = 1)
        {
            url = FileHelper.fixURL(url);

            fullPath = FileHelper.getFullFilePath(url, fullPath, prefix);
            fullPath = FileHelper.fixFileName(fullPath);
            percentDownloaded = 0;
            statusCode = downloadStatusCodes.OK;
            Stopwatch _timer = new Stopwatch();

            switch (method)
            {
                case 1:
                    using (WebClient webClient = new WebClient())
                    {
                        try
                        {
                            _timer.Reset();
                            _timer.Start();
                            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                            webClient.DownloadFileAsync(new Uri(@url), fullPath);

                            while (statusCode != downloadStatusCodes.Done && statusCode != downloadStatusCodes.UnableDownload)
                            {
                                
                            }

                            if (percentDownloaded < 100 && statusCode == downloadStatusCodes.UnableDownload)
                            {
                                webClient.CancelAsync();
                                (new FileInfo(fullPath)).Delete(); //  delete partial file
                                statusCode = downloadStatusCodes.UnableDownload;
                                return false;
                            }

                            if (statusCode == downloadStatusCodes.Done)
                            {
                                downloadedList.Add(fullPath);
                                return true;
                            }

                            else
                            {
                                return false;
                            }
                        }
                        catch (Exception)
                        {
                            statusCode = downloadStatusCodes.UnableDownload;
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

            return false;
        }

        public downloadStatusCodes getDownloadStatusCode()
        {
            return statusCode;
        }

        public SaveFile readTumblrFile(string location)
        {
            return FileHelper.readTumblrFile(location, "XML");
        }

        public bool saveTumblrFile(string location, SaveFile saveFile)
        {
            return FileHelper.saveTumblrFile(location, saveFile, "XML");
        }

        public void wc_DownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            percentDownloaded = e.ProgressPercentage;

            if (e.ProgressPercentage >= 100)
                fileSizeRecieved = Convert.ToDouble(e.TotalBytesToReceive);
            else
                fileSizeRecieved = Convert.ToDouble(e.BytesReceived);
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                statusCode = downloadStatusCodes.UnableDownload;
                return;
            }

            if (e.Error != null)
            {
                statusCode = downloadStatusCodes.UnableDownload;
                return;
            }

            if (e.Cancelled == false && e.Error == null)
            {
                statusCode = downloadStatusCodes.Done;
                totalSize += fileSizeRecieved;
                return;
            }
        }
    }
}