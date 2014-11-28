namespace Tumblr_Tool.Enums
{
    public enum processingCodes
    {
        OK,
        Initializing,
        checkingConnection,
        connectionOK,
        connectionError,
        gettingBlogInfo,
        blogInfoOK,
        blogInfoError,
        invalidURL,
        UnableDownload,
        Started,
        Crawling,
        Parsing,
        errorProcessing,
        saveFileOK,
        saveFileError,
        Done,
        Error
    }
}