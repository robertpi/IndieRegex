using IndieSystem.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

public class AsmRegexBenchmark
{
    private const string ObsfucationRegex = @"(?i)(?:p(?:ass)?w(?:or)?d|pass(?:_?phrase)?|secret|(?:api_?|private_?|public_?|access_?|secret_?)key(?:_?id)?|token|consumer_?(?:id|key|secret)|sign(?:ed|ature)?|auth(?:entication|orization)?)(?:(?:\s|%20)*(?:=|%3D)[^&]+|(?:""|%22)(?:\s|%20)*(?::|%3A)(?:\s|%20)*(?:""|%22)(?:%2[^2]|%[^2]|[^""%])+(?:""|%22))|bearer(?:\s|%20)+[a-z0-9\._\-]|token(?::|%3A)[a-z0-9]{13}|gh[opsu]_[0-9a-zA-Z]{36}|ey[I-L](?:[\w=-]|%3D)+\.ey[I-L](?:[\w=-]|%3D)+(?:\.(?:[\w.+\/=-]|%3D|%2F|%2B)+)?|[\-]{5}BEGIN(?:[a-z\s]|%20)+PRIVATE(?:\s|%20)KEY[\-]{5}[^\-]+[\-]{5}END(?:[a-z\s]|%20)+PRIVATE(?:\s|%20)KEY|ssh-rsa(?:\s|%20)*(?:[a-z0-9\/\.+]|%2F|%5C|%2B){100,}";
    private const string ReplaceString = "<redacted>";
    private static readonly string InputString = new('a', 2000);

    private static readonly Regex backtracking = new(ObsfucationRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex nonBacktracking = new(ObsfucationRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.NonBacktracking);

    [Benchmark]
    public void Backtracking()
    {
        backtracking.Replace(InputString, ReplaceString);
    }

    [Benchmark]
    public void NonBacktracking()
    {
        nonBacktracking.Replace(InputString, ReplaceString);
    }
}
