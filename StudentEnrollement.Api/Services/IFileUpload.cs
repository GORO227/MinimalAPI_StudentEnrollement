namespace StudentEnrollement.Api.Services
{
    public interface IFileUpload
    {
        string UploadStudentFile(byte[] file, string imageName);
    }
}
