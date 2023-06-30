namespace WebApi.Errors
{
    public class CodeErrorResponse
    {
        public CodeErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "La peticion enviada tiene errores",
                401 => "Se requiere autorizacion para este recurso",
                404 => "El recurso no se encontro",
                500 => "Se producieron errores en el servidor",
                _ => null,
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

    }
}

