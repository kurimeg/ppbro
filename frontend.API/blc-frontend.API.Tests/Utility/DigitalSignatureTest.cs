﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using frontend.API.Utility;

namespace blc_frontend.API.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSignVerify()
        {
            DigitalSignature ds = DigitalSignature.Generate();
            var priKey = ds.PrivateKey;
            var pubKey = ds.PublicKey;

            var value = "32b6ae2a-ade9-4808-ba47-196601497af6";

            var enc = EncodingUtil.GetEncoding();

            var rawSign = ds.Sign(enc.GetBytes(value));
            var sign = Convert.ToBase64String(rawSign);

            var v = ds.Verify(enc.GetBytes(value), rawSign);

            DigitalSignature verifier = DigitalSignature.FromKey(pubKey);
            var verified = verifier.Verify(enc.GetBytes(value), Convert.FromBase64String(sign));

            Assert.AreEqual(true, verifier.Verify(enc.GetBytes(value), Convert.FromBase64String(sign)));
        }
    }
}
