﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UnitTest.Tcp
{
    using System.Threading.Tasks;
    using Bond;
    using Bond.Comm.Tcp;
    using Bond.IO.Safe;
    using Bond.Protocols;
    using NUnit.Framework;

    [TestFixture]
    public class TcpConnectionTests
    {
        private const ProtocolErrorCode MeaninglessErrorCode = ProtocolErrorCode.GENERIC_ERROR;

        [Test]
        public void MakeProtocolErrorFrame_MakesAFrame()
        {
            var frame = TcpConnection.MakeProtocolErrorFrame(MeaninglessErrorCode);
            Assert.NotNull(frame);
            Assert.AreEqual(1, frame.Framelets.Count);
            Assert.AreEqual(FrameletType.ProtocolError, frame.Framelets[0].Type);

            var inputBuffer = new InputBuffer(frame.Framelets[0].Contents);
            var fastBinaryReader = new FastBinaryReader<InputBuffer>(inputBuffer, version: 1);
            var error = Deserialize<ProtocolError>.From(fastBinaryReader);
            Assert.AreEqual(MeaninglessErrorCode, error.error_code);
        }
    }
}
