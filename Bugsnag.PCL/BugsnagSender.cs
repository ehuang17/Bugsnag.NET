﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Bugsnag.Common;
using Bugsnag.PCL.Request;
using Newtonsoft.Json;

namespace Bugsnag.PCL
{
    static class BugsnagSender
    {
        public static IClient Client { get; } = new BugsnagClient();

        [Obsolete("All this does is delegate to an IClient instance. Prefer going straight to that.")]
        public static Task<HttpResponseMessage> SendAsync(INotice notice) => Client.SendAsync(notice);

        [Obsolete("All this does is delegate to an IClient instance. Prefer going straight to that.")]
        public static Task<HttpResponseMessage> SendAsync(INotice notice, bool useSSL) => Client.SendAsync(notice, useSSL);
    }
}
