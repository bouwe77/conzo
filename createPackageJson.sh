#!/bin/bash

# Define paths
SOURCE_PACKAGE_JSON="package.json"
DIST_PACKAGE_JSON="dist/package.json"

# Check if the source package.json exists
if [ ! -f "$SOURCE_PACKAGE_JSON" ]; then
  echo "Error: $SOURCE_PACKAGE_JSON not found!"
  exit 1
fi

# Read version and dependencies from the source package.json
VERSION=$(node -p "require('./$SOURCE_PACKAGE_JSON').version")
DEPENDENCIES=$(node -p "JSON.stringify(require('./$SOURCE_PACKAGE_JSON').dependencies || {})")

# Ensure the dist directory exists
mkdir -p dist

# Create the package.json content
cat <<EOF > "$DIST_PACKAGE_JSON"
{
  "name": "conzo",
  "version": "$VERSION",
  "type": "module",
  "main": "./index.js",
  "files": ["**/*"],
  "dependencies": $DEPENDENCIES,
  "author": "Bouwe",
  "license": "MIT"
}
EOF

echo "Generated $DIST_PACKAGE_JSON successfully."
