#!/usr/bin/env python3
"""
Template: on-deploy hook
Description: Runs when a deployment is executed
Available variables:
  - MJ2_DEPLOY_ENV: Environment (development, staging, production)
  - MJ2_DEPLOY_VERSION: Deployed version
  - MJ2_DEPLOY_STRATEGY: Strategy (blue-green, rolling, canary)
  - MJ2_DEPLOY_URL: Deployment URL
  - MJ2_DEPLOY_DURATION: Deployment duration in ms
  - MJ2_DEPLOY_STATUS: Status (success, failed)
  - MJ2_TIMESTAMP: ISO 8601 timestamp
"""

import os
import sys
import subprocess
from datetime import datetime


def main():
    """On-deploy hook execution."""
    # Get environment variables
    env = os.getenv('MJ2_DEPLOY_ENV', '')
    version = os.getenv('MJ2_DEPLOY_VERSION', '')
    strategy = os.getenv('MJ2_DEPLOY_STRATEGY', '')
    url = os.getenv('MJ2_DEPLOY_URL', '')
    duration = int(os.getenv('MJ2_DEPLOY_DURATION', '0'))
    status = os.getenv('MJ2_DEPLOY_STATUS', '')
    timestamp = os.getenv('MJ2_TIMESTAMP', datetime.utcnow().isoformat())

    # Log event
    print(f"[ON-DEPLOY] Environment: {env}")
    print(f"[ON-DEPLOY] Version: {version}")
    print(f"[ON-DEPLOY] Strategy: {strategy}")
    print(f"[ON-DEPLOY] URL: {url}")
    print(f"[ON-DEPLOY] Status: {status}")

    # ============================================
    # YOUR CODE HERE
    # ============================================

    # Example 1: Notify Slack
    # import requests
    # webhook_url = os.getenv('SLACK_WEBHOOK_URL')
    #
    # if webhook_url:
    #     if status == 'success':
    #         color = 'good'
    #         emoji = '🚀'
    #         message = 'Deployment successful'
    #     else:
    #         color = 'danger'
    #         emoji = '❌'
    #         message = 'Deployment failed'
    #
    #     payload = {
    #         'text': f'{emoji} {message} to {env}',
    #         'attachments': [{
    #             'color': color,
    #             'fields': [
    #                 {'title': 'Version', 'value': version, 'short': True},
    #                 {'title': 'Environment', 'value': env, 'short': True},
    #                 {'title': 'Strategy', 'value': strategy, 'short': True},
    #                 {'title': 'URL', 'value': url, 'short': False}
    #             ]
    #         }]
    #     }
    #     requests.post(webhook_url, json=payload)

    # Example 2: Register deployment in APM (New Relic, DataDog)
    # if status == 'success':
    #     import requests
    #     app_id = os.getenv('NEW_RELIC_APP_ID')
    #     api_key = os.getenv('NEW_RELIC_API_KEY')
    #
    #     if app_id and api_key:
    #         headers = {'X-Api-Key': api_key}
    #         payload = {
    #             'deployment': {
    #                 'revision': version,
    #                 'description': f'Deployed to {env}',
    #                 'user': os.getenv('USER', 'unknown')
    #             }
    #         }
    #         url = f'https://api.newrelic.com/v2/applications/{app_id}/deployments.json'
    #         requests.post(url, headers=headers, json=payload)

    # Example 3: Run smoke tests post-deployment
    # if env == 'production' and status == 'success':
    #     print(f"Running smoke tests against {url}")
    #
    #     import requests
    #     try:
    #         # Health check
    #         response = requests.get(f'{url}/health', timeout=10)
    #         if response.status_code != 200:
    #             print(f"❌ Smoke test failed: Health check returned {response.status_code}")
    #             # Trigger rollback
    #             # subprocess.run(['/mj2:5-deploy', '--rollback'], check=True)
    #         else:
    #             print("✅ Smoke tests passed")
    #     except requests.RequestException as e:
    #         print(f"❌ Smoke test failed: {e}")

    # Example 4: Create tag in GitHub
    # if env == 'production' and status == 'success':
    #     try:
    #         tag_name = f'deploy-{version}'
    #         subprocess.run(['git', 'tag', '-a', tag_name, '-m',
    #                       f'Deployed v{version} to production'], check=True)
    #         subprocess.run(['git', 'push', 'origin', tag_name], check=True)
    #         print(f"✅ Created tag: {tag_name}")
    #     except subprocess.CalledProcessError as e:
    #         print(f"⚠️  Failed to create tag: {e}")

    # Example 5: Update Jira issue status
    # try:
    #     result = subprocess.run(['git', 'log', '-1', '--pretty=%B'],
    #                           capture_output=True, text=True, check=True)
    #     commit_msg = result.stdout
    #
    #     # Extract JIRA issue (e.g., JIRA-123)
    #     import re
    #     match = re.search(r'JIRA-(\d+)', commit_msg)
    #     if match:
    #         issue_number = match.group(1)
    #         # Update issue status to "Deployed"
    #         # jira_api.transition_issue(f'JIRA-{issue_number}', 'Deployed')
    # except subprocess.CalledProcessError:
    #     pass

    sys.exit(0)


if __name__ == "__main__":
    main()
