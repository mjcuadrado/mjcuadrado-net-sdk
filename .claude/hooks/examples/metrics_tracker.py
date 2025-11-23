#!/usr/bin/env python3
"""
Hook: metrics_tracker
Description: Record MJ² command execution metrics
Event: post-command
Output: .mj2/metrics/commands.jsonl (JSON Lines format)
"""

import os
import sys
import json
from datetime import datetime
from pathlib import Path


def main():
    """Track command execution metrics."""
    # Create metrics directory if it doesn't exist
    metrics_dir = Path('.mj2/metrics')
    metrics_dir.mkdir(parents=True, exist_ok=True)

    # Metrics file (JSON Lines format)
    metrics_file = metrics_dir / 'commands.jsonl'

    # Get command variables
    command = os.getenv('MJ2_COMMAND', '')
    args = os.getenv('MJ2_ARGS', '')
    exit_code = int(os.getenv('MJ2_EXIT_CODE', '0'))
    duration = int(os.getenv('MJ2_DURATION', '0'))
    user = os.getenv('MJ2_USER', '')
    timestamp = os.getenv('MJ2_TIMESTAMP', datetime.utcnow().isoformat())

    # Determine status
    status = 'success' if exit_code == 0 else 'failed'

    # Record metric in JSON Lines format
    metric = {
        'command': command,
        'args': args,
        'exitCode': exit_code,
        'status': status,
        'duration': duration,
        'user': user,
        'timestamp': timestamp
    }

    with open(metrics_file, 'a') as f:
        f.write(json.dumps(metric) + '\n')

    print(f"✅ Metrics recorded to {metrics_file}")

    # Generate report for specific commands
    if command in ['2-run', '2f-build']:
        # Count total executions of this command
        total_count = 0
        success_count = 0
        durations = []

        with open(metrics_file, 'r') as f:
            for line in f:
                try:
                    entry = json.loads(line)
                    if entry.get('command') == command:
                        total_count += 1
                        if entry.get('status') == 'success':
                            success_count += 1
                        durations.append(entry.get('duration', 0))
                except json.JSONDecodeError:
                    pass

        if total_count > 0:
            success_rate = (success_count * 100) // total_count
            print(f"📊 Command: {command}")
            print(f"   Total executions: {total_count}")
            print(f"   Success rate: {success_rate}%")

            # Calculate average duration (last 10)
            if durations:
                recent_durations = durations[-10:]
                avg_duration = sum(recent_durations) // len(recent_durations)
                print(f"   Avg duration (last 10): {avg_duration}ms")

    # Alert if duration is abnormally high
    if duration > 120000:  # > 2 minutes
        print(f"⚠️  Warning: Command took longer than expected ({duration}ms)")

    # Generate daily report if it's the first command of the day
    current_date = datetime.now().strftime('%Y-%m-%d')
    last_report_file = metrics_dir / '.last_report'

    should_generate_report = (
        not last_report_file.exists() or
        last_report_file.read_text().strip() != current_date
    )

    if should_generate_report:
        # Generate report for yesterday
        yesterday = (datetime.now().date() - timedelta(days=1)).strftime('%Y-%m-%d')
        daily_report = metrics_dir / f'daily-report-{yesterday}.txt'

        # Check if we have data for yesterday
        has_yesterday_data = False
        with open(metrics_file, 'r') as f:
            for line in f:
                if yesterday in line:
                    has_yesterday_data = True
                    break

        if has_yesterday_data:
            commands = {}
            total = 0
            success = 0

            with open(metrics_file, 'r') as f:
                for line in f:
                    try:
                        entry = json.loads(line)
                        if yesterday in entry.get('timestamp', ''):
                            total += 1
                            cmd = entry.get('command', 'unknown')
                            commands[cmd] = commands.get(cmd, 0) + 1
                            if entry.get('status') == 'success':
                                success += 1
                    except json.JSONDecodeError:
                        pass

            if total > 0:
                with open(daily_report, 'w') as f:
                    f.write(f"📊 MJ² Daily Report - {yesterday}\n")
                    f.write("=" * 40 + "\n\n")
                    f.write("Commands executed:\n")
                    for cmd, count in sorted(commands.items()):
                        f.write(f"  {cmd}: {count}\n")
                    f.write(f"\nSuccess rate:\n")
                    f.write(f"  {success}/{total} ({(success * 100) // total}%)\n")

                print(f"📄 Daily report generated: {daily_report}")

        # Update last report date
        last_report_file.write_text(current_date)

    sys.exit(0)


# Import timedelta for daily report
from datetime import timedelta


if __name__ == "__main__":
    main()
