﻿using System;
using System.IO;

namespace MaxMind.Db
{
    internal class ArrayReader : IByteReader
    {
        private readonly byte[] _fileBytes;

        public ArrayReader(string file)
        {
            _fileBytes = File.ReadAllBytes(file);
        }

        public ArrayReader(byte[] array)
        {
            _fileBytes = array;
        }

        public int Length => _fileBytes.Length;

        public byte[] Read(int offset, int count)
        {
            // Not using an ArraySegment as you can cast it into an IList
            // in .NET 4. I also looked into ImmutableArray, but it appears
            // that it does a copy when creating a subarray.
            var bytes = new byte[count];
            Copy(offset, bytes);
            return bytes;
        }

        public byte ReadOne(int offset) => _fileBytes[offset];

        public void Copy(int offset, byte[] bytes)
        {
            if (bytes.Length > 0)
            {
                Array.Copy(_fileBytes, offset, bytes, 0, bytes.Length);
            }
        }

        public void Dispose()
        {
        }
    }
}