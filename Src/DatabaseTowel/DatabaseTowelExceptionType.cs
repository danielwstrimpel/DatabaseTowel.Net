namespace DatabaseTowel
{
    /// <summary>
    /// DatabaseTowelException Types.
    /// </summary>
    public enum DatabaseTowelExceptionType
    {
        /// <summary>
        /// Unknown exception.
        /// </summary>
        Unknown,

        /// <summary>
        /// Transaction failed exception.
        /// </summary>
        TransactionFailed,

        /// <summary>
        /// Transaction complete failed exception.
        /// </summary>
        TransactionCompleteFailed,

        /// <summary>
        /// Invalid Argument exception.
        /// </summary>
        InvalidArgument,

        /// <summary>
        /// Connection open failed exception.
        /// </summary>
        ConnectionOpenFailed,

        /// <summary>
        /// Command execute failed exception.
        /// </summary>
        CommandExecuteFailed,

        /// <summary>
        /// Command execute scalar failed exception.
        /// </summary>
        CommandExecuteScalarFailed,

        /// <summary>
        /// Command execute non query failed exception.
        /// </summary>
        CommandExecuteNonQueryFailed,

        /// <summary>
        /// Command execute reader failed exception.
        /// </summary>
        CommandExecuteReaderFailed,

        /// <summary>
        /// Command execute scalar stored procedure failed exception.
        /// </summary>
        CommandExecuteScalarStoredProcedureFailed,

        /// <summary>
        /// Command execute non query stored procedure failed exception.
        /// </summary>
        CommandExecuteNonQueryStoredProcedureFailed,

        /// <summary>
        /// Command execute reader stored procedure failed exception.
        /// </summary>
        CommandExecuteReaderStoredProcedureFailed,
    }
}
