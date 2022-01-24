namespace PayCal_API
{
    public class LogStrings
    {
        public string defaultmsg = ("Request returned with HTTP Status code: ");
        public string errormsg = ("An unexpected error has occured. ");
        public string http200 = ("200 (Ok)");
        public string http201 = ("201 (Created)");
        public string http204 = ("204 (No Content)");
        public string http400 = ("400 (Bad Request)");
        public string http404 = ("404 (Not Found)");
        public string context201 = ("A new employee entry as be successful created.");
        public string context204 = ("Request was executed, no request body to return.");
        public string context400 = ("Most likely a URI Syntax error or incorrect ID.");
        public string context404 = ("Either the requested ID does not exist or the wrong ID was requested. ");
    }
}
