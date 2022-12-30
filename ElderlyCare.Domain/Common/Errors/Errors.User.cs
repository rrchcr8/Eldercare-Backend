using ErrorOr;

namespace ElderlyCare.Domain.Common.Errors;

public static partial class Errors
{

    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "email already in use"
            );
    }
}
