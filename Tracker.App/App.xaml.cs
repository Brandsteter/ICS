// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.App.Shells;

namespace Tracker.App;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHJqVk1mQ1JbdF9AXnNOdFdxT2Bbby8Nf1dGYl9RQXZbQlpnTnpRcUxmUQ==;Mgo+DSMBPh8sVXJ1S0R+X1pCaVxdX2NLfUNxT2dedV1yZCQ7a15RRnVfR11nSXlXcUFhUX5ceA==;ORg4AjUWIQA/Gnt2VFhiQlJPcEBBQmFJfFBmQmldeFRzdkU3HVdTRHRcQlhjTX5UckJnW3ddcXw=;MTkyMDYyNkAzMjMxMmUzMjJlMzNMTlhZV1doQTBMTndCaGc4TEkxU2c5RTZiZTMzaEpxUjZwWkZyTWYrcUFjPQ==;MTkyMDYyN0AzMjMxMmUzMjJlMzNoN0IveFhzYW9yOFlNSlY4ZXdNSjBBNzMwem9BTE9KM3FYUkMzcjRYSDA4PQ==;NRAiBiAaIQQuGjN/V0d+Xk9HfVldXnxLflF1VWRTe1l6d1dWESFaRnZdQV1mSXpTcUFkXX1XcHdd;MTkyMDYyOUAzMjMxMmUzMjJlMzNnMERqNzBpN1l5aEJhZ0NVanphWFcycFBQOTJ2cWpBVTNONCtPQko1S3JBPQ==;MTkyMDYzMEAzMjMxMmUzMjJlMzNTZ05CK0FhMmlPSFpKTWhPMzAxZE5QTnFjcW5LbGJid0YrVFB0VHVqVXJvPQ==;Mgo+DSMBMAY9C3t2VFhiQlJPcEBBQmFJfFBmQmldeFRzdkU3HVdTRHRcQlhjTX5UckJnW3ZYd3w=;MTkyMDYzMkAzMjMxMmUzMjJlMzNFazJmd1huaVZtR0p4UXNiUWF0R2kxRjRVa0pCeGpyWlBIelZFdnJuaVpJPQ==;MTkyMDYzM0AzMjMxMmUzMjJlMzNCbzlzeEc2ZEZnTGx1YzZkQkM4Zm15bDdwcUc1U0NJemZJSUF6eXYzV2lZPQ==;MTkyMDYzNEAzMjMxMmUzMjJlMzNnMERqNzBpN1l5aEJhZ0NVanphWFcycFBQOTJ2cWpBVTNONCtPQko1S3JBPQ==");
        InitializeComponent();

        MainPage = serviceProvider.GetRequiredService<AppShell>();
    }
}
