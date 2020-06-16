using System;
using OpenQuantumSafe;
using System.Linq;
using System.Collections.Generic;

namespace dotnetOQSSample
{
    class Program
    {
        static void example_kem()
        {
            Console.WriteLine("Starting KEM example");

            // get the list of enabled KEM mechanisms
            Console.WriteLine("Enabled KEM mechanisms: ");
            foreach (string alg in KEM.EnabledMechanisms)
            {
                Console.WriteLine(" - " + alg);
            }

            // Instantiate the client and server
            using (KEM client = new KEM("DEFAULT"),
                       server = new KEM("DEFAULT"))
            {
                Console.WriteLine("Perform key exchange with DEFAULT mechanism");

                // Print out some info about the mechanism
                Console.WriteLine("Mechanism details:");
                Console.WriteLine(" - Alg name: " + client.AlgorithmName);
                Console.WriteLine(" - Alg version: " + client.AlgorithmVersion);
                Console.WriteLine(" - Claimed NIST level: " + client.ClaimedNistLevel);
                Console.WriteLine(" - Is IND-CCA?: " + client.IsIndCCA);
                Console.WriteLine(" - Secret key length: " + client.SecretKeyLength);
                Console.WriteLine(" - Public key length: " + client.PublicKeyLength);
                Console.WriteLine(" - Ciphertext length: " + client.CiphertextLength);
                Console.WriteLine(" - Shared secret length: " + client.SharedSecretLength);

                // Generate the client's key pair
                byte[] public_key;
                byte[] secret_key;
                client.keypair(out public_key, out secret_key);

                // The client sends the public key to the server

                // The server generates and encapsulates the shared secret
                byte[] ciphertext;
                byte[] server_shared_secret;
                server.encaps(out ciphertext, out server_shared_secret, public_key);

                // The server sends the ciphertext to the client

                // The client decapsulates the shared secret
                byte[] client_shared_secret;
                client.decaps(out client_shared_secret, ciphertext, secret_key);

                // The client and server now share a secret. Let's make sure it matches.
                if (server_shared_secret.SequenceEqual(client_shared_secret))
                {
                    Console.WriteLine("KEM completed successfully");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Error: shared secrets do not match!");
                }
            }
        }

        static void example_sig()
        {
            Console.WriteLine("Starting signature example");

            // get the list of enabled signature mechanisms
            Console.WriteLine("Enabled signature mechanisms: ");
            foreach (string alg in Sig.EnabledMechanisms)
            {
                Console.WriteLine(" - " + alg);
            }

            // Instantiate the signer and verifier
            using (Sig signer = new Sig("DEFAULT"),
                       verifier = new Sig("DEFAULT"))
            {
                Console.WriteLine("Sign with DEFAULT mechanism");

                // Print out some info about the mechanism
                Console.WriteLine("Mechanism details:");
                Console.WriteLine(" - Alg name: " + signer.AlgorithmName);
                Console.WriteLine(" - Alg version: " + signer.AlgorithmVersion);
                Console.WriteLine(" - Claimed NIST level: " + signer.ClaimedNistLevel);
                Console.WriteLine(" - Is EUF_CMA?: " + signer.IsEufCma);
                Console.WriteLine(" - Secret key length: " + signer.SecretKeyLength);
                Console.WriteLine(" - Public key length: " + signer.PublicKeyLength);
                Console.WriteLine(" - Signature length: " + signer.SignatureLength);

                // The message to sign
                byte[] message = new System.Text.UTF8Encoding().GetBytes("Message to sign");

                // Generate the signer's key pair
                byte[] public_key;
                byte[] secret_key;
                signer.keypair(out public_key, out secret_key);

                // The signer sends the public key to the verifier

                // The signer signs the message
                byte[] signature;
                signer.sign(out signature, message, secret_key);

                // The signer sends the signature to the verifier

                // The verifier verifies the signature
                if (verifier.verify(message, signature, public_key))
                { 
                    Console.WriteLine("Signature verification succeeded");
                }
                else 
                {
                    Console.WriteLine("Signature verification failed");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            example_kem();
            example_sig();
        }
    }
}