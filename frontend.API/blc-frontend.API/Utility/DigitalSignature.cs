﻿using System.Linq;
using System.Security.Cryptography;

namespace frontend.API.Utility
{
    /// <summary>
    /// 思いっきり https://qiita.com/yoship1639/items/6dd0cc8623d7f3969d78
    /// </summary>
    public class DigitalSignature
    {
        private ECDsa dsa;
        public byte[] PrivateKey { get; private set; }
        public byte[] PublicKey { get; private set; }

        private DigitalSignature(byte[] privateKey, byte[] publicKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        public static DigitalSignature Generate()
        {
            var dsa = ECDsa.Create("ECDsaCng");
            dsa.GenerateKey(ECCurve.CreateFromFriendlyName("secp256k1"));
            var param = dsa.ExportParameters(true);

            var ds = new DigitalSignature(param.D, param.Q.X.Concat(param.Q.Y).ToArray());
            ds.dsa = dsa;

            return ds;
        }

        public static DigitalSignature FromKey(byte[] publicKey)
        {
            if (publicKey == null || publicKey.Length != 64) return null;

            var param = new ECParameters()
            {
                Curve = ECCurve.CreateFromFriendlyName("secp256k1"),
                Q = new ECPoint()
                {
                    X = publicKey.Take(32).ToArray(),
                    Y = publicKey.Skip(32).Take(32).ToArray(),
                },
            };

            var ds = new DigitalSignature(null, publicKey);

            try
            {
                ds.dsa = ECDsa.Create(param);
            }
            catch
            {
                return null;
            }

            return ds;
        }

        public static DigitalSignature FromKey(byte[] privateKey, byte[] publicKey)
        {
            if (privateKey == null || privateKey.Length != 32) return null;
            if (publicKey == null || publicKey.Length != 64) return null;

            var param = new ECParameters()
            {
                Curve = ECCurve.CreateFromFriendlyName("secp256k1"),
                D = privateKey,
                Q = new ECPoint()
                {
                    X = publicKey.Take(32).ToArray(),
                    Y = publicKey.Skip(32).Take(32).ToArray(),
                },
            };

            var ds = new DigitalSignature(privateKey, publicKey);

            ds.dsa = ECDsa.Create(param);

            return ds;
        }


        public byte[] Sign(byte[] data)
        {
            return dsa.SignData(data, HashAlgorithmName.SHA256);
        }

        public bool Verify(byte[] data, byte[] sign)
        {
            return dsa.VerifyData(data, sign, HashAlgorithmName.SHA256);
        }

        //TODO
        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            return data;
        }

        //TODO
        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            return data;
        }
    }
}
