#!/usr/bin/env python3
"""
Hook: coverage_reporter
Description: Monitor test coverage and send alerts if below threshold
Event: on-test-run
Output: .mj2/metrics/coverage-history.jsonl
"""

import os
import sys
import json
from datetime import datetime
from pathlib import Path


def main():
    """Monitor coverage and generate reports."""
    # Configuration
    coverage_threshold = int(os.getenv('COVERAGE_THRESHOLD', '85'))
    metrics_dir = Path('.mj2/metrics')
    metrics_dir.mkdir(parents=True, exist_ok=True)

    coverage_file = metrics_dir / 'coverage-history.jsonl'

    # Get test variables
    result = os.getenv('MJ2_TEST_RESULT', '')
    total = int(os.getenv('MJ2_TEST_TOTAL', '0'))
    passed = int(os.getenv('MJ2_TEST_PASSED', '0'))
    failed = int(os.getenv('MJ2_TEST_FAILED', '0'))
    coverage = int(os.getenv('MJ2_COVERAGE', '0'))
    coverage_lines = int(os.getenv('MJ2_COVERAGE_LINES', '0'))
    coverage_branches = int(os.getenv('MJ2_COVERAGE_BRANCHES', '0'))
    timestamp = os.getenv('MJ2_TIMESTAMP', datetime.utcnow().isoformat())

    # Log test results
    print(f"[COVERAGE REPORTER] Tests: {result}")
    print(f"[COVERAGE REPORTER] Total: {total} (Passed: {passed}, Failed: {failed})")
    print(f"[COVERAGE REPORTER] Coverage: {coverage}%")
    print(f"[COVERAGE REPORTER] Threshold: {coverage_threshold}%")

    # Record coverage history
    metric = {
        'coverage': coverage,
        'lines': coverage_lines,
        'branches': coverage_branches,
        'tests': {
            'total': total,
            'passed': passed,
            'failed': failed
        },
        'result': result,
        'timestamp': timestamp
    }

    with open(coverage_file, 'a') as f:
        f.write(json.dumps(metric) + '\n')

    # Validate coverage threshold
    if coverage < coverage_threshold:
        diff = coverage_threshold - coverage
        print(f"❌ Coverage below threshold: {coverage}% (required: {coverage_threshold}%)")
        print(f"   Missing: {diff}% to reach threshold")

        # Send alert (if webhook configured)
        alert_webhook = os.getenv('COVERAGE_ALERT_WEBHOOK')
        if alert_webhook:
            try:
                import requests
                payload = {
                    'type': 'coverage_low',
                    'coverage': coverage,
                    'threshold': coverage_threshold,
                    'diff': diff,
                    'timestamp': timestamp
                }
                requests.post(alert_webhook, json=payload, timeout=10)
            except (ImportError, Exception) as e:
                print(f"⚠️  Failed to send alert: {e}")

        # Block PR if coverage drops (optional - uncomment to enable)
        # import subprocess
        # try:
        #     subprocess.run(['gh', 'pr', 'edit', '--add-label',
        #                   'coverage-below-threshold'], check=True)
        # except (subprocess.CalledProcessError, FileNotFoundError):
        #     pass

        sys.exit(1)  # Exit 1 to indicate coverage is below threshold
    else:
        print(f"✅ Coverage OK: {coverage}%")

        # Remove coverage label if it improved (optional)
        # import subprocess
        # try:
        #     subprocess.run(['gh', 'pr', 'edit', '--remove-label',
        #                   'coverage-below-threshold'], check=True)
        # except (subprocess.CalledProcessError, FileNotFoundError):
        #     pass

    # Calculate coverage trend (last 10 runs)
    if coverage_file.exists():
        coverages = []
        with open(coverage_file, 'r') as f:
            for line in f:
                try:
                    entry = json.loads(line)
                    coverages.append(entry.get('coverage', 0))
                except json.JSONDecodeError:
                    pass

        if len(coverages) > 1:
            recent = coverages[-10:]
            recent_str = ', '.join(str(c) for c in recent)
            print(f"📊 Coverage trend (last 10 runs): {recent_str}")

            # Calculate average
            avg_coverage = sum(recent) // len(recent)
            print(f"   Average: {avg_coverage}%")

            # Detect trend
            first = recent[0]
            last = coverage

            if last < first:
                trend_diff = first - last
                print(f"⚠️  Coverage is decreasing: -{trend_diff}% (from {first}% to {last}%)")
            elif last > first:
                trend_diff = last - first
                print(f"✅ Coverage is increasing: +{trend_diff}% (from {first}% to {last}%)")
            else:
                print(f"→  Coverage is stable at {last}%")

    # Generate coverage badge (shields.io)
    badge_dir = Path('.github/badges')
    badge_dir.mkdir(parents=True, exist_ok=True)

    # Determine badge color
    if coverage >= 90:
        color = 'brightgreen'
    elif coverage >= 80:
        color = 'green'
    elif coverage >= 70:
        color = 'yellow'
    elif coverage >= 60:
        color = 'orange'
    else:
        color = 'red'

    # Download badge
    try:
        import requests
        badge_url = f'https://img.shields.io/badge/coverage-{coverage}%25-{color}'
        response = requests.get(badge_url, timeout=10)
        with open(badge_dir / 'coverage.svg', 'wb') as f:
            f.write(response.content)
        print(f"🏷️  Coverage badge updated: {badge_dir / 'coverage.svg'}")
    except (ImportError, Exception) as e:
        print(f"⚠️  Failed to update badge: {e}")

    # Generate detailed report if coverage is very low
    if coverage < 70:
        report_file = metrics_dir / f"low-coverage-report-{datetime.now().strftime('%Y%m%d_%H%M%S')}.txt"

        with open(report_file, 'w') as f:
            f.write("⚠️  LOW COVERAGE ALERT\n")
            f.write("=" * 40 + "\n\n")
            f.write(f"Current Coverage: {coverage}%\n")
            f.write(f"Threshold: {coverage_threshold}%\n")
            f.write(f"Difference: -{coverage_threshold - coverage}%\n\n")
            f.write("Coverage Breakdown:\n")
            f.write(f"  Lines: {coverage_lines}%\n")
            f.write(f"  Branches: {coverage_branches}%\n\n")
            f.write("Test Results:\n")
            f.write(f"  Total: {total}\n")
            f.write(f"  Passed: {passed}\n")
            f.write(f"  Failed: {failed}\n\n")
            f.write(f"Timestamp: {timestamp}\n\n")
            f.write("Action Required:\n")
            f.write("- Add unit tests for uncovered code paths\n")
            f.write(f"- Aim for {coverage_threshold}% coverage minimum\n")

        print(f"📄 Low coverage report generated: {report_file}")

    sys.exit(0)


if __name__ == "__main__":
    main()
