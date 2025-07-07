# Contributing to Bank Management System

Thank you for your interest in contributing to the Bank Management System! This document provides guidelines and instructions for contributing to this project.

## 🤝 How to Contribute

### 1. Fork the Repository
```bash
git fork https://github.com/yourusername/bank-management-system.git
git clone https://github.com/yourusername/bank-management-system.git
cd bank-management-system
```

### 2. Set Up Development Environment
- Install Visual Studio 2019+ or VS Code with C# extension
- Install .NET Framework 4.7.2 or higher
- Set up SQL Server or SQL Server Express
- Configure database connection in App.config

### 3. Create a Feature Branch
```bash
git checkout -b feature/your-feature-name
```

### 4. Make Your Changes
- Follow the existing code style and conventions
- Add appropriate comments and documentation
- Include unit tests for new features
- Update documentation if necessary

### 5. Test Your Changes
- Run existing unit tests: `dotnet test`
- Test the application manually
- Verify all functionality works as expected
- Check for any breaking changes

### 6. Commit Your Changes
```bash
git add .
git commit -m "feat: add your feature description"
```

### 7. Push to Your Fork
```bash
git push origin feature/your-feature-name
```

### 8. Create a Pull Request
- Go to the original repository
- Click "New Pull Request"
- Select your feature branch
- Provide a clear description of your changes

## 📋 Contribution Guidelines

### Code Style
- Use PascalCase for class names and method names
- Use camelCase for variable names and parameters
- Include XML documentation for public methods
- Follow C# coding conventions and best practices

### Documentation
- Update README.md if adding new features
- Add inline comments for complex logic
- Include XML documentation for public APIs
- Update relevant documentation files

### Testing
- Write unit tests for new functionality
- Ensure all tests pass before submitting
- Test edge cases and error conditions
- Include integration tests where appropriate

### Security
- Never commit sensitive information (passwords, connection strings)
- Use secure coding practices
- Validate all user inputs
- Follow security best practices for data handling

## 🐛 Bug Reports

When reporting bugs, please include:
- **Clear title and description**
- **Steps to reproduce** the issue
- **Expected behavior** vs **actual behavior**
- **System information** (OS, .NET version, SQL Server version)
- **Screenshots** if applicable
- **Error messages** or stack traces

## 💡 Feature Requests

For new features, please:
- **Check existing issues** to avoid duplicates
- **Provide detailed use cases** and business value
- **Consider implementation complexity** and impact
- **Include mockups or diagrams** if helpful

## 📝 Pull Request Guidelines

### Before Submitting
- [ ] Code follows project style guidelines
- [ ] Self-review of code completed
- [ ] Comments added for complex areas
- [ ] Tests added/updated and passing
- [ ] Documentation updated if needed
- [ ] No merge conflicts with main branch

### PR Description Template
```markdown
## Description
Brief description of the changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
- [ ] Unit tests added/updated
- [ ] Manual testing completed
- [ ] All tests passing

## Checklist
- [ ] Code follows style guidelines
- [ ] Self-review completed
- [ ] Documentation updated
- [ ] Tests added/updated
```

## 🏷️ Issue Labels

We use the following labels to categorize issues:
- `bug` - Something isn't working
- `enhancement` - New feature or request
- `documentation` - Improvements to documentation
- `good first issue` - Good for newcomers
- `help wanted` - Extra attention needed
- `question` - Further information requested

## 🚀 Development Setup

### Prerequisites
- Visual Studio 2019+ or VS Code
- .NET Framework 4.7.2+
- SQL Server 2017+ or SQL Server Express
- Git

### Database Setup
1. Execute `DataBase Schema/BankManagementSystem_DDL.sql`
2. Optionally run `DataBase Schema/Sql.sql` for sample data
3. Update connection string in `App.config`

### Building the Project
```bash
# Using Visual Studio
# Open BankManagementSystem.sln and press F5

# Using command line
dotnet build
dotnet run
```

## 🔒 Security

If you discover a security vulnerability, please:
1. **Do not** open a public issue
2. Email the maintainers directly
3. Provide detailed information about the vulnerability
4. Allow time for the issue to be resolved before disclosure

## 📞 Getting Help

- **Documentation**: Check existing documentation files
- **Issues**: Search existing issues for similar problems
- **Discussions**: Use GitHub Discussions for questions
- **Contact**: Reach out to maintainers directly

## 🎉 Recognition

Contributors will be recognized in:
- README.md contributors section
- Release notes for significant contributions
- GitHub contributors graph

Thank you for contributing to the Bank Management System! 🙏
