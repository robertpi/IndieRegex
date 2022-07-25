using SysRegex = System.Text.RegularExpressions.Regex;
using SysRegexOptions = System.Text.RegularExpressions.RegexOptions;
using IndieSysRegex = IndieSystem.Text.RegularExpressions.Regex;
using IndieSysRegexOptions = IndieSystem.Text.RegularExpressions.RegexOptions;
using BenchmarkDotNet.Attributes;
using System;

public class AsmRegexBenchmark
{
    private const string ObsfucationRegex = @"(?i)(?:p(?:ass)?w(?:or)?d|pass(?:_?phrase)?|secret|(?:api_?|private_?|public_?|access_?|secret_?)key(?:_?id)?|token|consumer_?(?:id|key|secret)|sign(?:ed|ature)?|auth(?:entication|orization)?)(?:(?:\s|%20)*(?:=|%3D)[^&]+|(?:""|%22)(?:\s|%20)*(?::|%3A)(?:\s|%20)*(?:""|%22)(?:%2[^2]|%[^2]|[^""%])+(?:""|%22))|bearer(?:\s|%20)+[a-z0-9\._\-]|token(?::|%3A)[a-z0-9]{13}|gh[opsu]_[0-9a-zA-Z]{36}|ey[I-L](?:[\w=-]|%3D)+\.ey[I-L](?:[\w=-]|%3D)+(?:\.(?:[\w.+\/=-]|%3D|%2F|%2B)+)?|[\-]{5}BEGIN(?:[a-z\s]|%20)+PRIVATE(?:\s|%20)KEY[\-]{5}[^\-]+[\-]{5}END(?:[a-z\s]|%20)+PRIVATE(?:\s|%20)KEY|ssh-rsa(?:\s|%20)*(?:[a-z0-9\/\.+]|%2F|%5C|%2B){100,}";
    private const string ReplaceString = "<redacted>";
    private static readonly string InputString = new('a', 2000);
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(1);

    private static readonly IndieSysRegex backtracking = new(ObsfucationRegex, IndieSysRegexOptions.IgnoreCase | IndieSysRegexOptions.Compiled);
    private static readonly IndieSysRegex backtrackingTimeout = new(ObsfucationRegex, IndieSysRegexOptions.IgnoreCase | IndieSysRegexOptions.Compiled, Timeout);
    private static readonly IndieSysRegex nonBacktracking = new(ObsfucationRegex, IndieSysRegexOptions.IgnoreCase | IndieSysRegexOptions.Compiled | IndieSysRegexOptions.NonBacktracking);
    private static readonly IndieSysRegex nonBacktrackingTimeout = new(ObsfucationRegex, IndieSysRegexOptions.IgnoreCase | IndieSysRegexOptions.Compiled | IndieSysRegexOptions.NonBacktracking, Timeout);

    private static readonly SysRegex native = new(ObsfucationRegex, SysRegexOptions.IgnoreCase | SysRegexOptions.Compiled);
    private static readonly SysRegex nativeTimeout = new(ObsfucationRegex, SysRegexOptions.IgnoreCase | SysRegexOptions.Compiled, Timeout);

    [Benchmark]
    public void Backtracking()
    {
        backtracking.Replace(InputString, ReplaceString);
    }

    [Benchmark]
    public void BacktrackingTimeout()
    {
        backtrackingTimeout.Replace(InputString, ReplaceString);
    }


    [Benchmark]
    public void NonBacktracking()
    {
        nonBacktracking.Replace(InputString, ReplaceString);
    }

    [Benchmark]
    public void NonBacktrackingTimeout()
    {
        nonBacktrackingTimeout.Replace(InputString, ReplaceString);
    }

    [Benchmark]
    public void Native()
    {
        native.Replace(InputString, ReplaceString);
    }

    [Benchmark]
    public void NativeTimeout()
    {
        nativeTimeout.Replace(InputString, ReplaceString);
    }

}
