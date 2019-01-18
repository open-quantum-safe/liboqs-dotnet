using System;

namespace OpenQuantumSafe
{
    /// <summary>
    /// Base class for OQS mechanisms.
    /// </summary>
    public abstract class Mechanism : IDisposable
    {
        /// <summary>
        /// Handle to the OQS context.
        /// </summary>
        protected IntPtr oqs_ptr;

        protected abstract void OQS_free(IntPtr oqs_ptr);

        /// <summary>
        /// Frees the OQS context if initialized.
        /// </summary>
        protected void free_oqs_ctx()
        {
            if (oqs_ptr != IntPtr.Zero)
            {
                OQS_free(oqs_ptr);
                oqs_ptr = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Indicates if this class is usable (that is to say if <see cref="Dispose"/> has been called).
        /// </summary>
        public bool IsUsable
        {
            get {
                return oqs_ptr != IntPtr.Zero;
            }
        }

        /// <summary>
        /// Disposes of the native OQS resources. This renders the class 
        /// unusable.
        /// </summary>
        public void Dispose()
        {
            if (oqs_ptr != IntPtr.Zero)
            {
                OQS_free(oqs_ptr);
                oqs_ptr = IntPtr.Zero;
            }
        }
    }
}
