---
name: xunit
description: xUnit patterns and FluentAssertions for .NET testing
version: 0.1.0
tags: [dotnet, testing, xunit, fluentassertions]
---

# xUnit Patterns

Patrones completos para testing con xUnit + FluentAssertions + NSubstitute.

## Test Structure (AAA)

### Arrange-Act-Assert
```csharp
[Fact]
public void Login_ValidCredentials_ReturnsToken()
{
    // Arrange - Setup test data and dependencies
    var repository = Substitute.For<IUserRepository>();
    var user = new User { Id = 1, Email = "test@test.com" };
    repository.GetByEmailAsync("test@test.com").Returns(user);
    var sut = new AuthService(repository);

    // Act - Execute the method under test
    var result = sut.Login("test@test.com", "password");

    // Assert - Verify expected outcome
    result.Should().NotBeNull();
    result.Token.Should().NotBeNullOrEmpty();
}
```

### Test Naming Convention
```csharp
// Format: MethodName_Scenario_ExpectedResult

[Fact]
public void Add_TwoPositiveNumbers_ReturnsSum() { }

[Fact]
public void GetUser_InvalidId_ThrowsNotFoundException() { }

[Fact]
public void Login_EmptyEmail_ThrowsValidationException() { }
```

---

## FluentAssertions

### Basic Assertions

#### Strings
```csharp
// ✅ BIEN
result.Should().Be("expected");
result.Should().NotBe("unexpected");
result.Should().NotBeNullOrEmpty();
result.Should().NotBeNullOrWhiteSpace();
result.Should().StartWith("prefix");
result.Should().EndWith("suffix");
result.Should().Contain("substring");
result.Should().Match("pattern*");
```

```csharp
// ❌ MAL - Assert clásico
Assert.Equal("expected", result);  // ❌ Menos expresivo
Assert.NotNull(result);  // ❌ Mensaje de error genérico
Assert.True(result.StartsWith("prefix"));  // ❌ Menos legible
```

#### Numbers
```csharp
// ✅ BIEN
count.Should().Be(5);
count.Should().NotBe(0);
count.Should().BeGreaterThan(0);
count.Should().BeGreaterThanOrEqualTo(1);
count.Should().BeLessThan(100);
count.Should().BeLessThanOrEqualTo(100);
count.Should().BeInRange(1, 10);
count.Should().BePositive();
```

#### Booleans
```csharp
// ✅ BIEN
isValid.Should().BeTrue();
isValid.Should().BeFalse();
isValid.Should().Be(true);  // Menos preferido pero OK
```

#### Nullability
```csharp
// ✅ BIEN
user.Should().NotBeNull();
user.Should().BeNull();
user.Name.Should().NotBeNullOrEmpty();
```

### Collections

#### Basic Collection Assertions
```csharp
// ✅ BIEN
collection.Should().NotBeEmpty();
collection.Should().HaveCount(3);
collection.Should().ContainSingle();  // Exactly 1 item
collection.Should().Contain(item);
collection.Should().ContainSingle(x => x.Id == 1);
collection.Should().NotContain(item);
```

#### Order
```csharp
// ✅ BIEN
numbers.Should().BeInAscendingOrder();
numbers.Should().BeInDescendingOrder();
users.Should().BeInAscendingOrder(u => u.Name);
```

#### Equivalence
```csharp
// ✅ BIEN - Deep comparison
actual.Should().BeEquivalentTo(expected);

// ✅ BIEN - Con opciones
actual.Should().BeEquivalentTo(expected, options => options
    .Excluding(u => u.Id)
    .Excluding(u => u.CreatedAt));

// ✅ BIEN - Comparar colecciones
actualList.Should().BeEquivalentTo(expectedList);
```

### Exceptions

#### Exception Thrown
```csharp
// ✅ BIEN
Action act = () => service.Login("invalid", "wrong");
act.Should().Throw<UnauthorizedException>();

// ✅ BIEN - Con mensaje
act.Should().Throw<UnauthorizedException>()
    .WithMessage("Invalid credentials");

// ✅ BIEN - Con mensaje pattern
act.Should().Throw<UnauthorizedException>()
    .WithMessage("*credentials*");

// ✅ BIEN - Con inner exception
act.Should().Throw<InvalidOperationException>()
    .WithInnerException<ArgumentException>();
```

#### No Exception
```csharp
// ✅ BIEN
Action act = () => service.ValidateEmail("test@test.com");
act.Should().NotThrow();

// ✅ BIEN - Específico
act.Should().NotThrow<ArgumentException>();
```

### Async Tests

