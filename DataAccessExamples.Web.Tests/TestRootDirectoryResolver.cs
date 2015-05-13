using Nancy;
using System;

namespace DataAccessExamples.Web.Tests
{
    public class TestRootDirectoryResolver : IRootPathProvider
    {
        public string GetRootPath()
        {
            return Environment.CurrentDirectory;
        }
    }
}
