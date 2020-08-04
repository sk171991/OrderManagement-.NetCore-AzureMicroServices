namespace LoggingManagement
{
    public class Logger : ILogger
    {
        public void LogMessage(string logmessage) 
        {

            //TelemetryConfiguration configuration = TelemetryConfiguration.CreateDefault();
            //configuration.InstrumentationKey = "eed17b7b-10a3-458c-9454-aebdbc93d61d";
            //var telemetryClient = new TelemetryClient(configuration);
            //telemetryClient.TrackTrace(logmessage);
        }
    }
}
