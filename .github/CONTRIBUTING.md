# Contributing

Firstly: thanks for contributing in Obfuscar Mapping Parser.

Secondly: please review this file for some guidlines.

## Goals

### Parsing Issues

If you just have an issue with mapping parsing, you can just pull request with failed unit test.

### Optimizations

If you have an optimization idea, you are welcome to add a pull request. Just be aware that your changes should not break
the unit tests. Please see below for a few code standard restrictions.

### Other changes/fixes

For the UI fixes/improvements it is really better to write an issue. The Obfuscar Mapping Parser uses much of custom BrokenEvent's
closed code, so let us fix any issues by our own.

The bug can be explained in a few ways: 

* as usual QA report with WTR (preferred).
* as just text explanation of what's happening. (allowed only if bug is simple to reproduce and to describe).

### Feature Requests

Feature requests can be posted with the GitHub's Issues or on the Feature Requests [Trello Board](https://trello.com/c/BCIHDF4b/22-feature-requests).

## Code Standard

The code standard is really close to the Microsoft's default one, but
Broken Event uses some of its own code standard restrictions.

Here they are:

* No tabs. Only spaces are allowed, 2 spaces per indent.
* No prefixes for local variables and private fields.
* `consts` in UPPERCASE.
* No external dependencies. This is not a restriction, but it is highly recommended. Exceptions will be negotiated.
* `Linq` is highly NOT recommended to be used.
* No *expression body* syntax in properties.
* No `var` usage at all. We don't want to turn the C# code into javascript.
