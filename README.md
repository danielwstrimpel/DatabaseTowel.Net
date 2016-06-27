# DatabaseTowel.Net

DatabaseTowel.Net helps keep your ADO.Net code DRY and testable.

** _The documentation is a work in progress._ **

## Usage

```csharp
// The connectionString and providerName are the values that would be on a connectionString XML node in the web.config.
var towel = new DatabaseTowel(new DbProviderFactoryHelper("[Connection String]", "[Provider Name]"));

// This will handling managing a connection and wrap all database exceptions, keeping your code small.
var parameters = new List<DbParameter> { towel.DatabaseProviderFactoryHelper.CreateParameter("@foo", "bar", DbType.String) };
var dataTable = await towel.ExecuteStoredProcedureAsync(
    "StoredProcName"
	parameters,
	ex => { throw new OwnLibraryException("Throw your own exception or do something else instead.", ex); });
```

## Testing

### Moq Example

```csharp

```