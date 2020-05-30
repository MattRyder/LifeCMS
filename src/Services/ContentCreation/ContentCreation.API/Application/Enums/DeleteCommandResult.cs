namespace LifeCMS.Services.ContentCreation.API.Application.Enums
{
    public enum DeleteCommandResult
    {
        // Successfully deleted the Status
        Success,

        // Failed to find the Status
        NotFound,

        // Failed to delete the Status
        Failure
    }
}