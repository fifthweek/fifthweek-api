namespace Fifthweek.WebJobs.Thumbnails.Tests
{
    using System;
    using System.IO;
    using System.Threading;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class MockCloudBlobStream : CloudBlobStream
    {
        private readonly MemoryStream stream = new MemoryStream();

        public byte[] ToArray()
        {
            return this.stream.ToArray();
        }

        public override void Flush()
        {
            this.stream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.stream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.stream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.stream.Write(buffer, offset, count);
        }

        public bool IsCommitted { get; private set; }

        public override bool CanRead
        {
            get
            {
                return this.stream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return this.stream.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return this.stream.CanWrite;
            }
        }

        public override long Length
        {
            get
            {
                return this.stream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return this.stream.Position;
            }

            set
            {
                this.stream.Position = value;
            }
        }

        public override void Commit()
        {
            this.IsCommitted = true;
            this.stream.Flush();
        }

        public override ICancellableAsyncResult BeginCommit(AsyncCallback callback, object state)
        {
            this.IsCommitted = true;
            this.stream.Flush();
            return new MockAsyncResult();
        }

        public override void EndCommit(IAsyncResult asyncResult)
        {
        }

        public override ICancellableAsyncResult BeginFlush(AsyncCallback callback, object state)
        {
            this.stream.Flush();
            return new MockAsyncResult();
        }

        public override void EndFlush(IAsyncResult asyncResult)
        {
        }

        private class MockAsyncResult : ICancellableAsyncResult
        {
            public MockAsyncResult()
            {
                this.IsCompleted = true;
                this.CompletedSynchronously = true;
            }

            public bool IsCompleted { get; private set; }

            public WaitHandle AsyncWaitHandle { get; private set; }

            public object AsyncState { get; private set; }

            public bool CompletedSynchronously { get; private set; }

            public void Cancel()
            {
            }
        }
    }
}