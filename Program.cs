using System;
using NuGet.Frameworks;

using static NuGet.Frameworks.FrameworkConstants.CommonFrameworks;
using System.Runtime.Versioning;
using System.Linq;
using System.Collections.Generic;

namespace NuGetPackageInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: TargetFramework NuGetFramework1 [NuGetFramework2 ...]");
                return;
            }

            Run(new FrameworkName(args[0]), args.Skip(1));
        }

        private static void Run(FrameworkName frameworkName, IEnumerable<string> nugetProfiles)
        {
            var target = NuGetFramework.ParseFrameworkName(frameworkName.FullName, DefaultFrameworkNameProvider.Instance);
            var available = nugetProfiles.Select(NuGetFramework.ParseFolder).ToArray();

            Console.WriteLine($"From: {target}");
            Console.WriteLine("Available:");
            foreach (var profile in available)
            {
                Console.WriteLine($"\t{profile}");
            }

            var reducer = new FrameworkReducer();
            var result = reducer.GetNearest(target, available);

            if (result == null)
            {
                Console.WriteLine("No available framework");
            }
            else
            {
                Console.WriteLine($"Best framework: {result}");
            }
        }
    }
}
