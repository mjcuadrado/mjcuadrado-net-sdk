#!/usr/bin/env python3
"""
Hook: slack_notification
Description: Send Slack notification on production deployment
Event: post-deploy
Requires: SLACK_WEBHOOK_URL environment variable
"""

import os
import sys
import requests


def main():
    """Send Slack notification for deployments."""
    # Check if SLACK_WEBHOOK_URL is configured
    webhook_url = os.getenv('SLACK_WEBHOOK_URL')
    if not webhook_url:
        print("⚠️  Warning: SLACK_WEBHOOK_URL not set. Skipping Slack notification.")
        sys.exit(0)

    # Get deployment variables
    env = os.getenv('MJ2_DEPLOY_ENV', '')
    version = os.getenv('MJ2_DEPLOY_VERSION', '')
    strategy = os.getenv('MJ2_DEPLOY_STRATEGY', '')
    url = os.getenv('MJ2_DEPLOY_URL', '')
    duration = os.getenv('MJ2_DEPLOY_DURATION', '0')
    status = os.getenv('MJ2_DEPLOY_STATUS', '')

    # Only notify staging and production deployments
    if env not in ['staging', 'production']:
        print(f"ℹ️  Skipping Slack notification for {env} environment")
        sys.exit(0)

    # Determine color and emoji based on status
    if status == 'success':
        color = 'good'
        emoji = '🚀'
        message = 'Deployment successful'
    else:
        color = 'danger'
        emoji = '❌'
        message = 'Deployment failed'

    # Build Slack payload
    payload = {
        'text': f'{emoji} {message} to {env}',
        'attachments': [{
            'color': color,
            'fields': [
                {'title': 'Version', 'value': version, 'short': True},
                {'title': 'Environment', 'value': env, 'short': True},
                {'title': 'Strategy', 'value': strategy, 'short': True},
                {'title': 'Duration', 'value': f'{duration}ms', 'short': True},
                {'title': 'URL', 'value': url, 'short': False}
            ],
            'footer': 'MJ² Deploy System',
            'ts': int(os.times().system)
        }]
    }

    # Send notification to Slack
    try:
        response = requests.post(webhook_url, json=payload, timeout=10)
        if response.status_code == 200:
            print("✅ Slack notification sent successfully")
            sys.exit(0)
        else:
            print(f"❌ Failed to send Slack notification (HTTP {response.status_code})")
            sys.exit(1)
    except requests.RequestException as e:
        print(f"❌ Failed to send Slack notification: {e}")
        sys.exit(1)


if __name__ == "__main__":
    main()
