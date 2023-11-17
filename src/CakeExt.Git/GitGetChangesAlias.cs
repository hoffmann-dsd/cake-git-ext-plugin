using System.Text.RegularExpressions;
using Cake.Core;
using Cake.Core.Annotations;

namespace CakeExt.Git;

[CakeAliasCategory("Git")]
public static class DetectChangesAlias
{
    /// <summary>
    /// Gets the merge base
    /// </summary>
    [CakeMethodAlias]
    public static string GitMergeBase(this ICakeContext context, string remote = "origin", string mainBranch = "master") =>
        context.Git($"merge-base HEAD {remote}/{mainBranch}")
            .FirstOrDefault() ?? "";

    /// <summary>
    /// Gets the revision head commit id
    /// </summary>
    [CakeMethodAlias]
    public static string GitRevHead(this ICakeContext context) =>
        context.Git($"rev-parse HEAD")
            .FirstOrDefault() ?? "";

    /// <summary>
    /// Gets the diff filename between two commits
    /// </summary>
    [CakeMethodAlias]
    public static IEnumerable<string> GitDiff(this ICakeContext context, string to, string from = "HEAD") =>
        context.Git($"diff --name-only {from} {to}");

    /// <summary>
    /// Gets the changes files between the current commit and the merge base
    /// </summary>
    [CakeMethodAlias]
    public static IEnumerable<string> GitGetChanges(this ICakeContext context, string remote = "origin", string mainBranch = "master")
    {
        var mergeBase = context.GitMergeBase(remote, mainBranch);
        var revision = context.GitRevHead();

        // If MergeBase == Revision => We're on the master branch and diff against previous Commit
        // If not => We're in a feature branch and diff against merge-base
        return (mergeBase == revision)
            ? context.GitDiff("HEAD~")
            : context.GitDiff(mergeBase);
    }

    /// <summary>
    /// Gets a boolean value indicating whether any of the changed files matches the given pattern
    /// </summary>
    [CakeMethodAlias]
    public static bool GitGetChangeMatchPattern(
        this ICakeContext context,
        IEnumerable<string> changes,
        string pattern)
    {
        var regex = new Regex(pattern);
        return changes.Any(change => regex.IsMatch(change));
    }

    /// <summary>
    /// Gets a boolean value indicating whether any of the changed files matches the given pattern
    /// </summary>
    [CakeMethodAlias]
    public static bool GitGetChangeMatchPattern(
        this ICakeContext context,
        string pattern,
        string remote = "origin",
        string mainBranch = "master") =>
        GitGetChangeMatchPattern(context, context.GitGetChanges(remote, mainBranch), pattern);
}
