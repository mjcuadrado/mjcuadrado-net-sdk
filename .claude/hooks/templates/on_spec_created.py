#!/usr/bin/env python3
"""
Template: on-spec-created hook
Description: Runs when a new SPEC is created
Available variables:
  - MJ2_SPEC_ID: SPEC ID (e.g., "SPEC-AUTH-001")
  - MJ2_SPEC_PATH: Path to the SPEC file
  - MJ2_SPEC_PRIORITY: Priority (high, medium, low)
  - MJ2_SPEC_TYPE: Type (feature, bug, refactor, docs)
  - MJ2_TIMESTAMP: ISO 8601 timestamp
"""

import os
import sys
from datetime import datetime
from pathlib import Path


def main():
    """On-spec-created hook execution."""
    # Get environment variables
    spec_id = os.getenv('MJ2_SPEC_ID', '')
    spec_path = os.getenv('MJ2_SPEC_PATH', '')
    priority = os.getenv('MJ2_SPEC_PRIORITY', '')
    spec_type = os.getenv('MJ2_SPEC_TYPE', '')
    timestamp = os.getenv('MJ2_TIMESTAMP', datetime.utcnow().isoformat())

    # Log event
    print(f"[ON-SPEC-CREATED] SPEC ID: {spec_id}")
    print(f"[ON-SPEC-CREATED] Path: {spec_path}")
    print(f"[ON-SPEC-CREATED] Priority: {priority}")
    print(f"[ON-SPEC-CREATED] Type: {spec_type}")

    # ============================================
    # YOUR CODE HERE
    # ============================================

    # Example 1: Auto-backup to S3/Azure
    # import boto3
    # from datetime import datetime
    #
    # if Path(spec_path).exists():
    #     s3_bucket = os.getenv('S3_BACKUP_BUCKET', 's3://my-bucket/specs/')
    #     timestamp_str = datetime.now().strftime('%Y%m%d_%H%M%S')
    #     backup_name = f"{spec_id}_{timestamp_str}.md"
    #
    #     s3 = boto3.client('s3')
    #     bucket_name = s3_bucket.replace('s3://', '').split('/')[0]
    #     key = f"specs/backups/{backup_name}"
    #
    #     with open(spec_path, 'rb') as f:
    #         s3.upload_fileobj(f, bucket_name, key)
    #     print(f"✅ SPEC backed up to: s3://{bucket_name}/{key}")

    # Example 2: Create issue in Jira/Linear
    # if priority == "high":
    #     import requests
    #     linear_api_key = os.getenv('LINEAR_API_KEY')
    #     if linear_api_key:
    #         headers = {'Authorization': f'Bearer {linear_api_key}'}
    #         mutation = {
    #             'query': f'''
    #             mutation {{
    #                 issueCreate(input: {{title: "{spec_id}", priority: 1}}) {{
    #                     success
    #                 }}
    #             }}
    #             '''
    #         }
    #         requests.post('https://api.linear.app/graphql',
    #                      json=mutation, headers=headers)

    # Example 3: Validate SPEC format
    # if Path(spec_path).exists():
    #     with open(spec_path, 'r') as f:
    #         content = f.read()
    #         if "## EARS Format" not in content:
    #             print("⚠️  Warning: SPEC missing EARS format section")

    # Example 4: Notify team
    # import requests
    # webhook_url = os.getenv('SLACK_WEBHOOK_URL')
    # if webhook_url:
    #     payload = {
    #         'text': f'📋 New SPEC created: {spec_id}',
    #         'attachments': [{
    #             'color': 'good',
    #             'fields': [
    #                 {'title': 'Priority', 'value': priority, 'short': True},
    #                 {'title': 'Type', 'value': spec_type, 'short': True}
    #             ]
    #         }]
    #     }
    #     requests.post(webhook_url, json=payload)

    sys.exit(0)


if __name__ == "__main__":
    main()
