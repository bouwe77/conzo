# Contributing to Conzo

Thank you for your interest in contributing to Conzo! This document outlines the contribution process.

## Project Overview

See [README.md](./readme.md).

## How to Contribute

If you encounter any bugs or have suggestions for new features:

1. Check if an issue already exists in the issues list.
1. If not, open a new issue and include:
    * A clear description of the problem or suggestion.
    * Steps to reproduce the issue (if it’s a bug).
    * Your environment details (e.g., Node version, OS).
1. Use appropriate labels if available.

## Submitting Pull Requests

1. Fork the repository and create a new branch for your feature or bug fix.
1. Make sure your branch name is descriptive (e.g., feature/add-websocket-support).
1. Write clear, concise commit messages.
1. Ensure all tests pass before submitting your pull request.
1. Open a pull request (PR) and describe your changes. Link to related issues if applicable.

### Code Style

You are required to use Prettier for formatting.

### Testing

There are no automated tests (yet), so everything needs to be tested manually.

### Branching Strategy

> TODO Describe any specific branching strategy, like `main` and `develop` branches.

## Development Setup

1. Clone the repository:  

   ```bash
   git clone https://github.com/bouwe77/conzo.git
   ```

2. Install dependencies:  

   ```bash
   npm install
   ```

3. Start the development environment:  

   ```bash
   npm run dev
   ```
This command runs `tsc` and outputs the compiled JavaScript code to the `dist` folder.

> ⚠️ Watch this Terminal tab for compilation errors!

4. In another Terminal tab start the app:
   
   ```bash
   node dist/index.js
   ```

5. After each code change: Quit the app with `CTRL + C` and start again:

   ```bash
   node dist/index.js
   ```

### Execute the build

As `dist/index.js` contains a _shebang line_, it is executable. However, it still needs permissions, and a symlink from the executable to a folder that is in the `PATH`:

```
chmod +x /Users/bouwe/dev-bouwe/conzo/dist/index.js
ln -s /Users/bouwe/dev-bouwe/conzo/dist/index.js /Users/bouwe/.asdf/shims/conzo
```

Now you can start the app:

```
conzo
```

## Review Process

> TODO Explanation of how pull requests are reviewed and the expected timeline for feedback.

## Code of Conduct

> TODO Please follow our [Code of Conduct](./CODE_OF_CONDUCT.md) to maintain a constructive community.

## License

> TODO By contributing, you agree that your contributions will be licensed under the [project's license](./LICENSE).

## Communication

> TODO Information on where contributors can ask questions or discuss issues (e.g., GitHub Discussions, Slack, or Discord).
