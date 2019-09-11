using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace AssemblyInfoResourcePackage
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0")]
    [Guid(PackageGuidString)]
    public sealed class AssemblyInfoResourcePackage : AsyncPackage
    {
        public const string PackageGuidString = "7AD7241F-53E9-40A0-B14B-65078106CD9B";

        protected override Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
            => base.InitializeAsync(cancellationToken, progress);
    }
}
