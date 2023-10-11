namespace Application
{
    public enum ErrorCodes
    {
        // Persons related codes 1 to 500
        NOT_FOUND = 1,
    }

    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}