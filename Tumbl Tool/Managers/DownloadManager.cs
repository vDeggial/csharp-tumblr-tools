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
 *  Last Updated: September, 2015
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using Tumblr_Tool.Enums;
using Tumblr_Tool.Helpers;

namespace Tumblr_Tool.Managers
{
    public class DownloadManager
    {
        public DownloadManager()
        {
            DownloadedList = new HashSet<string>();
            TotalFileSize = 0;
            FileSizeRecieved = 0;
            SaveFileFormat = SaveFileFormats.JSON.ToString();
        }

        public HashSet<string> DownloadedList { get; set; }

        public double FileSizeRecieved { get; set; }

        public double PercentDownloaded { get; set; }

        public string SaveFileFormat { get; set; }

        public DownloadStatusCodes StatusCode { get; set; }

        public double TotalFileSize { get; set; }

        public int TotalFilesToDownload { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public bool DownloadFile(string url, string fullPath)
        {
            FileInfo file;

            url = WebHelper.RemoveTrailingBackslash(url);

            fullPath = FileHelper.GenerateLocalPathToFile(url, fullPath);
            fullPath = FileHelper.AddJpgExt(fullPath);
            this.PercentDownloaded = 0;
            this.StatusCode = DownloadStatusCodes.OK;

            if (WebHelper.UrlExists(@url))
            {
                using (MyWebClient webClient = new MyWebClient())
                {
                    try
                    {
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Wc_DownloadCompleted);
                        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Wc_DownloadProgressChanged);
                        webClient.DownloadFileAsync(new Uri(@url), fullPath);

                        while (this.StatusCode != DownloadStatusCodes.Done && this.StatusCode != DownloadStatusCodes.UnableDownload)
                        {
                            this.PercentDownloaded = this.PercentDownloaded;
                        }

                        if (this.StatusCode == DownloadStatusCodes.UnableDownload)
                        {
                            webClient.CancelAsync();

                            if (FileHelper.FileExists(fullPath))
                            {
                                file = new FileInfo(fullPath);

                                if (!FileHelper.IsFileLocked(file)) file.Delete();
                            }

                            return false;
                        }

                        if (this.StatusCode == DownloadStatusCodes.Done)
                        {
                            this.DownloadedList.Add(fullPath);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception exception)
                    {
                        string s = exception.Message;
                        this.StatusCode = DownloadStatusCodes.UnableDownload;
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

        public bool DownloadFilePostSharp(string url, string fullPath)
        {
            url = WebHelper.RemoveTrailingBackslash(url);

            fullPath = FileHelper.GenerateLocalPathToFile(url, fullPath);
            fullPath = FileHelper.AddJpgExt(fullPath);
            this.PercentDownloaded = 0;
            this.StatusCode = DownloadStatusCodes.OK;

            Uri uri = new Uri(url);
            string domain = uri.Host;
            string protocol = uri.Scheme + Uri.SchemeDelimiter;
            string path = uri.PathAndQuery;

            var client = new RestClient(string.Concat(protocol, domain));
            var request = new RestRequest(path, Method.GET);

            client.DownloadData(request).SaveAs(fullPath);
            this.PercentDownloaded = 100;
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Wc_DownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            this.PercentDownloaded = e.ProgressPercentage;

            if (e.ProgressPercentage >= 100)
                this.FileSizeRecieved = Convert.ToDouble(e.TotalBytesToReceive);
            else
                this.FileSizeRecieved = Convert.ToDouble(e.BytesReceived);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Wc_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                this.StatusCode = DownloadStatusCodes.UnableDownload;
                return;
            }

            if (e.Error != null)
            {
                this.StatusCode = DownloadStatusCodes.UnableDownload;
                return;
            }

            if (e.Cancelled == false && e.Error == null)
            {
                this.StatusCode = DownloadStatusCodes.Done;
                this.TotalFileSize += FileSizeRecieved;
                return;
            }
        }
    }
}