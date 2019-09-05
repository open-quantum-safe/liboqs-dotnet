using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQuantumSafe;
using System.Linq;
using System.Collections.Generic;

namespace dotnetOQSUnitTest
{
    [TestClass]
    public class KEMTests
    {
        private static void log(string message)
        {
            Console.WriteLine(message);
        }

        private static string BytesToHex(byte[] b)
        {
            return BitConverter.ToString(b).Replace("-", "");
        }

        private static void TestKEM(string kemAlg)
        {
            byte[] public_key;
            byte[] secret_key;
            byte[] ciphertext;
            byte[] shared_secret_1;
            byte[] shared_secret_2;
            Random random = new Random();

            // successful case
            KEM kem = new KEM(kemAlg);
            if (kemAlg != "DEFAULT")
            {
                Assert.AreEqual(kem.AlgorithmName, kemAlg);
            }
            Assert.IsTrue(kem.IsUsable, "IsUsable after constructor");

            kem.keypair(out public_key, out secret_key);
            log("public_key: " + BytesToHex(public_key));
            log("secret_key: " + BytesToHex(secret_key));
            Assert.IsTrue((UInt64)public_key.Length <= kem.PublicKeyLength, "public key length");
            Assert.IsTrue((UInt64)secret_key.Length <= kem.SecretKeyLength, "secret key length");

            kem.encaps(out ciphertext, out shared_secret_1, public_key);
            log("ciphertext: " + BytesToHex(ciphertext));
            log("shared_secret_1: " + BytesToHex(shared_secret_1));
            Assert.IsTrue((UInt64)ciphertext.Length <= kem.CiphertextLength, "ciphertext length");
            Assert.IsTrue((UInt64)shared_secret_1.Length <= kem.SharedSecretLength, "shared secret length");
            kem.decaps(out shared_secret_2, ciphertext, secret_key);
            log("shared_secret_2: " + BytesToHex(shared_secret_2));
            Assert.IsTrue(shared_secret_1.SequenceEqual(shared_secret_2), "shared secrets are not equal");

            // failure cases

            // wrong ciphertext
            byte[] wrong_ciphertext = new byte[ciphertext.Length];
            random.NextBytes(wrong_ciphertext);
            log("wrong_ciphertext: " + BytesToHex(wrong_ciphertext));
            try {
                kem.decaps(out shared_secret_2, wrong_ciphertext, secret_key);
                // if the wrong value didn't trigger an exception, make sure the shared secret do not match
                Assert.IsFalse(shared_secret_1.SequenceEqual(shared_secret_2), "wrong ciphertext, shared secrets should have been different");
            }
            catch (OQSException)
            {
                // exception expected
            }

            // wrong secret key
            byte[] wrong_secret_key = new byte[secret_key.Length];
            random.NextBytes(wrong_secret_key);
            log("wrong_secret_key: " + BytesToHex(wrong_secret_key));
            try
            {
                kem.decaps(out shared_secret_2, ciphertext, wrong_secret_key);
                // if the wrong value didn't trigger an exception, make sure the shared secret do not match
                Assert.IsFalse(shared_secret_1.SequenceEqual(shared_secret_2), "wrong ciphertext, shared secrets should have been different");
            }
            catch (OQSException)
            {
                // exception expected
            }

            // clean-up
            kem.Dispose();
            Assert.IsFalse(kem.IsUsable, "IsUsable after cleanup");
        }

        [TestMethod]
        public void TestKEMOQSDefault()
        {
            TestKEM("DEFAULT");
        }

        [TestMethod]
        public void TestAllKEMs()
        {
            var failedAlgs = new List<string>();
            foreach (string kem in KEM.EnabledMechanisms)
            {
                try {
                    TestKEM(kem);
                }
                catch (Exception)
                {
                    failedAlgs.Add(kem);
                }

                Assert.IsTrue(failedAlgs.Count == 0, string.Join(", ", failedAlgs));
            }
        }

        [TestMethod]
        public void TestKEMNotSupported()
        {
            Assert.ThrowsException<MechanismNotSupportedException>(() => new KEM("bogus"));
        }

        [TestMethod]
        public void TestKEMNotEnabled()
        {
            // find a supported-but-not-enabled mechanism
            foreach (string supported in KEM.SupportedMechanisms)
            {
                if (!KEM.EnabledMechanisms.Contains(supported))
                {
                    // found one
                    Assert.ThrowsException<MechanismNotEnabledException>(() => new KEM(supported));
                }
            }
        }
    }
}