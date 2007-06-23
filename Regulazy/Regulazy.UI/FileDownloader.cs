// Originally written by John Batte
// Modifications, API changes and cleanups by Phil Crosby

using System;
using System.IO;
using System.Net;

namespace Regulazy.UI
{
    public class FileDownloader
    {
        // Block size to download is by default 1K.
        private const int downloadBlockSize = 1024;

        // Determines whether the user has canceled or not.
        private bool canceled = false;

        private string downloadingTo;

        /// <summary>
        /// This is the name of the file we get back from the server when we
        /// try to download the provided url. It will only contain a non-null
        /// string when we've successfully contacted the server and it has started
        /// sending us a file.
        /// </summary>
        public string DownloadingTo
        {
            get { return downloadingTo; }
        }

        public void Cancel()
        {
            this.canceled = true;
        }

        /// <summary>
        /// Progress update
        /// </summary>
        public event DownloadProgressHandler ProgressChanged;
        
        /// <summary>
        /// Fired when progress reaches 100%.
        /// </summary>
        public event EventHandler DownloadComplete;

        private void OnDownloadComplete()
        {
            if (this.DownloadComplete != null)
                this.DownloadComplete(this, new EventArgs());
        }

        /// <summary>
        /// Begin downloading the file at the specified url, and save it to the current folder.
        /// </summary>
        public void Download(string url)
        {
            Download(url, "");
        }
        /// <summary>
        /// Begin downloading the file at the specified url, and save it to the given folder.
        /// </summary>
        public void Download(string url, string destFolder)
        {
            DownloadData data = null;
            this.canceled = false;

            try
            {
                // get download details
                data = DownloadData.Create(url, destFolder);
                // Find out the name of the file that the web server gave us.
                string destFileName = Path.GetFileName(data.Response.ResponseUri.ToString());
                this.downloadingTo = Path.GetFullPath(Path.Combine(destFolder, destFileName));

                // Create the file on disk here, so even if we don't receive any data of the file
                // it's still on disk. This allows us to download 0-byte files.
                if (!File.Exists(downloadingTo))
                {
                    FileStream fs = File.Create(downloadingTo);
                    fs.Close();
                }

                // create the download buffer
                byte[] buffer = new byte[downloadBlockSize];

                int readCount;

                // update how many bytes have already been read
                long totalDownloaded = data.StartPoint;

                bool gotCanceled = false;

                while ((int)(readCount = data.DownloadStream.Read(buffer, 0, downloadBlockSize)) > 0)
                {
                    // break on cancel
                    if (canceled)
                    {
                        gotCanceled = true;
                        data.Close();
                        break;
                    }

                    // update total bytes read
                    totalDownloaded += readCount;

                    // save block to end of file
                    SaveToFile(buffer, readCount, this.downloadingTo);

                    // send progress info
                    if (data.IsProgressKnown)
                        RaiseProgressChanged(totalDownloaded, data.FileSize);

                    // break on cancel
                    if (canceled)
                    {
                        gotCanceled = true;
                        data.Close();
                        break;
                    }
                }

                if (!gotCanceled)
                    OnDownloadComplete();
            }
            catch (UriFormatException e)
            {
                throw new ArgumentException(
                    String.Format("Could not parse the URL \"{0}\" - it's either malformed or is an unknown protocol.", url), e);
            }
            finally
            {
                if (data != null)
                    data.Close();
            }
        }
        /// <summary>
        /// Download a file from the url asynchronously.
        /// </summary>
        public void AsyncDownload(string url)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(
                new System.Threading.WaitCallback(this.WaitCallbackMethod), new string[] { url, "" });
        }
        /// <summary>
        /// Download a file from the url to the destination folder asynchronously.
        /// </summary>
        public void AsyncDownload(string url, string destFolder)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(
                new System.Threading.WaitCallback(this.WaitCallbackMethod), new string[] { url, destFolder });
        }
        /// <summary>
        /// A WaitCallback used by the AsyncDownload methods.
        /// </summary>
        private void WaitCallbackMethod(object data){
            // Expecting an array of two strings (url and dest folder)
            String[] strings = data as String[];
            this.Download(strings[0], strings[1]);
        }
        private void SaveToFile(byte[] buffer, int count, string fileName)
        {
            FileStream f = null;

            try
            {
                f = File.Open(fileName, FileMode.Append, FileAccess.Write);
                f.Write(buffer, 0, count);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(
                    String.Format("Error trying to save file \"{0}\": {1}", fileName, e.Message), e);
            }
            finally
            {
                if (f != null)
                    f.Close();
            }
        }
        private void RaiseProgressChanged(long current, long target)
        {
            if (this.ProgressChanged != null)
                this.ProgressChanged(this, new DownloadEventArgs(target, current));
        }
    }

    public class DownloadData
    {
        private WebResponse response;

        private Stream stream;
        private long size;
        private long start;

        public static DownloadData Create(string url, string destFolder)
        {
            // This is what we will return
            DownloadData downloadData = new DownloadData();

            long urlSize = GetFileSize(url);
            downloadData.size = urlSize;

            WebRequest req = GetRequest(url);
            try
            {
                downloadData.response = (WebResponse)req.GetResponse();
            }
            catch (Exception e)
            {
                throw new ArgumentException(String.Format(
                    "Error downloading \"{0}\": {1}", url, e.Message), e);
            }

            // Check to make sure the response isn't an error. If it is this method
            // will throw exceptions.
            ValidateResponse(downloadData.response, url);

            // Take the name of the file given to use from the web server.
            String fileName = System.IO.Path.GetFileName(downloadData.response.ResponseUri.ToString());

            String downloadTo = Path.Combine(destFolder, fileName);

            // If we don't know how big the file is supposed to be,
            // we can't resume, so delete what we already have if something is on disk already.
            if (!downloadData.IsProgressKnown && File.Exists(downloadTo))
                File.Delete(downloadTo);

            if (downloadData.IsProgressKnown && File.Exists(downloadTo))
            {
                // We only support resuming on http requests
                if (! (downloadData.Response is HttpWebResponse))
                {
                    File.Delete(downloadTo);
                }
                else
                {
                    // Try and start where the file on disk left off
                    downloadData.start = new FileInfo(downloadTo).Length;

                    // If we have a file that's bigger than what is online, then something 
                    // strange happened. Delete it and start again.
                    if (downloadData.start > urlSize)
                        File.Delete(downloadTo);
                    else if (downloadData.start < urlSize)
                    {
                        // Try and resume by creating a new request with a new start position
                        downloadData.response.Close();
                        req = GetRequest(url);
                        ((HttpWebRequest)req).AddRange((int)downloadData.start);
                        downloadData.response = req.GetResponse();
                        
                        if (((HttpWebResponse)downloadData.Response).StatusCode != HttpStatusCode.PartialContent)
                        {
                            // They didn't support our resume request. 
                            File.Delete(downloadTo);
                            downloadData.start = 0;
                        }
                    }
                }
            }
            return downloadData;
        }

        // Used by the factory method
        private DownloadData()
        {
        }

        private DownloadData(WebResponse response, long size, long start)
        {
            this.response = response;
            this.size = size;
            this.start = start;
            this.stream = null;
        }

        /// <summary>
        /// Checks whether a WebResponse is an error.
        /// </summary>
        /// <param name="response"></param>
        private static void ValidateResponse(WebResponse response, string url)
        {
            if (response is HttpWebResponse)
            {
                HttpWebResponse httpResponse = (HttpWebResponse)response;
                // If it's an HTML page, it's probably an error page. Comment this
                // out to enable downloading of HTML pages.
                if (httpResponse.ContentType.Contains("text/html") || httpResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ArgumentException(
                        String.Format("Could not download \"{0}\" - a web page was returned from the web server.",
                        url));
                }
            }
            else if (response is FtpWebResponse)
            {
                FtpWebResponse ftpResponse = (FtpWebResponse)response;
                if (ftpResponse.StatusCode == FtpStatusCode.ConnectionClosed)
                    throw new ArgumentException(
                        String.Format("Could not download \"{0}\" - FTP server closed the connection.", url));
            }
            // FileWebResponse doesn't have a status code to check.
        }

        /// <summary>
        /// Checks the file size of a remote file. If size is -1, then the file size
        /// could not be determined.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="progressKnown"></param>
        /// <returns></returns>
        private static long GetFileSize(string url)
        {
            WebResponse response = null;
            long size = -1;
            try
            {
                response = GetRequest(url).GetResponse();
                size = response.ContentLength;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }

            return size;
        }

        private static WebRequest GetRequest(string url)
        {
            WebRequest request = WebRequest.Create(url);
            if (request is HttpWebRequest)
                request.Credentials = CredentialCache.DefaultCredentials;
            return request;
        }

        public void Close()
        {
            this.response.Close();
        }

        #region Properties
        public WebResponse Response
        {
            get { return response; }
            set { response = value; }
        }
        public Stream DownloadStream
        {
            get
            {
                if (this.start == this.size)
                    return Stream.Null;
                if (this.stream == null)
                    this.stream = this.response.GetResponseStream();
                return this.stream;
            }
        }        
        public long FileSize
        {
            get
            {
                return this.size;
            }
        }
        public long StartPoint
        {
            get
            {
                return this.start;
            }
        }
        public bool IsProgressKnown
        {
            get
            {
                // If the size of the remote url is -1, that means we
                // couldn't determine it, and so we don't know
                // progress information.
                return this.size>-1;
            }
        }
        #endregion        
    }

    /// <summary>
    /// Progress of a file that's downloading.
    /// </summary>
    public class DownloadEventArgs : EventArgs
    {
        private int percentDone;
        private string downloadState;
        private long totalFileSize;

        public long TotalFileSize
        {
            get { return totalFileSize; }
            set { totalFileSize = value; }
        }
        private long currentFileSize;

        public long CurrentFileSize
        {
            get { return currentFileSize; }
            set { currentFileSize = value; }
        }

        public DownloadEventArgs(long totalFileSize, long currentFileSize)
        {
            this.totalFileSize = totalFileSize;
            this.currentFileSize = currentFileSize;

            this.percentDone = (int)((((double)currentFileSize) / totalFileSize) * 100);
        }

        public DownloadEventArgs(string state)
        {
            this.downloadState = state;
        }

        public DownloadEventArgs(int percentDone, string state)
        {
            this.percentDone = percentDone;
            this.downloadState = state;
        }

        public int PercentDone
        {
            get
            {
                return this.percentDone;
            }
        }

        public string DownloadState
        {
            get
            {
                return this.downloadState;
            }
        }
    }
    public delegate void DownloadProgressHandler(object sender, DownloadEventArgs e);
}