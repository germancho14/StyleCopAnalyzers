﻿// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.Settings
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.DocumentationRules;
    using StyleCop.Analyzers.Settings;
    using Xunit;
    using static StyleCop.Analyzers.Test.Verifiers.StyleCopCodeFixVerifier<
        StyleCop.Analyzers.DocumentationRules.FileHeaderAnalyzers,
        StyleCop.Analyzers.Settings.SettingsFileCodeFixProvider>;

    /// <summary>
    /// Unit tests for the <see cref="SettingsFileCodeFixProvider"/>.
    /// </summary>
    public class SettingsFileCodeFixProviderUnitTests
    {
        private const string TestCode = @"
namespace NamespaceName
{
}
";

        /// <summary>
        /// Verifies that a file without a header, but with leading trivia will produce the correct diagnostic message.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestMissingFileHeaderWithLeadingTriviaAsync()
        {
            await new CSharpTest
            {
                TestCode = TestCode,
                ExpectedDiagnostics = { Diagnostic(FileHeaderAnalyzers.SA1633DescriptorMissing).WithLocation(1, 1) },
                FixedCode = TestCode,
                RemainingDiagnostics = { Diagnostic(FileHeaderAnalyzers.SA1633DescriptorMissing).WithLocation(1, 1) },
                FixedAdditionalFiles =
                {
                    (SettingsHelper.SettingsFileName, SettingsFileCodeFixProvider.DefaultSettingsFileContent),
                },
                Settings = null,
            }.RunAsync(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that a code fix will be offered if the settings file does not exist.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestSettingsFileDoesNotExistAsync()
        {
            await new CSharpTest
            {
                TestCode = TestCode,
                ExpectedDiagnostics = { Diagnostic(FileHeaderAnalyzers.SA1633DescriptorMissing).WithLocation(1, 1) },
                FixedCode = TestCode,
                RemainingDiagnostics = { Diagnostic(FileHeaderAnalyzers.SA1633DescriptorMissing).WithLocation(1, 1) },
                FixedAdditionalFiles =
                {
                    (SettingsHelper.SettingsFileName, SettingsFileCodeFixProvider.DefaultSettingsFileContent),
                },
                Settings = null,
            }.RunAsync(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that a code fix will not be offered if the settings file is already present.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestSettingsFileAlreadyExistsAsync()
        {
            await new CSharpTest
            {
                TestCode = TestCode,
                ExpectedDiagnostics = { Diagnostic(FileHeaderAnalyzers.SA1633DescriptorMissing).WithLocation(1, 1) },
                FixedCode = TestCode,
                FixedState = { InheritanceMode = StateInheritanceMode.AutoInheritAll },
                Settings = "{}",
                SettingsFileName = SettingsHelper.SettingsFileName,
            }.RunAsync(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that a code fix will not be offered if the settings file is already present.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestDotPrefixedSettingsFileAlreadyExistsAsync()
        {
            await new CSharpTest
            {
                TestCode = TestCode,
                ExpectedDiagnostics = { Diagnostic(FileHeaderAnalyzers.SA1633DescriptorMissing).WithLocation(1, 1) },
                FixedCode = TestCode,
                FixedState = { InheritanceMode = StateInheritanceMode.AutoInheritAll },
                Settings = "{}",
                SettingsFileName = SettingsHelper.AltSettingsFileName,
            }.RunAsync(CancellationToken.None).ConfigureAwait(false);
        }
    }
}
