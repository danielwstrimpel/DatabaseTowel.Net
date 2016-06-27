namespace DatabaseTowel
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public static class ExtensionMethods
    {
        /// <summary>
        /// Adds all of the database parameters to the parameter collection.
        /// </summary>
        /// <param name="parameterCollection">The parameter collection.</param>
        /// <param name="databaseParameters">The database parameters.</param>
        /// <returns>
        /// The parameter collection.
        /// </returns>
        public static IDataParameterCollection AddMany(this IDataParameterCollection parameterCollection, IEnumerable<DbParameter> databaseParameters)
        {
            if (databaseParameters == null)
            {
                return parameterCollection;
            }

            foreach (var parameter in databaseParameters)
            {
                parameterCollection.Add(parameter);
            }

            return parameterCollection;
        }
    }
}
