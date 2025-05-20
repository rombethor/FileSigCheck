using System;
using System.IO;
using System.Linq;

namespace FileSigCheck
{
    public static class FileSigCheck
    {

        /// <summary>
        /// (Experimental) Check if a Data URI content is valid using the file signature.
        /// Note, it only works for some image and audio formats.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        /// <exception cref="FormatException">URI received is not a valid Data URI</exception>
        public static bool IsDataUriSignatureValid(string uri)
        {
            if (!uri.StartsWith("data:"))
                throw new FormatException("Not a valid data uri");

            string mimeType = uri.Substring(5, uri.IndexOf(';') - 5);

            bool isBase64 = uri.Contains(";base64,");

            int commaIndex = uri.IndexOf(',');
            if (commaIndex == 0)
            {
                throw new FormatException("Data URI format invalid");
            }

            var data = uri.Substring(commaIndex + 1);
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(data)))
            {
                switch(mimeType)
                {
                    case "image/png":
                        return IsFileSignatureValid(ms, ".png");
                    case "image/jpeg":
                        return IsFileSignatureValid(ms, ".jpg", ".jpeg");
                    case "image/gif":
                        return IsFileSignatureValid(ms, ".gif");
                    case "image/tiff":
                        return IsFileSignatureValid(ms, ".tiff");
                    case "audio/mpeg":
                        return IsFileSignatureValid(ms, ".mpg");
                    case "audio/ogg":
                        return IsFileSignatureValid(ms, ".ogg");
                    default:
                        return false; //Indeterminate result!
                };
            }
        }

        /// <summary>
        /// Check a file signature based on the filename's extension
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public static bool IsFileSignatureValid(string filename, Stream data)
        {
            if (string.IsNullOrWhiteSpace(filename) || data == null || data.Length == 0)
            {
                return false;
            }

            var ext = Path.GetExtension(filename).ToLowerInvariant();
            if (!FileSignatures.Signatures.ContainsKey(ext))
                throw new InvalidDataException($"File signature for {ext} has not been provided!  Rejecting file!");

            return IsFileSignatureValid(data, ext);
        }

        /// <summary>
        /// Checks the file data against the filename's extension to verify its type via its signature.
        /// A list of permitted extensions is compared against the filename's extension also to check if it is allowed.
        /// Note: If a text, csv or prn file, encoding is limited to the ASCII character set for security.
        /// </summary>
        /// <param name="filename">Name of the file</param>
        /// <param name="data">A stream of the file's data</param>
        /// <param name="permittedExtensions">A list of extensions permitted in the file name</param>
        /// <exception cref="ArgumentException">Thrown if a value in permitted signature is not within the keys of <see cref="FileSignatures.Signatures"/></exception>
        /// <exception cref="InvalidDataException">Thrown when the file signature is not provided in this library.</exception>
        /// <returns></returns>
        public static bool IsFileSignatureValid(Stream data, params string[] permittedExtensions)
        {
            if (permittedExtensions.Except(FileSignatures.Signatures.Select(s => s.Key)).Any())
                throw new ArgumentException("Not all permitted extensions are valid", nameof(permittedExtensions));

            using (var reader = new BinaryReader(data))
            {
                //Only read the bytes we need
                int maxLen = FileSignatures.Signatures
                    .Where(s => permittedExtensions.Contains(s.Key))
                    .SelectMany(s => s.Value)
                    .Max(s => s.Length);

                var headerbytes = reader.ReadBytes(maxLen);

                // Loop through extensions and their file signatures
                foreach (var ext in permittedExtensions)
                {
                    var signatures = FileSignatures.Signatures[ext];
                    foreach (var signature in signatures)
                    {
                        bool matched = true;
                        for (int i = 0; i < signature.Length; i++)
                        {
                            //treat null as wildcard
                            if (signature[i] is null)
                                continue;

                            if (signature[i] != headerbytes[i])
                            {
                                matched = false;
                                break;
                            }
                        }

                        // Only one signature pattern needs matching
                        if (matched) return true;
                    }
                }

                // No signature patterns matched
                return false;
            }
        }

    }
}
