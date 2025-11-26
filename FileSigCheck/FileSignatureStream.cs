using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileSigCheck
{
    public class FileSignatureStream : Stream
    {
        private readonly Stream _targetStream;
        private readonly string[] _permittedExtensions;
        private bool _signatureReadComplete = false;
        private byte[] _signatureBuffer = new byte[25];
        private int _initialBufferIndex = 0;

        public FileSignatureStream(Stream targetStream, params string[] permittedExtensions)
        {
            _targetStream = targetStream ?? throw new ArgumentNullException(nameof(targetStream));
            _permittedExtensions = permittedExtensions;
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => throw new NotSupportedException();

        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public override void Flush() => _targetStream.Flush();

        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

        public override void SetLength(long value) => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!_signatureReadComplete)
            {
                int maxLen = FileSignatures.Signatures
                    .Where(s => _permittedExtensions.Contains(s.Key))
                    .SelectMany(s => s.Value)
                    .Max(s => s.Length);

                // Fill the initial buffer with the first bytes
                int bytesToRead = Math.Min(count, _signatureBuffer.Length - _initialBufferIndex);

                Buffer.BlockCopy(buffer, offset, _signatureBuffer, _initialBufferIndex, bytesToRead);
                _initialBufferIndex += bytesToRead;

                bool matchExists = false;

                // Compare signatures
                foreach (var ext in _permittedExtensions)
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

                            if (signature[i] != _signatureBuffer[i])
                            {
                                matched = false;
                                break;
                            }
                        }

                        // Only one signature pattern needs matching
                        if (matched) matchExists = true;
                    }
                }

                if (!matchExists)
                {
                    throw new InvalidSignatureException()
                    {
                        DetectedBytes = _signatureBuffer,
                        PermittedExtensions = _permittedExtensions
                    };
                }

                _signatureReadComplete = true;
            }
            // Write all of the data to the target stream
            _targetStream.Write(buffer, offset, count);
        }

    }
}
