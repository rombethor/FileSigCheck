using System;
using System.Collections.Generic;

namespace FileSigCheck
{
    public class FileSignatures
    {
        //More signatures available at: https://filesignatures.net/
        public static readonly Dictionary<string, List<byte?[]>> Signatures = new Dictionary<string, List<byte?[]>>()
        {
            { 
                ".png", new List<byte?[]>()
                {
                    new byte?[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
                }
            },
            {
                ".jpg", new List<byte?[]>()
                {
                    new byte?[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A },
                    new byte?[] { 0xFF, 0xD8, 0xFF, 0xDB },
                    new byte?[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte?[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte?[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    new byte?[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    new byte?[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte?[] { 0xFF, 0x4F, 0xFF, 0x51 }
                }
            },
            { 
                ".jpeg", new List<byte?[]>()
                {
                    new byte?[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A },
                    new byte?[] { 0xFF, 0xD8, 0xFF, 0xDB },
                    new byte?[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte?[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte?[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    new byte?[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    new byte?[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte?[] { 0xFF, 0x4F, 0xFF, 0x51 }
                }
            },
            { 
                ".gif", new List<byte?[]>()
                {
                    new byte?[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 },
                    new byte?[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 }
                }
            },
            {
                //Tagged image file format
                ".tif", new List<byte?[]>()
                {
                    //little-endian
                    new byte?[] { 0x49, 0x49, 0x2A, 0x00 },
                    //big-endian
                    new byte?[] { 0x4D, 0x4D, 0x00, 0x2A },
                    //BigTIFF little-endian
                    new byte?[] { 0x49, 0x49, 0x2B, 0x00 },
                    //BigTIFF big-endian
                    new byte?[] { 0x4D, 0x4D, 0x00, 0x2B }
                }
            },
            {
                //Tagged image file format
                ".tiff", new List<byte?[]>()
                {
                    //little-endian
                    new byte?[] { 0x49, 0x49, 0x2A, 0x00 },
                    //big-endian
                    new byte?[] { 0x4D, 0x4D, 0x00, 0x2A },
                    //BigTIFF little-endian
                    new byte?[] { 0x49, 0x49, 0x2B, 0x00 },
                    //BigTIFF big-endian
                    new byte?[] { 0x4D, 0x4D, 0x00, 0x2B }
                }
            },
            {
                // Google WebP image file
                ".webp", new List<byte?[]>()
                {
                    // Wildcard bytes here actually indicate the file size
                    new byte?[] { 0x52, 0x49, 0x46, 0x46, null, null, null, null, 0x57, 0x45, 0x52, 0x50 }
                }
            },
            { 
                ".doc", new List<byte?[]>()
                {
                    new byte?[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                    new byte?[] { 0x0D, 0x44, 0x4F, 0x43 },
                    new byte?[] { 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1, 0x00 },
                    new byte?[] { 0xDB, 0xA5, 0x2D, 0x00 },
                    new byte?[] { 0xEC, 0xA5, 0xC1, 0x00 }
                }
            },
            { 
                ".docx", new List<byte?[]>()
                {
                    new byte?[] { 0x50, 0x4B, 0x03, 0x04 },
                    new byte?[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 }
                }
            },
            {
                ".ppt", new List<byte?[]>()
                {
                    new byte?[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                    new byte?[] { 0x00, 0x6E, 0x1E, 0xF0 },
                    new byte?[] { 0x0F, 0x00, 0xE8, 0x03 },
                    new byte?[] { 0xA0, 0x46, 0x1D, 0xF0 },
                    new byte?[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x0E, 0x00, 0x00, 0x00 },
                    new byte?[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x1C, 0x00, 0x00, 0x00 },
                    new byte?[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x43, 0x00, 0x00, 0x00 }
                }
            },
            {
                ".pptx", new List<byte?[]>()
                {
                    new byte?[] { 0x50, 0x4B, 0x03, 0x04 }
                }
            },
            {
                ".xls", new List<byte?[]>()
                {
                    new byte?[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                    new byte?[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 },
                    new byte?[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x10 },
                    new byte?[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x1F },
                    new byte?[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x22 },
                    new byte?[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x23 },
                    new byte?[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x28 },
                    new byte?[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x29 }
                }
            },
            {
                ".xlsx", new List<byte?[]>()
                {
                    new byte?[] { 0x50, 0x4B, 0x03, 0x04 }
                }
            },
            {
                ".rtf", new List<byte?[]>()
                {
                    new byte?[] { 0x7B, 0x5C, 0x72, 0x74, 0x66, 0x31 }
                }
            },
            { 
                ".pdf", new List<byte?[]>()
                {
                    new byte?[] { 0x25, 0x50, 0x44, 0x46 }
                }
            },
            { 
                ".mp3", new List<byte?[]>()
                {
                    new byte?[] { 0x49, 0x44, 0x33 }
                }
            },
            { 
                ".wav", new List<byte?[]>()
                {
                    new byte?[] { 0x52, 0x49, 0x46, 0x46, null, null, null, null, 0x57, 0x41, 0x56, 0x45 }
                }
            },
            {
                ".avi", new List<byte?[]>()
                {
                    new byte?[] { 0x52, 0x49, 0x46, 0x46, null, null, null, null, 0x41, 0x56, 0x49, 0x20 }
                }
            },
            {
                ".ogg", new List<byte?[]>()
                {
                    new byte ?[] { 0x4F, 0x67, 0x67, 0x53, 0x00, 0x02, 0x00, 0x00 }
                }
            },
            { 
                ".mpg", new List<byte?[]>()
                {
                    new byte?[] { 0x00, 0x00, 0x01, 0xBA },
                    new byte?[] { 0x00, 0x00, 0x01, 0xB3 }
                }
            },
            { 
                ".m4a", new List<byte?[]>()
                {
                    new byte?[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x41 }
                }
            },
            {
                ".xml", new List<byte?[]>()
                {
                    new byte?[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D }
                }
            },
            {
                ".rar", new List<byte?[]>()
                {
                    //v1.5+
                    new byte?[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x00 },
                    //v5+
                    new byte?[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x01, 0x00 }
                }
            },
            {
                ".exe", new List<byte?[]>()
                {
                    // DOS MZ Executable
                    new byte?[] { 0x4D, 0x5A },
                    // DOS ZM Executable (rare)
                    new byte?[] { 0x5A, 0x4D }
                }
            },
            {
                //blender model file
                ".blend", new List<byte?[]>()
                {
                    new byte?[] { 0x42, 0x4C, 0x45, 0x4E, 0x44, 0x45, 0x52 }
                }
            }
        };
    }
}
