namespace PageMonitor.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<FieldError> Errors { get; set; } = new List<FieldError>();

        public class FieldError
        {
            public string FieldName { get; set; }

            public string Error { get; set; }
        }
    }
}