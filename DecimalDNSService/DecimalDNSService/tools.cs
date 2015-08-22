namespace DecimalDNSService
{
    class tools
    {
        // gloval variables to be used across several .cs

        // hash - comes from settings.xml and it is used
        // to POST to server for IP update on server
        // this is the "secret" to update the client IP
        public static string hash = "";

        // logtofile - use 1 to log to file, other string won't log
        public static string logtofile = "1";

        // logtoEV - use 1 to log to eventviewer, other string won't log
        public static string logtoEV = "1";

        // updateinterval - miliseconds interval for the client to
        // update the IP on the server
        // default is 5 minutes (5 times 60 times 1000)
        // but updateinterval is read from settings.xml too
        public static int updateinterval = 5 * 60 * 1000;
    }
}
