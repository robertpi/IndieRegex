// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;
using System.Reflection.Emit;

namespace IndieSystem.Text.RegularExpressions
{
    internal sealed class CompiledRegexRunnerFactory : RegexRunnerFactory
    {
        private readonly DynamicMethod _scanMethod;
        /// <summary>This field will only be set if the pattern has backreferences and uses RegexOptions.IgnoreCase</summary>
        private readonly CultureInfo? _culture;

        // Delegate is lazily created to avoid forcing JIT'ing until the regex is actually executed.
        private CompiledRegexRunner.ScanDelegate? _scan;

        public CompiledRegexRunnerFactory(DynamicMethod scanMethod, CultureInfo? culture)
        {
            _scanMethod = scanMethod;
            _culture = culture;
        }

        protected internal override RegexRunner CreateInstance() =>
            new CompiledRegexRunner(
                _scan ??=
#if NET5_0_OR_GREATER
                    _scanMethod.CreateDelegate<CompiledRegexRunner.ScanDelegate>()
#else
                    (CompiledRegexRunner.ScanDelegate)_scanMethod.CreateDelegate(typeof(CompiledRegexRunner.ScanDelegate))
#endif

                , _culture);
    }
}
