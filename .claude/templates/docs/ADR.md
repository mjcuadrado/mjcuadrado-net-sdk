# ADR-{number}: {Title}

**Status:** {Proposed | Accepted | Deprecated | Superseded}

**Date:** {YYYY-MM-DD}

**Deciders:** {list decision makers}

**Technical Story:** {issue/ticket reference}

---

## Context

### Problem Statement

{Describe the issue or problem that is motivating this decision or change.}

### Background

{Provide relevant background information that helps understand why this decision is needed.}

### Current Situation

{Describe the current state and what's not working or what's missing.}

### Constraints

{List any constraints that limit the solution space:}
- Technical constraints
- Business constraints
- Timeline constraints
- Resource constraints

### Requirements

{List the key requirements this decision must satisfy:}
- Functional requirements
- Non-functional requirements (performance, security, scalability, etc.)

---

## Decision

### Chosen Solution

{Describe the decision that has been made or is being proposed.}

### Rationale

{Explain why this decision was chosen over alternatives.}

### Implementation Details

{Describe how this decision will be implemented:}

#### Architecture Changes

{Describe architectural changes needed}

```
[Diagrams, code examples, or architectural sketches]
```

#### Technology Stack

| Component | Technology | Version | Justification |
|-----------|------------|---------|---------------|
| {component_1} | {tech_1} | {version_1} | {reason_1} |
| {component_2} | {tech_2} | {version_2} | {reason_2} |

#### Migration Path

{If this replaces existing functionality, describe the migration path:}

1. Step 1
2. Step 2
3. Step 3

---

## Alternatives Considered

### Alternative 1: {Name}

**Description:** {Describe the alternative}

**Pros:**
- Pro 1
- Pro 2
- Pro 3

**Cons:**
- Con 1
- Con 2
- Con 3

**Why Rejected:** {Explain why this alternative was not chosen}

---

### Alternative 2: {Name}

**Description:** {Describe the alternative}

**Pros:**
- Pro 1
- Pro 2

**Cons:**
- Con 1
- Con 2

**Why Rejected:** {Explain why this alternative was not chosen}

---

### Alternative 3: Do Nothing

**Description:** Keep the current approach without changes

**Pros:**
- No implementation cost
- No risk of regression

**Cons:**
- Problem remains unsolved
- Technical debt accumulates

**Why Rejected:** {Explain why status quo is not acceptable}

---

## Consequences

### Positive Consequences

- **Benefit 1:** {Description}
- **Benefit 2:** {Description}
- **Benefit 3:** {Description}

### Negative Consequences

- **Trade-off 1:** {Description and mitigation}
- **Trade-off 2:** {Description and mitigation}

### Neutral Consequences

- **Impact 1:** {Description}
- **Impact 2:** {Description}

### Risks

| Risk | Probability | Impact | Mitigation |
|------|-------------|--------|------------|
| {risk_1} | {Low/Med/High} | {Low/Med/High} | {mitigation_strategy} |
| {risk_2} | {Low/Med/High} | {Low/Med/High} | {mitigation_strategy} |

---

## Compliance and Standards

### Security Implications

{Describe any security implications of this decision}

### Performance Implications

{Describe expected performance impact}

### Scalability Implications

{Describe how this decision affects system scalability}

### Maintainability Implications

{Describe how this decision affects code/system maintainability}

---

## Dependencies

### External Dependencies

- {dependency_1} - {reason}
- {dependency_2} - {reason}

### Internal Dependencies

- {dependency_1} - {reason}
- {dependency_2} - {reason}

### Related ADRs

- [ADR-{number}]({link}) - {relationship}
- [ADR-{number}]({link}) - {relationship}

---

## Implementation Plan

### Phase 1: {Phase Name}
**Timeline:** {duration}
**Tasks:**
- [ ] Task 1
- [ ] Task 2
- [ ] Task 3

### Phase 2: {Phase Name}
**Timeline:** {duration}
**Tasks:**
- [ ] Task 1
- [ ] Task 2

### Phase 3: {Phase Name}
**Timeline:** {duration}
**Tasks:**
- [ ] Task 1
- [ ] Task 2

---

## Success Metrics

### Key Performance Indicators

| Metric | Current | Target | Measurement Method |
|--------|---------|--------|--------------------|
| {metric_1} | {current_value} | {target_value} | {how_to_measure} |
| {metric_2} | {current_value} | {target_value} | {how_to_measure} |

### Acceptance Criteria

- [ ] Criterion 1
- [ ] Criterion 2
- [ ] Criterion 3

---

## Review and Updates

### Review Schedule

This decision will be reviewed:
- **Next review:** {date}
- **Review frequency:** {quarterly/annually/as-needed}

### Update History

| Date | Author | Change | Reason |
|------|--------|--------|--------|
| {date} | {author} | {change} | {reason} |

---

## References

### Documentation

- {link_1} - {description}
- {link_2} - {description}

### Research

- {link_1} - {description}
- {link_2} - {description}

### Discussion

- GitHub Issue: #{issue_number}
- Pull Request: #{pr_number}
- Meeting Notes: {link}

---

## Notes

{Any additional notes or comments}

---

**Author:** {author_name}
**Reviewers:** {reviewer_1}, {reviewer_2}
**Approvers:** {approver_1}, {approver_2}

---

## Appendix

### Technical Diagrams

```mermaid
{mermaid_diagram}
```

### Code Examples

```{language}
{code_example}
```

### Performance Benchmarks

{benchmark_data}
