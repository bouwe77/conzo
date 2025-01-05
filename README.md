# Conzo

Conzo is a text-based productivity app for macOS that brings the power of automation to your fingertips. 

Acting as both an app launcher and a versatile scripting tool, Conzo lets you add custom scripts—with or without UI elements—to execute tasks and streamline workflows with a single keystroke. 

Inspired by tools like ScriptKit and Raycast, Conzo is designed to make any task quickly accessible, enhancing productivity in a way that’s both flexible and fast.

# Getting Started

```
mkdir my-conzo-app
cd my-conzo-app
npm init -y
npm i conzo@latest
echo '#!/usr/bin/env node
import { createApp } from "conzo"
const app = createApp({ color: "red" }).start()' > index.js
chmod +x index.js
./index.js
```

