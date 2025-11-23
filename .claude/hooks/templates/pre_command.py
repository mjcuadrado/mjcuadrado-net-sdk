#!/usr/bin/env python3
"""
Template: pre-command hook
Description: Runs BEFORE executing a /mj2:* command
Available variables:
  - MJ2_COMMAND: Command to execute (e.g., "1-plan", "2-run")
  - MJ2_ARGS: Command arguments
  - MJ2_USER: User executing the command
  - MJ2_TIMESTAMP: ISO 8601 timestamp
"""

import os
import sys
from datetime import datetime


def main():
    """Pre-command hook execution."""
    # Get environment variables
    command = os.getenv('MJ2_COMMAND', '')
    args = os.getenv('MJ2_ARGS', '')
    user = os.getenv('MJ2_USER', '')
    timestamp = os.getenv('MJ2_TIMESTAMP', datetime.utcnow().isoformat())

    # Log start
    print(f"[PRE-COMMAND] Command: {command}")
    print(f"[PRE-COMMAND] Args: {args}")
    print(f"[PRE-COMMAND] User: {user}")
    print(f"[PRE-COMMAND] Timestamp: {timestamp}")

    # ============================================
    # YOUR CODE HERE
    # ============================================

    # Example 1: Check for uncommitted changes
    # import subprocess
    # try:
    #     result = subprocess.run(['git', 'status', '--porcelain'],
    #                           capture_output=True, text=True, check=True)
    #     if result.stdout.strip():
    #         print("⚠️  Warning: Uncommitted changes detected")
    # except subprocess.CalledProcessError:
    #     pass

    # Example 2: Validate requirements before execution
    # if command == "2-run":
    #     spec_path = ".mj2/specs/current-spec.md"
    #     if not os.path.exists(spec_path):
    #         print(f"❌ Error: No SPEC found at {spec_path}")
    #         print("   Run /mj2:1-plan first")
    #         sys.exit(1)  # Exit code 1 blocks execution

    # Example 3: Pre-checks for environment
    # database_url = os.getenv('DATABASE_URL')
    # if not database_url:
    #     print("⚠️  Warning: DATABASE_URL not set")

    # ============================================
    # IMPORTANT: Exit codes
    # ============================================
    # sys.exit(0)  - SUCCESS: Allows command to continue
    # sys.exit(1)  - ERROR: Blocks command execution
    # ============================================

    sys.exit(0)


if __name__ == "__main__":
    main()
