namespace OBiletCase.Exceptions
{
    public class ClientException:Exception
    {
        private readonly string _message;

        public ClientException(string message)
        {
            _message = message;
        }

        public string GetClientMessage()
        {
            return _message;
        }
    }
}
