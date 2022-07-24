// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;

namespace System.Text.RegularExpressions
{
    [Obsolete(Obsoletions.RegexCompileToAssemblyMessage
#if NET5_0_OR_GREATER
        ,DiagnosticId = Obsoletions.RegexCompileToAssemblyDiagId
        ,UrlFormat = Obsoletions.SharedUrlFormat
#endif
        )]
    public class RegexCompilationInfo
    {
        private string _pattern;
        private string _name;
        private string _nspace;

        private TimeSpan _matchTimeout;

        public RegexCompilationInfo(string pattern, RegexOptions options, string name, string fullnamespace, bool ispublic)
            : this(pattern, options, name, fullnamespace, ispublic, Regex.s_defaultMatchTimeout)
        {
        }

        public RegexCompilationInfo(string pattern, RegexOptions options, string name, string fullnamespace, bool ispublic, TimeSpan matchTimeout)
        {
            Pattern = pattern;
            Name = name;
            Namespace = fullnamespace;
            Options = options;
            IsPublic = ispublic;
            MatchTimeout = matchTimeout;
        }

        public bool IsPublic { get; set; }

        public TimeSpan MatchTimeout
        {
            get => _matchTimeout;
            set
            {
                Regex.ValidateMatchTimeout(value);
                _matchTimeout = value;
            }
        }

        public string Name
        {
            get => _name;
#if NET5_0_OR_GREATER
            [MemberNotNull(nameof(_name))]
#endif
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(Name));
                }
                _name = value;
            }
        }

        public string Namespace
        {
            get => _nspace;
#if NET5_0_OR_GREATER
            [MemberNotNull(nameof(_nspace))]
#endif
            set
            {
#if NET6_0_OR_GREATER
                ArgumentNullException.ThrowIfNull(value, nameof(Namespace));
                _pattern = value;
#else
                _pattern = value ?? throw new ArgumentNullException(nameof(Namespace));
#endif
            }
        }

        public RegexOptions Options { get; set; }

        public string Pattern
        {
            get => _pattern;
#if NET5_0_OR_GREATER
            [MemberNotNull(nameof(_pattern))]
#endif
            set
            {
#if NET6_0_OR_GREATER
                ArgumentNullException.ThrowIfNull(value, nameof(Pattern));
                _pattern = value;
#else
                _pattern = value ?? throw new ArgumentNullException(nameof(Pattern));
#endif
            }
        }
    }
}
