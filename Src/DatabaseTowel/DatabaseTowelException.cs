namespace DatabaseTowel
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// A Database Exception.
    /// </summary>
    [Serializable]
    public class DatabaseTowelException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTowelException"/> class.
        /// </summary>
        public DatabaseTowelException()
        {
            this.DatabaseTowelExceptionType = DatabaseTowelExceptionType.Unknown;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTowelException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DatabaseTowelException(string message) : base(message)
        {
            this.DatabaseTowelExceptionType = DatabaseTowelExceptionType.Unknown;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTowelException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DatabaseTowelException(string message, Exception innerException) : base(message, innerException)
        {
            this.DatabaseTowelExceptionType = DatabaseTowelExceptionType.Unknown;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTowelException"/> class.
        /// </summary>
        /// <param name="type">The <see cref="DatabaseTowelExceptionType"/></param>
        /// <param name="message">The message.</param>
        public DatabaseTowelException(DatabaseTowelExceptionType type, string message)
            : base(message)
        {
            this.DatabaseTowelExceptionType = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTowelException"/> class.
        /// </summary>
        /// <param name="type">The <see cref="DatabaseTowelExceptionType"/></param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DatabaseTowelException(DatabaseTowelExceptionType type, string message, Exception innerException)
            : base(message, innerException)
        {
            this.DatabaseTowelExceptionType = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTowelException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected DatabaseTowelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.DatabaseTowelExceptionType = (DatabaseTowelExceptionType)info.GetInt32("DatabaseTowelExceptionType");
        }

        /// <summary>
        /// Gets the <see cref="DatabaseTowelExceptionType"/>.
        /// </summary>
        /// <value>
        /// The <see cref="DatabaseTowelExceptionType"/>.
        /// </value>
        public DatabaseTowelExceptionType DatabaseTowelExceptionType { get; private set; }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException">Argument Null Exception if info is null.</exception>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            base.GetObjectData(info, context);

            info.AddValue("DatabaseTowelExceptionType", this.DatabaseTowelExceptionType);
        }
    }
}
