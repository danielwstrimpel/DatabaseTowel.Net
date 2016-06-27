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
        /// Invalid Argument exception.
        /// </summary>
        InvalidArgument,

        /// <summary>
        /// Connection open failed exception.
        /// </summary>
        ConnectionOpenFailed,

        /// <summary>
        /// Command execute non query failed exception.
        /// </summary>
        CommandExecuteNonQueryFailed,

        /// <summary>
        /// Command execute reader failed exception.
        /// </summary>
        CommandExecuteReaderFailed
    }
}
