#!/usr/bin/env python3
"""
Template: on-sync-done hook
Description: Runs when /mj2:3-sync completes
Available variables:
  - MJ2_SYNC_FILES_COUNT: Number of files synced
  - MJ2_SYNC_DURATION: Sync duration in ms
  - MJ2_SYNC_ERRORS: Number of errors during sync
  - MJ2_TIMESTAMP: ISO 8601 timestamp
"""

import os
import sys
from datetime import datetime


def main():
    """On-sync-done hook execution."""
    # Get environment variables
    files_count = int(os.getenv('MJ2_SYNC_FILES_COUNT', '0'))
    duration = int(os.getenv('MJ2_SYNC_DURATION', '0'))
    errors = int(os.getenv('MJ2_SYNC_ERRORS', '0'))
    timestamp = os.getenv('MJ2_TIMESTAMP', datetime.utcnow().isoformat())

    # Log event
    print(f"[ON-SYNC-DONE] Files synced: {files_count}")
    print(f"[ON-SYNC-DONE] Duration: {duration}ms")
    print(f"[ON-SYNC-DONE] Errors: {errors}")

    # ============================================
    # YOUR CODE HERE
    # ============================================

    # Example 1: Publish documentation to GitHub Pages
    # if errors == 0:
    #     import subprocess
    #     try:
    #         subprocess.run(['git', '-C', 'docs', 'add', '.'], check=True)
    #         subprocess.run(['git', '-C', 'docs', 'commit', '-m',
    #                       'docs: Update documentation', '--no-verify'],
    #                       check=True)
    #         subprocess.run(['git', '-C', 'docs', 'push',
    #                       'origin', 'gh-pages'], check=True)
    #         print("✅ Documentation published to GitHub Pages")
    #     except subprocess.CalledProcessError as e:
    #         print(f"⚠️  Failed to publish docs: {e}")

    # Example 2: Sync to Notion/Confluence
    # from pathlib import Path
    # docs_dir = Path('.mj2/docs')
    # if docs_dir.exists():
    #     for md_file in docs_dir.glob('*.md'):
    #         # Upload to Notion API
    #         # notion_page_id = upload_to_notion(md_file)
    #         # print(f"✅ Synced to Notion: {notion_page_id}")
    #         pass

    # Example 3: Generate PDF documentation
    # import subprocess
    # try:
    #     subprocess.run(['pandoc', '.mj2/docs/*.md',
    #                   '-o', 'documentation.pdf'], check=True)
    #     print("✅ PDF generated: documentation.pdf")
    # except (subprocess.CalledProcessError, FileNotFoundError):
    #     print("⚠️  pandoc not installed, skipping PDF generation")

    # Example 4: Notify that documentation is updated
    # if files_count > 0:
    #     print(f"📚 Documentation updated: {files_count} files")

    sys.exit(0)


if __name__ == "__main__":
    main()
