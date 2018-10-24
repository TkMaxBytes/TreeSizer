using System;

namespace com.treesizer.exceptions
{
    /// <summary>
    /// This Exception is used for indicating that the file is a zero length file.
    /// </summary>
    public class TSFileZeroLengthException : Exception
    {
        public TSFileZeroLengthException()
        {
        }

        public TSFileZeroLengthException(string message)
            : base(message)
        {
        }

        public TSFileZeroLengthException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
