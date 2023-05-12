﻿using System;
using System.Collections.Generic;

namespace FileSigCheck
{
    public class FileSignatures
    {
        //More signatures available at: https://filesignatures.net/
        public static readonly Dictionary<string, List<byte[]>> Signatures = new Dictionary<string, List<byte[]>>()
        {
            { 
                ".png", new List<byte[]>()
                {
                    new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
                }
            },
            { 
                ".jpg", new List<byte[]>()
                {
                    new byte[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte[] { 0xFF, 0x4F, 0xFF, 0x51 }
                }
            },
            { 
                ".jpeg", new List<byte[]>()
                {
                    new byte[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte[] { 0xFF, 0x4F, 0xFF, 0x51 }
                }
            },
            { 
                ".gif", new List<byte[]>()
                {
                    new byte[] { 0x47, 0x49, 0x46, 0x38 }
                }
            },
            { 
                ".doc", new List<byte[]>()
                {
                    new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                    new byte[] { 0x0D, 0x44, 0x4F, 0x43 },
                    new byte[] { 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1, 0x00 },
                    new byte[] { 0xDB, 0xA5, 0x2D, 0x00 },
                    new byte[] { 0xEC, 0xA5, 0xC1, 0x00 }
                }
            },
            { 
                ".docx", new List<byte[]>()
                {
                    new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                    new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 }
                }
            },
            {
                ".ppt", new List<byte[]>()
                {
                    new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                    new byte[] { 0x00, 0x6E, 0x1E, 0xF0 },
                    new byte[] { 0x0F, 0x00, 0xE8, 0x03 },
                    new byte[] { 0xA0, 0x46, 0x1D, 0xF0 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x0E, 0x00, 0x00, 0x00 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x1C, 0x00, 0x00, 0x00 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x43, 0x00, 0x00, 0x00 }
                }
            },
            {
                ".pptx", new List<byte[]>()
                {
                    new byte[] { 0x50, 0x4B, 0x03, 0x04 }
                }
            },
            {
                ".xls", new List<byte[]>()
                {
                    new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                    new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x10 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x1F },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x22 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x23 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x28 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x29 }
                }
            },
            {
                ".xlsx", new List<byte[]>()
                {
                    new byte[] { 0x50, 0x4B, 0x03, 0x04 }
                }
            },
            { 
                ".pdf", new List<byte[]>()
                {
                    new byte[] { 0x25, 0x50, 0x44, 0x46 }
                }
            },
            { 
                ".mp3", new List<byte[]>()
                {
                    new byte[] { 0x49, 0x44, 0x33 }
                }
            },
            { 
                ".wav", new List<byte[]>()
                {
                    new byte[] { 0x52, 0x49, 0x46, 0x46 }
                }
            },
            { 
                ".ogg", new List<byte[]>()
                {
                    new byte[] { 0x4F, 0x67, 0x67, 0x53, 0x00, 0x02, 0x00, 0x00 }
                }
            },
            { 
                ".mpg", new List<byte[]>()
                {
                    new byte[] { 0x00, 0x00, 0x01, 0xBA },
                    new byte[] { 0x00, 0x00, 0x01, 0xB3 }
                }
            },
            { 
                ".m4a", new List<byte[]>()
                {
                    new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x41 }
                }
            },
            {
                ".xml", new List<byte[]>()
                {
                    new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D }
                }
            }
        };
    }
}
