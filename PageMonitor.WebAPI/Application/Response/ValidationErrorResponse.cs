using PageMonitor.Application.Exceptions;

namespace PageMonitor.WebAPI.Application.Response
{
    public class ValidationErrorResponse
    {
        public ValidationErrorResponse()
        {
        }

        public ValidationErrorResponse(ValidationException validationException)
        {
            if (validationException != null)
            {
                if (validationException.Errors != null)
                {
                    Errors = validationException.Errors.Select(e => new FieldValidationError()
                    {
                        Error = e.Error,
                        FieldName = e.FieldName
                    }).ToList();
                }
            }
        }

        public List<FieldValidationError> Errors { get; set; } = new List<FieldValidationError>();

        public class FieldValidationError
        {
            public string FieldName { get; set; }

            public string Error { get; set; }
        }
    }
}