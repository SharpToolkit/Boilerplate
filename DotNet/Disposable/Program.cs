using System;

namespace Disposable
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dispose way.\r\n");

            using (var resource2 = new MyClass())
            {

            }

            Console.WriteLine("\r\n\r\nFinalizer way.\r\n");

            var resource = new MyClass();
            resource     = null;

            GC.Collect();

            Console.ReadKey();
        }
    }

    class MyClass : IDisposable
    {
        private bool disposed = false;

        protected virtual void DisposeManaged()
        {
            // Free any managed resources here, e.g:
            // this.resource.Dispose();

            Console.WriteLine("Successfully disposed managed resources.");
        }
        protected virtual void DisposeUnmanaged()
        {
            // Free any unmanaged resources here, e.g:
            // this.CloseHandle(this.handle);
            // This code may be executed by finalizer, 
            // so it MUST NOT reference any other objects,
            // since those might already be collected.

            Console.WriteLine("Successfully disposed unmanaged resources.");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                DisposeManaged();
            }

            DisposeUnmanaged();

            disposed = true;
        }

        ~MyClass()
        {
            Console.WriteLine("Finalizer called.");
            ReportFinalization();
            Dispose(false);
        }
        
        /// <summary>
        /// Use this if you want the finalizer calls to be reported.
        /// </summary>
        protected virtual void ReportFinalization()
        {
            Console.WriteLine("Object finalization is most likely a bug.");
        }
    }
}