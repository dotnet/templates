using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace Microsoft.VisualStudio.Templates.AssemblyInfo.Wizard
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true, RegisterUsing = RegistrationMethod.CodeBase)]
    [Guid(PackageGuidString)]
    public sealed class WizardPackage : AsyncPackage
    {
        public const string PackageGuidString = "e4051b64-1bd7-4198-a278-120a74a86009";

        protected override Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
            => base.InitializeAsync(cancellationToken, progress);
    }
}
