using ProjectLibrary_Back.Exceptions;

namespace ProjectLibrary_Back.Validation
{
    public class IsValid
    {
        public static void IsValidId(int? id)
        {
            if (id <= 0)
            {
                throw new LibraryManagementSystemException("ID mənfi və ya sıfır ola bilməz.");
            }
        }


        public static bool IsValidImageFormat(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return false;
            }

            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            return validExtensions.Any(ext => imageUrl.EndsWith(ext, StringComparison.OrdinalIgnoreCase));

        }
    }
}
