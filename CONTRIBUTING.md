# Contributing to OpenLedger Atlas

## Forking
1. Fork repo
2. Create feature branch
3. Submit PR

## Run Locally
docker compose up --build

## Coding Standards
- Domain logic never in Controllers
- No business logic in Infrastructure
- Async/await required
- No magic numbers

## Branch Naming
feature/...
bugfix/...
hotfix/...

## PR Requirements
- Tests required
- No direct pushes to main
- Must pass CI
