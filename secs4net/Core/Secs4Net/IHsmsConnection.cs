﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Secs4Net
{
    public interface IHsmsConnection
    {
        public event EventHandler<ConnectionState>? ConnectionChanged { add { } remove { } }

        /// <summary>
        /// Connection state
        /// </summary>
        ConnectionState State => ConnectionState.Selected;

        /// <summary>
        /// Is Active or Passive mode
        /// </summary>
        bool IsActive => false;

        /// <summary>
        /// When <see cref="IsActive">IsActive</see> is <see langword="true"/> the IP address will be treated remote device's IP address, 
        /// opposite the connection will bind on this IP address as Passive mode.
        /// </summary>
        IPAddress IpAddress => IPAddress.Loopback;

        /// <summary>
        /// When <see cref="IsActive">IsActive</see> is <see langword="true"/> the port number will be treated remote device's TCP port number, 
        /// opposite the connection will bind on the port number as Passive mode.
        /// </summary>
        int Port => 0;

        /// <summary>
        /// Remote device's IP address.<br/>
        /// In Active mode, it is the same as IPAddress property<br/>
        /// In Passive mode, remote IP Address can be resolved successfully only 
        /// when <see cref="State">State</see> is <see cref="ConnectionState.Connected">Connected</see> or <see cref="ConnectionState.Selected">Connected</see>, 
        /// otherwise, return "N/A"
        /// </summary>
        string DeviceIpAddress => string.Empty;

        bool LinkTestEnabled { get; set; }

        void Reconnect() { }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The number of bytes sent to</returns>
        internal ValueTask<int> SendAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken);
        internal PipeDecoder PipeDecoder { get; }
    }
}
