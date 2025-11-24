# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [Unreleased]

### Added
- New features that have been added to the unreleased version

### Changed
- Changes in existing functionality

### Deprecated
- Soon-to-be removed features

### Removed
- Features that have been removed

### Fixed
- Bug fixes

### Security
- Security improvements and vulnerability fixes

---

## [{version}] - {date}

### Added
- **{feature_name}** - {feature_description}
  - {detail_1}
  - {detail_2}
- **{feature_name_2}** - {feature_description_2}

### Changed
- **{change_name}** - {change_description}
  - {detail_1}
  - BREAKING: {breaking_change_detail}
- **{change_name_2}** - {change_description_2}

### Deprecated
- **{deprecated_feature}** - {deprecation_reason}
  - Will be removed in version {removal_version}
  - Migration path: {migration_instructions}

### Removed
- **{removed_feature}** - {removal_reason}
  - BREAKING: {breaking_change_detail}
  - Alternative: {alternative_solution}

### Fixed
- **{bug_fix}** - {fix_description}
  - Issue: #{issue_number}
  - Fixed by: @{contributor}
- **{bug_fix_2}** - {fix_description_2}

### Security
- **{security_fix}** - {security_description}
  - CVE: {cve_number}
  - Severity: {severity_level}

---

## [1.0.0] - YYYY-MM-DD

### Added
- Initial release
- Core functionality
- Basic API endpoints
- Documentation

---

## Release Notes Template

Use this template for new releases:

```markdown
## [X.Y.Z] - YYYY-MM-DD

### Added
- New feature 1
- New feature 2

### Changed
- Change 1
- Change 2

### Deprecated
- Deprecated feature 1

### Removed
- Removed feature 1

### Fixed
- Bug fix 1
- Bug fix 2

### Security
- Security improvement 1
```

---

## Versioning Guidelines

- **MAJOR** version (X.0.0): Incompatible API changes
- **MINOR** version (0.X.0): New functionality in a backwards compatible manner
- **PATCH** version (0.0.X): Backwards compatible bug fixes

### Breaking Changes

Breaking changes should be clearly marked with **BREAKING:** prefix and include:
- What changed
- Why it changed
- Migration path for users

Example:
```markdown
### Changed
- **API Authentication** - Updated to OAuth 2.0
  - BREAKING: API keys are no longer supported
  - Migration: See [OAuth Migration Guide](docs/migration/oauth.md)
  - Timeline: API keys deprecated in v2.0.0, removed in v3.0.0
```

---

## Links

[Unreleased]: https://github.com/{username}/{repo}/compare/v{latest_version}...HEAD
[{version}]: https://github.com/{username}/{repo}/compare/v{previous_version}...v{version}
[1.0.0]: https://github.com/{username}/{repo}/releases/tag/v1.0.0
