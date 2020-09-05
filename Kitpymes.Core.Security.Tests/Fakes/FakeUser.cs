using System.Collections.Generic;

namespace Kitypmes.Core.Security.Tests
{
    public class FakeUser
    {
        public int Id { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public IEnumerable<string>? Permissions { get; set; }
    }
}
