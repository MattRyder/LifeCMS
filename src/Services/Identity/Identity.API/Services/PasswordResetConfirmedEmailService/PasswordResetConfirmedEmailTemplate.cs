namespace LifeCMS.Services.Identity.API.Services.PasswordResetConfirmedEmailService
{
    public class PasswordResetConfirmedEmailTemplate
    {
        public static readonly string Subject = "Your password has been reset";

        public static readonly string Template = @"
        This email is to confirm the password has been changed for the LifeCMS account {{email_address}}.

        Thanks,
        The LifeCMS Team.
        ";
    }
}
