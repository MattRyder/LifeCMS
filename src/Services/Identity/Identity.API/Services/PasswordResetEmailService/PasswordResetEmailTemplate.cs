namespace LifeCMS.Services.Identity.API.Services.PasswordResetEmailService
{
    public class PasswordResetEmailTemplate
    {

        public static readonly string From = "noreply@lifecms.local";
        public static readonly string Subject = "Forgotten password reset";

        public static readonly string Template = @"
        Somebody has requested a new password for the LifeCMS account {{email_address}}.

        You can reset your password by clicking the link below:
        {{identity_api_host}}/password/reset?email={{email_address}}&token={{token}}

        This link will expire in 3 hours.

        If you didn't request this password reset, you can safely ignore this email.

        Thanks,
        The LifeCMS Team.
        ";
    }
}
