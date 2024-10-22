namespace ProjectLibrary_Back.Exceptions
{
    public class LibraryManagementSystemException : Exception

    {
        public LibraryManagementSystemException()
        {
        }

        public LibraryManagementSystemException(string message) : base(message)
        {
        }

        public LibraryManagementSystemException(string message, Exception inner)
        : base(message, inner)
        {
        }

    }
}
