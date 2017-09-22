using System;
using System.IO;

namespace PitneyBowes.Developer.ShippingApi
{
    /// <summary>
    /// Whatever is read or written to the stream is recorded in the file
    /// </summary>
    public sealed class RecordingStream : Stream, IDisposable
    {
        private string _path;
        private Stream _baseStream = null;
        private FileStream _recorder = null;
        private FileMode _fileMode;

        public RecordingStream(Stream baseStream, string path, FileMode fileMode )
        {
            _baseStream = baseStream;
            _path = path;
            _fileMode = fileMode;
            RecordAfterSeek = false;
        }

        /// <summary>
        /// Sets up recording to begin after the stream is positioned at correct place for reading or writing
        /// </summary>
        public bool RecordAfterSeek { get; set; }

        /// <summary>
        /// Turns recording on or off. This is a heavy operation - opens and closes the file. If you want to start and stop a lot, modify this method
        /// </summary>
        public bool IsRecording
        {
            get { return _recorder != null; }
            set
            {
                if ( value && _recorder == null )
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_path));
                    _recorder = new FileStream(_path, _fileMode, FileAccess.Write);
                }
                else if ( !value && _recorder != null )
                {
                    _recorder.Dispose();
                    _recorder = null;
                }
            }
        }
        public override bool CanRead => _baseStream.CanRead;

        public override bool CanSeek => _baseStream.CanSeek;

        public override bool CanWrite => _baseStream.CanWrite;

        public override long Length => _baseStream.Length;

        public override long Position
        {
            get => _baseStream.Position;
            set { _baseStream.Position = value; }
        }

        public override void Flush()
        {
            _baseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int r = _baseStream.Read( buffer, offset, count);
            if (IsRecording && r > 0)
                _recorder.Write(buffer, offset, r);
            return r;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if ( !IsRecording && RecordAfterSeek )
            {
                IsRecording = true;
            }
            return _baseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _baseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _recorder.Write(buffer, offset, count);
            _baseStream.Write(buffer, offset, count );
        }

        public new void Dispose()
        {
            if (_recorder != null)
            {
                _recorder.Dispose();
                _recorder = null;
            }
            base.Dispose();
        }
    }
}