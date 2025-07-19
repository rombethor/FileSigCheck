using System;

namespace FileSigCheck
{
    /// <summary>
    /// Invalid file signature exception.
    /// </summary>
    public class InvalidSignatureException : Exception
    {
        /// <summary>
        /// The detected bytes from the beginning of the file
        /// </summary>
        public byte[] DetectedBytes { get; set; } = new byte[0];

        /// <summary>
        /// The file signatures checked against, of which this file does not match any.
        /// </summary>
        public string[] PermittedExtensions { get; set; } = Array.Empty<string>();
    }
}
