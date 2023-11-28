using Cake.Common;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace CakeExt.Git;

[CakeAliasCategory("Git")]
public static class GitAlias
{
    /// <summary>
    /// Executes a Git command
    /// </summary>
    [CakeMethodAlias]
    public static IEnumerable<string> Git(this ICakeContext context, string argument)
    {
        var processArguments = new ProcessArgumentBuilder();
        processArguments.Append(argument);

        var processSettings = new ProcessSettings
        {
            Arguments = processArguments,
            RedirectStandardOutput = true,
        };

        var result = context.StartAndReturnProcess("git", processSettings);
        result.WaitForExit();

        var exitCode = result.GetExitCode();
        if (exitCode != 0)
            throw new Exception($"Git command failed with ExitCode {exitCode}.");

        return result.GetStandardOutput();
    }

    /// <summary>
    /// Gets the last commit message
    /// </summary>
    [CakeMethodAlias]
    public static string GitGetLastCommitMessage(this ICakeContext context) =>
        context.Git("log -1 --pretty=format:'%s'").FirstOrDefault() ?? "";

    /// <summary>
    /// Gets the current branch name
    /// </summary>
    [CakeMethodAlias]
    public static string GitGetBranchName(this ICakeContext context) =>
        context.Git("branch --show-current").FirstOrDefault() ?? "";
}