#### Basic Async
```csharp
[Fact]
public async Task GetUserAsync_ValidId_ReturnsUser()
{
    // Arrange
    var repository = Substitute.For<IUserRepository>();
    var expected = new User { Id = 1, Name = "John" };
    repository.GetByIdAsync(1).Returns(expected);
    var sut = new UserService(repository);

    // Act
    var result = await sut.GetUserAsync(1);

    // Assert
    result.Should().NotBeNull();
    result.Id.Should().Be(1);
    result.Name.Should().Be("John");
}
```

#### Async Exceptions
```csharp
[Fact]
public async Task GetUserAsync_InvalidId_ThrowsException()
{
    // Arrange
    var repository = Substitute.For<IUserRepository>();
    repository.GetByIdAsync(-1).ThrowsAsync<NotFoundException>();
    var sut = new UserService(repository);

    // Act
    Func<Task> act = async () => await sut.GetUserAsync(-1);

    // Assert
    await act.Should().ThrowAsync<NotFoundException>();
}
```

---

## Theory (Parametrized Tests)

### InlineData
```csharp
[Theory]
[InlineData(1, 2, 3)]
[InlineData(5, 5, 10)]
[InlineData(-1, 1, 0)]
[InlineData(0, 0, 0)]
public void Add_TwoNumbers_ReturnsSum(int a, int b, int expected)
{
    var result = Calculator.Add(a, b);
    result.Should().Be(expected);
}

[Theory]
[InlineData("test@test.com", true)]
[InlineData("invalid", false)]
[InlineData("", false)]
[InlineData(null, false)]
public void ValidateEmail_ReturnsExpected(string email, bool expected)
{
    var result = EmailValidator.IsValid(email);
    result.Should().Be(expected);
}
```

### MemberData
```csharp
public class CalculatorTests
{
    public static IEnumerable<object[]> GetTestData()
    {
        yield return new object[] { 1, 2, 3 };
        yield return new object[] { 5, 5, 10 };
        yield return new object[] { -1, 1, 0 };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Add_VariousInputs_ReturnsExpected(int a, int b, int expected)
    {
        var result = Calculator.Add(a, b);
        result.Should().Be(expected);
    }
}
```

### ClassData
```csharp
public class CalculatorTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 1, 2, 3 };
        yield return new object[] { 5, 5, 10 };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

[Theory]
[ClassData(typeof(CalculatorTestData))]
public void Add_FromClassData_ReturnsExpected(int a, int b, int expected)
{
    var result = Calculator.Add(a, b);
    result.Should().Be(expected);
}
```

---

## Mocking (NSubstitute)

### Basic Mocking
```csharp
// ✅ BIEN - Setup mock
var repository = Substitute.For<IUserRepository>();
var user = new User { Id = 1, Name = "John" };
repository.GetByIdAsync(1).Returns(user);

// ✅ BIEN - Use mock
var service = new UserService(repository);
var result = await service.GetUserAsync(1);

// ✅ BIEN - Verify
result.Should().NotBeNull();
result.Id.Should().Be(1);
```

### Verify Calls
```csharp
// ✅ BIEN - Verify called once
await repository.Received(1).GetByIdAsync(1);

// ✅ BIEN - Verify called exactly N times
await repository.Received(3).SaveAsync(Arg.Any<User>());

// ✅ BIEN - Verify never called
await repository.DidNotReceive().DeleteAsync(Arg.Any<int>());

// ✅ BIEN - Verify with any argument
await repository.Received().SaveAsync(Arg.Any<User>());
```

### Argument Matching
```csharp
// ✅ BIEN - Specific argument
repository.GetByIdAsync(1).Returns(user1);
repository.GetByIdAsync(2).Returns(user2);

// ✅ BIEN - Any argument
repository.GetByIdAsync(Arg.Any<int>()).Returns(user);

// ✅ BIEN - Conditional
repository.GetByIdAsync(Arg.Is<int>(x => x > 0)).Returns(user);

// ✅ BIEN - Multiple conditions
repository.SaveAsync(Arg.Is<User>(u =>
    u.Email != null &&
    u.Name.Length > 0
)).Returns(Task.CompletedTask);
```

### Throwing Exceptions
```csharp
// ✅ BIEN - Sync throw
repository.GetByIdAsync(-1).Throws<NotFoundException>();

// ✅ BIEN - Async throw
repository.GetByIdAsync(-1).ThrowsAsync<NotFoundException>();

// ✅ BIEN - With message
repository.GetByIdAsync(-1).ThrowsAsync(new NotFoundException("User not found"));
```

---

## Test Organization

