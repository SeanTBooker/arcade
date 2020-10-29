// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Build.Framework;
using System.IO;

namespace Microsoft.DotNet.Build.Tasks.SharedFramework.Sdk
{
    public partial class GenerateDebRepoUploadJsonFile : BuildTask
    {
        private const string _debianRevisionNumber = "1";
        [Required]
        public string RepoId { get; set; }
        [Required]
        public string UploadJsonFilename { get; set; }
        [Required]
        public string PackageName { get; set; }
        [Required]
        public string PackageVersion { get; set; }
        [Required]
        public string UploadUrl { get; set; }

        public override bool ExecuteCore()
        {
            File.Delete(UploadJsonFilename);

            using (var fileStream = File.Create(UploadJsonFilename))
            {
                using (StreamWriter sw = new StreamWriter(fileStream))
                {
                    sw.WriteLine("{");
                    sw.WriteLine($"  \"name\":\"{PackageName}\",");
                    sw.WriteLine($"  \"version\":\"{PackageVersion}-{_debianRevisionNumber}\",");
                    sw.WriteLine($"  \"repositoryId\":\"{RepoId}\",");
                    sw.WriteLine($"  \"sourceUrl\":\"{UploadUrl}\"");
                    sw.WriteLine("}");
                }
            }
            return !Log.HasLoggedErrors;
        }
    }
}