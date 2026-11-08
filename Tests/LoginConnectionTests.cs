using System;
using System.Threading.Tasks;
using karaoke.Helpers;
using karaoke.Services;

// Compile next to karaoke.exe; copy karaoke.exe.config to the test executable.
// Set KARAOKE_DEMO_USER and KARAOKE_DEMO_PASS for the successful-login case.
class LoginConnectionTests
{
    static void Main(string[] args)
    {
        var service = new AuthService();
        if (args.Length > 0 && args[0] == "--expect-connection-error")
        {
            UserSession.MaTK = 42;
            var failed = Task.Run(() => service.TryDangNhap("unused", "unused"))
                .GetAwaiter().GetResult();
            if (failed.Success || failed.Error == null || UserSession.IsLoggedIn)
                throw new Exception("Connection failure was not returned safely.");
            Console.WriteLine("PASS: connection failure returned without a faulted task; session cleared.");
            return;
        }

        string username = Environment.GetEnvironmentVariable("KARAOKE_DEMO_USER");
        string password = Environment.GetEnvironmentVariable("KARAOKE_DEMO_PASS");
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            throw new Exception("Set KARAOKE_DEMO_USER and KARAOKE_DEMO_PASS.");

        var success = Task.Run(() => service.TryDangNhap(username, password))
            .GetAwaiter().GetResult();
        if (!success.Success || success.Error != null || !UserSession.IsLoggedIn)
            throw new Exception("Valid login failed.");

        var invalid = Task.Run(() => service.TryDangNhap(username, password + "-wrong"))
            .GetAwaiter().GetResult();
        if (invalid.Success || invalid.Error != null || UserSession.IsLoggedIn)
            throw new Exception("Invalid password was not rejected cleanly.");
        Console.WriteLine("PASS: valid login and invalid password handled separately.");
    }
}
