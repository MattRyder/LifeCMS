using System.Threading.Tasks;

namespace LifeCMS.Services.ContentCreation.Infrastructure.Services.DeleteFileService
{
    public interface IDeleteFileService
    {
        Task<bool> DeleteFileAsync(string urn);
    }
}
