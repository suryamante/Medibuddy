#!/usr/bin/env sh
set -eu

SCHEMA_SQL_PATH="${1:-/app/Database_Schema.sql}"
SAMPLE_DATA_PATH="${2:-/app/data_seed/sample_data.sql}"
OUTPUT_DB_PATH="${3:-/app/Medibuddy/medibuddy.db}"

mkdir -p "$(dirname "$OUTPUT_DB_PATH")"

echo "PRAGMA foreign_keys = ON;" > /tmp/schema.sql
sed -E '/^[[:space:]]*CREATE DATABASE[[:space:]]+/Id;/^[[:space:]]*USE[[:space:]]+/Id' "$SCHEMA_SQL_PATH" \
  | sed -E 's/IDENTITY\([0-9]+,[0-9]+\)[[:space:]]*PRIMARY KEY/INTEGER PRIMARY KEY AUTOINCREMENT/Ig' \
  | sed -E 's/\bBIT\b/INTEGER/Ig' \
  | sed -E 's/,[[:space:]]*\)/)/g' \
  >> /tmp/schema.sql

cat /tmp/schema.sql "$SAMPLE_DATA_PATH" > /tmp/seed.sql
sqlite3 "$OUTPUT_DB_PATH" < /tmp/seed.sql

rm -f /tmp/schema.sql /tmp/seed.sql
