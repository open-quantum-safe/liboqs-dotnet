using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OpenQuantumSafe
{
    /// <summary>
    /// Offers post-quantum Key Encapsulation Mechanisms (KEM).
    /// </summary>
    public class KEM : Mechanism
    {
        /// <summary>
        /// A structure corresponding to the native OQS_KEM struct.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct OQS_KEM
        {
            public IntPtr method_name;
            public IntPtr alg_version;
            public byte claimed_nist_level;
            public byte ind_cca;
            public UIntPtr length_public_key;
            public UIntPtr length_secret_key;
            public UIntPtr length_ciphertext;
            public UIntPtr length_shared_secret;
            private IntPtr keypair_function; // unused
            private IntPtr encaps_function; // unused
            private IntPtr decaps_function; // unused
        }

        #region OQS native DLL functions
        [DllImport("oqs", CallingConvention = CallingConvention.Cdecl)]
        extern private static IntPtr OQS_KEM_new(string method_name);

        [DllImport("oqs", CallingConvention = CallingConvention.Cdecl)]
        extern private static int OQS_KEM_keypair(IntPtr kem, byte[] public_key, byte[] secret_key);

        [DllImport("oqs", CallingConvention = CallingConvention.Cdecl)]
        extern private static int OQS_KEM_encaps(IntPtr kem, byte[] ciphertext, byte[] shared_secret, byte[] public_key);

        [DllImport("oqs", CallingConvention = CallingConvention.Cdecl)]
        extern private static int OQS_KEM_decaps(IntPtr kem, byte[] shared_secret, byte[] ciphertext, byte[] secret_key);

        [DllImport("oqs", CallingConvention = CallingConvention.Cdecl)]
        extern private static void OQS_KEM_free(IntPtr kem);

        [DllImport("oqs", CallingConvention = CallingConvention.Cdecl)]
        extern private static IntPtr OQS_KEM_alg_identifier(int index);

        [DllImport("oqs", CallingConvention = CallingConvention.Cdecl)]
        extern private static int OQS_KEM_alg_count();

        [DllImport("oqs", CallingConvention = CallingConvention.Cdecl)]
        extern private static int OQS_KEM_alg_is_enabled(string method_name);
        #endregion

        /// <summary>
        /// List of supported mechanisms. Some mechanisms might have been disabled at runtime,
        /// see <see cref="EnableddMechanisms"/> for the list of enabled mechanisms.
        /// </summary>
        public static List<string> SupportedMechanisms { get; private set; }

        /// <summary>
        /// List of enabled mechanisms.
        /// </summary>
        public static List<string> EnabledMechanisms { get; protected set; }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static KEM()
        {
            // initialize list of supported/enabled mechanisms
            EnabledMechanisms = new List<string>();
            SupportedMechanisms = new List<string>();

            int count = OQS_KEM_alg_count();
            for (int i = 0; i < count; i++)
            {
                string alg = Marshal.PtrToStringAnsi(OQS_KEM_alg_identifier(i));
                SupportedMechanisms.Add(alg);
                // determine if the alg is enabled
                if (OQS_KEM_alg_is_enabled(alg) == 1)
                {
                    EnabledMechanisms.Add(alg);
                }
            }
        }

        /// <summary>
        /// The OQS KEM context.
        /// </summary>
        private OQS_KEM oqs_kem;

        /// <summary>
        /// Constructs a KEM object.
        /// </summary>
        /// <param name="kemAlg">An OQS KEM algorithm identifier; must be a value from <see cref="EnabledMechanisms"/>.</param>
        /// <exception cref="MechanismNotEnabledException">Thrown if the algorithm is supported but not enabled by the OQS runtime.</exception>
        /// <exception cref="MechanismNotSupportedException">Thrown if the algorithm is not supported by OQS.</exception>
        /// <exception cref="OQSException">Thrown if the OQS KEM object can't be initialized.</exception>
        public KEM(string kemAlg)
        {
            if (!SupportedMechanisms.Contains(kemAlg))
            {
                throw new MechanismNotSupportedException(kemAlg);
            }
            if (!EnabledMechanisms.Contains(kemAlg))
            {
                throw new MechanismNotEnabledException(kemAlg);
            }
            oqs_ptr = OQS_KEM_new(kemAlg);
            if (oqs_ptr == IntPtr.Zero)
            {
                throw new OQSException("Failed to initialize KEM mechanism");
            }
            oqs_kem = Marshal.PtrToStructure<OQS_KEM>(oqs_ptr);
        }

        /// <summary>
        /// The KEM algorithm name.
        /// </summary>
        public string AlgorithmName
        {
            get
            {
                return Marshal.PtrToStringAnsi(oqs_kem.method_name);
            }
        }

        /// <summary>
        /// The algorithm version.
        /// </summary>
        public string AlgorithmVersion
        {
            get
            {
                return Marshal.PtrToStringAnsi(oqs_kem.alg_version);
            }
        }

        /// <summary>
        /// The claimed NIST security level.
        /// </summary>
        public int ClaimedNistLevel
        {
            get
            {
                return oqs_kem.claimed_nist_level;
            }
        }

        /// <summary>
        /// <code>true</code> if the scheme is IND-CCA, <code>false</code> otherwise.
        /// </summary>
        public bool IsIndCCA
        {
            get
            {
                return oqs_kem.ind_cca == 1;
            }
        }

        /// <summary>
        /// Length of the public key.
        /// </summary>
        public UInt64 PublicKeyLength
        {
            get
            {
                return (UInt64) oqs_kem.length_public_key;
            }
        }

        /// <summary>
        /// Length of the secret key.
        /// </summary>

        public UInt64 SecretKeyLength
        {
            get
            {
                return (UInt64) oqs_kem.length_secret_key;
            }
        }

        /// <summary>
        /// Length of the ciphertext.
        /// </summary>

        public UInt64 CiphertextLength
        {
            get
            {
                return (UInt64) oqs_kem.length_ciphertext;
            }
        }

        /// <summary>
        /// Length of the shared secret.
        /// </summary>

        public UInt64 SharedSecretLength
        {
            get
            {
                return (UInt64) oqs_kem.length_shared_secret;
            }
        }


        /// <summary>
        /// Generates the KEM keypair.
        /// </summary>
        /// <param name="public_key">The returned public key.</param>
        /// <param name="secret_key">The returned secret key.</param>
        public void keypair(out byte[] public_key, out byte[] secret_key)
        {
            if (oqs_ptr == IntPtr.Zero)
            {
                throw new ObjectDisposedException("KEM");
            }

            byte[] my_public_key = new byte[PublicKeyLength];
            byte[] my_secret_key = new byte[SecretKeyLength];
            int returnValue = OQS_KEM_keypair(oqs_ptr, my_public_key, my_secret_key);
            if (returnValue != OpenQuantumSafe.Status.Success)
            {
                throw new OQSException(returnValue);
            }
            public_key = my_public_key;
            secret_key = my_secret_key;
        }

        /// <summary>
        /// Encapsulates the shared secret.
        /// </summary>
        /// <param name="ciphertext">The returned ciphertext.</param>
        /// <param name="shared_secret">The returned shared secret.</param>
        /// <param name="public_key">The target public key.</param>
        public void encaps(out byte[] ciphertext, out byte[] shared_secret, byte[] public_key)
        {
            if (oqs_ptr == IntPtr.Zero)
            {
                throw new ObjectDisposedException("KEM");
            }

            byte[] my_ciphertext = new byte[CiphertextLength];
            byte[] my_shared_secret = new byte[SharedSecretLength];
            int returnValue = OQS_KEM_encaps(oqs_ptr, my_ciphertext, my_shared_secret, public_key);
            if (returnValue != OpenQuantumSafe.Status.Success)
            {
                throw new OQSException(returnValue);
            }
            ciphertext = my_ciphertext;
            shared_secret = my_shared_secret;
        }

        /// <summary>
        /// Decapsulates the shared secret.
        /// </summary>
        /// <param name="shared_secret">The returned shared secret.</param>
        /// <param name="ciphertext">The reeceived ciphertext.</param>
        /// <param name="secret_key">The party's secret key.</param>
        public void decaps(out byte[] shared_secret, byte[] ciphertext, byte[] secret_key)
        {
            if (oqs_ptr == IntPtr.Zero)
            {
                throw new ObjectDisposedException("KEM");
            }

            byte[] my_shared_secret = new byte[SharedSecretLength];
            int returnValue = OQS_KEM_decaps(oqs_ptr, my_shared_secret, ciphertext, secret_key);
            if (returnValue != OpenQuantumSafe.Status.Success)
            {
                throw new OQSException(returnValue);
            }
            shared_secret = my_shared_secret;
        }

        protected override void OQS_free(IntPtr oqs_ptr)
        {
            OQS_KEM_free(oqs_ptr);
        }
    }
}
