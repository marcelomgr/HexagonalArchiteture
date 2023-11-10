namespace Application
{
    public enum ErrorCodes
    {
        // Persons related codes 1 to 500
        PERSON_NOT_FOUND = 1,
        COULD_NOT_STORE_DATA = 2,
        INVALID_PERSON_ID = 3,
        MISSING_REQUIRED_INFORMATION = 4,
        INVALID_CPF = 5,

        // Person Types related codes 501 to 600
        PERSON_TYPE_NOT_FOUND = 501,

        // Person Gender related codes 601 to 700
        PERSON_GENDER_NOT_FOUND = 601
    }

    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}