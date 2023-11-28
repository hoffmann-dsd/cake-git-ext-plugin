# cake-git-ext-plugin

[![GitHub license](https://img.shields.io/github/license/hoffmann-dsd/cake-git-ext-plugin.svg)](https://github.com/hoffmann-dsd/cake-git-ext-plugin/main/LICENSE)

## Overview 

CakeExt.Git is a set of Cake build script extensions that provide additional functionality for working with Git commands beyond what is included in the Cake.Git extension. These extensions aim to simplify the integration of Git-related tasks, especially for handling of changes between two commits into your Cake build scripts.

#### Git

Executes a Git command and returns the output as an IEnumerable<string>.

```csharp
ICakeContext context;

var output = context.Git("your-git-command-here");
```

#### GitGetLastCommitMessage
Returns the last commit message as a string.

```csharp
ICakeContext context;

var lastCommitMessage = context.GitGetLastCommitMessage();
```

#### GitGetBranchName
Gets the current branch name.

```csharp
ICakeContext context;

var branchName = context.GitGetBranchName();
```

### Change Detection

#### GitMergeBase
Gets the merge base between the current branch and a specified remote branch.

If the current branch is the main branch, the merge base is the latest commit on the remote branch. If the current branch is not the main branch, the merge base is the latest commit on the main branch that is also on the current branch.

```csharp
ICakeContext context;

var mergeBase = context.GitMergeBase("your-remote", "your-main-branch");
```

#### GitRevHead
Gets the revision head commit id.

```csharp
ICakeContext context;

var revHead = context.GitRevHead();
```

#### GitDiff
Gets the filenames of the changes between two commits.

```csharp
ICakeContext context;

var changes = context.GitDiff("commit-hash-1", "commit-hash-2");
```

#### GitGetChanges
Gets the changed files between the current commit and the merge base.

```csharp
ICakeContext context;

var changes = context.GitGetChanges("your-remote", "your-main-branch");
```

#### GitGetChangeMatchPattern
Checks if any of the changed files match a given regex pattern.

```csharp
ICakeContext context;
var hasMatch = context.GitGetChangeMatchPattern(changes, "your-regex-pattern");
```

## Usage
Add the CakeExt.Git NuGet package to your Cake build script project.
```csharp
#addin "nuget:?package=CakeExt.Git"
```

Import the namespace in your Cake build script.
```csharp
using CakeExt.Git;
```

Use the provided aliases in your Cake build script as needed.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
