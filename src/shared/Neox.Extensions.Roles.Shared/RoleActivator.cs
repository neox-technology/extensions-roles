using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Neox.Extensions.Roles.Shared;

internal static class RoleActivator {

    public static TRole Create<TRole>()
        where TRole : IRole, new() {
        try {
            return new TRole();
        }
        catch (TargetInvocationException exception) {
            ExceptionDispatchInfo.Capture(Unwrap(exception)).Throw();
            throw;
        }
    }

    private static Exception Unwrap(Exception exception) {
        while (exception is TargetInvocationException { InnerException: { } inner }) {
            exception = inner;
        }

        return exception;
    }
}