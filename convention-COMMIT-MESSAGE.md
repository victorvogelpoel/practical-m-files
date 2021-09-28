# Dramatic commit messages

Inspired by [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/#summary) and Angular Contributing guidelines.

```(generic)
<type>[optional scope>]: <description> [Optional trackID]
<BLANK LINE>
[optional body>]
<BLANK LINE>
[optional footer(s)]

```

The header is mandatory and is prefixed by a **type** and **scope**; the scope of the header is optional.
Note that the subject and body is separated by a BLANK LINE.
The footer should contain a closing reference to an issue if any.

**Rules** for the commit message

- Add type and optional scope and trackid to the subject line
- Limit the subject line to at most 72 characters
- Separate subject from body with a blank line
- Wrap the body text at 72 characters

See "Subject" below for rules about what the subject line should contain.

## Subject

The subject contains a succinct description of the change, along with a type/scope prefix and trackid postfix.

Every good commit should be able to complete the following sentence:

```(generic)
  If applied, this commit will: {{YOUR COMMIT MESSAGE}}
```

Rules for the description of the subject line:

- Prefix the description with a type and optional scope of the change
- use the imperative, present tense in the subject message: "change" not "changed" nor "changes"
- Do NOT capitalize the subject line
- Do NOT end the subject line with a period
- short message, at most 72 characters, including type and scope prefix (GitHub shows subject lines with more than 72 characters truncated)

Good description, for example (omitted type/scope for brevity)

- If applied, this commit will refactor subsystem X for readability
- If applied, this commit will update getting started documentation
- If applied, this commit will remove deprecated methods
- If applied, this commit will release version 1.0.0
- If applied, this commit will merge pull request #123 from user/branch

Notice how this DOESN'T WORK for the other non-imperative forms:

- If applied, this commit will fixed bug with Y
- If applied, this commit will changing behavior of X
- If applied, this commit will more fixes for broken stuff
- If applied, this commit will sweet new API methods

Remember: Use of the imperative is important only in the description of the subject line. You can relax this restriction when you're writing the body.

Prefix the commit message with a **type** and **(optional) scope**, and postfix the message with an issue track ID. See **Type** and **Scope** below for the supported words.

Example subject lines with type and scope:

- refactor(website): refactor subsystem website model for readability
- docs(api): change wording

## Revert

If the commit reverts a previous commit, it should begin with **revert:** , followed by the header of the reverted commit. In the body it should say:
This reverts commit <hash>., where the hash is the SHA of the commit being reverted.

## Type

- **build**: Changes that affect the build system or external dependencies
- **ci**: Changes to the CI configuration files and scripts
- **chore**: Other changes that don't modify src or test files; no production code change
- **docs**: documentation-only changes
- **resource**: resource changes; no production code change
- **feat**: new feature for the user (not a new feature for build script)
- **fix**: bug fix for the user (not a fix to a build script)
- **perf**: A code change that improves performance
- **revert**: Reverts a previous commit
- **refactor**: A code change that neither fixes a bug nor adds a feature
- **style**: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc); no production code change
- **test**: Adding missing tests or correcting existing tests; no production code change

## Scope

The scope should be the name of the subsystem affected (as perceived by the person reading the changelog generated from commit messages.
Use the practical sample (directory) name as a scope for the commit, eg "`feat(vaultapplication-cleanarch)`"


Other scopes
- **changelog**: used for updating the release notes in CHANGELOG.md
- **none/empty string**: useful for style, test and refactor changes that are done across all packages (e.g. style: add missing semicolons)
- **readme**
- **commit**: update to the git automation
- **deps**: eg, `build(deps)` - update nuget/library dependencies


## TrackID

TrackID is the ID of the issue that is being addressed in this commit. For GitHub issues for example, it is the number of the issue: "#<number>".

## Body

Just as in the subject, use the imperative, present tense: "change" not "changed" nor "changes". The body should include the **motivation** for the change and contrast this with previous behavior.

A good commit message should answer three questions about a patch:

- **Why is the change being made?** It may fix a bug, it may add a feature, it may improve performance, reliabilty, stability, or just be a change for the sake of correctness.
- **How does it address the issue?** For short obvious patches this part can be omitted, but it should be a high level description of what the approach was.
- **What effects does the patch have?** (In addition to the obvious ones, this may include benchmarks, side effects, etc.)
- Do not assume the reviewer understands what the original problem was.
- Do not assume the code is self-evident/self-documenting.
- Read the commit message to see if it hints at improved code structure.
- The first commit line is the most important.
- Describe any limitations of the current code.
- Do not include patch set-specific comments.

## Footer

The footer should contain any information about Breaking Changes and is also the place to reference GitHub issues that this commit **Closes**.
Breaking Changes should start with the word BREAKING CHANGE: with a space or two newlines. The rest of the commit message is then used for this.

This is where you can put useful meta-data regarding your commit, such as JIRA ticket numbers, GitHub issue numbers, co-author names, and additional links.
This can help to link important information together that relates to your change.

# Sample prefixes:

- build(deps):                       -> update library dependencies (nugets etc) - no other changes to production code
- feat:                              
- feat(vaultapplication-cleanarch)   -> add or updating a user feature
- docs:
- style: remove trailing whitespace
- resource:
- test:

