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
+ [Getting Started](#getting-started)
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

## Tutorial

To get a database connection to work with you can use either the `ExecuteSql` and `ExecuteSqlTransaction` methods, the
latter of which will wrap the code in a transaction. These methods will handle creating/opening the connection and
wrapping the code to catch database exceptions that occur, giving you the ability to handle them or throw your own
exception.

```csharp
towel.ExecuteSql(connection =>
{
	// you can use the connection to interact with the database
});
```

You can run the code asynchronously by appending **Async** to the end of the method name.

```csharp
await towel.ExecuteSqlAsync(async connection =>
{
	// you can use the connection to interact with the database
});
```

The methods accept an optional error context to run when an exception happens (handling of DbException is done in all
library code calls to the database). The typical use case would be to wrap this exception with your own (so you don't
need to wrap a try/catch every time), but could be some additional code that you want to run.

```csharp
towel.ExecuteSql(connection =>
{
	...
},
ex => { throw new OwnLibraryException("Throw your own exception or do something else instead.", ex); });
```

The higher-level helper methods should cover most cases for database access that you'll need. These methods all have
asynchronous versions by appending `Async` to the end of the method name.

+ `ExecuteScalar` - Executes query that returns single value
+ `ExecuteNonQuery` - Executes query that returns no value
+ `ExecuteReader` - Executes query that returns one or more records
+ `ExecuteStoredProcedure` - Executes a stored procedure, returning results as a `DataTable`
+ `ExecuteScalarStoredProcedure` - Executes a stored procedure that returns a single value

If you need to execute more than one command on a connection, there are versions of the method to call passing in
the connection to use (and thus require the use of the `ExecuteSql` and `ExecuteSqlTransaction` methods).

```csharp
towel.ExecuteSql(connection =>
{
	var parameters = new List<DbParameter> { };
	var scalar1 = towel.ExecuteScalar("[Command #1 text]", parameters, connection);
	var scalar2 = towel.ExecuteScalar("[Command #2 text]", parameters, connection);
});
```

If you only need to execute a single command, there are versions of the method to call ommitting the connection and
will handle it all for you (without a transaction) - meaning that you do not need to use the `ExecuteSql` method in
your code.

```csharp
var parameters = new List<DbParameter> { };
var scalar1 = towel.ExecuteScalar("[Command #1 text]", parameters);
```

These versions of the methods accept the optional error context as well.

```csharp
var parameters = new List<DbParameter> { };
var scalar1 = towel.ExecuteScalar("[Command #1 text]", parameters,
    ex => { throw new OwnLibraryException("Throw your own exception or do something else instead.", ex); });
```

There are other methods/use cases available, but these are the typical uses. For more lower-level access beyond what
the higher-level helper methods offer, refer to the usage documentation to see what is available.

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

This method creates the parameters to pass into a command. You should always use parameterized query statements instead
of putting directly into a command statement.

```csharp
var parameter = towel.CreateParameter("@foo", "bar", DbType.String);
```

### ExecuteSql Method

**_TODO: Missing details_**

```csharp
towel.ExecuteSql(connection =>
    {
        ...
    }
    ex => { throw new OwnLibraryException("Throw your own exception or do something else instead.", ex); });
```

### ExecuteSqlTransaction Method

**_TODO: Missing details_**

```csharp
towel.ExecuteSqlTransaction(connection =>
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