namespace PayCal.Logging
{
    public class LogStrings
    {
        // For Repositories
        public const string ID_NotFound = ("No Employees found with ID: ");

        // For API Controllers
        public const string defaultmsg = ("Request returned with HTTP Status code: ");
        public const string errormsg = ("An unexpected error has occured. ");
        public const string http200 = ("200 (Ok)");
        public const string http201 = ("201 (Created)");
        public const string http204 = ("204 (No Content)");
        public const string http400 = ("400 (Bad Request)");
        public const string http404 = ("404 (Not Found)");
        public const string context201 = ("A new employee entry as be successful created.");
        public const string context204 = ("Request was executed, no request body to return.");
        public const string context400 = ("Most likely a URI Syntax error or incorrect ID.");
        public const string context404 = ("Either the requested ID does not exist or the wrong ID was requested. ");
    }
}
