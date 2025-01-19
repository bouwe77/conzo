# Creates a Conzo app on the fly, to quickly test against the local build in the dist folder.

# Build Conzo into the dist folder
npm run build

# Remove the current folder
rm -rf app-for-quick-testing

# Create the folder again with some minimal files
mkdir app-for-quick-testing
cd app-for-quick-testing

# Create index.js that imports Conzo from the dist folder and starts the app
echo '#!/usr/bin/env node
import { createApp } from "../dist/index.js"
createApp({ theme: { color: "magentaBright" }}).start()' > index.js

chmod +x index.js

# Create package.json to start the server with `npm run dev`
echo '{
  "type": "module",
  "scripts": {
    "dev": "node --watch index.js"
  }
}' > package.json

# Create a readme to explain what this is
echo "# Conzo app for quick testing

This app is created on the fly to quickly test the Conzo app against the local build in the \`dist\` folder.
" > README.md

# Start the just created app
npm run dev