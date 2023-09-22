using System.Text;

namespace HealthHub.Application.Constants;

public static class EnvironmentVariables
{
    // Prefix

    public static string ENV_PREFIX => "ENV__";

    // AuthService

    public static string EMAIL_HOST => Environment.GetEnvironmentVariable(ENV_PREFIX + "EMAIL_HOST");
    public static int EMAIL_PORT => Convert.ToInt32(Environment.GetEnvironmentVariable(ENV_PREFIX + "EMAIL_PORT"));
    public static string EMAIL_USERNAME => Environment.GetEnvironmentVariable(ENV_PREFIX + "EMAIL_USERNAME");
    public static string EMAIL_USERNAME_ADDRESS => Environment.GetEnvironmentVariable(ENV_PREFIX + "EMAIL_USERNAME_ADDRESS");
    public static string EMAIL_PASSWORD => Environment.GetEnvironmentVariable(ENV_PREFIX + "EMAIL_PASSWORD");

    public static byte[] SECRET_KEY => Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable(ENV_PREFIX + "SECRET_KEY"));

    // TokenizationService

    public static int EXPIRATION_DATE_DAYS => Convert.ToInt32(Environment.GetEnvironmentVariable(ENV_PREFIX + "EXPIRATION_DATE_DAYS"));
}
