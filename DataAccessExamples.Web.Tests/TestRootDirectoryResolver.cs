using System;
using Nancy;

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
