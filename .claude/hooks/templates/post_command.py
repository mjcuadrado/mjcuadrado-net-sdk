#!/usr/bin/env python3
"""
Template: post-command hook
Description: Runs AFTER executing a /mj2:* command
Available variables:
  - MJ2_COMMAND: Command executed (e.g., "1-plan", "2-run")
  - MJ2_ARGS: Command arguments
  - MJ2_EXIT_CODE: Command exit code (0 = success)
  - MJ2_DURATION: Execution duration in ms
  - MJ2_USER: User who executed the command
  - MJ2_TIMESTAMP: ISO 8601 timestamp
"""

import os
import sys
import json
from datetime import datetime
from pathlib import Path


def main():
    """Post-command hook execution."""
    # Get environment variables
    command = os.getenv('MJ2_COMMAND', '')
    args = os.getenv('MJ2_ARGS', '')
    exit_code = int(os.getenv('MJ2_EXIT_CODE', '0'))
    duration = int(os.getenv('MJ2_DURATION', '0'))
    user = os.getenv('MJ2_USER', '')
    timestamp = os.getenv('MJ2_TIMESTAMP', datetime.utcnow().isoformat())

    # Log result
    print(f"[POST-COMMAND] Command: {command}")
    print(f"[POST-COMMAND] Exit Code: {exit_code}")
    print(f"[POST-COMMAND] Duration: {duration}ms")
    print(f"[POST-COMMAND] Timestamp: {timestamp}")

    # ============================================
    # YOUR CODE HERE
    # ============================================

    # Example 1: Record metrics
    # metrics_dir = Path(".mj2/metrics")
    # metrics_dir.mkdir(parents=True, exist_ok=True)
    #
    # metrics_file = metrics_dir / "commands.jsonl"
    # metric = {
    #     "command": command,
    #     "args": args,
    #     "exitCode": exit_code,
    #     "duration": duration,
    #     "user": user,
    #     "timestamp": timestamp
    # }
    #
    # with open(metrics_file, 'a') as f:
    #     f.write(json.dumps(metric) + '\n')
    # print(f"✅ Metrics recorded to {metrics_file}")

    # Example 2: Notify on failure
    # if exit_code != 0:
    #     print(f"❌ Command failed: {command}")
    #     # Send notification (Slack, email, etc.)
    #     # import requests
    #     # webhook_url = os.getenv('SLACK_WEBHOOK_URL')
    #     # if webhook_url:
    #     #     requests.post(webhook_url, json={'text': f'Command failed: {command}'})

    # Example 3: Auto-commit after successful /mj2:2-run
    # if command == "2-run" and exit_code == 0:
    #     import subprocess
    #     try:
    #         subprocess.run(['git', 'add', '.'], check=True)
    #         subprocess.run(['git', 'commit', '-m', f'feat: Implemented {args}',
    #                       '--no-verify'], check=True)
    #         print("✅ Auto-committed changes")
    #     except subprocess.CalledProcessError as e:
    #         print(f"⚠️  Auto-commit failed: {e}")

    # Example 4: Generate performance report
    # if duration > 60000:  # > 1 minute
    #     print(f"⚠️  Command took longer than expected: {duration}ms")

    # ============================================
    # IMPORTANT: Exit codes
    # ============================================
    # In post-command, exit code does NOT block anything
    # but can be used for logging/alerting
    # ============================================

    sys.exit(0)


if __name__ == "__main__":
    main()
