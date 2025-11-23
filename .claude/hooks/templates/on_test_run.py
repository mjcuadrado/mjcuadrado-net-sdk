#!/usr/bin/env python3
"""
Template: on-test-run hook
Description: Runs when tests are executed
Available variables:
  - MJ2_TEST_RESULT: Result (passed, failed)
  - MJ2_TEST_TOTAL: Total tests executed
  - MJ2_TEST_PASSED: Tests that passed
  - MJ2_TEST_FAILED: Tests that failed
  - MJ2_COVERAGE: Coverage % (e.g., 87)
  - MJ2_COVERAGE_LINES: Line coverage %
  - MJ2_COVERAGE_BRANCHES: Branch coverage %
  - MJ2_TEST_DURATION: Test duration in ms
  - MJ2_TIMESTAMP: ISO 8601 timestamp
"""

import os
import sys
import json
from datetime import datetime
from pathlib import Path


def main():
    """On-test-run hook execution."""
    # Get environment variables
    result = os.getenv('MJ2_TEST_RESULT', '')
    total = int(os.getenv('MJ2_TEST_TOTAL', '0'))
    passed = int(os.getenv('MJ2_TEST_PASSED', '0'))
    failed = int(os.getenv('MJ2_TEST_FAILED', '0'))
    coverage = int(os.getenv('MJ2_COVERAGE', '0'))
    coverage_lines = int(os.getenv('MJ2_COVERAGE_LINES', '0'))
    coverage_branches = int(os.getenv('MJ2_COVERAGE_BRANCHES', '0'))
    duration = int(os.getenv('MJ2_TEST_DURATION', '0'))
    timestamp = os.getenv('MJ2_TIMESTAMP', datetime.utcnow().isoformat())

    # Log event
    print(f"[ON-TEST-RUN] Result: {result}")
    print(f"[ON-TEST-RUN] Total: {total} (Passed: {passed}, Failed: {failed})")
    print(f"[ON-TEST-RUN] Coverage: {coverage}%")
    print(f"[ON-TEST-RUN] Duration: {duration}ms")

    # ============================================
    # YOUR CODE HERE
    # ============================================

    # Example 1: Alert if coverage drops below threshold
    # coverage_threshold = int(os.getenv('COVERAGE_THRESHOLD', '85'))
    # if coverage < coverage_threshold:
    #     print(f"⚠️  Coverage below threshold: {coverage}% (required: {coverage_threshold}%)")
    #
    #     # Send alert
    #     import requests
    #     alert_webhook = os.getenv('COVERAGE_ALERT_WEBHOOK')
    #     if alert_webhook:
    #         payload = {
    #             'type': 'coverage_low',
    #             'coverage': coverage,
    #             'threshold': coverage_threshold
    #         }
    #         requests.post(alert_webhook, json=payload)
    # else:
    #     print(f"✅ Coverage OK: {coverage}%")

    # Example 2: Record coverage history
    # metrics_dir = Path('.mj2/metrics')
    # metrics_dir.mkdir(parents=True, exist_ok=True)
    #
    # coverage_file = metrics_dir / 'coverage-history.jsonl'
    # metric = {
    #     'coverage': coverage,
    #     'lines': coverage_lines,
    #     'branches': coverage_branches,
    #     'timestamp': timestamp
    # }
    #
    # with open(coverage_file, 'a') as f:
    #     f.write(json.dumps(metric) + '\n')

    # Example 3: Block merge if tests fail
    # if result == "failed":
    #     print("❌ Tests failed - blocking merge")
    #     import subprocess
    #     try:
    #         subprocess.run(['gh', 'pr', 'edit', '--add-label',
    #                       'tests-failing'], check=True)
    #     except subprocess.CalledProcessError:
    #         pass
    #     sys.exit(1)

    # Example 4: Generate coverage badge
    # if coverage >= 90:
    #     color = 'brightgreen'
    # elif coverage >= 80:
    #     color = 'green'
    # elif coverage >= 70:
    #     color = 'yellow'
    # else:
    #     color = 'red'
    #
    # import requests
    # badge_dir = Path('.github/badges')
    # badge_dir.mkdir(parents=True, exist_ok=True)
    #
    # badge_url = f'https://img.shields.io/badge/coverage-{coverage}%25-{color}'
    # response = requests.get(badge_url)
    # with open(badge_dir / 'coverage.svg', 'wb') as f:
    #     f.write(response.content)

    sys.exit(0)


if __name__ == "__main__":
    main()
