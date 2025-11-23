#!/usr/bin/env python3
"""
Hook: spec_backup
Description: Automatic backup of SPECs to S3
Event: on-spec-created, on-spec-updated
Requires: boto3 installed and S3_BACKUP_BUCKET environment variable
"""

import os
import sys
import json
from datetime import datetime
from pathlib import Path


def main():
    """Backup SPECs to S3."""
    # Check if boto3 is available
    try:
        import boto3
    except ImportError:
        print("⚠️  Warning: boto3 not installed. Skipping backup.")
        print("   Install with: pip install boto3")
        sys.exit(0)

    # Check if S3_BACKUP_BUCKET is configured
    s3_bucket = os.getenv('S3_BACKUP_BUCKET')
    if not s3_bucket:
        print("⚠️  Warning: S3_BACKUP_BUCKET not set. Skipping backup.")
        print("   Set it to: s3://your-bucket-name/specs/")
        sys.exit(0)

    # Get SPEC variables
    spec_id = os.getenv('MJ2_SPEC_ID', '')
    spec_path = os.getenv('MJ2_SPEC_PATH', '')
    priority = os.getenv('MJ2_SPEC_PRIORITY', '')
    spec_type = os.getenv('MJ2_SPEC_TYPE', '')
    timestamp = os.getenv('MJ2_TIMESTAMP', datetime.utcnow().isoformat())

    # Check if SPEC file exists
    spec_file = Path(spec_path)
    if not spec_file.exists():
        print(f"❌ Error: SPEC file not found: {spec_path}")
        sys.exit(1)

    # Parse S3 bucket
    bucket_name = s3_bucket.replace('s3://', '').rstrip('/').split('/')[0]
    prefix = '/'.join(s3_bucket.replace('s3://', '').rstrip('/').split('/')[1:])
    if prefix:
        prefix += '/'

    # Create backup filename with timestamp
    timestamp_str = datetime.now().strftime('%Y%m%d_%H%M%S')
    backup_name = f"{spec_id}_{timestamp_str}.md"
    latest_name = f"{spec_id}_latest.md"

    print(f"📦 Backing up SPEC to S3...")
    print(f"   Source: {spec_path}")
    print(f"   Bucket: s3://{bucket_name}/{prefix}")

    try:
        s3_client = boto3.client('s3')

        # Upload timestamped backup
        backup_key = f"{prefix}backups/{backup_name}"
        with open(spec_file, 'rb') as f:
            s3_client.upload_fileobj(f, bucket_name, backup_key)
        print(f"✅ SPEC backed up successfully")
        print(f"   Backup: s3://{bucket_name}/{backup_key}")

        # Upload latest version
        latest_key = f"{prefix}latest/{latest_name}"
        with open(spec_file, 'rb') as f:
            s3_client.upload_fileobj(f, bucket_name, latest_key)
        print(f"   Latest: s3://{bucket_name}/{latest_key}")

        # Upload metadata
        metadata = {
            'specId': spec_id,
            'priority': priority,
            'type': spec_type,
            'timestamp': timestamp,
            'backupName': backup_name
        }
        metadata_key = f"{prefix}metadata/{spec_id}_metadata.json"
        s3_client.put_object(
            Bucket=bucket_name,
            Key=metadata_key,
            Body=json.dumps(metadata, indent=2),
            ContentType='application/json'
        )
        print(f"   Metadata: s3://{bucket_name}/{metadata_key}")

        sys.exit(0)

    except Exception as e:
        print(f"❌ Failed to backup SPEC to S3: {e}")
        sys.exit(1)


if __name__ == "__main__":
    main()