### Class Structure
```csharp
// ✅ BIEN - Constructor setup
public class AuthServiceTests
{
    private readonly IUserRepository _repository;
    private readonly ILogger<AuthService> _logger;
    private readonly AuthService _sut; // System Under Test

    public AuthServiceTests()
    {
        _repository = Substitute.For<IUserRepository>();
        _logger = Substitute.For<ILogger<AuthService>>();
        _sut = new AuthService(_repository, _logger);
    }

    [Fact]
    public void Login_ValidCredentials_ReturnsToken()
    {
        // Test implementation
    }

    [Fact]
    public void Login_InvalidCredentials_ThrowsException()
    {
        // Test implementation
    }
}
```

### Nested Classes (Context)
```csharp
// ✅ BIEN - Organize by method/context
public class AuthServiceTests
{
    public class Login
    {
        private readonly AuthService _sut;

        public Login()
        {
            _sut = new AuthService(/* dependencies */);
        }

        [Fact]
        public void ValidCredentials_ReturnsToken() { }

        [Fact]
        public void InvalidCredentials_ThrowsException() { }

        [Fact]
        public void EmptyEmail_ThrowsValidationException() { }
    }

    public class Logout
    {
        [Fact]
        public void ValidToken_Success() { }

        [Fact]
        public void ExpiredToken_ThrowsException() { }
    }
}
```

### IClassFixture (Shared Context)
```csharp
// ✅ BIEN - Compartir setup entre tests
public class DatabaseFixture : IDisposable
{
    public ApplicationDbContext Context { get; }

    public DatabaseFixture()
    {
        Context = CreateInMemoryContext();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}

public class UserRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly ApplicationDbContext _context;

    public UserRepositoryTests(DatabaseFixture fixture)
    {
        _context = fixture.Context;
    }

    [Fact]
    public async Task GetByIdAsync_ExistingUser_ReturnsUser()
    {
        // Test using shared _context
    }
}
```

---

## Integration Tests

### WebApplicationFactory
```csharp
// ✅ BIEN - Integration test setup
public class ApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUser_ValidId_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/users/1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var user = await response.Content.ReadFromJsonAsync<User>();
        user.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateUser_ValidData_ReturnsCreated()
    {
        // Arrange
        var newUser = new { Name = "John", Email = "john@test.com" };

        // Act
        var response = await _client.PostAsJsonAsync("/api/users", newUser);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
    }
}
```

### Custom WebApplicationFactory
```csharp
// ✅ BIEN - Override services for testing
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove real DbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            // Add in-memory database
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Add test data
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedTestData(context);
        });
    }
}
```

---

## Code Coverage

### Run with Coverage
```bash
# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Generate HTML report (requires ReportGenerator)
reportgenerator \
    -reports:"**/coverage.cobertura.xml" \
    -targetdir:"coverage-report" \
    -reporttypes:Html

# Open report
open coverage-report/index.html
```

### Coverage Targets
```
- Overall project:     ≥85%
- Business logic:      ≥95%
- Services:            ≥90%
- Controllers:         ≥70%
- DTOs/Entities:       ≥50% (mostly property tests)
```

### Exclude from Coverage
```csharp
// ✅ BIEN - Exclude generated code
[ExcludeFromCodeCoverage]
public class MigrationConfiguration { }

[ExcludeFromCodeCoverage]
public partial class ApplicationDbContext { }
```

---

## Test Data Builders

### Builder Pattern
```csharp
// ✅ BIEN - Test data builder
public class UserBuilder
{
    private int _id = 1;
    private string _name = "John Doe";
    private string _email = "john@test.com";
    private bool _isActive = true;

    public UserBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public UserBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public UserBuilder Inactive()
    {
        _isActive = false;
        return this;
    }

    public User Build() => new User
    {
        Id = _id,
        Name = _name,
        Email = _email,
        IsActive = _isActive
    };
}

// Usage
[Fact]
public void ProcessUser_InactiveUser_ThrowsException()
{
    var user = new UserBuilder()
        .WithId(123)
        .Inactive()
        .Build();

    Action act = () => service.ProcessUser(user);

    act.Should().Throw<InvalidOperationException>();
}
```

---

## Best Practices

### DO ✅
- Use AAA pattern (Arrange-Act-Assert)
- One assertion concept per test
- Descriptive test names
- FluentAssertions for readability
- Test behavior, not implementation
- Mock external dependencies
- Keep tests fast (<100ms)
- Maintain test code quality

### DON'T ❌
- Multiple unrelated assertions
- Complex test setup
- Test private methods directly
- Share state between tests
- Use production database
- Ignore failing tests
- Copy-paste test code

---

## Referencias

- [xUnit Documentation](https://xunit.net/)
- [FluentAssertions](https://fluentassertions.com/)
- [NSubstitute](https://nsubstitute.github.io/)
- [Microsoft Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
