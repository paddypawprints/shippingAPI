/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System;
using System.IO;
using System.Text;

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
        private bool _recording;

        public RecordingStream(Stream baseStream, string path, FileMode fileMode )
        {
            _baseStream = baseStream;
            _path = path;
            _fileMode = fileMode;
            _recording = false;
        }

        /// <summary>
        /// Turns recording on or off. This is a heavy operation - opens and closes the file. If you want to start and stop a lot, modify this method
        /// </summary>
        public bool IsRecording
        {
            get { return _recorder != null && _recording; }
            set { _recording = value; }
        }

        public void OpenRecord()
        {
            if (_recorder == null)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_path));
                _recorder = new FileStream(_path, _fileMode, FileAccess.Write);
            }
        }

        public void CloseRecord()
        {
            if ( _recorder != null)
            {
                _recording = false;
#if !NETSTANDARD1_3

                _recorder.Close();
#endif
                _recorder.Dispose();
                _recorder = null;
            }
        }
        public void WriteRecord( string s)
        {
            if (_recorder == null) return;
            if (!_recording) return;
            var b = Encoding.UTF8.GetBytes(s);
            _recorder.Write(b, 0, b.Length);
        }

        public void WriteRecordCRLF(string s)
        {
            if (_recorder == null) return;
            if (!_recording) return;
            WriteRecord(s);
            WriteRecord("\r\n"); 
        }


        public void FlushRecord()
        {
            if (_recorder == null) return;
            _recorder.Flush();

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
            CloseRecord();
            base.Dispose();
        }
    }
}