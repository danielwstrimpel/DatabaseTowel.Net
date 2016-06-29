# DatabaseTowel.Net

** _The documentation and library is a work in progress. I shouldn't have made it public yet until the documentation
was finalized. Still cleaning stuff up..._ **

DatabaseTowel.Net helps keep your ADO.Net code DRY (don't repeat yourself) and testable.

This library was born out of the problems inherent in writing unit tests for data layers in C# code. The issue is that
the ADO.Net libraries built into .Net are not set up to be easily mocked out so that a database is not required to
write unit tests. Additionally, there is a lot of code to be written each time to access a database. This consists of
creating and opening a database connection, handing errors, and processing the output. Developers either need to write
this over and over again, or come up with their own helper type libraries. This library attempts to help out with both
of these problems.

## Table of Contents

+ [Installation](#installation)
+ [Usage](#usage)
  + [DatabaseTowel Initialization](#databasetowel-initialization)
  + [CreateConnection Method](#createconnection-method)
  + [CreateCommand Method](#createcommand-method)
  + [CreateParameter Method](#createparameter-method)
  + [ExecuteSql Method](#executesql-method)
  + [ExecuteSqlTransaction Method](#executesqltransaction-method)
  + [ExecuteScalar Method](#executescalar-method)
  + [ExecuteNonQuery Method](#executenonquery-method)
  + [ExecuteReader Method](#executereader-method)
  + [ExecuteStoredProcedure Method](#executestoredprocedure-method)
  + [ExecuteScalarStoredProcedure Method](#executescalarstoredprocedure-method)
  + [DataReaderToDataTable Method](#datareadertodatatable-method)
+ [Testing](#testing)

## Installation

This library is realeased as a NuGet package (`DatabaseTowel`) which can be found in the public NuGet feed
(https://www.nuget.org/packages/DatabaseTowel/).

To install DatabaseTowel.Net, run the following command in the Package Manager Console:

```bash
Install-Package DatabaseTowel
```

## Usage

Most methods have both a synchronous and asynchronous version. The usage of the methods is the same. The methods that
don't have asynchronous versions are the create methods which are not contacting the database and thus do not need to
have an asynchronous version.

### DatabaseTowel Initialization

To initialize an instance, you need to provide the database connection string and the provider to use. These values are
what you would put on a `connectionString` XML node in the web.config. The provider name allows the library to be
abstracted away from the implemenation of the underlying database technology. Typcially developers in .Net will work
with SQL Server and will use the database objects specifically for that database technology (i.e. found in the
`System.Data.SqlClient` namespace). .Net actually provides mechanisms to abstract this by using the `DbProviderFactory`
for the given provider. Thus, we can abstract out the database stuff so that the library works with any provider
supported.

```csharp
// The connectionString and providerName are the values that would be on a connectionString XML node in the web.config.
var towel = new DatabaseTowel("[Connection String]", "[Provider Name]");
```

.Net's `DbProviderFactory` does not have an interface to allow us to mock things out easily, so the library has a
helper class (`DbProviderFactoryHelper`) that is instantiated under the hood using the connection string and provider
name provided. There is another constructor that allows this helper to be passed in, but is provided for testing
purposes (to test the library's code) and does not need to be used by developers using the library. Having that helper
class as another class allows us to mock the behavior at a lower level for our testing purposes, but you never need to
worry about it.

### CreateConnection Method

If none of the facilities for accessing data in the database will suffice, this is provided so that the code can be
testable (as a way to mock out the data access in unit tests). This method is provided for openness, but typically
should not be used (see the `ExecuteSql` and `ExecuteSqlTransaction` methods). If you choose to use this method instead
of the higher-level methods, you'll need to make sure that you catch any `DbException`s that occur while interacting with
the connection (i.e. `connection.Open()`).

```csharp
var connection = towel.CreateConnection();
```

### CreateCommand Method

This method simply creates the command which can be used to execute queries against the database. If none of the methods
provided will work for your needs, you can simply use this to get a command and process things on your own. Be sure that
you catch any `DbException`s that occur while interacting with the command (i.e. `command.ExecuteNonQuery()`).

```csharp
var command = towel.CreateCommand("[Command Text]", connection);
```

### CreateParameter Method

**_TODO: Missing details_**

```csharp
var parameter = towel.CreateParameter("@foo", "bar", DbType.String);
```

### ExecuteSql Method

**_TODO: Missing details_**

```csharp
var dataTable = towel.ExecuteSql(connection =>
    {
        ...
    }
    ex => { throw new OwnLibraryException("Throw your own exception or do something else instead.", ex); });
```

### ExecuteSqlTransaction Method

**_TODO: Missing details_**

```csharp
var dataTable = towel.ExecuteSqlTransaction(connection =>
    {
        ...
    }
    ex => { throw new OwnLibraryException("Throw your own exception or do something else instead.", ex); });
```

### ExecuteScalar Method

**_TODO: Missing details_**

```csharp
var scalar = towel.ExecuteScalar(command);
```

### ExecuteNonQuery Method

**_TODO: Missing details_**

```csharp
towel.ExecuteNonQuery(command);
```

### ExecuteReader Method

**_TODO: Missing details_**

```csharp
towel.ExecuteReader(command, dataReader =>
    {
        ...
    });
```

### ExecuteStoredProcedure Method

**_TODO: Missing details_**

```csharp
var parameters = new List<DbParameter> { towel.CreateParameter("@foo", "bar", DbType.String) };
var dataTable = towel.ExecuteStoredProcedure("TableResultStoredProcName", parameters,
    ex => { throw new OwnLibraryException("Throw your own exception or do something else instead.", ex); });
```

### ExecuteScalarStoredProcedure Method

**_TODO: Missing details_**

```csharp
var parameters = new List<DbParameter> { towel.CreateParameter("@foo", "bar", DbType.String) };
var scalar = towel.ExecuteScalarStoredProcedure("ScalarResultStoredProcName", parameters,
    ex => { throw new OwnLibraryException("Throw your own exception or do something else instead.", ex); });
```

### DataReaderToDataTable Method

**_TODO: Missing details_**

```csharp
var dataTable = towel.DataReaderToDataTable(dataReader);
```

## Testing

### Moq Example

```csharp

```