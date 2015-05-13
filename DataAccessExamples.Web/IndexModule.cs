namespace DataAccessExamples.Web
{
    using Nancy;

    /// <summary>
    ///   Landing page for the web interface
    /// </summary>
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return View["Home"];
            };
        }
    }
}