using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileSigCheck
{
    public static class FileSigCheck
    {

        /// <summary>
        /// Check to see if base64 string is a base64 image (png, jpg/jpeg or gif)!
        /// </summary>
        /// <param name="b64">The base64 data</param>
        /// <returns>True if a valid png, jpg or gif image.  False otherwise.</returns>
        public static bool IsValidBase64Image(string b64)
        {
            if (b64.StartsWith("data:"))
            {
                int commaIndex = b64.IndexOf(',');
                if (commaIndex > 0)
                    b64 = b64.Substring(commaIndex + 1);
            }
            //get bytes
            byte[] data = Convert.FromBase64String(b64);

            //image extensions
            var exts = new List<string>() { ".png", ".jpg", ".jpeg", ".gif" };

            //if matches a file signature, allow it to pass
            foreach (var picType in FileSignatures.Signatures.Where(f => exts.Contains(f.Key)))
            {
                foreach (var sig in picType.Value)
                {
                    if (data.Take(sig.Length).SequenceEqual(sig))
                        return true;
                }
            }
            //no signature matched, not a valid image
            return false;
        }

        /// <summary>
        /// Checks the file data against the filename's extension to verify its type via its signature.
        /// A list of permitted extensions is compared against the filename's extension also to check if it is allowed.
        /// Note: If a text, csv or prn file, encoding is limited to the ASCII character set for security.
        /// </summary>
        /// <param name="filename">Name of the file</param>
        /// <param name="data">A stream of the file's data</param>
        /// <param name="permittedExtensions">A list of extensions permitted in the file name</param>
        /// <exception cref="InvalidDataException">Thrown when the file signature is not provided in this library.</exception>
        /// <returns></returns>
        public static bool IsValidFileSignature(string filename, Stream data, string[] permittedExtensions)
        {
            if (string.IsNullOrWhiteSpace(filename) || data == null || data.Length == 0)
            {
                return false;
            }

            var ext = Path.GetExtension(filename).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                return false;

            data.Position = 0;

            using (var reader = new BinaryReader(data))
            {
                //These files have no signature, so limit encoding to ASCII characters
                if (ext.Equals(".txt") || ext.Equals(".csv") || ext.Equals(".prn"))
                {
                    for (var i = 0; i < data.Length; i++)
                    {
                        var b = reader.ReadByte();
                        if (b > sbyte.MaxValue)
                            return false;
                    }

                    return true;
                }
                if (!FileSignatures.Signatures.ContainsKey(ext))
                    throw new InvalidDataException($"File signature for {ext} has not been provided!  Rejecting file!");
                var signatures = FileSignatures.Signatures[ext];
                var headerbytes = reader.ReadBytes(signatures.Max(s => s.Length));
                return signatures.Any(s => headerbytes.Take(s.Length).SequenceEqual(s));
            }
        }

    }
}
